using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace tacocat.Controllers.MapaHelpers
{
    public class BusquedaMapDelegate : MKMapViewDelegate
    {
		string pId = "PinAnnotation";
		UIButton detailButton;
		public event EventHandler RegionChangedEvent;

        public BusquedaMapDelegate()
        {
        }

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
			anView.Draggable = false;

			detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);


			anView.RightCalloutAccessoryView = detailButton;

			return anView;
		}


		public override void RegionChanged(MKMapView mapView, bool animated)
		{
			if (RegionChangedEvent != null)
			{
				RegionChangedEvent(this, new EventArgs());
			}
		}
	}

	class BasicMapAnnotation : MKAnnotation
	{
		protected CLLocationCoordinate2D coord;

		public override CLLocationCoordinate2D Coordinate
		{
			get
			{
				return coord;
			}
		}

		protected string title;
		protected string subtitle;
        protected int? _id;

		
		public override string Title
		{ get { return title; } }

        public int? Id
		{ get { return _id; } }


		public override string Subtitle
		{ get { return subtitle; } }

        public BasicMapAnnotation(CLLocationCoordinate2D coord, string title, string subTitle, int? id)
			: base()
		{
			this.coord = coord;
			this.title = title;
			this.subtitle = subTitle;
			this._id = id;
		}
    }


}
