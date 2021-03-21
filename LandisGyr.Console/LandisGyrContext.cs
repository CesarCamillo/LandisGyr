using LandisGyr.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LandisGyr.ConsoleApp
{
    /// <summary>
    /// Contexto para o aplicativo de console
    /// </summary>
    public class LandisGyrContext : DbContext
    {
        /// <summary>
        /// String de conexão com o Db
        /// </summary>
        public const string ConnectionStringKey = "LandisGyr";

        /// <summary>
        /// Novo contexto para o aplicativo
        /// </summary>
        /// <param name="options">Opções a serem definidas para o contexto</param>
        public LandisGyrContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Tabela de Endpoints
        /// </summary>
        public DbSet<Endpoint> Endpoints { get; protected set; }

        /// <summary>
        /// Configuração de criação dos modelos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
