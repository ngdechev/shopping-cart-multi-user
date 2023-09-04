namespace ShoppingCartMultiUser.entity
{
    [Serializable]
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public Product(string name, float price, int quantity, string description)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
        }

        public override string? ToString()
        {
            return $"Product ID -> {Id} \n " +
                $"Name -> {Name} \n " +
                $"Price -> {Price} \n " +
                $"Quantity -> {Quantity} \n " +
                $"Description -> {Description}\n";
        }
    }
}
