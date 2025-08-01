using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_MedFiliacionLarga
{
    internal class TipoRetrasoMap : IEntityTypeConfiguration<TipoRetraso>
    {
        public void Configure(EntityTypeBuilder<TipoRetraso> builder) 
        {
            builder.ToTable("C_TIPODERETRASO")
                   .HasKey(a => a.IdTipoDeRetraso);


            builder.Property(a => a.IdTipoDeRetraso)
                   .HasDefaultValueSql("newId()");
        }
    }
}
