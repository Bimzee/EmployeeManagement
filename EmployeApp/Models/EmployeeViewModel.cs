using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeApp.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string EmpType { get; set; }
        public int? WorkingHours { get; set; }
        public int? PayScale { get; set; }
        public double Total { get; set; }
    }
}