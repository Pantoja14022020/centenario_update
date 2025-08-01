using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Usuarios
{
    public class BitacoraUsuarioMap : IEntityTypeConfiguration<BitacoraUsuario>
    {
        public void Configure(EntityTypeBuilder<BitacoraUsuario> builder)
        {
            builder.ToTable("CA_BITACORAUSUARIOS")
                 .HasKey(a => a.IdBitacoraUsuario);

            builder.Property(a => a.IdBitacoraUsuario)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdBitacoraUsuario)
           .HasDefaultValueSql("newId()");
        }
    }
}
