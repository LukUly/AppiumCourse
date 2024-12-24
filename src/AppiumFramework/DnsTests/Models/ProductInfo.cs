using System.Text.RegularExpressions;

namespace AppiumFramework.DnsTests.Models
{
    public class ProductInfo
    {
        public string ProductCode;
        public string ProductName;
        public string ProductPrice;

        public ProductInfo(string name, string price) 
        {
            ProductName = name;
            ProductPrice = price;
            ProductCode = ExtractCode(name);
        }

        public override bool Equals(object obj)
        {
            if (obj is ProductInfo other)
            {
                return this.ProductPrice == other.ProductPrice &&
                       this.ProductCode == other.ProductCode;
            }
            return false;
        }

        public override string ToString() => $"Продукт [{ProductCode}] со стоимостью {ProductPrice}";

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ProductPrice.GetHashCode();
                hash = hash * 23 + (ProductCode?.GetHashCode() ?? 0);
                return hash;
            }
        }
        private static string ExtractCode(string name)
        {
            string pattern = @"\[[^\]]+\]";
            Match match = Regex.Match(name, pattern);

            if (match.Success)
            {
                return match.Groups[0].Value;
            }
            else 
            {
                throw new Exception($"Не удалось извлечь код продукта {name}");
            }
        }
    }
}
