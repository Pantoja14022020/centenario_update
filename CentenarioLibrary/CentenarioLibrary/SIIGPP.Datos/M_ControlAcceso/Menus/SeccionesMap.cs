using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Menus
{
    public class SeccionesMap : IEntityTypeConfiguration<Seccion>
    {
        public void Configure(EntityTypeBuilder<Seccion> builder)
        {
            builder.ToTable("CA_SECCIONES")
                    .HasKey(a => a.IdSeccion);
            builder.Property(a => a.IdSeccion)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
        }
    }
}
