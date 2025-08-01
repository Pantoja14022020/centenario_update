using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_INEGI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_INEGI
{
    public class VialidadMap : IEntityTypeConfiguration<Vialidad>
    {
        public void Configure(EntityTypeBuilder<Vialidad> builder)
        {
            builder.ToTable("C_TIPO_VIALIDAD")
                 .HasKey(a => a.IdVialidad);

            builder
            .Property(a => a.IdVialidad)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdVialidad)
           .HasDefaultValueSql("newId()");
        }
    }
}
