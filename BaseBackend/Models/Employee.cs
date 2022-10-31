using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseBackend.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string EmailId { get; set; }
        public DateTime? DOJ { get; set; }
        public string PhotoFileName { get; set; }
    }
}
