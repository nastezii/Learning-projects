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
        public MenuItem(){}
    }
}
