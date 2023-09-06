using Newtonsoft.Json;
using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.services;

namespace ShoppingCartMultiUser
{
    internal class ProductDatabaseService : IDatabaseService
    {
        private Application _app;
        private List<Product>? _products;
        private string _filePath;
        private int _productId;

        public ProductDatabaseService(Application app)
        {
            _app = app;
            _products = _app.GetProducts();
            //_products = new();
            _filePath = _app.GetFilePath();
            
            _productId = 0;
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

            product.Id = _productId++;

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

            return $"There is no product with id {productId}.";
        }

        public string ListProducts()
        {
            string msg = "";

            if (_products.Count != 0)
                foreach (Product p in _products)
                    msg += p.ToString();
            else
                msg = "The product list is empty!";

            return msg;
        }

        public string SearchProducts(string searchTerm)
        {
            List<Product> searchResults = _products.FindAll(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            string products = "";

            if (searchTerm == "")
                return "Search term cannot be empty!";
            if (searchResults.Count < 0)
                return "No products found.";

            foreach (Product p in _products)
                products += p.ToString();

            return products;
        }

        public string UpdateProduct(int productId, string fieldToEdit, string newValue)
        {
            Product? productToEdit = GetProductById(productId);

            if (productToEdit == null)
                return $"Product with ID of {productId} was not found!";

            switch (fieldToEdit.ToLower())
            {
                case "name":
                    if (fieldToEdit == "name" && string.IsNullOrWhiteSpace(newValue))
                        return "Invalid product name. Product name cannot be empty or whitespace.";

                    productToEdit.Name = newValue;
                    break;
                case "description":
                    if (fieldToEdit == "description" && string.IsNullOrWhiteSpace(newValue))
                        return "Invalid description. Description cannot be empty or whitespace.";

                    productToEdit.Description = newValue;
                    break;
                case "quantity":
                    if (!int.TryParse(newValue, out int newQuantity) || newQuantity < 0)
                        return "Invalid quantity value. Please enter a non-negative integer.";

                    productToEdit.Quantity = newQuantity;
                    break;
                case "price":
                    if (!float.TryParse(newValue, out float newPrice) || newPrice < 0)
                        return "Invalid price value. Please enter a non-negative decimal.";

                    productToEdit.Price = newPrice;
                    break;
                default:
                    return "Invalid field for edit. Available options: name, description, quantity, price";
            }

            return $"Value for [{fieldToEdit}] was changed to [{newValue}]!";
        }

        public string UpdateProductQuantity(int productId, int productQuantity)
        {
            Product? productToEdit = GetProductById(productId);

            if (productToEdit == null)
                return $"Product with ID of {productId} was not found!";

            productToEdit.Quantity = productQuantity;

            return $"Product quantity is set to {productQuantity}.";
        }

        public Product GetProductById(int productId)
        {
            Product? foundedProduct = null;

            foreach (Product product in _products)
                if (product.Id == productId)
                    foundedProduct = product;
                else
                    return null;

            return foundedProduct;
        }
    }
}
