using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class InventoryServices : IInventoryServices
    {
        private readonly Inventory _inventory;
        public InventoryServices()
        {
            _inventory = new Inventory();
        }
        public void AddProduct(Product product)
        {
            _inventory.AddProduct(product);
        }
    }
}
