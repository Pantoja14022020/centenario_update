using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PersonaDesap;
namespace SIIGPP.Datos.M_Cat.PersonaDesap
{
    public class MarcaTelefonoMap : IEntityTypeConfiguration<MarcaTelefono>
    {
        public void Configure(EntityTypeBuilder<MarcaTelefono> builder)
        {
            builder.ToTable("C_MARCAS_TELEFONOS").HasKey(a => a.IdMarcaTelefono);
            builder.Property(a => a.IdMarcaTelefono).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL").IsRequired();
            builder.Property(a => a.IdMarcaTelefono).HasDefaultValueSql("newid()");         
        }

    }
}