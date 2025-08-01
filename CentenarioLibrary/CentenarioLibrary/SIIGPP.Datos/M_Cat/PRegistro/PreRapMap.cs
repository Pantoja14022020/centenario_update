using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PRegistro;
using SIIGPP.Entidades.M_Cat.PAtencion;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.PRegistro
{
    public class PreRapMap : IEntityTypeConfiguration<PreRap>
    {
        public void Configure(EntityTypeBuilder<PreRap> builder)
        {
            builder.ToTable("PRE_RAP").HasKey(a => a.IdPRap);
            builder.Property(a => a.IdPRap).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdPRap).HasDefaultValueSql("newId()");
            builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(a => a.PAtencionId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();

        }
    }
}
