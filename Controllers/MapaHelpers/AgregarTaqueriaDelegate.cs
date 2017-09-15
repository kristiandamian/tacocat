using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace tacocat.Controllers.MapaHelpers
{
    public class AgregarTaqueriaView : MKMapViewDelegate
    {
        string pId = "PinAnnotation";
        UIButton detailButton;

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView anView;

            if (annotation is MKUserLocation)
                return null;


            // show pin annotation
            anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

            if (anView == null)
                anView = new MKPinAnnotationView(annotation, pId);

            ((MKPinAnnotationView)anView).PinColor = MKPinAnnotationColor.Red;
            anView.CanShowCallout = true;
            ((MKPinAnnotationView)anView).AnimatesDrop = true;
            anView.Draggable = true;

            detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);


            anView.RightCalloutAccessoryView = detailButton;

            return anView;
        }

        public override void ChangedDragState(MKMapView mapView, MKAnnotationView annotationView, MKAnnotationViewDragState newState, MKAnnotationViewDragState oldState)
        {
            if (newState == MKAnnotationViewDragState.Ending)
            {
                //((TaqueriaMapAnnotation)annotationView).SetCoordinate()
            }
        }
    }

    class TaqueriaMapAnnotation : BasicMapAnnotation
    {
        
        public TaqueriaMapAnnotation(CLLocationCoordinate2D coord, string title, string subTitle, int? id) : base(coord, title, subTitle, id)
        {
            
        }

        public override void SetCoordinate(CLLocationCoordinate2D value)
        {
            coord = value;
			var handler = LocationChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
        }

        public event EventHandler LocationChanged;

		public ILocation Location
		{
			get { return new Location { Latitude = Coordinate.Latitude, Longitude = Coordinate.Longitude }; }
			set
			{
				if (value.Equals(Location))
					return;

				UIView.Animate(1.0, () =>
				{
					WillChangeValue("coordinate");
					coord = new CLLocationCoordinate2D(value.Latitude, value.Longitude);
					DidChangeValue("coordinate");
				});
			}
		}
    }

    public class Location : ILocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public interface ILocation
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
