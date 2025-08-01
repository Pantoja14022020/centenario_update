using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_IL.Agendas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_IL.Agendas
{
    public class AgendaMap : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("IL_AGENDA")
                    .HasKey(a => a.IdAgenda);

            builder.Property(a => a.IdAgenda)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER ")
            .IsRequired();

            builder
           .Property(a => a.IdAgenda)
           .HasDefaultValueSql("newId()");
        }
    }
}
