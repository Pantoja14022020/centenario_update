using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.PersonasVisita;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PI.PersonasVisita
{
    public class PIPersonaVisitaMap : IEntityTypeConfiguration<PIPersonaVisita>
    {
        public void Configure(EntityTypeBuilder<PIPersonaVisita> builder)
        {
            builder.ToTable("PI_PERSONAVISITA")
                    .HasKey(a => a.IdPIPersonaVisita);

            builder.Property(a => a.IdPIPersonaVisita)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdPIPersonaVisita)
           .HasDefaultValueSql("newId()");
        }
    }
}
