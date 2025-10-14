namespace miniprojectE.Models.Entities
{
    public partial class OrderProgress
    {
        public int OrderProgressID { get; set; }
        public required int OrderID { get; set; }
        //public Orders Order { get; set; }
        public required string OrderStatus { get; set; }
        public required Guid UserID { get; set; }
        //public Users User { get; set; }
        public DateOnly UpdatedAt { get; set; }

    }
}
