using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Domain
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Specification { get; set; }
        public List<ProductImage> Images { get; set; }

        public Product()
        {

        }
    }
}
