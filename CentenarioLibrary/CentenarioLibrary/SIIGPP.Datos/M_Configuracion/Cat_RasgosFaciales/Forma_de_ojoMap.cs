using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_RasgosFaciales;

namespace SIIGPP.Datos.M_Configuracion.Cat_RasgosFaciales
{
    public class Forma_de_ojoMap : IEntityTypeConfiguration<Forma_de_ojo>
    {
        public void Configure(EntityTypeBuilder<Forma_de_ojo> builder)
        {
            builder.ToTable("C_FORMA_OJO")
                .HasKey(a => a.IdForma_de_ojo);

            builder
           .Property(a => a.IdForma_de_ojo)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();

            builder
           .Property(a => a.IdForma_de_ojo)
           .HasDefaultValueSql("newId()");
        }

    }
}
