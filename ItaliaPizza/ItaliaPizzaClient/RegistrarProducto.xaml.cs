using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        private string nombre = "";
        private string marca = "";
        private string tipo = "";
        private string precio = "";
        private string stock = "";

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
                "Comida",
                "Bebida"
            };
            cbxTipo.ItemsSource = items;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
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

        private void AccionRegistrar()
        {
            nombre = tbxNombre.Text;
            marca = tbxMarca.Text;
            tipo = cbxTipo.Text;
            precio = tbxPrecio.Text;
            stock = tbxStock.Text;
            ItaliaPizzaServer.ProductManagerClient client = new ItaliaPizzaServer.ProductManagerClient();

            ItaliaPizzaServer.Productos nuevoProducto = new ItaliaPizzaServer.Productos()
            {
                Nombre = nombre,
                Marca = marca,
                Tipo = tipo,
                Precio = double.Parse(precio),
                Stock = int.Parse(stock),
            };

            var result = false;

            if (CamposVacios())
            {
                result = true;

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

        public bool CamposVacios()
        {
            if(tbxNombre.Text == string.Empty || tbxMarca.Text == string.Empty || tbxPrecio.Text == string.Empty 
                || tbxStock.Text == string.Empty || cbxTipo.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
