using System.Collections.Generic;
using System.Threading.Tasks;
using Laboratorium_3.Controllers;
using Laboratorium_3.Models.PostModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Laboratorium_9___UnitTests
{
    public class PostControllerTest
    {
        private PostController _controller;
        private Mock<IPostService> _serviceMock;

        public PostControllerTest()
        {
            _serviceMock = new Mock<IPostService>();
            _controller = new PostController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            var validModel = new Post();

            var result = await _controller.Create(validModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Update_ValidModel_RedirectsToIndex()
        {
            var validModel = new Post();

            var result = await _controller.Update(validModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_ValidModel_RedirectsToIndex()
        {
            var validModel = new Post { Id = 1 };

            var result = await _controller.Delete(validModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            _serviceMock.Verify(s => s.DeleteByIdAsync(validModel.Id), Times.Once);
        }
    }
}   
