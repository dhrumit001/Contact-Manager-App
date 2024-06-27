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
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed contact
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "Dhrumit", PhoneNumber = "8000191594", EmailAddress = "pateldhrumit7@gmail.com" }
            );
        }
    }
}
