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
        
        private string cantidad;
        

        public RegistrarStockInsumo(Insumos insumo)
        {
            InitializeComponent();
            

            idInsumos = insumo.IdInsumos;
            nombre = insumo.Nombre;
            
            cantidad = insumo.CantidadDeEmpaque;
            
            tbxNombre.Text = nombre;
            
            tbxCantidad.Text = cantidad;
           
            
        }









        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                if (StringValidos(nombre))
                {
                    try
                    {
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
            if (string.IsNullOrEmpty(tbxNombre.Text) || 
                string.IsNullOrEmpty(tbxCantidad.Text))
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
        #endregion
    }
}