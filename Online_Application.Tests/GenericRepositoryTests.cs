using Application.Dtos;
using Application.Mapping;
using Application.Services.Implementation.Generic;
using DAL.Data.Context;
using Domain;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Application.Tests
{
    public class GenericServiceTests
    {
        private Mock<IGenericRepository<Product>>? _repositoryMock;
        private Mock<IMappingService> ?_mapperMock;
        private GenericService<Product, ProductDto>? _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<Product>>();
            _mapperMock = new Mock<IMappingService>();
            _service = new GenericService<Product, ProductDto>(_repositoryMock.Object, _mapperMock.Object);


            _mapperMock.Setup(m => m.Map<ProductDto, Product>(It.IsAny<ProductDto>()))

           .Returns((ProductDto dto) => new Product { Id = dto.Id, Name = dto.Name });

            _mapperMock.Setup(m => m.Map<Product, ProductDto>(It.IsAny<Product>()))
                       .Returns((Product p) => new ProductDto { Id = p.Id, Name = p.Name });

            _mapperMock.Setup(m => m.MapList<Product, ProductDto>(It.IsAny<List<Product>>()))
                       .Returns((List<Product> list) => list.ConvertAll(p => new ProductDto { Id = p.Id, Name = p.Name }));

        }
        [Test]
        public async Task Add()
        {
            // Arrange
            var dto = new ProductDto { Id = Guid.NewGuid(), Name = "Test Product" };
            _repositoryMock!.Setup(r => r.Add(It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _service!.Add(dto);

            // Assert
            Assert.That(result, Is.True);

            _repositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task Delete()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock!.Setup(r => r.Delete(id)).ReturnsAsync(true);

            // Act
            var result = await _service!.Delete(id);

            // Assert
            Assert.That(result, Is.True);

            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }

        [Test]
        public async Task Update()
        {
            // Arrange
            var dto = new ProductDto { Id = Guid.NewGuid(), Name = "Updated Product" };
            _repositoryMock!.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _service!.Update(dto);

            // Assert
            Assert.That(result, Is.True);

            _repositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        }

    }
}
