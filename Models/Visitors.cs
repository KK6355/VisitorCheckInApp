namespace VisitorCheckInApp.Models
{
    public class Visitors
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Business { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
        public DateTime? CheckOut { get; set; } 
        public Guid StaffId { get; set; }
        public Staff? Staff { get; set; }

    }
}
