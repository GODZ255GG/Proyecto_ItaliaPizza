using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {

        public RegistrarProducto()
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Pizza",
                "Bebida",
                "Postre",
                "Pasta"
            };
            cbxTipo.ItemsSource = items;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = tbxNombre.Text;
            var codigoProducto = tbxCodigoProducto.Text;
            var marca = tbxMarca.Text;
            var precio = tbxPrecio.Text;

            if (CamposVacios())
            {
                if (StringValidos(codigoProducto, precio, nombre, marca) && StringLargos(nombre, marca))
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

        private void AccionRegistrar()
        {
            var nombre = tbxNombre.Text;
            var codigoProducto = tbxCodigoProducto.Text;
            var marca = tbxMarca.Text;
            var tipo = cbxTipo.Text;
            var precio = tbxPrecio.Text;

            ItaliaPizzaServer.ProductManagerClient client = new ItaliaPizzaServer.ProductManagerClient();

            ItaliaPizzaServer.Productos nuevoProducto = new ItaliaPizzaServer.Productos()
            {
                Nombre = nombre,
                CodigoProducto = codigoProducto,
                Marca = marca,
                Tipo = tipo,
                Precio = double.Parse(precio)
            };

            var result = false;

            if (CamposVacios())
            {

                if (!client.ProductoYaRegistrado(nombre))
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Este producto ya se encuentra registrado en el sistema, intente con otro", "Producto duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (result)
                {
                    var aux = client.RegistrarProducto(nuevoProducto);
                    if (aux)
                    {
                        MessageBox.Show("El producto se ha registrado exitosamente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar el producto", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el producto", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese la información solicitada para continuar.", "Campos Vacios", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if(tbxNombre.Text == string.Empty || tbxMarca.Text == string.Empty || tbxPrecio.Text == string.Empty 
                || tbxCodigoProducto.Text == string.Empty || cbxTipo.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string codigo, string precio, string nombre, string marca)
        {
            var esValido = false;
            if (Regex.IsMatch(precio, @"^\d+(\.\d+)?$") && Regex.IsMatch(codigo, @"^[a-zA-Z0-9]+$") && Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$")
                && Regex.IsMatch(marca, @"^[a-zA-Z\s]+$"))
            {
                esValido = true;
            }
            return esValido;
        }

        private bool StringLargos(string nombre, string marca)
        {
            var noSonLargos = false;
            if (nombre.Length <= 45 || marca.Length <= 45)
            {
                noSonLargos = true;
            }
            return noSonLargos;
        }
        #endregion
    }
}
