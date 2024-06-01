using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using System;
using Logs;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Domain;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ListaProductos.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        ProductManagerClient productoServer = new ProductManagerClient();
        InsumoManagerClient InsumoManagerClient = new InsumoManagerClient();
        private static readonly ILog Log = Logger.GetLogger();
        public Stock()
        {
            InitializeComponent();
            MostrarInformacionPedidos();
            MostrarInformacionInsumos();
        }


        private void ActualizarTablaProductos(object sender, EventArgs e)
        {
            MostrarInformacionPedidos();
        }


        private void ActualizarTablaInsumos(object sender, EventArgs e)
        {
            MostrarInformacionInsumos();
        }


        private void ImgRegresar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void MostrarInformacionPedidos()
        {
            try
            {
                // Llama al método del servidor para obtener la lista de productos
                Productos[] productopArray = productoServer.ObtenerListaProductos();
                List<Productos> productos = productopArray.ToList();

                // Llama al método del servidor para obtener la lista de inventarios
                InventarioDeProductos[] inventarioArray = productoServer.InventarioDeProductos();
                List<InventarioDeProductos> inventario = inventarioArray.ToList();

                // Combina las listas de productos e inventario usando left join
                var productosConInventario = from p in productos
                                             join i in inventario on p.IdProductos equals i.IdProductos into pi
                                             from subI in pi.DefaultIfEmpty()
                                             select new Producto
                                             {
                                                 IdProductos = p.IdProductos,
                                                 Nombre = p.Nombre,
                                                 CodigoProducto = p.CodigoProducto,
                                                 CantidadTotal = subI?.CantidadTotal ?? 0


                                             };

                // Asigna la lista combinada al DataGrid
                dgListaProductos.ItemsSource = productosConInventario.ToList();
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void DgListaProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgListaProductos.SelectedItem != null)
                {
                    Producto productoSeleccionado = dgListaProductos.SelectedItem as Producto;
                    if (productoSeleccionado != null)
                    {
                        RegistrarStockProducto consulta = new RegistrarStockProducto(productoSeleccionado);
                        consulta.Closed += ActualizarTablaProductos;
                        consulta.Show();
                    }

                }
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

 
        private void MostrarInformacionInsumos()
        {
            try
            {
                // Llama al método del servidor para obtener la lista de productos
                Insumos[] insumoArray = InsumoManagerClient.ObtenerListaInsumos();
                List<Insumos> insumos = insumoArray.ToList();

                // Llama al método del servidor para obtener la lista de inventarios
                InventarioDelInsumo[] inventarioArray = InsumoManagerClient.InventarioDeInsumos();
                List<InventarioDelInsumo> inventario = inventarioArray.ToList();

                // Combina las listas de productos e inventario usando left join
                var productosConInventario = from p in insumos
                                             join i in inventario on p.IdInsumos equals i.IdInsumos into pi
                                             from subI in pi.DefaultIfEmpty()
                                             select new Insumo
                                             {
                                                 IdInsumos = p.IdInsumos,
                                                 Nombre = p.Nombre,
                                                 CantidadDeEmpaque = p.CantidadDeEmpaque,
                                                 CantidadTotal = subI?.CantidadTotal ?? 0


                                             };

                // Asigna la lista combinada al DataGrid
                dgListaInsumos.ItemsSource = productosConInventario.ToList();
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
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
            try
            {
                if (dgListaInsumos.SelectedItem != null)
                {
                    Insumo insumoSeleccionado = dgListaInsumos.SelectedItem as Insumo;
                    if (insumoSeleccionado != null)
                    {
                        RegistrarStockInsumo consulta = new RegistrarStockInsumo(insumoSeleccionado);
                        consulta.Closed += ActualizarTablaInsumos;
                        consulta.Show();
                    }
                }
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }
    }
}
