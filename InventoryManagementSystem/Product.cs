namespace InventoryManagementSystem
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }
}