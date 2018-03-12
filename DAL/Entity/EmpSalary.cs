using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class EmpSalary
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int EmpTypeID { get; set; }
        public int? WorkingHours { get; set; }
        public int? PayScale { get; set; }
        public Int64 Total { get; set; }

    }
    public enum EmployeeType
    {
        Permenent=1,
        Contract
    }
}
