namespace CafeOrderManagement
{
    internal class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<MenuItem> Details = new List<MenuItem>();
        public float Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public OrderStatus Status { get; set; }

        public List<MenuItem> CreateOrderDetails(Order newOrder)
        {
            List<MenuItem> orderDetails = new List<MenuItem>();
            bool flag = true;
            Console.WriteLine("Enter menu item. To finish, type \"End\"");

            while (flag)
            {
                string item = Console.ReadLine();
                if (item.Equals("End", StringComparison.OrdinalIgnoreCase))
                {
                    flag = false;
                    break;
                }

                var menuItem = MenuManagement.menu.Find(itm => itm.Name.Equals(item, StringComparison.OrdinalIgnoreCase));
                if (menuItem != null)
                {
                    orderDetails.Add(menuItem);
                }
                else
                {
                    menuItem.AddMenuItem(item, orderDetails);
                }
            }
            return orderDetails;
        }
    }
}
