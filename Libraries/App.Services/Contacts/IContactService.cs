using App.Core;
using App.Core.Domain.Contacts;
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
    public interface IContactService
    {
        #region Contacts

        /// <summary>
        /// Get contact with associated addresses
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation and return contact</returns>
        Task<Contact> GetContactDetailsByIdAsync(int id);

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
        Task<IPagedList<Contact>> GetAllContactsAsync(
            string? email = null, string? name = null, string? phoneNumber = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        /// <summary>
        /// Insert a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertContactAsync(Contact contact);

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateContactAsync(Contact contact);

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteContactAsync(Contact contact);

        #endregion

        #region Contact Addresses

        /// <summary>
        /// Insert a address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertAddressAsync(Address address);

        /// <summary>
        /// Update a address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateAddressAsync(Address address);

        /// <summary>
        /// Validate add address (as per business logic)
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>return true in case of allow to add address otherwise false</returns>
        bool CanAddAddress(Address address);

        #endregion
    }
}
