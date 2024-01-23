using Laboratorium_3.Controllers;
using Laboratorium_3.Models;
using Laboratorium_3.Models.ContactModels;
using Laboratorium_3.Models.ReservationModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Laboratorium_9___UnitTests
{
    public class ContactControllerTest
    {
        private ContactController _controller;
        private IContactService _service;
        public ContactControllerTest()
        {
            _service = new MemoryContactService(new CurrentDateTimeProvider());
            _service.Add(new Contact() { Name = "Test1" });
            _service.Add(new Contact() { Name = "Test2" });
            _controller = new ContactController(_service);
        }

        [Fact]
        public async Task IndexTest()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
            var view = result as ViewResult;
            Assert.IsType<List<Contact>>(view?.Model);
            List<Contact>? contacts = view.Model as List<Contact>;
            Assert.NotNull(contacts);
            Assert.Equal(2, contacts.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DetailsTestForExistingContacts(int id)
        {
            var result = await _controller.Details(id);
            Assert.IsType<ViewResult>(result);
            var view = result as ViewResult;
            Assert.IsType<Contact>(view?.Model);
            Contact? contact = view.Model as Contact;
            Assert.NotNull(contact);
            Assert.Equal(id, contact.Id);
        }

        [Fact]
        public async Task DetailsTestForNonExistingContacts()
        {
            var result = await _controller.Details(3);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateTest()
        {
            Contact model = new Contact() { Name = "Test", Email = "test@wsei.pl", Phone = "123" };
            var count = (await _service.FindAllAsync()).Count;
            var result = await _controller.Create(model);
            Assert.Equal(count + 1, (await _service.FindAllAsync()).Count);
        }
    }
}