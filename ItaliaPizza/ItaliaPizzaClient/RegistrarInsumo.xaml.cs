﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarInsumo.xaml
    /// </summary>
    public partial class RegistrarInsumo : Window
    {
        public RegistrarInsumo()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
            tbxCodigoInsumo.Text = "IM" + Utilidades.Utilidades.GenerarCodigo();
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

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = tbxNombre.Text;
            var marca = tbxMarca.Text;

            if (!CamposVacios())
            {
                if (StringValidos(nombre, marca))
                {
                    try
                    {
                        AccionRegistrar();
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.Utilidades.MostrarMensajeConfirmacionRegresar())
            {
                this.Close();
            }
        }

        private void AccionRegistrar()
        {
            var nombre = tbxNombre.Text;
            var codigoInsumo = tbxCodigoInsumo.Text;
            var marca = tbxMarca.Text;
            var tipo = cbxTipo.Text;
            var cantidad = tbxCantidad.Text;

            ItaliaPizzaServer.InsumoManagerClient client = new ItaliaPizzaServer.InsumoManagerClient();

            ItaliaPizzaServer.Insumos nuevoInsumo = new ItaliaPizzaServer.Insumos()
            {
                Nombre = nombre,
                CodigoInsumo = codigoInsumo,
                Marca = marca,
                Tipo = tipo,
                CantidadDeEmpaque = cantidad
            };

            if (client.InsumoYaRegistrado(nombre))
            {
                Utilidades.Utilidades.MostrarMensaje("Este insumo ya se encuentra registrado en el sistema, intente con otro", "Insumo duplicado", MessageBoxImage.Error);
                return;
            }

            client.RegistrarInsumo(nuevoInsumo);
            Utilidades.Utilidades.MostrarMensaje("El insumo se ha registrado exitosamente", "Registro exitoso", MessageBoxImage.Information);
            this.Close();
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