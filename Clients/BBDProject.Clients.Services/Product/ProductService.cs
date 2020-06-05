using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Product;
using BBDProject.Shared.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBDProject.Clients.Repositories.Product;

namespace BBDProject.Clients.Services.Product
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Create(ProductForm productForm)
        {
            return await _productRepository.Create(productForm);
        }

        public async Task<ProductViewModel> Get(int id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<ProductViewModel> Get(string name)
        {
            return await _productRepository.Get(name);
        }

        public async Task<List<ProductViewModel>> GetAll(string searchedPhrase = "")
        {
            return await _productRepository.GetAll(searchedPhrase);
        }
    }
}
