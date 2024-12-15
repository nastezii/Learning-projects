using CafeOrderManagament;

void AddOrder()
{
    List<MenuItem> orderDetails = new List<MenuItem>(); 
    Order newOrder = new Order();
    Console.WriteLine("Enter the name of customer:");
    string customerName = Console.ReadLine();
    Console.WriteLine("Enter menu item. To finish, type \"End\"");
    bool flag = true;

    while (flag)
    {
        string item = Console.ReadLine();
        if (item.Equals("End", StringComparison.OrdinalIgnoreCase))
        {
            flag = false;
            break;
        }

        var menuItem = newOrder.Details.Find(itm => itm.Name.Equals(item, StringComparison.OrdinalIgnoreCase));
        if (menuItem != null)
        {
            newOrder.Details.Add(menuItem);
        }
        else
        {
            Console.WriteLine("This item is not on the menu. Do you want to add a new item to the menu? Enter Yes/No");
            string input = Console.ReadLine();
            if (input.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                float price;
                Console.WriteLine("Enter the price of the new item:");
                while (!float.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("Invalid price. Please enter a positive number:");
                }

                MenuItem newItem = new(item, price);
                newOrder.Details.Add(newItem);
                orderDetails.Add(newItem);
            }
            else if (input.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Item not added to the menu.");
            }
            else
            {
                Console.WriteLine("Invalid input. Skipping this item.");
            }

            newOrder.CustomerName = customerName;
            newOrder.Details = orderDetails;
            newOrder.Status = OrderStatus.Pending;
            newOrder.Amount = orderDetails.Sum(x => x.Price);
            newOrder.CreationTime = DateTime.Now;

            OrderManagement orderManagement = new OrderManagement();
            orderManagement.AddOrder(newOrder);

            Console.WriteLine($"Order for {newOrder.CustomerName} created successfully.");
            Console.WriteLine($"Total amount: ${newOrder.Amount:F2}");
            Console.WriteLine("Order details:");
            foreach (var detail in newOrder.Details)
            {
                Console.WriteLine($"- {detail.Name}: ${detail.Price:F2}");
            }
        }
    }
}

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
        public List<MenuItem> Details = new List<MenuItem>();  
        public float Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class OrderManagement
    {
        private List<Order> orders = new List<Order>();
        private int lastOrderId = 0;
        private int GenerateId() => lastOrderId++;

        public void AddOrder(Order newOrder)
        {
            orders.Add(newOrder);
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
            if (customerOrders.Count == 0)
            {
                Console.WriteLine("There is no such customer in the database.");
            }
            else
            {
                Console.WriteLine($"{customer} orders:");
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
            if (ordersByStatus.Count == 0)
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

        public float DayAmountCalculating()
        {
            float sum = 0;
            foreach (var order in orders)
            {
                foreach (var dishes in order.Details)
                {
                    sum += dishes.Price;
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
