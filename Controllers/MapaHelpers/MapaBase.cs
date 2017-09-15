using System;
using System.Threading.Tasks;
using CoreLocation;
using MapKit;
using UIKit;

namespace tacocat.Controllers.MapaHelpers
{
    public class MapaBase : UIViewController
    {
        protected CLLocationCoordinate2D mapCenter;//posicion inicial del mapa
        protected BusquedaMapDelegate mapDel;
        protected CLLocationManager locationManager = new CLLocationManager();

		protected string pais = string.Empty;
		protected string ciudad = string.Empty;

        protected MKMapView map;

        public MapaBase(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            locationManager.RequestWhenInUseAuthorization();
        }

        protected void AgregoMapa()
        {
			map.MapType = MKMapType.Standard;
			map.Bounds = UIScreen.MainScreen.Bounds;//full screen baby

			map.ShowsUserLocation = false;

			MKReverseGeocoder geo = new MKReverseGeocoder(locationManager.Location.Coordinate);
			CLGeocoder geocoder = new CLGeocoder();

            Task.Factory.StartNew(async () =>
            {
				var res = await geocoder.ReverseGeocodeLocationAsync(locationManager.Location);
				Console.WriteLine(res[0].AdministrativeArea);

				pais = res[0].Country;
				ciudad = res[0].Locality;

            });

			// centro el mapa y pongo el zoom en la region
			mapCenter = new CLLocationCoordinate2D(locationManager.Location.Coordinate.Latitude,
													  locationManager.Location.Coordinate.Longitude);
			var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 2000, 2000);
			map.CenterCoordinate = mapCenter;
			map.Region = mapRegion;
        }
    }
}
