using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Configuracion.Cat_MedFiliacionLarga;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Configuracion.Cat_MedFiliacionLarga
{
    public class UnasNoSaludablesMap : IEntityTypeConfiguration<UnasNoSaludables>
    {
        public void Configure(EntityTypeBuilder<UnasNoSaludables> builder)
        {
            builder.ToTable("C_UÑASNOSALUDABLE")
                   .HasKey(a => a.IdUñasNoSaludables);

            builder
           .Property(a => a.IdUñasNoSaludables)
           .HasDefaultValueSql("newId()");
        }
    }
}
