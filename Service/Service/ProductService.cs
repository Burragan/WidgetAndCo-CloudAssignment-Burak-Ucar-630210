using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Service.Interface;

namespace WidgetAndCo.Service.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository ProductRepository;
        private IProductImageRepository ProductImageRepository;

        public ProductService(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            ProductRepository = productRepository;
            ProductImageRepository = productImageRepository;
        }

        public Product CreateProduct(Product product)
        {
            return ProductRepository.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            ProductRepository.Delete(product);
        }

        public List<Product> GetAllProducts()
        {
            return ProductRepository.GetAllProducts();
        }

        public Product GetProduct(int id)
        {
            return ProductRepository.GetSingle(id);
        }

        public Product UpdateProduct(Product product)
        {
            List<ProductImage> productImages = ProductImageRepository.getProductImageByProductId(product.Id);
            return ProductRepository.Update(product);
        }
    }
}
