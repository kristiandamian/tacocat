using System;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace tacocat.cosmosdb
{
    public class Config : Resource
    {
        public static readonly string PrimaryKey = "";
        public static readonly string SecondaryKey = "";
		public static readonly string EndpointUri = "";
		public static readonly string DatabaseName = "";
		public static readonly string CollectionName = "";

		public static DocumentClient conexion()
		{
			ConnectionPolicy politicaConexion = new ConnectionPolicy();
			politicaConexion.ConnectionProtocol = Protocol.Https;
			var uri = new Uri(Config.EndpointUri);
			return new DocumentClient(uri, Config.PrimaryKey, politicaConexion);
		}

		public static Uri GenerarURI()
		{
			return UriFactory.CreateDocumentCollectionUri(Config.DatabaseName, Config.CollectionName);
		}
    }
}
