using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_Configuracion.Cat_FiscaliaOestados;

namespace SIIGPP.Datos.M_Configuracion.Cat_FiscaliaOestados
{
    public class FiscaliaOestadoMap : IEntityTypeConfiguration<FiscaliaOestado>
    {
        public void Configure(EntityTypeBuilder<FiscaliaOestado> builder)
        {
            builder.ToTable("C_FISCALIAOESTADOS")
                   .HasKey(a => a.IdFiscaliaOestado);

            builder
           .Property(a => a.IdFiscaliaOestado)
           .HasDefaultValueSql("newId()");


            builder
            .Property(a => a.IdFiscaliaOestado)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();


            

        }
    }
}
