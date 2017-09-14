using Foundation;
using System;
using UIKit;

namespace tacocat
{
    public partial class MapaController : UIViewController
    {
        public MapaController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            this.NavigationController.NavigationBar.PrefersLargeTitles = true;
        }
    }
}