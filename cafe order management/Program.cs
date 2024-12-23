using CafeOrderManagement;

OrderManagement orderManagement = new OrderManagement();
MenuManagement menuManagement = new MenuManagement();

Console.WriteLine("Order commands:\nAdd order\nUpdate order status\nSorting orders by customer\nSorting orders by status\n");
Console.WriteLine("Menu commands: \nShow menu\nAdd menu item\nDelete menu item\n");
Console.WriteLine("To create a report enter \"Create a report\"");
Console.WriteLine("To finish enter \"End\".");
bool running = true;
string option;
while (running)
{
    Console.Write("\nEnter command: ");
    option = Console.ReadLine();

    if (option.Equals("Add order", StringComparison.OrdinalIgnoreCase))
    {
        AddOrder();
    }
    else if (option.Equals("Update order status", StringComparison.OrdinalIgnoreCase))
    {
        UpdateOrderStatus();
    }
    else if (option.Equals("Sorting orders by customer", StringComparison.OrdinalIgnoreCase))
    {
        FilterOrdersByCustomerName();
    }
    else if (option.Equals("Sorting orders by status", StringComparison.OrdinalIgnoreCase))
    {
        FilterOrdersByStatus();
    }
    else if (option.Equals("Show menu", StringComparison.OrdinalIgnoreCase))
    {
        menuManagement.ShowMenu();
    }
    else if (option.Equals("Add menu item", StringComparison.OrdinalIgnoreCase))
    {
        AddMenuItem();
    }
    else if (option.Equals("Delete menu item", StringComparison.OrdinalIgnoreCase))
    {
        MenuItemDeleting();
    }
    else if (option.Equals("Create a report", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Enter the file name:");
        string fileName = Console.ReadLine();
        orderManagement.CreateReport(fileName);
    }
    else if (option.Equals("End", StringComparison.OrdinalIgnoreCase))
    {
        running = false;
        Console.WriteLine("Exiting the program.");
    }
    else
    {
        Console.WriteLine("Invalid option. Please, try again.");
    }
}

void AddOrder()
{ 
    Order newOrder = new Order();

    Console.WriteLine("Enter the name of customer:");
    string customerName = Console.ReadLine();

    orderManagement.AddOrder(newOrder, menuManagement, customerName);

    Console.WriteLine($"Order #{newOrder.Id} for {newOrder.CustomerName} created successfully.");
    Console.WriteLine($"Total amount: ${newOrder.Amount:F2}");
    Console.WriteLine("Order details:");
    foreach (var detail in newOrder.Details)
    {
        Console.WriteLine($"- {detail.Name}: ${detail.Price:F2}");
    }
}

void UpdateOrderStatus()
{
    Console.WriteLine("Enter the order ID:");
    int id;
    while (!int.TryParse(Console.ReadLine(), out id))
    {
        Console.WriteLine("ID must be a positive integer. Enter the order ID:");
    }
    Console.WriteLine("Enter a new order status :");
    string status = Console.ReadLine();
    OrderStatus newStatus = new OrderStatus();
    if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
    {
        newStatus = OrderStatus.Pending;
    }
    else if (status.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
    {
        newStatus = OrderStatus.InProgress;
    }
    else if (status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
    {
        newStatus = OrderStatus.Completed;
    }
    else if (status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase)) 
    {
        newStatus = OrderStatus.Cancelled;
    }
    else
    {
        Console.WriteLine("Invalid status entered. Please try again.");
        return; 
    }
    orderManagement.UpdateOrderStatus(id, newStatus);
}

void FilterOrdersByCustomerName ()
{
    Console.WriteLine("Enter the name of customer:");
    string customerName = Console.ReadLine();
    orderManagement.FilterOrdersByCustomerName(customerName);
}

void FilterOrdersByStatus()
{
    string input;
    bool flag = true;
    while (flag)
    {
        Console.WriteLine("Enter order status:");
        input = Console.ReadLine();
        if (Enum.TryParse(typeof(OrderStatus), input, true, out var result))
        {
            var newStatus = (OrderStatus)result;
            if (Enum.IsDefined(typeof(OrderStatus), newStatus))
            {
                flag = false;
                orderManagement.FilterOrdersByStatus(newStatus);
            }
            else
            {
                Console.WriteLine("This status is invalid. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }
}

void AddMenuItem()
{
    Console.WriteLine("Enter the name of the menu position:");
    string item = Console.ReadLine();
    menuManagement.AddMenuItem(item);
}

void MenuItemDeleting()
{
    Console.WriteLine("Enter the name of the menu position:");
    string item = Console.ReadLine();
    menuManagement.RemoveMenuItem(item);
}

public enum OrderStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}


