namespace CafeOrderManagement
{
    internal class MenuManagement
    {
        public static List<MenuItem> menu = new List<MenuItem>
        {
            new MenuItem { Name = "Coffee", Price = 2.50f },
            new MenuItem { Name = "Tea", Price = 1.99f },
            new MenuItem { Name = "Juice", Price = 2.99f },
            new MenuItem { Name = "Soda", Price = 1.50f },
            new MenuItem { Name = "Pizza", Price = 8.99f },
            new MenuItem { Name = "Pasta", Price = 6.99f },
            new MenuItem { Name = "Burger", Price = 7.49f },
            new MenuItem { Name = "Salad", Price = 5.99f },
            new MenuItem { Name = "Steak", Price = 12.99f },
            new MenuItem { Name = "Ice Cream", Price = 3.99f }
        };

        public void AddMenuItem(string item)
        {
            float price;
            Console.WriteLine("Enter the price of the new item:");
            while (!float.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Invalid price. Please enter a positive number:");
            }
            MenuItem newItem = new(item, price);
            menu.Add(newItem);
            Console.WriteLine("New item successfully added.");
        }

        public void AddItemInTheProcess(string item, List<MenuItem> orderDetails)
        {
            float price;
            Console.WriteLine("Enter the price of the new item:");
            while (!float.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Invalid price. Please enter a positive number:");
            }
            MenuItem newItem = new(item, price);
            orderDetails.Add(newItem);
            menu.Add(newItem);
            Console.WriteLine("New item successfully added.");
        }

        public void RemoveMenuItem(string name)
        {
            if (menu.Any(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                menu.RemoveAll(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine("Item successfully removed from menu.");
            }
            else
            {
                Console.WriteLine("A menu item with that name does not exist.");
            }
        }

        public void ShowMenu()
        {
            if (menu.Count == 0)
            {
                Console.WriteLine("The menu is currently empty.");
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
    }
}
