using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    interface IProductService
    {
        bool AddProduct(AddProductOptions options);
    }
}
