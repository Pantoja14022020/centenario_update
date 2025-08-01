using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_MedFiliacionLarga
{
    public class TipoLesionesMap : IEntityTypeConfiguration<TipoLesiones>
    {
        public void Configure(EntityTypeBuilder<TipoLesiones> builder)
        {
            builder.ToTable("C_TIPODELESIONES")
                   .HasKey(a => a.IdTipoDeLesiones);

            builder
           .Property(a => a.IdTipoDeLesiones)
           .HasDefaultValueSql("newId()");
        }
    }
}
