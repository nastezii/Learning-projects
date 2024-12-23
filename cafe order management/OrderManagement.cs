namespace CafeOrderManagement
{
    internal class OrderManagement
    {
        private static List<Order> orders = new List<Order>();
        private static int lastOrderId = 1;
        private int GenerateId() => lastOrderId++;

        private float OrderAmountCalculation(List<MenuItem> orderDetails)
        {
            float sum = 0;
            foreach (var items in orderDetails)
            {
                sum += items.Price;
            }
            return sum;
        }

        private float DayAmountCalculation()
        {
            float sum = 0;
            foreach (var order in orders)
            {
                if (order.Status != OrderStatus.Cancelled){sum += order.Amount;}
            }
            return sum;
        }

        public void CreateReport(string fileName)
        { 
            float totalAmount = DayAmountCalculation();
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Order id,Customer name,Order amount,Status");
                foreach (var order in orders)
                {
                    writer.WriteLine($"{order.Id},{order.CustomerName},{order.Amount},{order.Status}");
                }
                writer.WriteLine($"Total: {totalAmount}");
            }
            Console.WriteLine($"Report saved to {fileName}");
        }

        public void AddOrder(Order newOrder, MenuManagement menuManagament, string customerName)
        {
            List<MenuItem> orderDetails = newOrder.CreateOrderDetails(newOrder, menuManagament);
            newOrder.Id = GenerateId();
            newOrder.CustomerName = customerName;
            newOrder.Status = OrderStatus.Pending;
            newOrder.Details = orderDetails;
            newOrder.Amount = OrderAmountCalculation(orderDetails);
            newOrder.CreationTime = DateTime.Now;
            orders.Add(newOrder);
        }

        public void UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = orders.FirstOrDefault(order => order.Id == orderId);
            if (order != null)
            {
                if (order.Status == newStatus)
                {
                    Console.WriteLine("The order already has this status.");
                }
                else
                {
                    order.Status = newStatus;
                    Console.WriteLine("Status of the order successfully changed.");
                }
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }

        public void FilterOrdersByCustomerName(string customer)
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

        public void FilterOrdersByStatus(OrderStatus status)
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
    }
}
