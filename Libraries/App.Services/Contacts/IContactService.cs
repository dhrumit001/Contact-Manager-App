﻿using App.Core;
using App.Core.Domain.Contacts;

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
        Task<Contact> GetDetailsByIdAsync(int id);

        /// <summary>
        /// Gets all contacts
        /// </summary>
        /// <param name="emailAddress">email; null to load all contacts</param>
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
            string emailAddress = null, string name = null, string phoneNumber = null,
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

        #endregion
    }
}
