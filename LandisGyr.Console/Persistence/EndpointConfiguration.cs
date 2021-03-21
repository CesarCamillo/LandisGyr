using LandisGyr.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LandisGyr.ConsoleApp.Persistence
{
    /// <summary>
    /// Configuração do Endpoint
    /// </summary>
    public class EndpointConfiguration : IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            // Definindo nome da tabela a ser usada
            builder.ToTable("Endpoints");

            // Mapeando a chave primária
            builder.HasKey(e => e.SerialNumber);

            // Mapeando as propriedades do Endpoint
            builder
                .Property(e => e.SerialNumber)
                .HasMaxLength(255);

            builder
                .Property(e => e.MeterModel)
                .IsRequired();

            builder
                .Property(e => e.MeterNumber)
                .IsRequired();

            builder
                .Property(e => e.MeterFirmwareVersion)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(e => e.SwitchState)
                .IsRequired();
        }
    }
}
