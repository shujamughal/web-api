using Lecture1.Controllers;
using Lecture1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture1.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public void GetAll_ReturnsOkWithProducts()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }


        [Fact]
        public void GetById_WhenProductExists_ReturnsOk()
        {
            // Arrange
            var controller = new ProductsController();

            var createResult = controller.Create(new Product
            {
                Name = "Test Product",
                Price = 100
            }) as CreatedAtActionResult;

            var createdProduct = createResult!.Value as Product;

            // Act
            var result = controller.GetById(createdProduct!.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        /*[Fact]
        public void GetById_WhenProductExists_ReturnsOk()
        {
            // Arrange
            var controller = new ProductsController();

            
            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }*/
        [Fact]
        public void GetById_WhenProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void GetExpensiveProducts_ReturnsOk()
        {
            // Arrange
            var controller = new ProductsController();

            // ensure expensive product exists
            controller.Create(new Product
            {
                Name = "Very Expensive",
                Price = 2000 // > 500
            });

            // Act
            var result = controller.GetExpensiveProducts();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        /*[Fact]
        public void GetExpensiveProducts_ReturnsOk()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.GetExpensiveProducts();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }*/
        [Fact]
        public void GetCheapProducts_ReturnsOk()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.GetCheapProducts();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Create_WhenValidProduct_ReturnsCreated()
        {
            // Arrange
            var controller = new ProductsController();
            var product = new Product
            {
                Name = "Test Product",
                Price = 100
            };

            // Act
            var result = controller.Create(product);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void CreateWithRoute_WhenValidProduct_ReturnsCreated()
        {
            // Arrange
            var controller = new ProductsController();
            var product = new Product
            {
                Name = "Smartwatch",
                Price = 200
            };

            // Act
            var result = controller.CreateWithRoute(product);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public void Update_WhenProductExists_ReturnsNoContent()
        {
            // Arrange
            var controller = new ProductsController();

            var createResult = controller.Create(new Product
            {
                Name = "Temp Product",
                Price = 100
            }) as CreatedAtActionResult;

            var createdProduct = createResult!.Value as Product;

            var updatedProduct = new Product
            {
                Id = createdProduct!.Id,
                Name = "Updated Name",
                Price = 150
            };

            // Act
            var result = controller.Update(createdProduct.Id, updatedProduct);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /*[Fact]
        public void Update_WhenProductExists_ReturnsNoContent()
        {
            // Arrange
            var controller = new ProductsController();
            var updatedProduct = new Product
            {
                Id = 1,
                Name = "Updated",
                Price = 150
            };

            // Act
            var result = controller.Update(1, updatedProduct);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }*/

        [Fact]
        public void Delete_WhenProductExists_ReturnsNoContent()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }





    }
}
