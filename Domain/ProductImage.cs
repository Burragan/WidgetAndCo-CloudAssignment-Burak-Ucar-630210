using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Domain
{
    public class ProductImage : IBaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageLink { get; set; }
    }
}
