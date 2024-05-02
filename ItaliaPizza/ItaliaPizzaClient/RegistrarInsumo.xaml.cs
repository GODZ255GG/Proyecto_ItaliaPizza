using System;
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
            var codigoInsumo = tbxCodigoInsumo.Text;
            var marca = tbxMarca.Text;
            var cantidad = tbxCantidad.Text;

            if (CamposVacios())
            {
                if (StringValidos(codigoInsumo, cantidad, nombre, marca) && StringLargos(nombre, marca, codigoInsumo, cantidad))
                {
                    try
                    {
                        AccionRegistrar();
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
                else
                {
                    MessageBox.Show("Los datos ingresados no son validos. Verifique sus datos", "Datos Invalidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Ingrese la información solicitada para continuar", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            var result = false;
            if (!client.InsumoYaRegistrado(nombre))
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Este insumo ya se encuentra registrado en el sistema, intente con otro", "Insumo duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (result)
            {
                var aux = client.RegistrarInsumo(nuevoInsumo);
                if (aux)
                {
                    MessageBox.Show("El insumo se ha registrado exitosamente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el insumo", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo registrar el insumo", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (tbxNombre.Text == string.Empty || tbxMarca.Text == string.Empty || tbxCantidad.Text == string.Empty
                || tbxCodigoInsumo.Text == string.Empty || cbxTipo.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string codigo, string cantidad, string nombre, string marca)
        {
            if (Regex.IsMatch(cantidad, @"^[a-zA-Z0-9\s]+$") && Regex.IsMatch(codigo, @"^[a-zA-Z0-9]+$") && Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$")
                && Regex.IsMatch(marca, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return true;
            }
            return false;
        }

        private bool StringLargos(string nombre, string marca, string codigo, string cantidad)
        {
            if (nombre.Length <= 45 || marca.Length <= 45 || codigo.Length <= 45 || cantidad.Length >= 45)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}