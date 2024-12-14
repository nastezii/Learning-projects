


public struct Order
{
    public int OrderNumber { get; set; }
    public string CustomerName { get; set; }
    public List<string> OrderDetails { get; set; }
    public float OrderAmount { get; set; }
    public DateTime OrderCreationTime { get; set; }
    public string OrderStatus { get; set; }
}


enum OrderStatus
{
    Pending, 
    InProgress, 
    Completed, 
    Cancelled
}
