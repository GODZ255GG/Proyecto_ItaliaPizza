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
        private float precio = 0;
        private int stock = 0;

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
                MessageBox.Show("NO jala", "EndPonit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("NO jala", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("NO jala", "Time", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AccionRegistrar()
        {
            nombre = tbxNombre.Text;
            marca = tbxMarca.Text;
            tipo = cbxTipo.Text;
            precio = float.Parse(tbxPrecio.Text);
            stock = int.Parse(tbxStock.Text);
            ItaliaPizzaServer.ProductManagerClient client = new ItaliaPizzaServer.ProductManagerClient();

            ItaliaPizzaServer.Productos nuevoProducto = new ItaliaPizzaServer.Productos()
            {
                Nombre = nombre,
                Marca = marca,
                Tipo = tipo,
                Precio = precio,
                Stock = stock,
            };

            var result = false;

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
    }
}
