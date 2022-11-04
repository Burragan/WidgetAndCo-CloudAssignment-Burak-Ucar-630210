using Domain.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Domain.DTO
{
    public class ProductDTO
    {
        [JsonRequired]
        public string Name { get; set; }
        [JsonRequired]
        public double Price { get; set; }
        [JsonRequired]
        public string Specification { get; set; }
        [JsonRequired]
        public List<ProductImageDTO> Images { get; set; }
    }
}
