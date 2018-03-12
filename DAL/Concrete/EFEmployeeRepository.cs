using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using DAL.Abstract;
using DAL.Concrete;

using System.Data;

namespace DAL.Concrete
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public void EditEmployee(Employee employee)
        {
            var emp = context.Employees.Find(employee.Id);
            if(emp!= null)
            {
                context.Entry(emp).CurrentValues.SetValues(employee);
                context.SaveChanges();
            }
        }

        public void EditEmpSalary(EmpSalary empSalary)
        {
            try
            {
                var empSal = context.EmpSalary.Find(empSalary.EmployeeID);
                if (empSal != null)
                {
                    empSalary.Id = empSal.Id;
                    context.Entry(empSal).CurrentValues.SetValues(empSalary);
                    context.SaveChanges();
                }
                else
                {
                    context.EmpSalary.Add(empSalary);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetDetails()
        {
            DataTable dt = new DataTable("EmployeeDetails");
            dt.Columns.Add("ID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Age");
            dt.Columns.Add("EmployeeTypeID");
            dt.Columns.Add("EmployeeType");
            dt.Columns.Add("PayScale");
            dt.Columns.Add("WorkingHours");
            dt.Columns.Add("Total");

            var res = (from e in context.Employees
                       join es in context.EmpSalary on e.Id equals es.EmployeeID into ed
                       from x in ed.DefaultIfEmpty()
                       select new
                       {
                           ID = e.Id,
                           FirstName = e.FirstName,
                           LastName = e.LastName,
                           Age = (e.Age == null ? 0 : e.Age),
                           EmployeeTypeID=(x.EmpTypeID==null?0:x.EmpTypeID) ,
                           EmplyoeeType = (x.EmpTypeID==1?"Permenent":"Contract"),
                           PayScale = (x.PayScale == null ? 0 : x.PayScale),
                           WorkingHours = (x.WorkingHours == null ? 0 : x.WorkingHours),
                           Total= (x.PayScale == null ? 0 : x.PayScale) * (x.WorkingHours == null ? 0 : x.WorkingHours)
                       });

            foreach (var item in res)
            {
                dt.Rows.Add(item.ID,
                        item.FirstName,
                        item.LastName,
                        item.Age,
                        item.EmployeeTypeID, 
                        item.EmplyoeeType, 
                        item.PayScale, 
                        item.WorkingHours, 
                        item.Total
                        );
            }

            return dt;
        }

        public Employee GetEmployeeByID(int id)
        {
            var emp = context.Employees.Where(e => e.Id == id);
            return emp.FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public EmpSalary GetEmpSalary(int id)
        {
            var empSalarys = context.EmpSalary.Where(eid => eid.EmployeeID == id);
            if (empSalarys.Count() > 0)
            {
                var empSalary = empSalarys.FirstOrDefault();
                return new EmpSalary
                {
                    Id = empSalary.Id,
                    EmpTypeID = empSalary.EmpTypeID,
                    PayScale = empSalary.PayScale,
                    WorkingHours = empSalary.WorkingHours,
                    Total = empSalary.Total,
                    EmployeeID = empSalary.EmployeeID
                };
            }
            else
                return new EmpSalary();
        }

        public void SaveEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void SaveEmpSalary(EmpSalary empSalary)
        {
            try
            {
                context.EmpSalary.Add(empSalary);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
