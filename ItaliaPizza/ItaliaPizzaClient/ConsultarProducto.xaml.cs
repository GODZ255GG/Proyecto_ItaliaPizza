using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            MessageBoxResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                client.EliminarProducto(idProducto);
                MessageBox.Show("Producto eliminado correctamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
