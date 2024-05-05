namespace App.Database
{
    public class Order
    {
        public long OrderID { get; set; }
        public string Username { get; set; }
        public Dictionary<string, int> Items { get; set; } //string = ItemName, int = Quantity
        public string Status { get; set; }
    }
}
