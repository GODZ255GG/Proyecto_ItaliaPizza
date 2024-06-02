using Domain;
using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using Logs;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarInsumo.xaml
    /// </summary>
    public partial class ConsultarInsumo : Window
    {
        InsumoManagerClient client = new InsumoManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

        private int idInsumos;
        private string nombre;
        private string marca;
        private string tipo;
        private string cantidad;
        private string codigo;
        private string medida;

        public ConsultarInsumo(Insumos insumo)
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;

            idInsumos = insumo.IdInsumos;
            nombre = insumo.Nombre;
            marca = insumo.Marca;
            tipo = insumo.Tipo;
            cantidad = insumo.CantidadDeEmpaque;
            codigo = insumo.CodigoInsumo;
            medida = insumo.UnidadDeMedida;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxCantidad.Text = cantidad;
            tbxCodigoInsumo.Text = codigo;
            cbxTipo.SelectedItem = tipo;
            cbxMedida.SelectedItem = medida;
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Masa y Harina",
                "Salsas y Bases",
                "Quesos",
                "Toppings y Proteínas",
                "Verduras y Hierbas",
                "Aceites y Condimentos",
                "Extras Creativos"
            };
            cbxTipo.ItemsSource = items;
        }

        private void CbxMedida_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Mililitro",
                "Gramo"
            };
            cbxMedida.ItemsSource = items;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarInsumoConStock(idInsumos))
            {
                Utilidades.Utilidades.MostrarMensaje("No se puede eliminar el insumo porque tiene stock asociado.", "Eliminación no permitida", MessageBoxImage.Warning);
                return;
            }
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el insumo?", "Confirmar eliminación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    client.EliminarInsumo(idInsumos);
                    Utilidades.Utilidades.MostrarMensaje("Insumo eliminado exitosamente", "Eliminación exitosa", MessageBoxImage.Information);
                    this.Close();
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

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Title = "Modificar Insumo";
            lbTitulo.Content = "Modificar Insumo";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = false;
            tbxMarca.IsReadOnly = false;
            tbxCantidad.IsReadOnly = false;
            cbxTipo.IsEnabled = true;
            cbxMedida.IsEnabled = true;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
           if (!CamposVacios())
            {
                if (StringValidos(nombre, marca))
                {
                    try
                    {
                        AccionModificar();
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
                else
                {
                    Utilidades.Utilidades.MostrarMensajeCamposInvalidos();
                }
            }
            else
            {
                Utilidades.Utilidades.MostrarMensajeCamposVacios();
            }
        }

        private void AccionModificar()
        {
            var nuevoNombre = tbxNombre.Text;
            var nuevaMarca = tbxMarca.Text;
            var nuevoTipo = cbxTipo.Text;
            var nuevaCantidad = tbxCantidad.Text;
            var nuevoCodigo = tbxCodigoInsumo.Text;
            var nuevaMedida = cbxMedida.Text;

            client.ActualizarInsumo(idInsumos, nuevoNombre, nuevoCodigo, nuevaMarca, nuevoTipo, nuevaCantidad, nuevaMedida);
            Utilidades.Utilidades.MostrarMensaje("El Insumo se ha actualizado exitosamente", "Actualización exitosa", MessageBoxImage.Information);
            this.Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Utilidades.MostrarMensajeConfirmacionCancelar())
            {
                Title = "Consultar Insumo";
                lbTitulo.Content = "Información del Insumo";

                btnAceptar.Visibility = Visibility.Hidden;
                btnCancelar.Visibility = Visibility.Hidden;
                btnModificar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;

                tbxNombre.IsReadOnly = true;
                tbxMarca.IsReadOnly = true;
                tbxCantidad.IsReadOnly = true;
                tbxCodigoInsumo.IsReadOnly = true;
                cbxTipo.IsEnabled = false;
                cbxMedida.IsEnabled = false;

                tbxNombre.Text = nombre;
                tbxMarca.Text = marca;
                tbxCantidad.Text = cantidad;
                tbxCodigoInsumo.Text = codigo;
                cbxTipo.SelectedItem = tipo;
                cbxMedida.SelectedItem = medida;
            }
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxMarca.Text) ||
                string.IsNullOrEmpty(tbxCantidad.Text) || string.IsNullOrEmpty(cbxTipo.Text))
            {
                return true;
            }
            return false;
        }
        private bool StringValidos(string nombre, string marca)
        {
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                !Regex.IsMatch(marca, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return false;
            }

            if (nombre.Length > 45 || marca.Length > 45)
            {
                return false;
            }

            return true;
        }
        #endregion

        private void TbxCantidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbxNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TbxMarca_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool ValidarInsumoConStock(int idInsumo)
        {
            try
            {
                var inventario = client.ObtenerInventarioDeInsumo(idInsumo);
                if (inventario != null && inventario.CantidadTotal > 0)
                {
                    return false; // No se puede eliminar si hay stock
                }
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
                return false;
            }
            catch (CommunicationException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
                return false;
            }
            catch (TimeoutException ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
                return false;
            }

            return true; // Se puede eliminar si no hay stock
        }
    }
}