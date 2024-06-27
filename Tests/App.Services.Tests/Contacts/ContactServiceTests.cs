using App.Core.Domain.Contacts;
using App.Data.Repository;
using App.Services.Contacts;
using LearnProject.Services.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Tests.Contacts
{
    public class ContactServiceTests : ServiceTestBase
    {
        private IContactService? _contactService;
        private IRepository<Contact>? _contactRepository;
        private IRepository<Address>? _adressRepository;

        #region Setup

        [OneTimeSetUp]
        public new void Setup()
        {
            base.Setup();
            _contactRepository = ServiceProvider.GetService<IRepository<Contact>>();
            _adressRepository = ServiceProvider.GetService<IRepository<Address>>();
            _contactService = new ContactService(_contactRepository, _adressRepository);
        }

        #endregion

        #region Methods

        #region Contacts

        [TestCase(null, null, null, 0, 3, ExpectedResult = 3)]
        [TestCase("dhrumit", null, null, 0, 3, ExpectedResult = 1)]
        [TestCase("pateldhrumit7@gmail.com", null, null, 0, 3, ExpectedResult = 1)]
        [TestCase("dhrumit.patel@gmail.com", "dharmesh", null, 0, 3, ExpectedResult = 0)]
        public async Task<int> GetAllContactsAsync_Test(string? emailAddress = null, string? name = null, string? phoneNumber = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {

            //Arrange
            await AddMockDataAsync();

            //Act
            var result = await _contactService.GetAllContactsAsync(emailAddress, name, phoneNumber, pageIndex, pageSize);

            //Assert
            return result.Count;
        }

        #endregion

        #region Utilities

        private async Task AddMockDataAsync()
        {

            if (_contactRepository == null) return;


            //Remove all exisitng data
            foreach (var contact in _contactRepository.Table.ToList())
                await _contactRepository.DeleteAsync(contact);

            //Add mock data
            await _contactRepository.InsertAsync(new Contact
            {
                Name = "Dhurmit Patel",
                CreatedOnUtc = DateTime.UtcNow,
                EmailAddress = "pateldhrumit7@gmail.com",
                PhoneNumber = "8000191594"
            });

            await _contactRepository.InsertAsync(new Contact
            {
                Name = "Dharmesh",
                CreatedOnUtc = DateTime.UtcNow,
                EmailAddress = "dharmesh.vasani@gmail.com",
                PhoneNumber = "8000191596"
            });

            await _contactRepository.InsertAsync(new Contact
            {
                Name = "Ajay",
                CreatedOnUtc = DateTime.UtcNow,
                EmailAddress = "ajay.chauhan@gmail.com",
                PhoneNumber = "8000191598"
            });
        }

        #endregion

        #endregion
    }
}
