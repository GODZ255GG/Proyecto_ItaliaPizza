using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ListaInsumos.xaml
    /// </summary>
    public partial class ListaInsumos : UserControl
    {
        InsumoManagerClient insumoServer = new InsumoManagerClient();

        public ListaInsumos()
        {
            InitializeComponent();
            MostrarInformacionInsumos();
        }

        private void MostrarInformacionInsumos()
        {
            try
            {
                Insumos[] insumoArray = insumoServer.ObtenerListaInsumos();
                List<Insumos> productos = insumoArray.ToList();

                dgListaInsumos.ItemsSource = productos;

            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void BtnRegistrarInsumo_Click(object sender, RoutedEventArgs e)
        {
            RegistrarInsumo registrar = new RegistrarInsumo();
            registrar.Closed += ActualizarTablaInsumos;
            registrar.Show();
        }

        private void ActualizarTablaInsumos(object sender, EventArgs e)
        {
            MostrarInformacionInsumos();
        }

        private void DgListaInsumos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgListaInsumos.SelectedItem != null)
            {
                Insumos insumoSeleccionado = dgListaInsumos.SelectedItem as Insumos;
                ConsultarInsumo consulta = new ConsultarInsumo(insumoSeleccionado);
                consulta.Closed += ActualizarTablaInsumos;
                consulta.Show();
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Insumos[] insumosArray = insumoServer.ObtenerListaInsumos();
            List<Insumos> insumos = insumosArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = insumos.Where(x => x.Nombre.Contains(tbx.Text)).ToList();
                dgListaInsumos.ItemsSource = null;
                dgListaInsumos.ItemsSource = filtrarLista;
            }
            else
            {
                dgListaInsumos.ItemsSource = insumos;
            }
        }
    }
}