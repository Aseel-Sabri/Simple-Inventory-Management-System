using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Inventory
    {
        public List<Product> Products { get; set; }
        public Inventory()
        {
            Products = new List<Product>();
        }

        public Inventory(List<Product> products)
        {
            Products = products;    
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
