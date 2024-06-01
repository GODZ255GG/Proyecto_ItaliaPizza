using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para Inventario.xaml
    /// </summary>
    public partial class Inventario : UserControl
    {
        public Inventario()
        {
            InitializeComponent();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaProductos();
        }

        private void BtnInsumos_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaInsumos();
        }

        private void BtnStock_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Stock();
        }
    }
}
