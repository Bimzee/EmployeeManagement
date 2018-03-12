using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Entity;
using EmployeApp.Models;
using PagedList;


namespace EmployeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        
        //Constructor DI using Unity
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
       
        // GET: Employee
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            ViewBag.CurrentSort = (String.IsNullOrEmpty(sortOrder)?"ASC":CurrentSort);
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Emp_ID" : sortOrder;

            IPagedList<DataRow> empl = null;

            DataTable empDT = _employeeRepository.GetDetails();

            if (!String.IsNullOrEmpty(sortOrder))
            {
                if (!String.IsNullOrEmpty(CurrentSort) && CurrentSort=="ASC")
                {
                    empl = empDT.AsEnumerable().OrderBy(et => et["EmployeeTypeID"].ToString()).ToPagedList(pageIndex,pageSize);
                }
                else
                {
                    empl = empDT.AsEnumerable().OrderByDescending(et => et["EmployeeTypeID"]).ToPagedList(pageIndex, pageSize);
                }
            }

            return View(empl);


        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployeeByID(id);
            ViewBag.EmployeeName = employee.FirstName + " " + employee.LastName;

            var empSalary = _employeeRepository.GetEmpSalary(id);
            ViewBag.EmpSalary = empSalary.PayScale * empSalary.WorkingHours;

            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                Employee emp = new Employee()
                {
                    Age = Convert.ToInt32(Request.Form["Age"]),
                    FirstName = Convert.ToString(Request.Form["FirstName"]),
                    LastName = Convert.ToString(Request.Form["LastName"])
                };
                _employeeRepository.SaveEmployee(emp);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployeeByID(id);
            var empSalary = _employeeRepository.GetEmpSalary(id);

            EmployeeViewModel empModel = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                PayScale = empSalary.PayScale,
                WorkingHours = empSalary.WorkingHours,
                Total = empSalary.Total,
                EmpType = (empSalary.EmpTypeID == 1 ? "Permenent" : "Contract")

            };
            return View(empModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var employee = _employeeRepository.GetEmployeeByID(id);
                var empSalary = _employeeRepository.GetEmpSalary(id);
                employee.Id = id; ;
                employee.FirstName = Request.Form["FirstName"];
                employee.LastName = Request.Form["LastName"];
                employee.Age = Convert.ToInt32(Request.Form["Age"]);
                empSalary.EmployeeID = id;
                empSalary.EmpTypeID = Convert.ToInt32(Request.Form["EmpType"]);// (Request.Form["EmpType"] == "Permenent" ? 1 : 2);
                empSalary.PayScale = Convert.ToInt32(Request.Form["PayScale"]);
                empSalary.WorkingHours = Convert.ToInt32(Request.Form["WorkingHours"]);
                empSalary.Total = Convert.ToInt64(empSalary.PayScale*empSalary.WorkingHours);

                _employeeRepository.EditEmployee(employee);
                _employeeRepository.EditEmpSalary(empSalary);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #region Not Implemented
        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } 
        #endregion
    }
}
