using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Hipotesis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Hipotesis
{
    
        public class HipotesisMap : IEntityTypeConfiguration<Hipotesis>
        {
            public void Configure(EntityTypeBuilder<Hipotesis> builder)
            {
                builder.ToTable("C_HIPOTESIS")
                        .HasKey(a => a.IdHipotesis);
                builder
                .Property(a => a.IdHipotesis)
                .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                .IsRequired();
                builder
               .Property(a => a.IdHipotesis)
               .HasDefaultValueSql("newId()");
            }
        }
 }

