using Newtonsoft.Json;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Spatial;
using System.Collections.Generic;

namespace tacocat.cosmosdb.models
{
    public class Taqueria : Resource
    {
        public string id_Taqueria { get; set; }
        public string Nombre { get; set; }
		public string Calle { get; set; }
		public string NumExterior { get; set; }
		public string NumInterior { get; set; }
		public string TelefonoUbicacion { get; set; }
		public string CorreoUbicacion { get; set; }
		public string Colonia { get; set; }
        public string Ciudad { get; set; }
		public string Estado { get; set; }
		public string Pais { get; set; }
        public Point Punto { get; set; }
        public List<Taco> Menu { get; set; }
    }
}
