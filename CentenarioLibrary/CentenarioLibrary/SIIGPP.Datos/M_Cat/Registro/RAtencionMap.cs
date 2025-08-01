using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Registro
{
    public class RAtencionMap : IEntityTypeConfiguration<RAtencion>
    {
        public void Configure(EntityTypeBuilder<RAtencion> builder)
        {
            builder.ToTable("CAT_RATENCON")
                   .HasKey(a => a.IdRAtencion);
            builder.Property(a => a.FechaHoraRegistro)
                   .HasColumnType("DateTime");
            builder.Property(a => a.u_Nombre)
                   .HasColumnType("NvarChar(500)");
            builder.Property(a => a.DistritoInicial)
                   .HasColumnType("NvarChar(500)");
            builder.Property(a => a.AgenciaInicial)
                   .HasColumnType("NvarChar(500)");
            builder.Property(a => a.StatusAtencion)
                   .HasColumnType("bit");
            builder.Property(a=>a.StatusRegistro)
                   .HasColumnType("bit"); 
            builder.Property(a => a.FechaHoraAtencion)
                   .HasColumnType("DateTime");
            builder.Property(a => a.FechaHoraCierre)
                   .HasColumnType("DateTime");

            builder.Property(a => a.IdRAtencion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.racId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdRAtencion)
           .HasDefaultValueSql("newId()");
        }
    }
}
