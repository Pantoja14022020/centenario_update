using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.RActosInvestigacion;
using SIIGPP.Entidades.M_Administracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Administracion
{
    public class LogRActosInvestigacionMap : IEntityTypeConfiguration<LogRActoInvestigacion>
    {
        public void Configure(EntityTypeBuilder<LogRActoInvestigacion> builder)
        {
            builder.ToTable("LOG_RACTOSINVESTIGACION")
                .HasKey(a => a.IdAdminRActosInvestigacion);

            builder.Property(a => a.IdAdminRActosInvestigacion)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();

            builder.Property(a => a.RHechoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(a => a.DistritoId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder
                .Property(a => a.IdAdminRActosInvestigacion)
                .HasDefaultValueSql("newId()");

            builder.Property(a => a.LogAdmonId)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
        }
    }
}