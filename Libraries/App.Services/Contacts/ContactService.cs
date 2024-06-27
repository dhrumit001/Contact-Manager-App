using App.Core;
using App.Core.Domain.Contacts;
using App.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Contacts
{
    /// <summary>
    /// Customer service interface
    /// </summary>
    public class ContactService : IContactService
    {
        #region Fields

        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<Address> _addressRepository;

        #endregion

        #region Ctor

        public ContactService(IRepository<Contact> contactRepository
            , IRepository<Address> addressRepository)
        {
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;
        }

        #endregion

        #region Methods

        #region Contacts

        /// <summary>
        /// Get contact with associated addresses
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation and return contact</returns>
        public async Task<Contact> GetContactDetailsByIdAsync(int id)
        {
            var contact = await _contactRepository.Table
                .Include(s => s.ContactAddress)
                .SingleOrDefaultAsync(x => x.Id == id);

            return contact;
        }

        /// <summary>
        /// Gets all contacts
        /// </summary>
        /// <param name="email">email; null to load all contacts</param>
        /// <param name="name">name; null to load all contacts</param>
        /// <param name="phoneNumber">phone number; null to load all contacts</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="getOnlyTotalCount">A value in indicating whether you want to load only total number of records. Set to "true" if you don't want to load data from database</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the contacts
        /// </returns>
        public async Task<IPagedList<Contact>> GetAllContactsAsync(
            string emailAddress = null, string name = null, string phoneNumber = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var customers = await _contactRepository.GetAllPagedAsync(query =>
            {

                if (!string.IsNullOrWhiteSpace(emailAddress))
                    query = query.Where(c => c.EmailAddress.Contains(emailAddress));

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(c => c.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(phoneNumber))
                    query = query.Where(c => c.PhoneNumber.Contains(phoneNumber));


                query = query.OrderByDescending(c => c.CreatedOnUtc);

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount);

            return customers;
        }


        /// <summary>
        /// Insert a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task InsertContactAsync(Contact contact)
        {
            contact.CreatedOnUtc = DateTime.UtcNow;
            return _contactRepository.InsertAsync(contact);
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task UpdateContactAsync(Contact contact)
        {
            contact.UpdatedOnUtc = DateTime.UtcNow;
            return _contactRepository.UpdateAsync(contact);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task DeleteContactAsync(Contact contact)
        {
            return _contactRepository.DeleteAsync(contact);
        }

        #endregion

        #region Contact Addresses

        /// <summary>
        /// Insert a address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task InsertAddressAsync(Address address)
        {
            return _addressRepository.InsertAsync(address);
        }

        /// <summary>
        /// Update a address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task UpdateAddressAsync(Address address)
        {
            return _addressRepository.UpdateAsync(address);
        }

        #endregion

        #endregion
    }
}
