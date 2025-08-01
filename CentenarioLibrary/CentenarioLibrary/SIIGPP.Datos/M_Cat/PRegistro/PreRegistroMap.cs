using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIIGPP.Entidades.M_Cat.PRegistro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIGPP.Datos.M_Cat.PRegistro
{
    public class PreRegistroMap: IEntityTypeConfiguration<PreRegistro>
    {
        public void Configure(EntityTypeBuilder<PreRegistro> builder)
        {
            builder.ToTable("PRE_REGISTRO").HasKey(a => a.IdPRegistro);

            builder.Property(a=>a.IdPRegistro).HasColumnType("UNIQUEIDENTIFIER ROWGUIDCOL")
            .IsRequired();

            builder
           .Property(a => a.IdPRegistro)
           .HasDefaultValueSql("newId()");


            builder.Property(a => a.DistritoId)
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

            builder.Property(a => a.Indicador)
                   .HasColumnType("NvarChar(MAX)");
            builder.Property(a => a.CveDistrito)
                   .HasColumnType("NvarChar(MAX)");

            builder.Property(a => a.Ano)
                   .HasColumnType("int");

            builder.Property(a => a.noReg)
                   .HasColumnType("varchar(25)");
            builder.Property(a => a.Asignado)
                   .HasColumnType("bit");
            builder.Property(a => a.Ndenuncia)
                   .HasColumnType("NvarChar(500)");

        }
    }
}
