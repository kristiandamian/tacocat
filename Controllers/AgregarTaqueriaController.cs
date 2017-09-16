using Foundation;
using System;
using UIKit;
using tacocat.Controllers.MapaHelpers;
using CoreLocation;
using CoreGraphics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace tacocat
{
    public partial class AgregarTaqueriaController : MapaBase
    {
        bool taqueriaAgregada = false;

        public AgregarTaqueriaController (IntPtr handle) : base (handle)
        {
            
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.NavigationController.NavigationBar.PrefersLargeTitles = true;
            ConfiguracionMapa();
            OcultoTeclado();
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
        }
		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
		}

        async partial void btnGuardarTouch(UIButton sender)
        {
            var annotations = map.Annotations;
            if (txtNombre.Text != null && txtNombre.Text != string.Empty && annotations!=null && annotations.Length>0)
            {
                var pin = annotations[0];
                cosmosdb.models.Taqueria taqueria = new cosmosdb.models.Taqueria();
                taqueria.Nombre = txtNombre.Text;
                taqueria.Ciudad = this.ciudad;
                taqueria.Pais = this.pais;
                taqueria.Particion = $"{this.pais}{this.ciudad}";
                taqueria.Punto = new Microsoft.Azure.Documents.Spatial.Point(pin.Coordinate.Longitude,
                                                                             pin.Coordinate.Latitude);
                var menu = new System.Collections.Generic.List<cosmosdb.models.Taco>();
                AgregoTaco(txtNombre1, txtDescripcion1, txtPrecio1, ref menu);
                AgregoTaco(txtNombre2, txtDescripcion2, txtPrecio2, ref menu);
                AgregoTaco(txtNombre3, txtDescripcion3, txtPrecio3, ref menu);
                taqueria.Menu = menu;
                await GraboTaqueria(taqueria);
            }
        }
        void AgregoTaco(UITextField nombre, UITextField Desc, UITextField precio, ref List<cosmosdb.models.Taco> menu)
        {
            if(nombre.Text!=string.Empty)
            {
                double costo = 0;
                double.TryParse(precio.Text, out costo);
                var taco = new cosmosdb.models.Taco();
                taco.Nombre = nombre.Text;
                taco.Precio = costo;
                taco.Descripcion = Desc.Text;
                //TODO hacer geolocalizacion inversa para obtener la dirección desde las coordenadas
                menu.Add(taco);
            }
        }

        async Task GraboTaqueria(cosmosdb.models.Taqueria taqueria)
        {
            await Task.Run(() => cosmosdb.Taqueria.InsertoTaqueria(taqueria));
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

        void OcultoTeclado()
        {
            txtNombre.ShouldReturn += (textField) =>
                                    {
                                        textField.ResignFirstResponder();
                                        return true;
                                    };
			txtNombre1.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtNombre2.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtNombre3.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtPrecio1.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtPrecio2.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtPrecio3.ShouldReturn += (textField) =>
                        			{
                        				textField.ResignFirstResponder();
                        				return true;
                        			};
			txtDescripcion1.ShouldReturn += (textField) =>
    			{
    				textField.ResignFirstResponder();
    				return true;
    			};
			txtDescripcion2.ShouldReturn += (textField) =>
    			{
    				textField.ResignFirstResponder();
    				return true;
    			};
			txtDescripcion3.ShouldReturn += (textField) =>
    			{
    				textField.ResignFirstResponder();
    				return true;
    			};
        }
	}
}