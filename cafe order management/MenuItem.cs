namespace CafeOrderManagement
{
    internal class MenuItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public MenuItem(string name, float price)
        {
            Name = name;
            Price = price;
        }

        public void AddMenuItem(string item, List<MenuItem> orderDetails)
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
        }
    }
}
