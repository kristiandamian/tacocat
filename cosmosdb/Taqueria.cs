using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Spatial;
using tacocat.cosmosdb.helpers;

namespace tacocat.cosmosdb
{
    public class Taqueria
    {
        public Taqueria()
        {
        }

		static Uri collectionLink;

        public static async Task<List<models.Taqueria>> BuscarTaqueriasAsync(List<models.Punto> puntos, string ciudad, string pais)
        {
			List<models.Taqueria> taqueria = new List<models.Taqueria>();
			FeedResponse<models.Taqueria> docs = new FeedResponse<models.Taqueria>();
			DocumentClient client = Config.conexion();
			collectionLink = Config.GenerarURI();
            pais = pais.RemoveAccentsWithRegEx();

            var particion = $"{pais.ToUpper()}{ciudad.ToUpper()}";
			try
			{
				if (puntos.Count == 4)
				{
					Polygon poligono = new Polygon(new[]
					{
					new LinearRing(new [] {// se debe generar el poligono en orden inverso del reloj
                            new Position(puntos[0].Longitud, puntos[0].Latitud),//esquina superior izq.
                            new Position(puntos[3].Longitud, puntos[3].Latitud),//esquina inferior derecha.
                            new Position(puntos[2].Longitud, puntos[2].Latitud),//esquina inferior izq.
                            new Position(puntos[1].Longitud, puntos[1].Latitud),//esquina superior derecha.
                            new Position(puntos[0].Longitud, puntos[0].Latitud),//esquina superior izq. Se CIERRA el poligono
                    })
				});
					var query = client.CreateDocumentQuery<models.Taqueria>(collectionLink, new FeedOptions()
					{
						PartitionKey = new PartitionKey(particion)
					})
									.Where(d => d.Ciudad.ToUpper() == ciudad.ToUpper() &&
												d.Pais.ToUpper() == pais.ToUpper() &&
												d.Punto.Within(poligono))

									.AsDocumentQuery();

					docs = await query.ExecuteNextAsync<models.Taqueria>();
				}
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
				throw ex;
			}
			return docs.ToList<models.Taqueria>();
        }

        public static async Task InsertoTaqueria(models.Taqueria taqueria)
        {
			try
			{
                DocumentClient client = Config.conexion();
				collectionLink = Config.GenerarURI();
				await client.CreateDocumentAsync(collectionLink, taqueria);
			}
			catch (DocumentClientException de)
			{   
			    Console.WriteLine(de.Message);	
				throw;
			}
        }
    }
}
