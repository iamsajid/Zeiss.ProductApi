namespace ProductApi.Common.Constants;

public static class RouteConstants
{
    public const string CreateProduct = "POST api/products";
    public const string GetAllProducts = "GET api/products";
    public const string GetProductById = "GET api/products/{id}";
    public const string UpdateProduct = "PUT api/products/{id}";
    public const string DeleteProduct = "DELETE api/products/{id}";
    public const string AddToStock = "PUT api/products/add-to-stock/{id}/{quantity}";
    public const string RemoveFromStock = "PUT api/products/decrement-stock/{id}/{quantity}";
}