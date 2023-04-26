using Xunit;
using MacroFit.API;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MacroFit;
using MacroFit.Data;
using MacroFit.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MacroFit.Tests
{
    public class SearchControllerTests
    {
        private readonly SearchController _controller;
        private readonly MacroFitContext _context;

        public SearchControllerTests()
        {
            var builder = WebApplication.CreateBuilder(new string[] { });

            builder.Services.AddDbContext<MacroFitContext>(options =>
                options.UseInMemoryDatabase(databaseName: "test"));

            builder.Services.AddHttpClient();

            var app = builder.Build();

            _context = app.Services.GetService<MacroFitContext>();

            _controller = new SearchController(app.Services.GetService<HttpClient>(), _context);
        }

        [Fact]
        public async Task SearchFoods_ReturnsBadRequest_WhenQueryIsNullOrEmpty()
        {
            // Act
            var result = await _controller.SearchFoods(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchFoods_ReturnsNotFound_WhenNoFoodsMatchQuery()
        {
            // Arrange
            var testFood = new Food { Barcode = "1234567890123", Name = "Test Food" };
            _context.Foods.Add(testFood);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.SearchFoods("ThisQueryShouldNotMatchAnyFood");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);

            // Clean up
            _context.Foods.Remove(testFood);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task SearchFoods_ReturnsCorrectFoods_WhenFoodsMatchQuery()
        {
            // Arrange
            var testFood1 = new Food { Barcode = "1234567890123", Name = "Test Food 1" };
            var testFood2 = new Food { Barcode = "2345678901234", Name = "Test Food 2" };
            _context.Foods.Add(testFood1);
            _context.Foods.Add(testFood2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.SearchFoods("Test");

            // Assert
            var foods = Assert.IsType<List<Food>>(result.Value);
            Assert.True(foods.Count >= 2);
            foreach (var food in foods)
            {
                if (food.Barcode == testFood1.Barcode)
                {
                    Assert.Equal(testFood1.Name, food.Name);
                }
                else if (food.Barcode == testFood2.Barcode)
                {
                    Assert.Equal(testFood2.Name, food.Name);
                }
            }
            // Clean up
            _context.Foods.Remove(testFood1);
            _context.Foods.Remove(testFood2);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAsync_ReturnsBadRequest_WhenCodeIsNullOrEmpty()
        {
            // Act
            var result = await _controller.GetAsync(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenBarcodeDoesNotExist()
        {
            // Act
            var result = await _controller.GetAsync("01010101013");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchController.FoodResponse>(okResult.Value);
            Assert.Equal(400, response.Status);
        }

        [Fact]
        public async Task GetAsync_ReturnsCorrectFood_WhenBarcodeExists()
        {
            // Act
            var result = await _controller.GetAsync("5060161309317");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchController.FoodResponse>(okResult.Value);
            Assert.Equal(200, response.Status);
            Assert.Equal("5060161309317", response.Barcode);
            Assert.Equal("Scimx", response.FoodResponseDetails.Name);
            Assert.Equal(14.2, response.FoodResponseDetails.Fat);
            Assert.Equal(30, response.FoodResponseDetails.Protein);
            Assert.Equal(35.5, response.FoodResponseDetails.Carbohydrates);
        }
    }
}
