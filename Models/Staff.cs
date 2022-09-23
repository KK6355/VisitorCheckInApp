using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorCheckInApp.Models
{
    public class Staff
    {
        public Guid Id { get; set; }
        public string StaffName { get; set; }
        public string Department { get; set; }
     
        public int VisitorCount { get; set; } = 0;
    }
}
