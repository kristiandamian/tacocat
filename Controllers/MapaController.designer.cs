// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace tacocat
{
    [Register ("MapaController")]
    partial class MapaController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapa { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mapa != null) {
                mapa.Dispose ();
                mapa = null;
            }
        }
    }
}