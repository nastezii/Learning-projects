namespace CafeOrderManagement
{
    internal class MenuManagement
    {
        public static List<MenuItem> menu = new List<MenuItem>();
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
