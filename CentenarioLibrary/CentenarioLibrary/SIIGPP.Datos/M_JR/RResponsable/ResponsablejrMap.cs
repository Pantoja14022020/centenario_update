using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_JR.RResponsable;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_JR.RResponsable
{
    public class ResponsablejrMap : IEntityTypeConfiguration<Responsablejr>

    {
       
            public void Configure(EntityTypeBuilder<Responsablejr> builder)
            {
                builder.ToTable("JR_RESPONSABLE")
                       .HasKey(a => a.IdResponsable);

                builder.Property(a => a.IdResponsable)
                       .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
                       .IsRequired();

                builder.Property(a => a.IdResponsable)
                       .HasDefaultValueSql("newId()");

                builder.Property(a => a.PersonaId)
                       .HasColumnType("UNIQUEIDENTIFIER ")
                       .IsRequired();

                //builder.Property(a => a.FechaNacimiento)
                  //      .HasColumnType("Varchar");

              

                

                

                
            }

        
    }
}
