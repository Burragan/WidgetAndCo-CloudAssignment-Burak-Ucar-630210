using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Service.Interface;

namespace Service.Service
{
    public class ProductImageService : IProductImageService
    {
        private IProductImageRepository ProductImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            ProductImageRepository = productImageRepository;
        }

        public ProductImage AddImageToDB(ProductImage productImage)
        {
            return ProductImageRepository.Add(productImage);
        }
    }
}
