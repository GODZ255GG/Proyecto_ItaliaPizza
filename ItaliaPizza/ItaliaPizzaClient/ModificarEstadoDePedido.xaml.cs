using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for ModificarEstadoDePedido.xaml
    /// </summary>
    public partial class ModificarEstadoDePedido : Window
    {
        public Pedidos Pedido { get; set; }
        private string rol = Domain.Empleados.EmpleadosClient.Rol;

        public ModificarEstadoDePedido(Pedidos pedido)
        {
            InitializeComponent();
            Pedido = pedido;
            DataContext = Pedido;

            if (rol == "Cajero" || rol =="Gerente")
            {
                CargarBotonesCajero();
            }
            else if (rol == "Cocinero")
            {
                CargarBotonesCocinero();
            }
        }

        private void BtnAceptarEstado_Clic(object sender, RoutedEventArgs e)
        {
            string nuevoEstado = "";

            if (rbEnPreparacion.IsChecked == true)
            {
                nuevoEstado = "En preparación";
            }
            else if (rbListoParaEntrega.IsChecked == true)
            {
                nuevoEstado = "Listo para entrega";
            }
            else if (rbEnRutaDeEntrega.IsChecked == true)
            {
                nuevoEstado = "En ruta de entrega";
            }
            else if (rbEntregado.IsChecked == true)
            {
                nuevoEstado = "Entregado";
            }
            else if (rbCancelado.IsChecked == true)
            {
                nuevoEstado = "Cancelado";
            }

            if (nuevoEstado == Pedido.EstadoDelPedido)
            {
                Utilidades.Utilidades.MostrarMensaje("El estado seleccionado es igual al estado actual del pedido.", "Mismo estado seleccionado", MessageBoxImage.Warning);
                return;
            }

            try
            {
                ItaliaPizzaServer.OrderManagerClient modificarEstadoServer = new ItaliaPizzaServer.OrderManagerClient();
                bool modificacionExitosa = modificarEstadoServer.ModificarEstadoDePedido(Pedido.IdPedidos, nuevoEstado);

                if (modificacionExitosa)
                {
                    Utilidades.Utilidades.MostrarMensaje("El estado del pedido se ha modificado correctamente.", "Estado cambiado", MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Error al modificar el estado del pedido.");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }

            this.Close();
        }

        private void CargarBotonesCajero()
        {
            string estado = Pedido.EstadoDelPedido;
            if (estado == "En preparación")
            {
                rbListoParaEntrega.Visibility = Visibility.Visible;
                rbEntregado.Visibility = Visibility.Visible;
                rbCancelado.Visibility = Visibility.Visible;
                rbEnRutaDeEntrega.Visibility = Visibility.Visible;
            }
            else
            {
                rbEnPreparacion.Visibility = Visibility.Visible;
                rbListoParaEntrega.Visibility = Visibility.Visible;
                rbEntregado.Visibility = Visibility.Visible;
                rbCancelado.Visibility = Visibility.Visible;
                rbEnRutaDeEntrega.Visibility = Visibility.Visible;
            }
        }

        private void CargarBotonesCocinero()
        {
            string estado = Pedido.EstadoDelPedido;
            if (estado == "En preparación")
            {
                rbListoParaEntrega.Visibility = Visibility.Visible;
            }
            else
            {
                rbEnPreparacion.Visibility = Visibility.Visible;
                rbListoParaEntrega.Visibility = Visibility.Visible;
            }
        }

        private void BtnCancelarEstado_Clic(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
