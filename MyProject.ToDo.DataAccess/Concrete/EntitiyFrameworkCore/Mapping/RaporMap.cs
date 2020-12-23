﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.ToDo.Entities.Concrete;

namespace MyProject.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Mapping
{
    public class RaporMap : IEntityTypeConfiguration<Rapor>
    {
        public void Configure(EntityTypeBuilder<Rapor> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.Tanim).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Detay).HasColumnType("ntext");

            builder.HasOne(I => I.Gorev).WithMany(I => I.Raporlar).HasForeignKey(I => I.GorevId);
        }
    }

}
