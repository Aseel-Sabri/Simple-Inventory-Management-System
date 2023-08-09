using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class ProductValidation
    {
        public static bool ValidateProductName(string? productName)
        {
            return !string.IsNullOrWhiteSpace(productName);
        }

        public static bool ValidateProductPrice(string? productPrice)
        {
            if (!string.IsNullOrWhiteSpace(productPrice))
            {
                if (double.TryParse(productPrice, out double price) && price > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateProductQuantity(string? productQuantity)
        {
            if (!string.IsNullOrWhiteSpace(productQuantity))
            {
                if (int.TryParse(productQuantity, out int quantity) && quantity > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
