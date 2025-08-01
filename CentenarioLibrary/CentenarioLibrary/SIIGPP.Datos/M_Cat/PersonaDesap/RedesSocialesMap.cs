using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;

namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class RedesSocialesMap : IEntityTypeConfiguration<RedesSociales>
    {
        public void Configure(EntityTypeBuilder<RedesSociales> builder)
        {
            builder.ToTable("C_REDESSOCIALES").HasKey(a => a.IdRedSocial);
            builder.Property(a => a.IdRedSocial).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdRedSocial).HasDefaultValueSql("newid()");
        }
    }
}
