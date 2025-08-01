using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_PI.CMedicosPR;
using System;
using System.Collections.Generic;
using System.Text;
namespace SIIGPP.Datos.M_PI.CMedicosPR
{
    public class CMedicoPRMap : IEntityTypeConfiguration<CMedicoPR>
    {
        public void Configure(EntityTypeBuilder<CMedicoPR> builder)
        {
            builder.ToTable("PI_CMEDICOPR")
                    .HasKey(a => a.IdCMedicoPR);

            builder.Property(a => a.IdCMedicoPR)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.PersonaId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdCMedicoPR)
           .HasDefaultValueSql("newId()");
        }
    }
}
