using System.Net;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Services.Chat;
using BBDProject.Clients.Services.Product;
using BBDProject.Clients.Services.User;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBDProject.Clients.WebApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            return View(await _productService.GetAll());
        }
    }
}