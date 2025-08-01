using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("CA_USUARIO")
                 .HasKey(a => a.IdUsuario);

            builder.Property(a => a.IdUsuario)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();


            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.RolId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdUsuario)
           .HasDefaultValueSql("newId()");
        }
    }
}
