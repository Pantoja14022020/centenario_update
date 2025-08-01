using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_ControlAcceso.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_ControlAcceso.Menus
{
    public class ModulosMap : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            builder.ToTable("CA_MODULOS")
                    .HasKey(a => a.IdModulo);
            builder.Property(a => a.IdModulo)
                    .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                    .IsRequired();
            builder.Property(a => a.IdModulo)
                    .HasDefaultValueSql("newId()");
           
        }
    }
}
