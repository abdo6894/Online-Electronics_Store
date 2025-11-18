using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation;
using Domain;
using Infrastructure.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Application.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository>? _productRepoMock;
        private Mock<IMappingService>? _mapperMock;
        private ProductService? _productService;

        [SetUp]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMappingService>();
            _productService = new ProductService(_productRepoMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetProductsWithCategory_ReturnsMappedDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name="Phone"},
                new Product { Id = Guid.NewGuid(), Name="Laptop"}
            };

            var mappedProducts = new List<ProductDto>
            {
                new ProductDto { Id = products[0].Id, Name="Phone"},
                new ProductDto { Id = products[1].Id, Name="Laptop"}
            };

            _productRepoMock!
                .Setup(r => r.GetAllWithCategoryAsync())
                .ReturnsAsync(products);

            _mapperMock!
                .Setup(m => m.MapList<Product, ProductDto>(products))
                .Returns(mappedProducts);

            // Act
            var result = await _productService!.GetProductsWithCategory();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Phone"));

        }


        [Test]
        public async Task GetProductWithCategoryById_ReturnsMappedDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product { Id = id, Name = "TV" };
            var mappedProduct = new ProductDto { Id = id, Name = "TV" };

            _productRepoMock!
                .Setup(r => r.GetByIdWithCategoryAsync(id))
                .ReturnsAsync(product);

            _mapperMock!
                .Setup(m => m.Map<Product, ProductDto>(product))
                .Returns(mappedProduct);

            // Act
            var result = await _productService!.GetProductWithCategoryById(id);

            // Assert
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo("TV"));

        }
    }
}