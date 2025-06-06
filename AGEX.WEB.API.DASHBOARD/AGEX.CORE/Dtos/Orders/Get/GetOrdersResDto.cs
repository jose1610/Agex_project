namespace AGEX.CORE.Dtos.Orders.Get
{
    public class GetOrdersResDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string WeightOrder { get; set; }
        public string NumberProducts { get; set; }
        public string AmountTotal { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
    }
}
