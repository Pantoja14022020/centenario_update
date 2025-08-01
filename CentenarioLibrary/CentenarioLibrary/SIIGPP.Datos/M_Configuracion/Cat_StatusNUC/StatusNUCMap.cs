using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_StatusNUC;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_StatusNUC
{
    public class StatusNUCMap : IEntityTypeConfiguration<StatusNUC>
    {
        public void Configure(EntityTypeBuilder<StatusNUC> builder)
        {
            builder.ToTable("C_STATUSNUC")
                .HasKey(a => a.IdStatusNuc);
            builder
           .Property(a => a.IdStatusNuc)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdStatusNuc)
           .HasDefaultValueSql("newId()");
        }
    }
}
