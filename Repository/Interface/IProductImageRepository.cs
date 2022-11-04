using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;

namespace WidgetAndCo.Repository.Interface
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        List<ProductImage> getProductImageByProductId(int productId);
    }
}
