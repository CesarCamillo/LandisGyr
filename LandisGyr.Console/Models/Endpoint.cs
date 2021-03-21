using System.Linq;

namespace LandisGyr.ConsoleApp.Models
{
    /// <summary>
    /// Endpoint da empresa de energia
    /// </summary>
    public class Endpoint
    {
        /// <summary>
        /// Número serial do Endpoint
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Modelo do medidor do endpoint
        /// </summary>
        public MeterModels MeterModel { get; set; }
        
        /// <summary>
        /// Número do medidor do endpoint
        /// </summary>
        public int MeterNumber { get; set; }

        /// <summary>
        /// Versão do firmware do medidor do endpoint
        /// </summary>
        public string MeterFirmwareVersion { get; set; }

        /// <summary>
        /// Estadp do switch do endpoint
        /// </summary>
        public SwitchStates SwitchState { get; set; }

    }

    /// <summary>
    /// Modelos do medidor do endpoint
    /// </summary>
    public enum MeterModels
    {
        NSX1P2W = 16,
        NSX1P3W = 17,
        NSX2P3W = 18,
        NSX3P4W = 19
    }
    
    /// <summary>
    /// Estados do switch de um Endpoint
    /// </summary>
    public enum SwitchStates
    {
        Disconnected = 0,
        Connected = 1,
        Armed = 2
    }

    /// <summary>
    /// Queries relacionadas a Endpoints
    /// </summary>
    public static class EndpointQueries
    {
        /// <summary>
        /// Obtém todos os Endpoints
        /// </summary>
        /// <param name="endpoints"></param>
        /// <returns></returns>
        public static IQueryable<Endpoint> AllEndpoints(this IQueryable<Endpoint> endpoints) => endpoints;

        /// <summary>
        /// Filtra os Endpoints baseado num número serial
        /// </summary>
        /// <param name="endpoints"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static IQueryable<Endpoint> WithSerialNumber(this IQueryable<Endpoint> endpoints, string serial)
        {
            return endpoints
                .Where(e => e.SerialNumber.Equals(serial));
        }
    }
}
