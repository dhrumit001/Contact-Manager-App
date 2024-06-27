using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Domain.Contacts;
using System.Reflection.Emit;

namespace App.Data.Mappings
{

    /// <summary>
    /// Represents a contact entity mapping with database
    /// </summary>
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact));
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.ContactAddress)
                .WithOne(e => e.Contact);
        }
    }
}
