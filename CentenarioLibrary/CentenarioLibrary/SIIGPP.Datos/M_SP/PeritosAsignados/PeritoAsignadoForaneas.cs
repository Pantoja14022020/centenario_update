using System;
using System.Collections.Generic;
using System.Text;
using SIIGPP.Entidades.M_SP.PeritosAsignados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SIIGPP.Datos.M_SP.PeritosAsignados
{
    public class PeritoAsignadoForaneasMap : IEntityTypeConfiguration<PeritosAsignadoForaneas>
    {
        public void Configure(EntityTypeBuilder<PeritosAsignadoForaneas> builder)
        {

            builder.ToTable("SP_PERITOSASIGNADOSFORANEAS")
                .HasKey(a => a.IdPeritosAsignadoForaneas);

            builder.Property(a => a.IdPeritosAsignadoForaneas)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder.Property(a => a.ModuloServicioId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();


            builder.Property(a => a.RDiligenciasForaneasId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder
           .Property(a => a.IdPeritosAsignadoForaneas)
           .HasDefaultValueSql("newId()");

        }

    }
}
