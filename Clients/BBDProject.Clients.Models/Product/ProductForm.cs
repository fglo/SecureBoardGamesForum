using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBDProject.Clients.Models.Product
{
    public class ProductForm
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public byte[] Image{ get; set; }
    }
}
