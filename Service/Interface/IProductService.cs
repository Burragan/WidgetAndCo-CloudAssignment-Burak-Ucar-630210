using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;

namespace WidgetAndCo.Service.Interface
{
    public interface IProductService
    {
        Product CreateProduct(Product product);

        List<Product> GetAllProducts();
        Product GetProduct(int id);
        Product UpdateProduct(Product product);
        void DeleteProduct(Product product);

    }
}
