using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarProducto.xaml
    /// </summary>
    public partial class ConsultarProducto : Window
    {
        ProductManagerClient client = new ProductManagerClient();
        private int idProducto;
        public ConsultarProducto(Productos producto)
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;
            tbxNombre.Text = producto.Nombre;
            tbxMarca.Text = producto.Marca;
            tbxPrecio.Text = producto.Precio.ToString();
            tbxStock.Text = producto.Stock.ToString();
            cbxTipo.SelectedItem = producto.Tipo;
            idProducto = producto.IdProductos;
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el producto?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try{
                    client.EliminarProducto(idProducto);
                    MessageBox.Show("Producto eliminado exitosamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
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
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
