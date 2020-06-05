using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Product;
using BBDProject.Shared.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BBDProject.Clients.Repositories.Product
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<int> Create(ProductForm productForm)
        {
            var dao = Mapper.Map<DaoProduct>(productForm);
            dao.DateAdded = DateTime.Now;
            dao.NameNormalized = productForm.Name.SoftNormalize();
            var entityEntry = await DbContext.AddAsync(dao);
            await DbContext.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }

        public async Task<ProductViewModel> Get(int id)
        {
            return Mapper.Map<ProductViewModel>(await DbContext.Products.FirstOrDefaultAsync(_ => _.Id == id));
        }

        public async Task<ProductViewModel> Get(string name)
        {
            return Mapper.Map<ProductViewModel>(await DbContext.Products.FirstOrDefaultAsync(_ => _.NameNormalized.Equals(name.SoftNormalize())));
        }

        public async Task<List<ProductViewModel>> GetAll(string searchedPhrase = "")
        {
            var products = DbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchedPhrase))
            {
                products = products.Where(p => p.NameNormalized.Contains(searchedPhrase.SoftNormalize()));
            }

            products = products.OrderBy(p => p.DateAdded);
            return Mapper.Map<List<ProductViewModel>>(await products.ToListAsync());
        }
    }
}
