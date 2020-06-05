using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Product;

namespace BBDProject.Clients.Services.Product
{
    public interface IProductService
    {
        Task<int> Create(ProductForm productForm);
        Task<ProductViewModel> Get(int id);
        Task<ProductViewModel> Get(string name);
        Task<List<ProductViewModel>> GetAll(string searchedPhrase = "");
    }
}
