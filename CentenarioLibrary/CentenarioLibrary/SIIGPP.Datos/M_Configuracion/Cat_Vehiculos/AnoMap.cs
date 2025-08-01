using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Vehiculos;


namespace SIIGPP.Datos.M_Configuracion.Cat_Vehiculos
{
    public class AnoMap : IEntityTypeConfiguration<Ano>
    {
        public void Configure(EntityTypeBuilder<Ano> builder)
        {
            builder.ToTable("CV_ANO")
                .HasKey(a => a.IdAno);
            builder
           .Property(a => a.IdAno)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
           .Property(a => a.IdAno)
           .HasDefaultValueSql("newId()");
        }

    }
}
