using App.Data.Repository;
using App.Services.Contacts;
using App.Core.Domain.Contacts;
using App.Web.Extensions;
using App.Web.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor;
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
            IMapper mapper,
            IRazorViewEngine razorViewEngine
            ) : base(razorViewEngine)
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

            var contact = _mapper.Map<Contact>(model);
            await _contactService.InsertContactAsync(contact);

            if (contact.Id > 0 && !model.Address.HasEmptyAddress())
            {
                var address = _mapper.Map<Address>(model.Address);
                address.ContactId = contact.Id;
                await _contactService.InsertAddressAsync(address);
            }

            TempData["SuccessNotification"] = "Contact added successfully.";
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<JsonResult> LoadContactViewPartial(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Contact does not exist"
                    });
                }

                var contact = await _contactService.GetDetailsByIdAsync(id);

                if (contact == null)
                    return Json(new
                    {
                        success = false,
                        message = "Contact does not exist"
                    });

                var model = _mapper.Map<ContactModel>(contact);

                return Json(new
                {
                    Result = RenderPartialViewToString("_LoadContactViewPartial", model),
                    Success = true
                });
            }
            catch
            {
                //Need to log exception here (somewhere in db or file)
                return Json(new { success = false, message = "Something went wrong." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Bad request." });

                var contact = await _contactService.GetDetailsByIdAsync(model.Id);

                if (contact is null)
                    return Json(new
                    {
                        success = false,
                        message = "Contact does not exist"
                    });

                contact.Name = model.Name;
                contact.EmailAddress = model.EmailAddress;
                contact.PhoneNumber = model.PhoneNumber;

                await _contactService.UpdateContactAsync(contact);
                model.Address.ContactId = contact.Id;
                await InsertUpdateAddress(model.Address, contact?.ContactAddress);

                return Json(new
                {
                    success = true,
                    message = "Contact updated successfully."
                });
            }
            catch
            {
                //Need to log exception here (somewhere in db or file)
                return Json(new { success = false, message = "Something went wrong." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveContact(int id)
        {
            try
            {
                var contact = await _contactService.GetDetailsByIdAsync(id);

                if (contact is null)
                    return Json(new
                    {
                        success = false,
                        message = "Contact does not exist"
                    });

                await _contactService.DeleteContactAsync(contact);

                return Json(new
                {
                    success = true,
                    message = "Contact removed successfully."
                });
            }
            catch
            {
                //Need to log exception here (somewhere in db or file)
                return Json(new { success = false, message = "Something went wrong." });
            }
        }

        #endregion

        #region Utilities()

        public async Task InsertUpdateAddress(ContactAddressModel model, Address address)
        {
            if ((!model.HasEmptyAddress() && address is not null)
                || model.HasEmptyAddress() && address is not null)
            {
                address.Street = model?.Street;
                address.City = model?.City;
                address.State = model?.State;
                address.Country = model?.Country;
                address.ZipPostalCode = model?.ZipPostalCode;

                await _contactService.UpdateAddressAsync(address);
            }
            else if (!model.HasEmptyAddress() && address is null)
            {
                var addressEntity = _mapper.Map<Address>(model);
                await _contactService.InsertAddressAsync(addressEntity);
            }
        }

        public async Task<ContactListModel> PrepareContactListModel(ContactSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get contacts
            var contacts = await _contactService.GetAllContactsAsync(
                emailAddress: searchModel.SearchEmailAddress,
                name: searchModel.SearchName,
                phoneNumber: searchModel.SearchPhoneNumber,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ContactListModel().PrepareToGrid(searchModel, contacts, () =>
            {
                return contacts.Select(contact =>
                {
                    var contactModel = _mapper.Map<ContactModel>(contact);
                    return contactModel;
                });
            });

            return model;
        }

        #endregion
    }
}
