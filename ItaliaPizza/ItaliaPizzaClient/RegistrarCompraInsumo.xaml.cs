using ItaliaPizzaClient.ItaliaPizzaServer;
using ItaliaPizzaClient.Utilidades;
using log4net;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarCompraInsumo.xaml
    /// </summary>
    public partial class RegistrarCompraInsumo : Window
    {
        ItaliaPizzaServer.ProcurementManagerClient compra = new ItaliaPizzaServer.ProcurementManagerClient();
        ItaliaPizzaServer.SupplierManagerClient proveedor = new ItaliaPizzaServer.SupplierManagerClient();

        private static readonly ILog Log = Logger.GetLogger();
        private List<int> idsInsumosSeleccionados = new List<int>();
        private List<ItaliaPizzaServer.Insumos> insumosSeleccionados = new List<ItaliaPizzaServer.Insumos>();
        string categoria = "";
        int idProveedor = 0;
        public RegistrarCompraInsumo()
        {
            InitializeComponent();
            Loaded += CbxProveedores_Loaded;
        }

        private void CbxProveedores_Loaded(object sender, RoutedEventArgs e)
        {
            Proveedor[] proveedores = proveedor.ObtenerListaProveedor();
            cbxProveedores.ItemsSource = proveedores;
            cbxProveedores.DisplayMemberPath = "NombreContacto";
            cbxProveedores.SelectedValuePath = "IdProveedores";
        }

        private void CbxProveedores_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxProveedores.SelectedItem != null)
            {
                Proveedor proveedorSeleccionado = (Proveedor)cbxProveedores.SelectedItem;
                idProveedor = proveedorSeleccionado.IdProveedores;
                categoria = proveedorSeleccionado.CategoriaProveedor;
                MostrarInformacionInsumos();
            }
        }

        private void MostrarInformacionInsumos()
        {
            try
            {
                Insumos[] insumoArray = compra.RecuperarInsumosPorCategoria(categoria);
                List<Insumos> productos = insumoArray.ToList();
                dgInsumos.ItemsSource = productos;
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Utilidades.MostrarMensajeConfirmacionRegresar())
            {
                this.Close();
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistroAccion();
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

        private void RegistroAccion()
        {
            int[] idInsumosArray = idsInsumosSeleccionados.ToArray();

            if (cbxProveedores.SelectedItem != null && stackInsumos != null)
            {
                compra.RegistrarNuevaCompra(idProveedor, idInsumosArray);
                Utilidades.Utilidades.MostrarMensaje("Registro de compra de insumos exitosamente", "Compra De Insumos", MessageBoxImage.Information);
                this.Close();
            }
        }

        private void BtnLimpiarPedido_Click(object sender, RoutedEventArgs e)
        {
            insumosSeleccionados.Clear();
            stackInsumos.Children.Clear();
            ValidarEstadoComboBox();
        }

        private void MostrarInsumosSeleccionados()
        {
            stackInsumos.Children.Clear();

            Dictionary<int, int> insumosContados = new Dictionary<int, int>();

            foreach (var insumo in insumosSeleccionados)
            {
                if (insumosContados.ContainsKey(insumo.IdInsumos))
                {
                    insumosContados[insumo.IdInsumos]++;
                }
                else
                {
                    insumosContados.Add(insumo.IdInsumos, 1);
                }
            }

            foreach (var kvp in insumosContados)
            {
                var insumo = insumosSeleccionados.FirstOrDefault(i => i.IdInsumos == kvp.Key);
                if (insumo != null)
                {
                    CrearItemInsumo(insumo, kvp.Value);
                }
            }

            // Validar si hay insumos seleccionados para bloquear/desbloquear el ComboBox
            ValidarEstadoComboBox();
        }

        private void CrearItemInsumo(ItaliaPizzaServer.Insumos insumos, int cantidad)
        {
            StackPanel item = new StackPanel();
            item.Orientation = Orientation.Horizontal;

            TextBlock insumoBlock = new TextBlock();
            insumoBlock.Text = $"{insumos.Nombre}(x{cantidad})";
            insumoBlock.FontSize = 16;
            item.Children.Add(insumoBlock);

            Button eliminar = new Button();
            eliminar.Content = "Eliminar";
            eliminar.Tag = insumos;
            eliminar.Click += EliminarInsumos_Click;
            item.Children.Add(eliminar);

            stackInsumos.Children.Add(item);
        }

        private void EliminarInsumos_Click(object sender, RoutedEventArgs e)
        {
            Button eliminar = sender as Button;
            if(eliminar != null)
            {
                var insumo = eliminar.Tag as ItaliaPizzaServer.Insumos;
                if(insumo != null )
                {
                    insumosSeleccionados.Remove(insumo);
                    idsInsumosSeleccionados.Remove(insumo.IdInsumos);

                    MostrarInsumosSeleccionados();

                }
            }
        }

        private void DgInsumos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgInsumos.SelectedItem != null)
            {
                var insumo = dgInsumos.SelectedItem as ItaliaPizzaServer.Insumos;
                if (insumo != null)
                {
                    int cantidadActual = insumosSeleccionados.Count(i => i.IdInsumos == insumo.IdInsumos);

                    if (cantidadActual >= 30)
                    {
                        Utilidades.Utilidades.MostrarMensaje("No puedes añadir más de 30 insumos del mismo tipo", "Límite de insumos alcanzados", MessageBoxImage.Warning);
                        return;
                    }

                    insumosSeleccionados.Add(insumo);
                    idsInsumosSeleccionados.Add(insumo.IdInsumos);
                    MostrarInsumosSeleccionados();
                }
            }
        }

        private void ValidarEstadoComboBox()
        {
            if (insumosSeleccionados.Any())
            {
                cbxProveedores.IsEnabled = false;
            }
            else
            {
                cbxProveedores.IsEnabled = true;
            }
        }
    }
}
