using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using System.Data;

namespace DAL.Abstract
{
    public interface IEmployeeRepository
    {
        DataTable GetDetails();
        Employee GetEmployeeByID(int id);
        EmpSalary GetEmpSalary(int id);
        IEnumerable<Employee> GetEmployees();
        void SaveEmployee(Employee employee);
        void SaveEmpSalary(EmpSalary empSalary);
        void EditEmployee(Employee employee);
        void EditEmpSalary(EmpSalary empSalary);
    }
}
