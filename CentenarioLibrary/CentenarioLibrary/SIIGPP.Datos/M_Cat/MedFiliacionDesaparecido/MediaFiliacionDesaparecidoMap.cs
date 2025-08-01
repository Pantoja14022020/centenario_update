using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.MedFiliacionDesaparecido;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.MedFiliacionDesaparecido
{
    internal class MediaFiliacionDesaparecidoMap : IEntityTypeConfiguration<MediaFiliacionDesaparecido>
    {
        public void Configure(EntityTypeBuilder<MediaFiliacionDesaparecido> builder)
        {
            builder.ToTable("CAT_MEDIAFILIACIONDESAPARECIDO")
                   .HasKey(a => a.IdMediaFiliacionDesaparecido);

            builder
           .Property(a => a.IdMediaFiliacionDesaparecido)
           .HasDefaultValueSql("newId()");

            builder.Property(a => a.MediaFiliacionId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();
        }
    }
}
