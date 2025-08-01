using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Tipo_de_FrenteMap : IEntityTypeConfiguration<Tipo_de_Frente>
    {
        public void Configure(EntityTypeBuilder<Tipo_de_Frente> builder)
        {
            builder.ToTable("C_TIPO_DE_FRENTE")
                .HasKey(a => a.IdTipoFrente);
            builder
           .Property(a => a.IdTipoFrente)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdTipoFrente)
           .HasDefaultValueSql("newId()");
        }

    }
}
