using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_Estructura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_Estructura
{
    public class AgenciaMap : IEntityTypeConfiguration<Agencia>
    {
        public void Configure(EntityTypeBuilder<Agencia> builder)
        {
            builder.ToTable("C_AGENCIA")
                   .HasKey(a => a.IdAgencia);
            builder
           .Property(a => a.DSPId)
           .HasColumnType("UNIQUEIDENTIFIER ")
           .IsRequired();
            builder
           .Property(a => a.IdAgencia)
           .HasDefaultValueSql("newId()");
        }
    }
}
