using Logs;
using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;
using Domain;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarProducto.xaml
    /// </summary>
    public partial class RegistrarStockProducto : Window
    {
        ProductManagerClient client = new ProductManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

        private int idProducto;
        private string nombre;
        private string marca;
        
        private string codigo;

        public RegistrarStockProducto(Producto producto)
        {
            InitializeComponent();

            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto), "El producto no puede ser null");
            }

            idProducto = producto.IdProductos;
            nombre = producto.Nombre;
            codigo = producto.CodigoProducto;
            marca = producto.Marca;

            tbxNombre.Text = nombre;
            tbxCodigoProducto.Text = codigo;
            
        }




  

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                if (StringValidos(nombre) && NumeroValido(tbxCantidad.Text))
                {
                    try
                    {
                        // Aquí puedes guardar la cantidad en la base de datos
                        int cantidad = int.Parse(tbxCantidad.Text);
                        GuardarCantidadEnBaseDeDatos(idProducto, cantidad);

                        Utilidades.Utilidades.MostrarMensaje("Registro del stock exitoso; ", "Registro exitoso", MessageBoxImage.Information);
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


        #region Validaciones

        public bool CamposVacios()
        {
            if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxCodigoProducto.Text) || string.IsNullOrEmpty(tbxCantidad.Text))
            {
                return true;
            }
            return false;
        }

        private bool StringValidos(string nombre)
        {
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return false;
            }

            if (nombre.Length > 45)
            {
                return false;
            }

            return true;
        }

        private bool NumeroValido(string cantidad)
        {
            if (int.TryParse(cantidad, out int valor))
            {
                if (valor >= 1 && valor <= 999)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        private void GuardarCantidadEnBaseDeDatos(int idProducto, int cantidad)
        {
            client.RegistrarStockProducto(idProducto, cantidad);
        }
    }
}
