using System.Windows.Controls;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ListaCompras.xaml
    /// </summary>
    public partial class ListaCompras : UserControl
    {
        public ListaCompras()
        {
            InitializeComponent();
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            contentControl.Content = new Finanzas();
        }
    }
}
