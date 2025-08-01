using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Usuarios
{
    public class ClavesAgenciaMap : IEntityTypeConfiguration<ClavesAgencia>
    {
        public void Configure(EntityTypeBuilder<ClavesAgencia> builder)
        {
            builder.ToTable("C_CLAVESAGENCIAS")
                      .HasKey(a => a.IdClave);

            builder.Property(a => a.IdClave)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdClave)
           .HasDefaultValueSql("newId()");
        }
    }
}
