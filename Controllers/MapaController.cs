using Foundation;
using System;
using UIKit;
using CoreLocation;
using tacocat.Controllers.MapaHelpers;
using System.Collections.Generic;
using tacocat.cosmosdb.models;
using System.Threading.Tasks;

namespace tacocat
{
    public partial class MapaController : MapaBase
    {
		List<Taqueria> taquerias;

		public MapaController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationController.NavigationBar.PrefersLargeTitles = true;
            ConfiguracionMapa();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        void ConfiguracionMapa()
        {
            this.map = mapa;
            AgregoMapa();
			// pongo el delegado
			mapDel = new BusquedaMapDelegate();
			mapDel.RegionChangedEvent += MapDel_RegionChangedEvent;
			map.Delegate = mapDel;

			this.BusquedaTaquerias();
        }

        void LimpiarMarkers()
		{
			map.RemoveAnnotations(map.Annotations);
		}
		void AgregarMarker(CLLocationCoordinate2D coordenadas, string titulo, string subtitulo, int? id_doc)
		{
			var m = new BasicMapAnnotation(coordenadas, titulo, subtitulo, id_doc);

			map.AddAnnotation(m);
		}

		void MapDel_RegionChangedEvent(object sender, EventArgs e)
		{
			this.BusquedaTaquerias();
		}

		async void BusquedaTaquerias()
		{
            try
            {
                var izqSuperior = new CLLocationCoordinate2D(map.CenterCoordinate.Latitude + map.Region.Span.LatitudeDelta / 2, map.CenterCoordinate.Longitude - map.Region.Span.LongitudeDelta / 2);
                var derSuperior = new CLLocationCoordinate2D(map.CenterCoordinate.Latitude + map.Region.Span.LatitudeDelta / 2, map.CenterCoordinate.Longitude + map.Region.Span.LongitudeDelta / 2);
                var izqInferior = new CLLocationCoordinate2D(map.CenterCoordinate.Latitude - map.Region.Span.LatitudeDelta / 2, map.CenterCoordinate.Longitude + map.Region.Span.LongitudeDelta / 2);
                var derInferior = new CLLocationCoordinate2D(map.CenterCoordinate.Latitude - map.Region.Span.LatitudeDelta / 2, map.CenterCoordinate.Longitude - map.Region.Span.LongitudeDelta / 2);

                var puntos = new List<Punto>();
                puntos.Add(new Punto() { Latitud = izqSuperior.Latitude, Longitud = izqSuperior.Longitude });
                puntos.Add(new Punto() { Latitud = derSuperior.Latitude, Longitud = derSuperior.Longitude });
                puntos.Add(new Punto() { Latitud = izqInferior.Latitude, Longitud = izqInferior.Longitude });
                puntos.Add(new Punto() { Latitud = derInferior.Latitude, Longitud = derInferior.Longitude });// Se debe cerrar el poligono


                taquerias = await Task.Run(() => cosmosdb.Taqueria.BuscarTaqueriasAsync(puntos, ciudad, pais));

                LimpiarMarkers();

                foreach (var tac in taquerias)
                {

                    int id = -1;
                    int.TryParse(tac.Id, out id);
                    AgregarMarker(new CLLocationCoordinate2D()
                    {
                        Latitude = tac.Punto.Position.Latitude,
                        Longitude = tac.Punto.Position.Longitude
                    }, tac.Nombre, $"{tac.Calle} {tac.NumExterior} {tac.Colonia}", id);
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
    }
}