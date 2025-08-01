using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;

namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class VehiculoPersonaDesapMap : IEntityTypeConfiguration<VehiculoPersonaDesap>
    {
        public void Configure(EntityTypeBuilder<VehiculoPersonaDesap> builder)
        {
            builder.ToTable("CAT_VEHICULO_DESAPARICIONPERSONA").HasKey(a => a.IdVehDesaparicionPersona);
            builder.Property(a => a.IdVehDesaparicionPersona).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdVehDesaparicionPersona).HasDefaultValueSql("newid()");
            builder.Property(a => a.PersonaDesaparecidaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            //builder.Property(a => a.TipovId).HasColumnType("UNIQUEIDENTIFIER");
            //builder.Property(a => a.AnoId).HasColumnType("UNIQUEIDENTIFIER");
            //builder.Property(a => a.ColorId).HasColumnType("UNIQUEIDENTIFIER");
            //builder.Property(a => a.ModeloId).HasColumnType("UNIQUEIDENTIFIER");
            //builder.Property(a => a.MarcaId).HasColumnType("UNIQUEIDENTIFIER");
        }
    }
}
