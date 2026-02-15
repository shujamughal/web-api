using Lecture1.Controllers;
using Lecture1.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Lecture1.Tests
{
    public class ProductsControllerTestsPro
    {
        // -------------------------------------------------------
        // Helper: creates controller
        // -------------------------------------------------------
        private ProductsController CreateController()
        {
            return new ProductsController();
        }

        // -------------------------------------------------------
        // TEST 1 — GET ALL
        // -------------------------------------------------------
        [Fact]
        public void GetAll_ReturnsOkWithProducts()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.NotEmpty(products);
        }

        // -------------------------------------------------------
        // TEST 2 — GET BY ID SUCCESS
        // -------------------------------------------------------
        [Fact]
        public void GetById_WhenProductExists_ReturnsCorrectProduct()
        {
            // Arrange
            var controller = CreateController();

            var created = controller.Create(new Product
            {
                Name = "Test Product",
                Price = 100
            }) as CreatedAtActionResult;

            var createdProduct = created!.Value as Product;

            // Act
            var result = controller.GetById(createdProduct!.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(createdProduct.Id, returnedProduct.Id);
            Assert.Equal("Test Product", returnedProduct.Name);
        }

        // -------------------------------------------------------
        // TEST 3 — GET BY ID NOT FOUND
        // -------------------------------------------------------
        [Fact]
        public void GetById_WhenProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.GetById(99999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // -------------------------------------------------------
        // TEST 4 — GET EXPENSIVE PRODUCTS
        // -------------------------------------------------------
        [Fact]
        public void GetExpensiveProducts_WhenExists_ReturnsOnlyExpensive()
        {
            // Arrange
            var controller = CreateController();

            controller.Create(new Product
            {
                Name = "Very Expensive",
                Price = 2000
            });

            // Act
            var result = controller.GetExpensiveProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.All(products, p => Assert.True(p.Price > 500));
        }

        // -------------------------------------------------------
        // TEST 5 — GET CHEAP PRODUCTS
        // -------------------------------------------------------
        [Fact]
        public void GetCheapProducts_ReturnsOkWithCheapProducts()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.GetCheapProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.All(products, p => Assert.True(p.Price <= 500));
        }

        // -------------------------------------------------------
        // TEST 6 — CREATE PRODUCT
        // -------------------------------------------------------
        [Fact]
        public void Create_WhenValidProduct_ReturnsCreatedWithProduct()
        {
            // Arrange
            var controller = CreateController();

            var newProduct = new Product
            {
                Name = "New Product",
                Price = 50
            };

            // Act
            var result = controller.Create(newProduct);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedProduct = Assert.IsType<Product>(createdResult.Value);

            Assert.Equal("New Product", returnedProduct.Name);
            Assert.True(returnedProduct.Id > 0);
        }

        // -------------------------------------------------------
        // TEST 7 — CREATE WITH ROUTE
        // -------------------------------------------------------
        [Fact]
        public void CreateWithRoute_WhenValidProduct_ReturnsCreatedAtRoute()
        {
            // Arrange
            var controller = CreateController();

            var product = new Product
            {
                Name = "Smartwatch",
                Price = 200
            };

            // Act
            var result = controller.CreateWithRoute(product);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnedProduct = Assert.IsType<Product>(createdResult.Value);

            Assert.Equal("Smartwatch", returnedProduct.Name);
        }

        // -------------------------------------------------------
        // TEST 8 — UPDATE SUCCESS
        // -------------------------------------------------------
        [Fact]
        public void Update_WhenProductExists_ReturnsNoContentAndUpdates()
        {
            // Arrange
            var controller = CreateController();

            var created = controller.Create(new Product
            {
                Name = "Temp",
                Price = 100
            }) as CreatedAtActionResult;

            var product = created!.Value as Product;

            var updatedProduct = new Product
            {
                Id = product!.Id,
                Name = "Updated Name",
                Price = 150
            };

            // Act
            var updateResult = controller.Update(product.Id, updatedProduct);
            var getResult = controller.GetById(product.Id);

            // Assert
            Assert.IsType<NoContentResult>(updateResult);

            var okResult = Assert.IsType<OkObjectResult>(getResult.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal("Updated Name", returnedProduct.Name);
            Assert.Equal(150, returnedProduct.Price);
        }

        // -------------------------------------------------------
        // TEST 9 — DELETE SUCCESS
        // -------------------------------------------------------
        [Fact]
        public void Delete_WhenProductExists_RemovesProduct()
        {
            // Arrange
            var controller = CreateController();

            var created = controller.Create(new Product
            {
                Name = "To Delete",
                Price = 10
            }) as CreatedAtActionResult;

            var product = created!.Value as Product;

            // Act
            var deleteResult = controller.Delete(product!.Id);
            var getResult = controller.GetById(product.Id);

            // Assert
            Assert.IsType<NoContentResult>(deleteResult);
            Assert.IsType<NotFoundResult>(getResult.Result);
        }
    }
}
