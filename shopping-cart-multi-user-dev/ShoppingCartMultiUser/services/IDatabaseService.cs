namespace ShoppingCartMultiUser.services
{
    internal interface IDatabaseService
    {
        public void Init();
        public void Cleanup();

        public string AddProduct(string productName, float productPrice, int productQuantity, string productDescription);
        public string DeleteProduct(int productId);
        public string UpdateProduct(int parsedProductId, string productField, string newValue);
        public string ListProducts();
        public string SearchProducts(string searchTerm);
        public string UpdateProductQuantity(int parsedProductId, int parsedProductQuantity);
    }
}
