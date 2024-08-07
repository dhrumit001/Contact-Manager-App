﻿using App.Core.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Mappings
{
    /// <summary>
    /// Represents a address entity mapping with database
    /// </summary>
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(nameof(Address));
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Country).HasMaxLength(100);
            builder.Property(s => s.State).HasMaxLength(100);
            builder.Property(s => s.City).HasMaxLength(100);
            builder.Property(s => s.Street).HasMaxLength(500);
            builder.Property(s => s.ZipPostalCode).HasMaxLength(50);
        }
    }
}
