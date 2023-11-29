//using Android.Net;
using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class NuevoProductoPage : ContentPage
{
    private Producto _producto;
    private readonly APIService _APIService;
    public NuevoProductoPage(APIService apiservice)
	{
		InitializeComponent();
        _APIService = apiservice;
    }

    public NuevoProductoPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        if (_producto != null)
        {
            Nombre.Text = _producto.Nombre;
            cantidad.Text = _producto.cantidad.ToString();
            Descripcion.Text = _producto.Descripcion;
        }
    }

    private async void OnClickGuardarNuevoProducto(object sender, EventArgs e)
    {
        if (_producto != null)
        {
            _producto.Nombre=Nombre.Text;
            _producto.cantidad = Int32.Parse(cantidad.Text);
            _producto.Descripcion = Descripcion.Text;
            await _APIService.PutProducto(_producto.IdProducto, _producto);
        }
        else
        {
            //int id = Utils.Utils.ListaProductos.Count + 1;
            Producto producto = new Producto()
            {
                //IdProducto = id,
                Nombre = Nombre.Text,
                Descripcion = Descripcion.Text,
                cantidad = Int32.Parse(cantidad.Text)
            };

            /*System.NullReferenceException
              Mensaje = Object reference not set to an instance of an object.*/
            await _APIService.PostProducto(producto);
            //Utils.Utils.ListaProductos.Add(producto);
        }
        await Navigation.PopAsync();

    }
}