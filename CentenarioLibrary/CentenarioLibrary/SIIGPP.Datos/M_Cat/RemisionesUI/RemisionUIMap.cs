using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RemisionesUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.RemisionesUI
{
    public class RemisionUIMap : IEntityTypeConfiguration<RemisionUI>
    {
        public void Configure(EntityTypeBuilder<RemisionUI> builder)
        {
            builder.ToTable("CAT_REMISIONUI")
                    .HasKey(a => a.IdRemisionUI);

            builder.Property(a => a.IdRemisionUI)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRemisionUI)
           .HasDefaultValueSql("newId()");

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();
        }
    }
}
