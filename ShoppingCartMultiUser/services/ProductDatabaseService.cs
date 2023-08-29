using Newtonsoft.Json;
using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.services;

namespace ShoppingCartMultiUser
{
    internal class ProductDatabaseService : IDatabaseService
    {
        private Application _app;
        private List<Product> _products = new List<Product>();
        private string _filePath;

        public ProductDatabaseService(Application app)
        {
            _app = app;
            _products = _app.GetProducts();
            _filePath = _app.GetFilePath();
        }

        public void Init()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    _products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading data from file: {ex.Message}");
                    
                    _products = new List<Product>();
                }
            }
            else
                _products = new List<Product>();
        }

        public void Cleanup()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_products, Formatting.Indented);
                
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }

        public string AddProduct(string productName, float productPrice, int productQuantity, string productDescription)
        {
            Product product = new(productName, productPrice, productQuantity, productDescription);
            product.Id = GenerateUniqueProductId(_products);

            _products.Add(product);

            return $"{product.Name} is added to product list!";
        }

        public string DeleteProduct(int productId)
        {
            Product product = _products.Find(p => p.Id == productId);

            if (product != null)
            {
                _products.Remove(product);

                return $"{product.Name} is removed from product list!";
            }
            else return $"There is no product with id {productId}.";
        }

        public string ListProducts()
        {
            foreach (Product p in _products)
                if (_products.Count != 0)
                    return p.ToString();

            return "The product list is empty!";
        }

        public string SearchProducts(string searchTerm)
        {
            List<Product> searchResults = _products.FindAll(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

            if (searchTerm == "")
                return "Search term cannot be empty!";
            if (searchResults.Count < 0)
                return "No products found.";

             return searchResults.ToString();
        }

        public string UpdateProduct()
        {
            return "";
        }

        public string UpdateProductQuantity()
        {
            return "";
        }

        public static int GenerateUniqueProductId(List<Product> products)
        {
            int maxProductId = products.Any() ? products.Max(p => p.Id) : 0;

            return maxProductId + 1;
        }

        public Product GetProductById(int productId)
        {
            Product? foundedProduct = null;

            foreach (Product product in _app.GetProducts())
                if (product.Id == productId)
                    foundedProduct = product;
                else
                    Console.WriteLine($"There is no product with id {productId}.");

            return foundedProduct;
        }
    }
}
