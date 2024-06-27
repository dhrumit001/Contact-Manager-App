using App.Core.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    /// <summary>
    /// Represents an extension methods of ModelBuilder
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Methods to add prepopulate some data on database
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed contact data
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    Name = "Dhrumit",
                    PhoneNumber = "8000191594",
                    EmailAddress = "pateldhrumit7@gmail.com",
                    CreatedOnUtc = DateTime.UtcNow,
                },
                new Contact
                {
                    Id = 2,
                    Name = "Dharmesh",
                    PhoneNumber = "8000191595",
                    EmailAddress = "dharmesh.vasani1990@gmail.com",
                    CreatedOnUtc = DateTime.UtcNow,
                },
                new Contact
                {
                    Id = 3,
                    Name = "Ajay",
                    PhoneNumber = "8000191596",
                    EmailAddress = "ajay.chauhan@gmail.com",
                    CreatedOnUtc = DateTime.UtcNow,
                }
            );

            // Seed contact address
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    ContactId = 1,
                    Country = "India",
                    State = "Gujarat",
                    City = "Surat",
                    Street = "Shrushti Residency,Kosad,Amroli",
                    ZipPostalCode = "394107"
                },
                new Address
                {
                    Id = 2,
                    ContactId = 2,
                    Country = "India",
                    State = "Gujarat",
                    City = "Surat",
                    Street = "Kiran Pearl,Kosad,Amroli",
                    ZipPostalCode = "394107"
                }
            );
        }
    }
}
