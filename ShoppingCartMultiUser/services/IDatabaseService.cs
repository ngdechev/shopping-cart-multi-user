namespace ShoppingCartMultiUser.services
{
    internal interface IDatabaseService
    {
        void Init(); // read
        void Cleanup(); // save

        public string AddProduct(string productName, float productPrice, int productQuantity, string productDescription);
        public string DeleteProduct(int productId);
        public string UpdateProduct();
        public string ListProducts();
        public string SearchProducts(string searchTerm);
        public string UpdateProductQuantity();
    }
}
