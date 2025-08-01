using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Armas;

namespace SIIGPP.Datos.M_Configuracion.Cat_Armas
{
    public class ArmaObjetoMap: IEntityTypeConfiguration<ArmaObjeto>
    {
        public void Configure(EntityTypeBuilder<ArmaObjeto> builder)
        {
            builder.ToTable("CAR_ARMAOBJETO")
                .HasKey(a => a.IdArmaObjeto);

            builder
           .Property(a => a.IdArmaObjeto)
           .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
           .IsRequired();
            builder
          .Property(a => a.ClasificacionArmaId)
          .HasColumnType("UNIQUEIDENTIFIER")
          .IsRequired();
            builder
           .Property(a => a.IdArmaObjeto)
           .HasDefaultValueSql("newId()");
        }

       

    }
}
