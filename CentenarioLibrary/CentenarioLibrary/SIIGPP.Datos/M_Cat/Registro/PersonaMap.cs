using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using SIIGPP.Entidades.M_Cat.Registro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.Registro
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("CAT_PERSONA")
                    .HasKey(a => a.IdPersona);
            builder.Property(a => a.TipoPersona)
                  .HasColumnType("nvarchar(100)");
            builder.Property(a => a.RFC)
                  .HasColumnType("nvarchar(50)");
            builder.Property(a => a.RazonSocial)
                  .HasColumnType("nvarchar(100)");
            
            builder.Property(a => a.Nombre)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.ApellidoPaterno)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.ApellidoMaterno)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.FechaNacimiento)
                   .HasColumnType("nvarchar(20)");
            builder.Property(a => a.RFC)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Sexo)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.EstadoCivil)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Medionotificacion)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Telefono1)
                  .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Telefono2)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Correo)
                   .HasColumnType("nvarchar(100)");
            builder.Property(a => a.Nacionalidad)
                   .HasColumnType("nvarchar(50)");
            builder.Property(a => a.Ocupacion)
                   .HasColumnType("nvarchar(350)");
            builder.Property(a => a.NivelEstudio)
                  .HasColumnType("nvarchar(350)");
            builder.Property(a => a.Lengua)
                  .HasColumnType("nvarchar(200)");
            builder.Property(a => a.Religion)
                  .HasColumnType("nvarchar(250)");
            builder.Property(a => a.Discapacidad)
                  .HasColumnType("bit");
            builder.Property(a => a.TipoDiscapacidad)
                  .HasColumnType("nvarchar(500)");

            builder.Property(a => a.IdPersona)
            .HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdPersona)
           .HasDefaultValueSql("newId()");


        }
    }
}
