namespace ProductApi.Common.Interfaces;

public interface IProductIdGenerator
{
    Task<int> GenerateUniqueProductIdAsync();
}
