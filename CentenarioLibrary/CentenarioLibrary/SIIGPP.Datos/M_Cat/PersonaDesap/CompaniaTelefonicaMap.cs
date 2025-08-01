using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class CompaniaTelefonicaMap : IEntityTypeConfiguration<CompaniaTelefonica>
    {
        public void Configure(EntityTypeBuilder<CompaniaTelefonica> builder)
        {
            builder.ToTable("C_COMPANIA_TELEFONICA").HasKey(a => a.IdCompaniaTelefonica);
            builder.Property(a => a.IdCompaniaTelefonica).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdCompaniaTelefonica).HasDefaultValueSql("newid()");
            builder.Property(a => a.IdCompaniaTelefonica).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
        }

    }
}