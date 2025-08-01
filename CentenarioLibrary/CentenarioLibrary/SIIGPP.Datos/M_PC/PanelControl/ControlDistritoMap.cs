using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Panel.M_PanelControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_PC
{
    public class ControlDistritoMap : IEntityTypeConfiguration<ControlDistrito>
    {
  

        public void Configure(EntityTypeBuilder<ControlDistrito> builder)
        {
            builder.ToTable("C_CONTROLDISTRITOS")
                     .HasKey(a => a.IdControlDistrito);

            builder.Property(a => a.IdControlDistrito)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdControlDistrito)
           .HasDefaultValueSql("newId()");
        }
    
    
    }
}
