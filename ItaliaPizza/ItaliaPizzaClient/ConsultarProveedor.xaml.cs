using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ConsultarProveedor.xaml
    /// </summary>
    public partial class ConsultarProveedor : Window
    {
        SupplierManagerClient proveedor = new SupplierManagerClient();

        private int idProveedor;
        private string nombreCompañia;
        private string nombreContacto;
        private string telefono;
        private string ciudad;
        private string direccion;

        public ConsultarProveedor(Proveedor proveedor)
        {
            InitializeComponent();

            Loaded += CbxTipo_Loaded;

            idProveedor = proveedor.IdProveedores;
            nombreCompañia = proveedor.NombreCompañia;
            nombreContacto = proveedor.NombreContacto;
            telefono = proveedor.Telefono;
            ciudad = proveedor.Ciudad;
            direccion = proveedor.Direccion;

            tbxNombreCompañia.Text = nombreCompañia;
            tbxNombreContacto.Text = nombreContacto;
            tbxTelefono.Text = telefono;
            tbxDireccion.Text = direccion;
            cbxCiudad.SelectedItem = ciudad;

            DataContext = proveedor;
        }

        private void CbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> estados = new List<string>
                {
                    "Acajete", "Acatlón", "Acayucan", "Actopan", "Acula", "Acultzingo", "Camarón de Tejeda", "AlpaTlahuac", "Alto Lucero de Gutierrez Barrios", "Altotonga", "Alvarado", "Amatitlón", "Naranjos Amatlón", "Amatlón de los Reyes", "Angel R. Cabada", "La Antigua", "Apazapan", "Aquila", "Astacinga", "Atlahuilco", "Atoyac", "Atzacan", "Atzalan", "Tlaltetela", "Ayahualulco", "Banderilla", "Benito Juárez", "Boca del Rio", "Calcahualco", "Camerino Z. Mendoza", "Carrillo Puerto", "Catemaco", "Cazones de Herrera", "Cerro Azul", "Citlaltépetl", "Coacoatzintla", "Coahuitlón", "Coatepec", "Coatzacoalcos", "Coatzintla", "Coetzala", "Colipa", "Comapa", "Córdoba", "Cosamaloapan de Carpio", "Cosautlón de Carvajal", "Coscomatepec", "Cosoleacaque", "Cotaxtla", "Coxquihui", "Coyutla", "Cuichapa", "CuiTlahuac", "Chacaltianguis", "Chalma", "Chiconamel", "Chiconquiaco", "Chicontepec", "Chinameca", "Chinampa de Gorostiza", "Las Choapas", "Chocamén", "Chontla", "Chumatlón", "Emiliano Zapata", "Espinal", "Filomeno Mata", "Fortín", "Gutierrez Zamora", "Hidalgotitlón", "Huatusco", "Huayacocotla", "Hueyapan de Ocampo", "Huiloapan de Cuauhtemoc", "Ignacio de la Llave", "Ilamatlón", "Isla", "Ixcatepec", "Ixhuacón de los Reyes", "Ixhuatlón del Café", "Ixhuatlancillo", "Ixhuatlón del Sureste", "Ixhuatlón de Madero", "Ixmatlahuacan", "Ixtaczoquitlón", "Jalacingo", "Xalapa", "Jalcomulco", "Jáltipan", "Jamapa", "Jesés Carranza", "Xico", "Jilotepec", "Juan Rodríguez Clara", "Juchique de Ferrer", "Landero y Coss", "Lerdo de Tejada", "Magdalena", "Maltrata", "Manlio Fabio Altamirano", "Mariano Escobedo", "Martínez de la Torre", "Mecatlón", "Mecayapan", "Medellón de Bravo", "Miahuatlón", "Las Minas", "Minatitlón", "Misantla", "Mixtla de Altamirano", "Moloacón", "Naolinco", "Naranjal", "Nautla", "Nogales", "Oluta", "Omealca", "Orizaba", "Otatitlón", "Oteapan", "Ozuluama de Mascareñas", "Pajapan", "Panuco", "Papantla", "Paso del Macho", "Paso de Ovejas", "La Perla", "Perote", "Platón sénchez", "Playa Vicente", "Poza Rica de Hidalgo", "Las Vigas de Ramírez", "Pueblo Viejo", "Puente Nacional", "Rafael Delgado", "Rafael Lucio", "Los Reyes", "Rio Blanco", "Saltabarranca", "San Andrés Tenejapan", "San Andrés Tuxtla", "San Juan Evangelista", "Santiago Tuxtla", "Sayula de Alemén", "Soconusco", "Sochiapa", "Soledad Atzompa", "Soledad de Doblado", "Soteapan", "Tamalón", "Tamiahua", "Tampico Alto", "Tancoco", "Tantima", "Tantoyuca", "Tatatila", "Castillo de Teayo", "Tecolutla", "Tehuipango", "Álamo Temapache", "Tempoal", "Tenampa", "Tenochtitlán", "Teocelo", "Tepatlaxco", "Tepetlón", "Tepetzintla", "Tequila", "José Azueta", "Texcatepec", "Texhuacón", "Texistepec", "Tezonapa", "Tierra Blanca", "Tihuatlón", "Tlacojalpan", "Tlacolulan", "Tlacotalpan", "Tlacotepec de Mejía", "Tlachichilco", "Tlalixcoyan", "Tlalnelhuayocan", "Tlapacoyan", "Tlaquilpa", "Tlilapan", "Tomatlón", "Tonayán", "Totutla", "Tuxpan", "Tuxtilla", "Ursulo Galván", "Vega de Alatorre", "Veracruz", "Villa Aldama", "Xoxocotla", "Yanga", "Yecuatla", "Zacualpan", "Zaragoza", "Zentla", "Zongolica", "Zontecomatlón de Lopez y Fuentes", "Zozocolco de Hidalgo", "Agua Dulce", "El Higo", "Nanchital de Lázaro Cárdenas del Rio", "Tres Valles", "Carlos A. Carrillo", "Tatahuicapan de Juárez", "Uxpanapa", "San Rafael", "Santiago Sochiapan"
                };
            cbxCiudad.ItemsSource = estados;
        }

        private void ImgRegresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            Title = "Modificar Proveedor";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombreCompañia.IsEnabled = true;
            tbxNombreContacto.IsEnabled = true;
            tbxTelefono.IsEnabled = true;
            tbxDireccion.IsEnabled = true;
            cbxCiudad.IsEnabled = true;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quiere eliminar el proveedor?", "Confirmar eliminación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    proveedor.EliminarProveedor(idProveedor);
                    MessageBox.Show("El proveedor se ha eliminado exitosamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

            Title = "Consultar Producto";

            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;

            tbxNombreCompañia.IsEnabled = false;
            tbxNombreContacto.IsEnabled = false;
            tbxTelefono.IsEnabled = false;
            tbxDireccion.IsEnabled = false;
            cbxCiudad.IsEnabled = false;

            tbxNombreCompañia.Text = nombreCompañia;
            tbxNombreContacto.Text = nombreContacto;
            tbxTelefono.Text = telefono;
            tbxDireccion.Text = direccion;
            cbxCiudad.SelectedItem = ciudad;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

            string nuevoNombreCompañia;
            string nuevoNombreContacto;
            string nuevoTelefono;
            string nuevaDireccion;
            string nuevaCiudad;

            nuevoNombreCompañia = tbxNombreCompañia.Text;
            nuevoNombreContacto = tbxNombreContacto.Text;
            nuevoTelefono = tbxTelefono.Text;
            nuevaDireccion = tbxDireccion.Text;
            nuevaCiudad = cbxCiudad.SelectedItem.ToString();

            if (CamposVacios())
            {
                if (StringValidos(nombreCompañia, nombreContacto, telefono, direccion) && StringLargos(nombreCompañia, nombreContacto, telefono, direccion))
                {
                    try
                    {
                        proveedor.ActualizarProveedor(idProveedor, nuevoNombreCompañia, nuevoNombreContacto, nuevoTelefono, nuevaCiudad, nuevaDireccion);
                        MessageBox.Show("Proveedor se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public bool CamposVacios()
        {
            if (tbxNombreCompañia.Text == string.Empty || tbxNombreContacto.Text == string.Empty || tbxTelefono.Text == string.Empty
                || tbxDireccion.Text == string.Empty || cbxCiudad.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string nombreCompañia, string nombreContacto, string telefono, string direccion)
        {
            var esValido = false;
            if (Regex.IsMatch(nombreCompañia, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") && Regex.IsMatch(nombreContacto, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$")
                && Regex.IsMatch(telefono, @"^\d+$") && Regex.IsMatch(direccion, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                esValido = true;
            }
            return esValido;
        }

        private bool StringLargos(string nombreCompañia, string nombreContacto, string telefono, string direccion)
        {
            var noSonLargos = false;
            if (nombreCompañia.Length <= 50 || nombreContacto.Length <= 45 || telefono.Length <= 24 || direccion.Length <= 45)
            {
                noSonLargos = true;
            }
            return noSonLargos;
        }
    }
}
