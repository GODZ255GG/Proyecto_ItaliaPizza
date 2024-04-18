using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarProducto.xaml
    /// </summary>
    public partial class ConsultarProducto : Window
    {
        ProductManagerClient client = new ProductManagerClient();

        private int idProducto;
        private string nombre;
        private string marca;
        private string tipo;
        private string precio;
        private string codigo;

        public ConsultarProducto(Productos producto)
        {
            InitializeComponent();

            Loaded += CbxTipo_Loaded;

            idProducto = producto.IdProductos;
            nombre = producto.Nombre;
            marca = producto.Marca;
            tipo = producto.Tipo;
            precio = producto.Precio.ToString();
            codigo = producto.CodigoProducto;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxPrecio.Text = precio;
            tbxCodigoProducto.Text = codigo;
            cbxTipo.SelectedItem = tipo;
            
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el producto?", "Confirmar eliminación", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try{
                    client.EliminarProducto(idProducto);
                    MessageBox.Show("Producto eliminado exitosamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (EndpointNotFoundException ex)
                {
                    MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", 
                        "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", 
                        "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException ex)
                {
                    MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", 
                        "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {

            Title = "Modificar Producto";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = false;
            tbxMarca.IsReadOnly = false;
            tbxPrecio.IsReadOnly = false;
            tbxCodigoProducto.IsReadOnly = false;
            cbxTipo.IsEnabled = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

            Title = "Consultar Producto";

            btnAceptar.Visibility=Visibility.Hidden;
            btnCancelar.Visibility=Visibility.Hidden;
            btnModificar.Visibility=Visibility.Visible;
            btnEliminar.Visibility=Visibility.Visible;

            tbxNombre.IsReadOnly = true;
            tbxMarca.IsReadOnly = true;
            tbxPrecio.IsReadOnly = true;
            tbxCodigoProducto.IsReadOnly = true;
            cbxTipo.IsEnabled = false;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxPrecio.Text = precio;
            tbxCodigoProducto.Text = codigo;
            cbxTipo.SelectedItem = tipo;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

            string nuevoNombre;
            string nuevaMarca;
            string nuevoTipo;
            double nuevoPrecio;
            string nuevoCodigo;

            nuevoNombre = tbxNombre.Text;
            nuevaMarca = tbxMarca.Text;
            nuevoTipo = cbxTipo.Text;
            nuevoPrecio = Convert.ToDouble(tbxPrecio.Text);
            nuevoCodigo = tbxCodigoProducto.Text;

            if (CamposVacios())
            {
                if (StringValidos(codigo, precio, nombre, marca) && StringLargos(nombre, marca))
                {
                    try
                    {
                        client.ActualizarProducto(idProducto, nuevoNombre, nuevoCodigo, nuevaMarca, nuevoTipo, nuevoPrecio);
                        MessageBox.Show("Producto se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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

        #region Validaciones
        public bool CamposVacios()
        {
            if (tbxNombre.Text == string.Empty || tbxMarca.Text == string.Empty || tbxPrecio.Text == string.Empty
                || tbxCodigoProducto.Text == string.Empty || cbxTipo.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string codigo, string precio, string nombre, string marca)
        {
            if (Regex.IsMatch(precio, @"^\d+(\.\d+)?$") && Regex.IsMatch(codigo, @"^[a-zA-Z0-9]+$") && Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$")
                && Regex.IsMatch(marca, @"^[a-zA-Z\s]+$"))
            {
                return true;
            }
            return false;
        }

        private bool StringLargos(string nombre, string marca)
        {
            if (nombre.Length <= 45 || marca.Length <= 45)
            {
                 return true;
            }
            return false;
        }
        #endregion
    }
}
