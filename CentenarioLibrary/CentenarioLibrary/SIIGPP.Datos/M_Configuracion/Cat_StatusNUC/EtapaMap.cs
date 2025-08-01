using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_StatusNUC;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_StatusNUC
{
    public class EtapaMap : IEntityTypeConfiguration<Etapa>
    {
        public void Configure(EntityTypeBuilder<Etapa> builder)
        {
            builder.ToTable("C_ETAPA")
                .HasKey(a => a.IdEtapa);
            builder
           .Property(a => a.IdEtapa)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdEtapa)
           .HasDefaultValueSql("newId()");
        }
    }
}
