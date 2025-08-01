using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Forma_de_cabelloMap : IEntityTypeConfiguration<Forma_de_cabello>
    {
        public void Configure(EntityTypeBuilder<Forma_de_cabello> builder)
        {
            builder.ToTable("C_FORMA_DE_CABELLO")
                .HasKey(a => a.IdFormaCabello);
            builder
           .Property(a => a.IdFormaCabello)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdFormaCabello)
           .HasDefaultValueSql("newId()");
        }

    }
}
