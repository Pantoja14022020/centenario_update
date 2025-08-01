using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using SIIGPP.Entidades.M_ControlAcceso.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControAcceso.Roles
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("CA_ROL")
           .HasKey(a => a.IdRol);

            builder.Property(a => a.IdRol)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdRol)
           .HasDefaultValueSql("newId()");
        }
    }
}
