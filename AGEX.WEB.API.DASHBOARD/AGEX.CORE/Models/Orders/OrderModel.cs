namespace AGEX.CORE.Models.Orders
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string Description { get; set; }
        public string WeightOrder { get; set; }
        public string NumberProducts { get; set; }
        public string AmountTotal { get; set; }
        public string Status { get; set; }
    }
}
