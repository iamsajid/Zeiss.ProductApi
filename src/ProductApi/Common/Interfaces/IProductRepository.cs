namespace ProductApi.Common.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Domain;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
}
