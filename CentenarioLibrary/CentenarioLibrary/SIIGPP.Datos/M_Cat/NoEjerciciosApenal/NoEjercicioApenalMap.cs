using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.NoEjerciciosApenal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.NoEjerciciosApenal
{
    public class NoEjercicioApenalMap : IEntityTypeConfiguration<NoEjercicioApenal>
    {
        public void Configure(EntityTypeBuilder<NoEjercicioApenal> builder)
        {
            builder.ToTable("CAT_NOEJERCICIOAPENAL")
                .HasKey(a => a.IdNoEjercicioApenal);

            builder.Property(a => a.IdNoEjercicioApenal)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.RHechoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdNoEjercicioApenal)
           .HasDefaultValueSql("newId()");

        }
    }
}
