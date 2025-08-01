using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;


namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class DireccionOcupacionMap : IEntityTypeConfiguration<DireccionOcupacion>
    {
        public void Configure(EntityTypeBuilder<DireccionOcupacion> builder)
        {
            builder.ToTable("CAT_DIRECCION_OCUPACION").HasKey(a => a.IdDOcupacion);
            builder.Property(a => a.IdDOcupacion).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdDOcupacion).HasDefaultValueSql("newid()");
            builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
        }
    }
}
