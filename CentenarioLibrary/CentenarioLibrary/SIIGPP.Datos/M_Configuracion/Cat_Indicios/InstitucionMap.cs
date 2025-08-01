using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Indicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Indicios
{
    public class InstitucionMap : IEntityTypeConfiguration<Institucion>
    {

        public void Configure(EntityTypeBuilder<Institucion> builder)
        {
            builder.ToTable("CI_Institucion")
                .HasKey(a => a.IdInstitucion);
            builder
            .Property(a => a.IdInstitucion)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();
            builder
           .Property(a => a.IdInstitucion)
           .HasDefaultValueSql("newId()");

        }
    }
}
