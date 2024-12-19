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

        public List<MenuItem> CreateOrderDetails(Order newOrder,MenuManagement menuManagement)
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
                    Console.WriteLine("This item is not on the menu. Do you want to add a new item to the menu? Enter Yes/No");
                    string input = Console.ReadLine();
                    if (input.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        menuManagement.AddItemInTheProcess(item, orderDetails);
                    }
                    else if (input.Equals("No", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Item not added to the menu.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Skipping this item.");
                    }
                }
            }
            return orderDetails;
        }
    }
}
