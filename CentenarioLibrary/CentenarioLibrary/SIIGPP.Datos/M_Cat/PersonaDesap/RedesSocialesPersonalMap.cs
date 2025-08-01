using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;

namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class RedesSocialesPersonalMap : IEntityTypeConfiguration<RedesSocialesPersonal>
    {
        public void Configure(EntityTypeBuilder<RedesSocialesPersonal> builder)
        {
            builder.ToTable("CAT_REDESSOCIALES_PERSONAL").HasKey(a => a.IdRedesSocialesPersonal);
            builder.Property(a => a.IdRedesSocialesPersonal).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdRedesSocialesPersonal).HasDefaultValueSql("newid()");
            builder.Property(a => a.PersonaId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(a => a.RedSocialId).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
        }
    }
}
