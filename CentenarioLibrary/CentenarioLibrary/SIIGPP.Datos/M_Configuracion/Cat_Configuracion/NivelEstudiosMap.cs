using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Configuracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Configuracion
{
    public class NivelEstudiosMap : IEntityTypeConfiguration<NivelEstudios>
    {
        public void Configure(EntityTypeBuilder<NivelEstudios> builder)
        {
            builder.ToTable("C_NIVELESTUDIOS")
             .HasKey(a => a.IdNivelEstudios);
            builder
          .Property(a => a.IdNivelEstudios)
          .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
          .IsRequired();
            builder
           .Property(a => a.IdNivelEstudios)
           .HasDefaultValueSql("newId()");
        }
    }
}
