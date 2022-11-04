using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Service.Interface
{
    public interface IProductImageService
    {
        ProductImage AddImageToDB(ProductImage productImage);
    }
}
