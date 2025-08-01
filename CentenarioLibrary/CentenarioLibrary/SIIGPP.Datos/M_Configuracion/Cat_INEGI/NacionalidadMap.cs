using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class NacionalidadMap : IEntityTypeConfiguration<Nacionalidad>
    {
        public void Configure(EntityTypeBuilder<Nacionalidad> builder)
        {
            builder.ToTable("C_NACIONALIDAD")
                  .HasKey(a => a.IdNacionalidad);
            builder
            .Property(a => a.IdNacionalidad)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdNacionalidad)
           .HasDefaultValueSql("newId()");

        }
    }
}
