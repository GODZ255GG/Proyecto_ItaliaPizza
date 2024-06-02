using Domain;
using ItaliaPizzaClient.ItaliaPizzaServer;
using log4net;
using Logs;
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
    public partial class RegistrarStockInsumo : Window
    {
        InsumoManagerClient client = new InsumoManagerClient();
        private static readonly ILog Log = Logger.GetLogger();

        private int idInsumos;
        private string nombre;
        private string cantidadEmpaque;
        

        public RegistrarStockInsumo(Insumo insumo)
        {
            InitializeComponent();

            if (insumo == null)
            {
                throw new ArgumentNullException(nameof(insumo), "El insumo no puede ser null");
            }


            idInsumos = insumo.IdInsumos;
            nombre = insumo.Nombre;

            cantidadEmpaque = insumo.CantidadDeEmpaque;
            
            tbxNombre.Text = nombre;
            
            tbxCantidad.Text = cantidadEmpaque;
           
            
        }









        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                if (StringValidos(nombre) && NumeroValido(tbxCantidadTotal.Text))
                {
                    try
                    {
                        // Aquí puedes guardar la cantidad en la base de datos
                        int cantidad = int.Parse(tbxCantidadTotal.Text);
                        GuardarCantidadEnBaseDeDatos(idInsumos, cantidad);

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
            if (string.IsNullOrEmpty(tbxNombre.Text) || string.IsNullOrEmpty(tbxCantidadTotal.Text) || string.IsNullOrEmpty(tbxCantidad.Text))
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

        private void GuardarCantidadEnBaseDeDatos(int idInsumo, int cantidad)
        {
            // Lógica para guardar la cantidad de stock en la base de datos
            // Por ejemplo, podrías llamar a un método en el servicio WCF:
            client.RegistrarStockInsumo(idInsumo, cantidad);
        }
    }
}
