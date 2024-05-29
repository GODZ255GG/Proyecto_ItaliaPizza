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
    /// Interaction logic for Finanzas.xaml
    /// </summary>
    public partial class Finanzas : UserControl
    {
        public Finanzas()
        {
            InitializeComponent();
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaProveedores();
        }

        private void BtnCorteDeCaja_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaDeCortesDeCaja();
        }

        private void BtnCompra_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaCompras();
        }
    }
}
