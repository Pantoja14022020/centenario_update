using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;


namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class RPersonaDesapMap : IEntityTypeConfiguration<RPersonaDesap>
    {
        public void Configure(EntityTypeBuilder<RPersonaDesap> builder)
        {
            builder.ToTable("CAT_PERSONADESAPARECIDA").HasKey(a => a.IdPersonaDesaparecida);
            builder.Property(a => a.IdPersonaDesaparecida).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdPersonaDesaparecida).HasDefaultValueSql("newid()");
            builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
        }

    }
}