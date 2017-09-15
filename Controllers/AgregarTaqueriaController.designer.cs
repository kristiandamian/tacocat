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
    [Register ("AgregarTaqueriaController")]
    partial class AgregarTaqueriaController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapa { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint MapaSizeConstraint { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint stackConstraintBottom { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDescripcion1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDescripcion2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDescripcion3 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombre { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombre1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombre2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombre3 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPrecio1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPrecio2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPrecio3 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mapa != null) {
                mapa.Dispose ();
                mapa = null;
            }

            if (MapaSizeConstraint != null) {
                MapaSizeConstraint.Dispose ();
                MapaSizeConstraint = null;
            }

            if (stackConstraintBottom != null) {
                stackConstraintBottom.Dispose ();
                stackConstraintBottom = null;
            }

            if (txtDescripcion1 != null) {
                txtDescripcion1.Dispose ();
                txtDescripcion1 = null;
            }

            if (txtDescripcion2 != null) {
                txtDescripcion2.Dispose ();
                txtDescripcion2 = null;
            }

            if (txtDescripcion3 != null) {
                txtDescripcion3.Dispose ();
                txtDescripcion3 = null;
            }

            if (txtNombre != null) {
                txtNombre.Dispose ();
                txtNombre = null;
            }

            if (txtNombre1 != null) {
                txtNombre1.Dispose ();
                txtNombre1 = null;
            }

            if (txtNombre2 != null) {
                txtNombre2.Dispose ();
                txtNombre2 = null;
            }

            if (txtNombre3 != null) {
                txtNombre3.Dispose ();
                txtNombre3 = null;
            }

            if (txtPrecio1 != null) {
                txtPrecio1.Dispose ();
                txtPrecio1 = null;
            }

            if (txtPrecio2 != null) {
                txtPrecio2.Dispose ();
                txtPrecio2 = null;
            }

            if (txtPrecio3 != null) {
                txtPrecio3.Dispose ();
                txtPrecio3 = null;
            }
        }
    }
}