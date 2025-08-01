using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Usuarios
{
    public class TiempoSesionMap : IEntityTypeConfiguration<TiempoSesion>
    {
        public void Configure(EntityTypeBuilder<TiempoSesion> builder)
        {
            builder.ToTable("C_TIEMPOSESION")
                 .HasKey(a => a.IdTiempo);

            builder.Property(a => a.IdTiempo)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdTiempo)
           .HasDefaultValueSql("newId()");
        }
    }
}
