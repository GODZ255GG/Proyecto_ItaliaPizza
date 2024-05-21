using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarInsumo.xaml
    /// </summary>
    public partial class ConsultarInsumo : Window
    {
        InsumoManagerClient client = new InsumoManagerClient();

        private int idInsumos;
        private string nombre;
        private string marca;
        private string tipo;
        private string cantidad;
        private string codigo;

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

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxCantidad.Text = cantidad;
            tbxCodigoInsumo.Text = codigo;
            cbxTipo.SelectedItem = tipo;
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
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

            client.ActualizarInsumo(idInsumos, nuevoNombre, nuevoCodigo, nuevaMarca, nuevoTipo, nuevaCantidad);
            MessageBox.Show("Producto se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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

                tbxNombre.Text = nombre;
                tbxMarca.Text = marca;
                tbxCantidad.Text = cantidad;
                tbxCodigoInsumo.Text = codigo;
                cbxTipo.SelectedItem = tipo;
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
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") ||
                !Regex.IsMatch(marca, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
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
    }
}