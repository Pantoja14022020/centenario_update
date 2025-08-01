using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class DistritoMap : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.ToTable("C_DISTRITO")
                   .HasKey(a => a.IdDistrito);
            builder
             .Property(a => a.IdDistrito)
             .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
             .IsRequired();
            builder
           .Property(a => a.IdDistrito)
           .HasDefaultValueSql("newId()");
        }
    }
}
