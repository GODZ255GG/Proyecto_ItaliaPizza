using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para ConsultarInsumo.xaml
    /// </summary>
    public partial class ConsultarInsumo : Window
    {
        InsumoManagerClient client = new InsumoManagerClient();

        private int idInsumos;
        private string nombre;
        private string marca;
        private string tipo;
        private string cantidad;
        private string codigo;

        public ConsultarInsumo(Insumos insumo)
        {
            InitializeComponent();
            Loaded += CbxTipo_Loaded;

            idInsumos = insumo.IdInsumos;
            nombre = insumo.Nombre;
            marca = insumo.Marca;
            tipo = insumo.Tipo;
            cantidad = insumo.CantidadDeEmpaque;
            codigo = insumo.CodigoInsumo;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxCantidad.Text = cantidad;
            tbxCodigoInsumo.Text = codigo;
            cbxTipo.SelectedItem = tipo;
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar el insumo?", "Confirmar eliminación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    client.EliminarInsumo(idInsumos);
                    MessageBox.Show("Insumo eliminado exitosamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Title = "Modificar Insumo";
            lbTitulo.Content = "Modificar Insumo";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = false;
            tbxMarca.IsReadOnly = false;
            tbxCantidad.IsReadOnly = false;
            tbxCodigoInsumo.IsReadOnly = false;
            cbxTipo.IsEnabled = true;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre;
            string nuevaMarca;
            string nuevoTipo;
            string nuevaCantidad;
            string nuevoCodigo;

            nuevoNombre = tbxNombre.Text;
            nuevaMarca = tbxMarca.Text;
            nuevoTipo = cbxTipo.Text;
            nuevaCantidad = tbxCantidad.Text;
            nuevoCodigo = tbxCodigoInsumo.Text;

            if (CamposVacios())
            {
                if (StringValidos(codigo, cantidad, nombre, marca) && StringLargos(nombre, marca, codigo, cantidad))
                {
                    try
                    {
                        client.ActualizarInsumo(idInsumos, nuevoNombre, nuevoCodigo, nuevaMarca, nuevoTipo, nuevaCantidad);
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Title = "Consultar Insumo";
            lbTitulo.Content = "Información del Insumo";

            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = true;
            tbxMarca.IsReadOnly = true;
            tbxCantidad.IsReadOnly = true;
            tbxCodigoInsumo.IsReadOnly = true;
            cbxTipo.IsEnabled = false;

            tbxNombre.Text = nombre;
            tbxMarca.Text = marca;
            tbxCantidad.Text = cantidad;
            tbxCodigoInsumo.Text = codigo;
            cbxTipo.SelectedItem = tipo;
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