using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_MultasCitatorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_MultasCitatorios
{
    public class MultaCitatoriosMap : IEntityTypeConfiguration<MultaCitatorios>
    {
        public void Configure(EntityTypeBuilder<MultaCitatorios> builder)
        {
            builder.ToTable("C_MULTACITATORIO")
                    .HasKey(a => a.IdMultaCitatorio);
            builder
           .Property(a => a.IdMultaCitatorio)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdMultaCitatorio)
           .HasDefaultValueSql("newId()");
           
        }
    }
}
