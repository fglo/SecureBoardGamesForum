using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Clients.Models.Product;

namespace BBDProject.Clients.Repositories.Product
{
    public interface IProductRepository
    {
        Task<int> Create(ProductForm productForm);
        Task<ProductViewModel> Get(int id);
        Task<ProductViewModel> Get(string name);
        Task<List<ProductViewModel>> GetAll(string searchedPhrase = "");
    }
}
