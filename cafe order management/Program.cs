


namespace CafeOrderManagament
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }
    public class MenuItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public MenuItem(string name, float price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<KeyValuePair<string, float>> Details = new List<KeyValuePair<string, float>>() ;
        public float Amount { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; }
    }

    public class OrderManagement
    { 
        private List<Order> orders = new List<Order>();
        private int lastOrderId = 0;
        private int GenerateId() => lastOrderId++;

        public void AddOrder(string customerName, List<KeyValuePair<string, float>> orderDetails, float orderAmount)
        { 
            var order = new Order();
            order.CustomerName = customerName;
            order.Id = GenerateId();
            order.Details = orderDetails;
            order.Amount = orderAmount;

            orders.Add(order);
            Console.WriteLine("Order successfully added.");
        }

        public void UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = orders.FirstOrDefault(order => order.Id == orderId);
            if (order != null)
            {
                order.Status = newStatus;
                Console.WriteLine("Status of order successfully changed.");
            }
            else 
            {
                Console.WriteLine("Order not found.");
            }
            if (order.Status == OrderStatus.Cancelled)
            {
                orders.Remove(order);
            }
        }

        public void FilteringByName(string customer)
        {
            List<Order> customerOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (order.CustomerName == customer)
                { 
                    customerOrders.Add(order);
                }
            }
            if (customerOrders.Count != 0)
            {
                Console.WriteLine("There is no such customer in the database.");
            }
            else
            {
                Console.WriteLine($"{customer} orders :");
                foreach (var order in customerOrders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, date: {order.CreationTime}, status: {order.Status}.");
                }
            }
        }

        public void FilteringByStatus(OrderStatus status)
        {
            List<Order> ordersByStatus = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Status == status)
                {
                    ordersByStatus.Add(order);
                }
            }
            if (ordersByStatus.Count != 0)
            {
                Console.WriteLine("There are currently no orders with this status.");
            }
            else
            {
                Console.WriteLine($"Orders by status \"{status}\":");
                foreach (var order in ordersByStatus)
                {
                    Console.WriteLine($"Customer name: {order.CustomerName}, order ID: {order.Id}, date: {order.CreationTime}.");
                }
            }
        }

        public float AmountCalculating()
        {
            float sum = 0;
            foreach (var order in orders)
            {
                foreach (var dishes in order.Details)
                {
                    sum += dishes.Value;
                }
            }
            return sum;
        }
    }

    public class MenuManagement
    {
        private List<MenuItem> menu = new List<MenuItem>();

        public void AddMenuItem(string name, float price)
        {
            if (MenuItemExists(name))
            {
                Console.WriteLine("A menu item with that name already exists. New item not added.");
            }
            else
            {
                MenuItem newItem = new MenuItem(name, price);
                menu.Add(newItem);
                Console.WriteLine("Item successfully added to the menu.");
            }
        }

        public void RemoveMenuItem(string name)
        {
            if (MenuItemExists(name))
            {
                menu.RemoveAll(item => item.Name == name);
                Console.WriteLine("Item successfully removed from menu.");
            }
            else
            {
                Console.WriteLine("A menu item with that name do not exists.");
            }
        }

        public void ShowMenu()
        {
            if (menu.Count == 0)
            {
                Console.WriteLine("There is no item in the menu.");
            }
            else
            {
                Console.WriteLine("Menu in our cafe:");
                foreach (MenuItem item in menu)
                {
                    Console.WriteLine($"{item.Name} - {item.Price} $");
                }
            }
        }

        private bool MenuItemExists(string name)
        {
            return menu.Any(item => item.Name == name);
        }
    }
}