using App.Data.Repository;
using App.Services.Contacts;
using App.Core.Domain.Contacts;
using App.Web.Extensions;
using App.Web.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace App.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService,
            IRepository<Contact> contactRepository,
            IRepository<Address> addressRepository,
            IMapper mapper
            )
        {
            _contactService = contactService;
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        #region Contacts

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            //prepare model
            var model = new ContactSearchModel();
            model.SetGridPageSize();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ContactList(ContactSearchModel searchModel)
        {
            //prepare model
            var model = await PrepareContactListModel(searchModel);

            return DataTableJson(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var contactEntity = new Contact
            {
                Name = model.Name,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber
            };

            await _contactService.InsertContactAsync(contactEntity);

            ViewBag.SuccessNotification = "Contact added successfully.";
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var contact = await _contactRepository.GetByIdAsync(model.Id);
            contact.Name = model.Name;
            contact.EmailAddress = model.EmailAddress;
            contact.PhoneNumber = model.PhoneNumber;

            await _contactService.UpdateContactAsync(contact);

            ViewBag.SuccessNotification = "Contact updated successfully.";
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> View(int id)
        {
            var contact = await _contactService.GetContactDetailsByIdAsync(id);

            return View();
        }

        #endregion

        #region Utilities()

        public async Task<ContactListModel> PrepareContactListModel(ContactSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get contacts
            var contacts = await _contactService.GetAllContactsAsync(
                email: searchModel.SearchEmail,
                name: searchModel.SearchName,
                phoneNumber: searchModel.SearchPhoneNumber,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ContactListModel().PrepareToGrid(searchModel, contacts, () =>
            {
                return contacts.Select(contact =>
                {
                    //fill in model values from the entity
                    var contactModel = new ContactModel();

                    contactModel.Id = contact.Id;
                    contactModel.EmailAddress = contact.EmailAddress;
                    contactModel.PhoneNumber = contact.PhoneNumber;
                    contactModel.Name = contact.Name;

                    return contactModel;
                });
            });

            return model;
        }

        #endregion
    }
}
