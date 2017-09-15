using Foundation;
using System;
using UIKit;
using tacocat.Controllers.MapaHelpers;
using CoreLocation;
using CoreGraphics;

namespace tacocat
{
    public partial class AgregarTaqueriaController : MapaBase
    {
        bool taqueriaAgregada = false;
        NSObject keyboardObserver;

        public AgregarTaqueriaController (IntPtr handle) : base (handle)
        {
            
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.NavigationController.NavigationBar.PrefersLargeTitles = true;
            ConfiguracionMapa();
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			keyboardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillChangeFrameNotification, OnKeyboardChangeFrame);
		}
		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardObserver, UIKeyboard.WillChangeFrameNotification, null);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		void ConfiguracionMapa()
		{
            this.map = mapa;
            AgregoMapa();
            var tapRecogniser = new UITapGestureRecognizer(this, new ObjCRuntime.Selector("MapTapSelector:"));
			map.AddGestureRecognizer(tapRecogniser);

		    map.Delegate = new AgregarTaqueriaView();
		}

        [Export("MapTapSelector:")]
        protected void OnMapTapped(UIGestureRecognizer sender)
        {
            if (!taqueriaAgregada)
            {
                CLLocationCoordinate2D tappedLocationCoord = map.ConvertPoint(sender.LocationInView(map), map);
                TaqueriaMapAnnotation pin = new TaqueriaMapAnnotation(tappedLocationCoord, "Taquería agregada", "Arrastre para mover", -1);

                this.map.AddAnnotation(pin);

                taqueriaAgregada = true;//solo puedo agregar un pin
            }
        }

		#region subo y bajo el text y boton
		void OnKeyboardChangeFrame(NSNotification obj)
		{
			var frame = View.ConvertRectFromView(UIKeyboard.FrameEndFromNotification(obj), null);
			var curve = UIKeyboard.AnimationCurveFromNotification(obj);
			var offset = View.Bounds.Height - frame.Y;
			View.LayoutIfNeeded();
			UIView.Animate(
				duration: UIKeyboard.AnimationDurationFromNotification(obj),
				delay: 0,
				options: (UIViewAnimationOptions)(curve << 16),
				animation: () => {
					stackConstraintBottom.Constant = offset - BottomLayoutGuide.Length;
					View.LayoutIfNeeded();
				},
				completion: null
			);
           
		}
		#endregion
	}
}