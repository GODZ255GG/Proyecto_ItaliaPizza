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
        private string estado;
        private string categoriaProveedor;

        public ConsultarProveedor(Proveedor proveedor)
        {
            InitializeComponent();

            Loaded += CbxCategoriaProveedor_Loaded;
            Loaded += (sender, e) =>
            {
                CargarEstados();
                InicializarEstadoCombobox();
            };

            idProveedor = proveedor.IdProveedores;
            nombreCompañia = proveedor.NombreCompañia;
            nombreContacto = proveedor.NombreContacto;
            telefono = proveedor.Telefono;
            ciudad = proveedor.Ciudad;
            direccion = proveedor.Direccion;
            estado = proveedor.Estado;
            categoriaProveedor = proveedor.CategoriaProveedor;

            tbxNombreCompañia.Text = nombreCompañia;
            tbxNombreContacto.Text = nombreContacto;
            tbxTelefono.Text = telefono;
            tbxDireccion.Text = direccion;
            cbxCiudad.SelectedItem = ciudad;
            cbxEstado.SelectedItem = estado;
            cbxCategoriaProveedor.SelectedItem = categoriaProveedor;

            DataContext = proveedor;
        }

        private void CargarEstados()
        {
            cbxEstado.ItemsSource = estadosYMunicipios.Keys;
        }

        private void InicializarEstadoCombobox()
        {
            cbxCiudad.IsEnabled = false;
        }

        private void CbxEstado_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbxEstado.SelectedItem != null)
            {
                string estadoSeleccionado = cbxEstado.SelectedItem.ToString();
                if (estadosYMunicipios.ContainsKey(estadoSeleccionado))
                {
                    cbxCiudad.ItemsSource = estadosYMunicipios[estadoSeleccionado];
                    cbxCiudad.IsEnabled = true;
                }
                else
                {
                    cbxCiudad.ItemsSource = null;
                    cbxCiudad.IsEnabled = false;
                }
            }
            else
            {
                cbxCiudad.ItemsSource = null;
                cbxCiudad.IsEnabled = false;
            }
        }

        private void CbxCategoriaProveedor_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> items = new List<string>
            {
                "Carnes",
                "Frutas",
                "Verduras",
                "Harinas",
                "Lacteos",
                "Postres",
                "Bebidas"
            };
            cbxCategoriaProveedor.ItemsSource = items;
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
            cbxEstado.IsEnabled = true;
            cbxCategoriaProveedor.IsEnabled = true;
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
            cbxEstado.IsEnabled = false;
            cbxCategoriaProveedor.IsEnabled = false;

            tbxNombreCompañia.Text = nombreCompañia;
            tbxNombreContacto.Text = nombreContacto;
            tbxTelefono.Text = telefono;
            tbxDireccion.Text = direccion;
            cbxCiudad.SelectedItem = ciudad;
            cbxEstado.SelectedItem = estado;
            cbxCategoriaProveedor.SelectedItem = categoriaProveedor;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombreCompañia = tbxNombreCompañia.Text;
            string nuevoNombreContacto = tbxNombreContacto.Text;
            string nuevoTelefono = tbxTelefono.Text;
            string nuevaDireccion = tbxDireccion.Text;
            string nuevaCiudad = cbxCiudad.SelectedItem?.ToString();
            string nuevoEstado = cbxEstado.SelectedItem?.ToString();
            string nuevaCategoriaProveedor = cbxCategoriaProveedor.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(nuevaCiudad))
            {
                MessageBox.Show("Seleccione una ciudad para continuar", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CamposVacios(nuevoNombreCompañia, nuevoNombreContacto, nuevoTelefono, nuevaDireccion, nuevaCiudad, nuevoEstado, nuevaCategoriaProveedor))
            {
                MessageBox.Show("Ingrese la información solicitada para continuar", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (!StringsValidos(nuevoNombreCompañia, nuevoNombreContacto, nuevoTelefono, nuevaDireccion))
            {
                MessageBox.Show("Los datos ingresados no son válidos. Verifique sus datos", "Datos Inválidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TelefonoValido(nuevoTelefono))
            {
                MessageBox.Show("El teléfono ingresado no cumple con la longitud requerida. Verifique sus datos", "Datos Inválidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                proveedor.ActualizarProveedor(idProveedor, nuevoNombreCompañia, nuevoNombreContacto, nuevoTelefono, nuevaCiudad, nuevaDireccion, nuevoEstado, nuevaCategoriaProveedor);
                MessageBox.Show("Proveedor se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CamposVacios(string nombreCompañia, string nombreContacto, string telefono, string direccion, string ciudad, string estado, string categoriaProveedor)
        {
            return string.IsNullOrEmpty(nombreCompañia) || string.IsNullOrEmpty(nombreContacto) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(ciudad) || string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(categoriaProveedor);
        }

        private bool StringsValidos(string nombreCompañia, string nombreContacto, string telefono, string direccion)
        {
            return Regex.IsMatch(nombreCompañia, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") &&
                   Regex.IsMatch(nombreContacto, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$") &&
                   Regex.IsMatch(telefono, @"^\d+$") &&
                   Regex.IsMatch(direccion, @"^[a-zA-Z0-9\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$");
        }

        private bool TelefonoValido(string telefono)
        {
            return telefono.Length == 10;
        }

        private Dictionary<string, List<string>> estadosYMunicipios = new Dictionary<string, List<string>>
        {
            { "Aguascalientes", new List<string> { "Aguascalientes", "Asientos", "Calvillo", "Cosio", "El Llano", "Jesus Maria", "Pabellon de Arteaga", "Rincon de Romos", "San Francisco de los Romo", "San Jose de Gracia", "Tepezala" } },
            { "Baja California", new List<string> { "Ensenada", "Mexicali", "Playas de Rosarito", "Tecate", "Tijuana" } },
            { "Baja California Sur", new List<string> { "Comondu", "La Paz", "Loreto", "Los Cabos", "Mulege" } },
            { "Campeche", new List<string> { "Calakmul", "Calkini", "Campeche", "Candelaria", "Carmen", "Champoton", "Escarcega", "Hecelchakan", "Hopelchen", "Palizada", "Tenabo" } },
            { "Coahuila", new List<string> { "Abasolo", "Acuna", "Allende", "Arteaga", "Candela", "Castanos", "Cuatro Cienegas", "Escobedo", "Francisco I. Madero", "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jimenez", "Juarez", "Lamadrid", "Matamoros", "Monclova", "Morelos", "Muzquiz", "Nadadores", "Nava", "Ocampo", "Parras", "Piedras Negras", "Progreso", "Ramos Arizpe", "Sabinas", "Sacramento", "Saltillo", "San Buenaventura", "San Juan de Sabinas", "San Pedro", "Sierra Mojada", "Torreon", "Viesca", "Villa Union", "Zaragoza" } },
            { "Colima", new List<string> { "Armeria","Colima","Comala","Coquimatlan","Cuauhtemoc","Ixtlahuacan","Manzanillo","Minatitlan","Tecoman","Villa de Alvarez" } },
            { "Chiapas", new List<string> { "Acacoyagua","Acala","Acapetahua","Aldama","Altamirano","Amatenango de la Frontera","Amatenango del Valle","Amatan","Angel Albino Corzo","Arriaga","Bejucal de Ocampo","Bella Vista","Benemerito de las Americas","Berriozabal","Bochil","Cacahoatan","Capitan Luis Angel Vidal","Catazaja","Chalchihuitan","Chamula","Chanal","Chapultenango","Chenalho","Chiapa de Corzo","Chiapilla","Chicoasen","Chicomuselo","Chilon","Cintalapa","Coapilla","Comitan de Dominguez","Copainala","El Bosque","El Parral","El Porvenir","Emiliano Zapata","Escuintla","Francisco Leon","Frontera Comalapa","Frontera Hidalgo","Huehuetan","Huitiupan","Huixtla","Huixtan","Ixhuatan","Ixtacomitan","Ixtapa","Ixtapangajoya","Jiquipilas","Jitotol","Juarez","La Concordia","La Grandeza","La Independencia","La Libertad","La Trinitaria","Larrainzar","Las Margaritas","Las Rosas","Mapastepec","Maravilla Tenejapa","Marques de Comillas","Mazapa de Madero","Mazatan","Metapa","Mezcalapa","Mitontic","Montecristo de Guerrero","Motozintla","Nicolas Ruiz","Ocosingo","Ocotepec","Ocozocoautla de Espinosa","Ostuacan","Osumacinta","Oxchuc","Palenque","Pantelho","Pantepec","Pichucalco","Pijijiapan","Pueblo Nuevo Solistahuacan","Rayon","Reforma","Rincon Chamula San Pedro","Sabanilla","Salto de Agua","San Andres Duraznal","San Cristobal de las Casas","San Fernando","San Juan Cancuc","San Lucas","Santiago el Pinar","Siltepec","Simojovel","Sitala","Socoltenango","Solosuchiapa","Soyalo","Suchiapa","Suchiate","Sunuapa","Tapachula","Tapalapa","Tapilula","Tecpatan","Tenejapa","Teopisca","Tila","Tonala","Totolapa","Tumbala","Tuxtla Chico","Tuxtla Gutierrez","Tuzantan","Tzimol","Union Juarez","Venustiano Carranza","Villa Comaltitlan","Villa Corzo","Villaflores","Yajalon","Zinacantan" } },
            { "Chihuahua", new List<string> { "Ahumada","Aldama","Allende","Aquiles Serdan","Ascension","Bachiniva","Balleza","Batopilas de Manuel Gomez Morin","Bocoyna","Buenaventura","Camargo","Carichi","Casas Grandes","Chihuahua","Chinipas","Coronado","Coyame del Sotol","Cuauhtemoc","Cusihuiriachi","Delicias","Dr. Belisario Dominguez","El Tule","Galeana","Gran Morelos","Guachochi","Guadalupe y Calvo","Guadalupe","Guazapares","Guerrero","Gomez Farias","Hidalgo del Parral","Huejotitan","Ignacio Zaragoza","Janos","Jimenez","Julimes","Juarez","La Cruz","Lopez","Madera","Maguarichi","Manuel Benavides","Matachi","Matamoros","Meoqui","Morelos","Moris","Namiquipa","Nonoava","Nuevo Casas Grandes","Ocampo","Ojinaga","Praxedis G. Guerrero","Riva Palacio","Rosales","Rosario","San Francisco de Borja","San Francisco de Conchos","San Francisco del Oro","Santa Barbara","Santa Isabel","Satevo","Saucillo","Temosachic","Urique","Uruachi","Valle de Zaragoza" } },
            { "Ciudad de Mexico", new List<string> { "Alvaro Obregon","Azcapotzalco","Benito Juarez","Coyoacan","Cuajimalpa de Morelos","Cuauhtemoc","Gustavo A. Madero","Iztacalco","Iztapalapa","La Magdalena Contreras","Miguel Hidalgo","Milpa Alta","Tlalpan","Tlahuac","Venustiano Carranza","Xochimilco" } },
            { "Durango", new List<string> { "Canatlan","Canelas","Coneto de Comonfort","Cuencame","Durango","El Oro","General Simon Bolivar","Gomez Palacio","Guadalupe Victoria","Guanacevi","Hidalgo","Inde","Lerdo","Mapimi","Mezquital","Nazas","Nombre de Dios","Nuevo Ideal","Ocampo","Otaez","Panuco de Coronado","Penon Blanco","Poanas","Pueblo Nuevo","Rodeo","San Bernardo","San Dimas","San Juan de Guadalupe","San Juan del Rio","San Luis del Cordero","San Pedro del Gallo","Santa Clara","Santiago Papasquiaro","Suchil","Tamazula","Tepehuanes","Tlahualilo","Topia","Vicente Guerrero" } },
            { "Guanajuato", new List<string> { "Abasolo","Acambaro","Apaseo el Alto","Apaseo el Grande","Atarjea","Celaya","Comonfort","Coroneo","Cortazar","Cueramaro","Doctor Mora","Dolores Hidalgo Cuna de la Independencia Nacional","Guanajuato","Huanimaro","Irapuato","Jaral del Progreso","Jerecuaro","Leon","Manuel Doblado","Moroleon","Ocampo","Penjamo","Pueblo Nuevo","Purisima del Rincon","Romita","Salamanca","Salvatierra","San Diego de la Union","San Felipe","San Francisco del Rincon","San Jose Iturbide","San Luis de la Paz","San Miguel de Allende","Santa Catarina","Santa Cruz de Juventino Rosas","Santiago Maravatio","Silao de la Victoria","Tarandacuao","Tarimoro","Tierra Blanca","Uriangato","Valle de Santiago","Victoria","Villagran","Xichu","Yuriria" } },
            { "Guerrero", new List<string> { "Acapulco de Juarez","Acatepec","Ahuacuotzingo","Ajuchitlan del Progreso","Alcozauca de Guerrero","Alpoyeca","Apaxtla","Arcelia","Atenango del Rio","Atlamajalcingo del Monte","Atlixtac","Atoyac de Alvarez","Ayutla de los Libres","Azoyu","Benito Juarez","Buenavista de Cuellar","Chilapa de Alvarez","Chilpancingo de los Bravo","Coahuayutla de Jose Maria Izazaga","Cochoapa el Grande","Cocula","Copala","Copalillo","Copanatoyac","Coyuca de Benitez","Coyuca de Catalan","Cuajinicuilapa","Cualac","Cuautepec","Cuetzala del Progreso","Cutzamala de Pinzon","Eduardo Neri","Florencio Villarreal","General Canuto A. Neri","General Heliodoro Castillo","Huamuxtitlan","Huitzuco de los Figueroa","Iguala de la Independencia","Igualapa","Iliatenco","Ixcateopan de Cuauhtemoc","Jose Joaquin de Herrera","Juan R. Escudero","Juchitan","La Union de Isidoro Montes de Oca","Leonardo Bravo","Malinaltepec","Marquelia","Martir de Cuilapan","Metlatonoc","Mochitlan","Olinala","Ometepec","Pedro Ascencio Alquisiras","Petatlan","Pilcaya","Pungarabato","Quechultenango","San Luis Acatlan","San Marcos","San Miguel Totolapan","Taxco de Alarcon","Tecoanapa","Tecpan de Galeana","Teloloapan","Tepecoacuilco de Trujano","Tetipac","Tixtla de Guerrero","Tlacoachistlahuaca","Tlacoapa","Tlalchapa","Tlalixtaquilla de Maldonado","Tlapa de Comonfort","Tlapehuala","Xalpatlahuac","Xochihuehuetlan","Xochistlahuaca","Zapotitlan Tablas","Zihuatanejo de Azueta","Zirandaro","Zitlala" } },
            { "Hidalgo", new List<string> { "Acatlan","Acaxochitlan","Actopan","Agua Blanca de Iturbide","Ajacuba","Alfajayucan","Almoloya","Apan","Atitalaquia","Atlapexco","Atotonilco el Tavajo","Atotonilco el Grande","Calnali","Cardonal","Chapantongo","Chapulhuacan","Chilcuautla","Cuautepec de Hinojosa","El Arenal","Eloxochitlan","Emiliano Zapata","Epazoyucan","Francisco I. Madero","Huasca de Ocampo","Huautla","Huazalingo","Huehuetla","Huejutla de Reyes","Huichapan","Ixmiquilpan","Jacala de Ledezma","Jaltocan","Juarez Hidalgo","La Mision","Lolotla","Metepec","Metztitlan","Mineral de la Reforma","Mineral del Chico","Mineral del Monte","Mixquiahuala de Juarez","Molango de Escamilla","Nicolas Flores","Nopala de Villagran","Omitlan de Juarez","Pachuca de Soto","Pacula","Pisaflores","Progreso de Obregon","San Agustin Metzquititlan","San Agustin Tlaxiaca","San Bartolo Tutotepec","San Felipe Orizatlan","San Salvador","Santiago Tulantepec de Lugo Guerrero","Santiago de Anaya","Singuilucan","Tasquillo","Tecozautla","Tenango de Doria","Tepeapulco","Tepehuacan de Guerrero","Tepeji del Rio de Ocampo","Tepetitlan","Tetepango","Tezontepec de Aldama","Tianguistengo","Tizayuca","Tlahuelilpan","Tlahuiltepa","Tlanalapa","Tlanchinol","Tlaxcoapan","Tolcayuca","Tula de Allende","Tulancingo de Bravo","Villa de Tezontepec","Xochiatipan","Xochicoatlan","Yahualica","Zacualtipan de Angeles","Zapotlan de Juarez","Zempoala","Zimapan" } },
            { "Jalisco", new List<string> { "Acatic","Acatlan de Juarez","Ahualulco de Mercado","Amacueca","Amatitan","Ameca","Arandas","Atemajac de Brizuela","Atengo","Atenguillo","Atotonilco el Alto","Atoyac","Autlan de Navarro","Ayotlan","Ayutla","Bolanos","Cabo Corrientes","Canadas de Obregon","Casimiro Castillo","Chapala","Chimaltitan","Chiquilistlan","Cihuatlan","Cocula","Colotlan","Concepcion de Buenos Aires","Cuautitlan de Garcia Barragan","Cuautla","Cuquio","Degollado","Ejutla","El Arenal","El Grullo","El Limon","El Salto","Encarnacion de Diaz","Etzatlan","Gomez Farias","Guachinango","Guadalajara","Hostotipaquillo","Huejucar","Huejuquilla el Alto","Ixtlahuacan de los Membrillos","Ixtlahuacan del Rio","Jalostotitlan","Jamay","Jesus Maria","Jilotlan de los Dolores","Jocotepec","Juanacatlan","Juchitlan","La Barca","La Huerta","La Manzanilla de la Paz","Lagos de Moreno","Magdalena","Mascota","Mazamitla","Mexticacan","Mezquitic","Mixtlan","Ocotlan","Ojuelos de Jalisco","Pihuamo","Poncitlan","Puerto Vallarta","Quitupan","San Cristobal de la Barranca","San Diego de Alejandria","San Gabriel","San Ignacio Cerro Gordo","San Juan de los Lagos","San Juanito de Escobedo","San Julian","San Marcos","San Martin Hidalgo","San Martin de Bolanos","San Miguel el Alto","San Pedro Tlaquepaque","San Sebastian del Oeste","Santa Maria de los Angeles","Santa Maria del Oro","Sayula","Tala","Talpa de Allende","Tamazula de Gordiano","Tapalpa","Tecalitlan","Techaluta de Montenegro","Tecolotlan","Tenamaxtlan","Teocaltiche","Teocuitatlan de Corona","Tepatitlan de Morelos","Tequila","Teuchitlan","Tizapan el Alto","Tlajomulco de Zuniga","Toliman","Tomatlan","Tonala","Tonaya","Tonila","Totatiche","Tototlan","Tuxcacuesco","Tuxcueca","Tuxpan","Union de San Antonio","Union de Tula","Valle de Guadalupe","Valle de Juarez","Villa Corona","Villa Guerrero","Villa Hidalgo","Villa Purificacion","Yahualica de Gonzalez Gallo","Zacoalco de Torres","Zapopan","Zapotiltic","Zapotitlan de Vadillo","Zapotlan del Rey","Zapotlan el Grande","Zapotlanejo" } },
            { "Estado de México", new List<string> { "Acambay de Ruiz Castaneda","Acolman","Aculco","Almoloya de Alquisiras","Almoloya de Juarez","Almoloya del Rio","Amanalco","Amatepec","Amecameca","Apaxco","Atenco","Atizapan de Zaragoza","Atizapan","Atlacomulco","Atlautla","Axapusco","Ayapango","Calimaya","Capulhuac","Chalco","Chapa de Mota","Chapultepec","Chiautla","Chicoloapan","Chiconcuac","Chimalhuacan","Coacalco de Berriozabal","Coatepec Harinas","Cocotitlan","Coyotepec","Cuautitlan Izcalli","Cuautitlan","Donato Guerra","Ecatepec de Morelos","Ecatzingo","El Oro","Huehuetoca","Hueypoxtla","Huixquilucan","Isidro Fabela","Ixtapaluca","Ixtapan de la Sal","Ixtapan del Oro","Ixtlahuaca","Jaltenco","Jilotepec","Jilotzingo","Jiquipilco","Jocotitlan","Joquicingo","Juchitepec","La Paz","Lerma","Luvianos","Malinalco","Melchor Ocampo","Metepec","Mexicaltzingo","Morelos","Naucalpan de Juarez","Nextlalpan","Nezahualcoyotl","Nicolas Romero","Nopaltepec","Ocoyoacac","Ocuilan","Otumba","Otzoloapan","Otzolotepec","Ozumba","Papalotla","Polotitlan","Rayon","San Antonio la Isla","San Felipe del Progreso","San Jose del Rincon","San Martin de las Piramides","San Mateo Atenco","San Simon de Guerrero","Santo Tomas","Soyaniquilpan de Juarez","Sultepec","Tecamac","Tejupilco","Temamatla","Temascalapa","Temascalcingo","Temascaltepec","Temoaya","Tenancingo","Tenango del Aire","Tenango del Valle","Teoloyucan","Teotihuacan","Tepetlaoxtoc","Tepetlixpa","Tepotzotlan","Tequixquiac","Texcaltitlan","Texcalyacac","Texcoco","Tezoyuca","Tianguistenco","Timilpan","Tlalmanalco","Tlalnepantla de Baz","Tlatlaya","Toluca","Tonanitla","Tonatico","Tultepec","Tultitlan","Valle de Bravo","Valle de Chalco Solidaridad","Villa Guerrero","Villa Victoria","Villa de Allende","Villa del Carbon","Xalatlaco","Xonacatlan","Zacazonapan","Zacualpan","Zinacantepec","Zumpahuacan","Zumpango" } },
            { "Michoacan", new List<string> { "Acuitzio", "Aguililla", "Alvaro Obregon", "Angamacutiro", "Angangueo", "Apatzingan", "Aporo", "Aquila", "Ario", "Arteaga", "Brisenas", "Buenavista", "Caracuaro", "Charapan", "Charo", "Chavinda", "Cheran", "Chilchota", "Chinicuila", "Chucandiro", "Churintzio", "Churumuco", "Coahuayana", "Coalcoman de Vazquez Pallares", "Coeneo", "Cojumatlan de Regules", "Contepec", "Copandaro", "Cotija", "Cuitzeo", "Ecuandureo", "Epitacio Huerta", "Erongaricuaro", "Gabriel Zamora", "Hidalgo", "Huandacareo", "Huaniqueo", "Huetamo", "Huiramba", "Indaparapeo", "Irimbo", "Ixtlan", "Jacona", "Jimenez", "Jiquilpan", "Jose Sixto Verduzco", "Juarez", "Jungapeo", "La Huacana", "La Piedad", "Lagunillas", "Lazaro Cardenas", "Los Reyes", "Madero", "Maravatio", "Marcos Castellanos", "Morelia", "Morelos", "Mugica", "Nahuatzen", "Nocupetaro", "Nuevo Parangaricutiro", "Nuevo Urecho", "Numaran", "Ocampo", "Pajacuaran", "Panindicuaro", "Paracho", "Paracuaro", "Patzcuaro", "Penjamillo", "Periban", "Purepero", "Puruandiro", "Querendaro", "Quiroga", "Sahuayo", "Salvador Escalante", "San Lucas", "Santa Ana Maya", "Senguio", "Susupuato", "Tacambaro", "Tancitaro", "Tangamandapio", "Tangancicuaro", "Tanhuato", "Taretan", "Tarimbaro", "Tepalcatepec", "Tingambato", "Tinguindin", "Tiquicheo de Nicolas Romero", "Tlalpujahua", "Tlazazalca", "Tocumbo", "Tumbiscatio", "Turicato", "Tuxpan", "Tuzantla", "Tzintzuntzan", "Tzitzio", "Uruapan", "Venustiano Carranza", "Villamar", "Vista Hermosa", "Yurecuaro", "Zacapu", "Zamora", "Zinaparo", "Zinapecuaro", "Ziracuaretiro", "Zitacuaro" } },
            { "Morelos", new List<string> { "Amacuzac","Atlatlahucan","Axochiapan","Ayala","Coatlan del Rio","Cuautla","Cuernavaca","Emiliano Zapata","Huitzilac","Jantetelco","Jiutepec","Jojutla","Jonacatepec de Leandro Valle","Mazatepec","Miacatlan","Ocuituco","Puente de Ixtla","Temixco","Temoac","Tepalcingo","Tepoztlan","Tetecala","Tetela del Volcan","Tlalnepantla","Tlaltizapan de Zapata","Tlaquiltenango","Tlayacapan","Totolapan","Xochitepec","Yautepec","Yecapixtla","Zacatepec","Zacualpan de Amilpas" } },
            { "Nayarit", new List<string> { "Acaponeta", "Ahuacatlan", "Amatlan de Canas", "Bahia de Banderas", "Compostela", "Del Nayar", "Huajicori", "Ixtlan del Rio", "Jala", "La Yesca", "Rosamorada", "Ruiz", "San Blas", "San Pedro Lagunillas", "Santa Maria del Oro", "Santiago Ixcuintla", "Tecuala", "Tepic", "Tuxpan", "Xalisco" } },
            { "Nuevo Leon", new List<string> { "Abasolo", "Agualeguas", "Allende", "Anahuac", "Apodaca", "Aramberri", "Bustamante", "Cadereyta Jimenez", "Cerralvo", "China", "Cienega de Flores", "Doctor Arroyo", "Doctor Coss", "Doctor Gonzalez", "El Carmen", "Galeana", "Garcia", "General Bravo", "General Escobedo", "General Teran", "General Trevino", "General Zaragoza", "General Zuazua", "Guadalupe", "Hidalgo", "Higueras", "Hualahuises", "Iturbide", "Juarez", "Lampazos de Naranjo", "Linares", "Los Aldamas", "Los Herreras", "Los Ramones", "Marin", "Melchor Ocampo", "Mier y Noriega", "Mina", "Montemorelos", "Monterrey", "Paras", "Pesqueria", "Rayones", "Sabinas Hidalgo", "Salinas Victoria", "San Nicolas de los Garza", "San Pedro Garza Garcia", "Santa Catarina", "Santiago", "Vallecillo", "Villaldama" } },
            { "Oaxaca", new List<string> { "Abejones", "Acatlan de Perez Figueroa", "Animas Trujano", "Asuncion Cacalotepec", "Asuncion Cuyotepeji", "Asuncion Ixtaltepec", "Asuncion Nochixtlan", "Asuncion Ocotlan", "Asuncion Tlacolulita", "Ayoquezco de Aldama", "Ayotzintepec", "Calihuala", "Candelaria Loxicha", "Capulalpam de Mendez", "Chahuites", "Chalcatongo de Hidalgo", "Chiquihuitlan de Benito Juarez", "Cienega de Zimatlan", "Ciudad Ixtepec", "Coatecas Altas", "Coicoyan de las Flores", "Concepcion Buenavista", "Concepcion Papalo", "Constancia del Rosario", "Cosolapa", "Cosoltepec", "Cuilapam de Guerrero", "Cuna de la Independencia de Oaxaca", "Cuyamecalco Villa de Zaragoza", "El Barrio de la Soledad", "El Espinal", "Eloxochitlan de Flores Magon", "Fresnillo de Trujano", "Guadalupe Etla", "Guadalupe de Ramirez", "Guelatao de Juarez", "Guevea de Humboldt" } },
            { "Puebla", new List<string> { "Acajete","Acateno","Acatlan","Acatzingo","Acteopan","Ahuacatlan","Ahuatlan","Ahuazotepec","Ahuehuetitla","Ajalpan","Albino Zertuche","Aljojuca","Altepexi","Amixtlan","Amozoc","Aquixtla","Atempan","Atexcal","Atlequizayan","Atlixco","Atoyatempan","Atzala","Atzitzihuacan","Atzitzintla","Axutla","Ayotoxco de Guerrero","Calpan","Caltepec","Camocuautla","Canada Morelos","Caxhuacan","Chalchicomula de Sesma","Chapulco","Chiautla","Chiautzingo","Chichiquila","Chiconcuautla","Chietla","Chigmecatitlan","Chignahuapan","Chignautla","Chila de la Sal","Chila","Chilchotla","Chinantla","Coatepec","Coatzingo","Cohetzala","Cohuecan","Coronango","Coxcatlan","Coyomeapan","Coyotepec","Cuapiaxtla de Madero","Cuautempan","Cuautinchan","Cuautlancingo","Cuayuca de Andrade","Cuetzalan del Progreso","Cuyoaco","Domingo Arenas","Eloxochitlan","Epatlan","Esperanza","Francisco Z. Mena","General Felipe Angeles","Guadalupe Victoria","Guadalupe","Hermenegildo Galeana","Honey","Huaquechula","Huatlatlauca","Huauchinango","Huehuetla","Huehuetlan el Chico","Huehuetlan el Grande","Huejotzingo","Hueyapan","Hueytamalco","Hueytlalpan","Huitzilan de Serdan","Huitziltepec","Ixcamilpa de Guerrero","Ixcaquixtla","Ixtacamaxtitlan","Ixtepec","Izucar de Matamoros","Jalpan","Jolalpan","Jonotla","Jopala","Juan C. Bonilla","Juan Galindo","Juan N. Mendez","La Magdalena Tlatlauquitepec","Lafragua","Libres","Los Reyes de Juarez","Mazapiltepec de Juarez","Mixtla","Molcaxac","Naupan" } },
            { "Queretaro", new List<string>{ "Amealco de Bonfil", "Arroyo Seco", "Cadereyta de Montes", "Colon", "Corregidora", "El Marques", "Ezequiel Montes", "Huimilpan", "Jalpan de Serra", "Landa de Matamoros", "Pedro Escobedo", "Penamiller", "Pinal de Amoles", "Queretaro", "San Joaquin", "San Juan del Rio", "Tequisquiapan", "Toliman" } },
            { "Quintana Roo", new List<string>{ "Bacalar", "Benito Juarez", "Cozumel", "Felipe Carrillo Puerto", "Isla Mujeres", "Jose Maria Morelos", "Lazaro Cardenas", "Othon P. Blanco", "Puerto Morelos", "Solidaridad", "Tulum" } },
            { "San Luis Potosi", new List<string>{ "Ahualulco", "Alaquines", "Aquismon", "Armadillo de los Infante", "Axtla de Terrazas", "Cardenas", "Catorce", "Cedral", "Cerritos", "Cerro de San Pedro", "Charcas", "Ciudad Fernandez", "Ciudad Valles", "Ciudad del Maiz", "Coxcatlan", "Ebano", "El Naranjo", "Guadalcazar", "Huehuetlan", "Lagunillas", "Matehuala", "Matlapa", "Mexquitic de Carmona", "Moctezuma", "Rayon", "Rioverde", "Salinas", "San Antonio", "San Ciro de Acosta", "San Luis Potosi", "San Martin Chalchicuautla", "San Nicolas Tolentino", "San Vicente Tancuayalab", "Santa Catarina", "Santa Maria del Rio", "Santo Domingo", "Soledad de Graciano Sanchez", "Tamasopo", "Tamazunchale", "Tampacan", "Tampamolon Corona", "Tamuin", "Tancanhuitz", "Tanlajas", "Tanquian de Escobedo", "Tierra Nueva", "Vanegas", "Venado", "Villa Hidalgo", "Villa Juarez", "Villa de Arista", "Villa de Arriaga", "Villa de Guadalupe", "Villa de Ramos", "Villa de Reyes", "Villa de la Paz", "Xilitla", "Zaragoza" } },
            { "Sinaloa", new List<string>{ "Ahome", "Angostura", "Badiraguato", "Choix", "Concordia", "Cosala", "Culiacan", "El Fuerte", "Elota", "Escuinapa", "Guasave", "Mazatlan", "Mocorito", "Navolato", "Rosario", "Salvador Alvarado", "San Ignacio", "Sinaloa" } },
            { "Sonora", new List<string>{ "Aconchi", "Agua Prieta", "Alamos", "Altar", "Arivechi", "Arizpe", "Atil", "Bacadehuachi", "Bacanora", "Bacerac", "Bacoachi", "Bacum", "Banamichi", "Baviacora", "Bavispe", "Benito Juarez", "Benjamin Hill", "Caborca", "Cajeme", "Cananea", "Carbo", "Cucurpe", "Cumpas", "Divisaderos", "Empalme", "Etchojoa", "Fronteras", "General Plutarco Elias Calles", "Granados", "Guaymas", "Hermosillo", "Huachinera", "Huasabas", "Huatabampo", "Huepac", "Imuris", "La Colorada", "Magdalena", "Mazatan", "Moctezuma", "Naco", "Nacori Chico", "Nacozari de Garcia", "Navojoa", "Nogales", "Onavas", "Opodepe", "Oquitoa", "Pitiquito", "Puerto Penasco", "Quiriego", "Rayon", "Rosario", "Sahuaripa", "San Felipe de Jesus", "San Ignacio Rio Muerto", "San Javier", "San Luis Rio Colorado", "San Miguel de Horcasitas", "San Pedro de la Cueva", "Santa Ana", "Santa Cruz", "Saric", "Soyopa", "Suaqui Grande", "Tepache", "Trincheras", "Tubutama", "Ures", "Villa Hidalgo", "Villa Pesqueira", "Yecora" } },
            { "Tabasco", new List<string>{ "Balancan", "Cardenas", "Centla", "Centro", "Comalcalco", "Cunduacan", "Emiliano Zapata", "Huimanguillo", "Jalapa", "Jalpa de Mendez", "Jonuta", "Macuspana", "Nacajuca", "Paraiso", "Tacotalpa", "Teapa", "Tenosique" } },
            { "Tamaulipas", new List<string>{ "Abasolo", "Aldama", "Altamira", "Antiguo Morelos", "Burgos", "Bustamante", "Camargo", "Casas", "Ciudad Madero", "Cruillas", "El Mante", "Gomez Farias", "Gonzalez", "Guemez", "Guerrero", "Gustavo Diaz Ordaz", "Hidalgo", "Jaumave", "Jimenez", "Llera", "Mainero", "Matamoros", "Mendez", "Mier", "Miguel Aleman", "Miquihuana", "Nuevo Laredo", "Nuevo Morelos", "Ocampo", "Padilla", "Palmillas", "Reynosa", "Rio Bravo", "San Carlos", "San Fernando", "San Nicolas", "Soto la Marina", "Tampico", "Tula", "Valle Hermoso", "Victoria", "Villagran", "Xicotencatl" } },
            { "Tlaxcala", new List<string>{ "Acuamanala de Miguel Hidalgo", "Amaxac de Guerrero", "Apetatitlan de Antonio Carvajal", "Apizaco", "Atlangatepec", "Atltzayanca", "Benito Juarez", "Calpulalpan", "Chiautempan", "Contla de Juan Cuamatzi", "Cuapiaxtla", "Cuaxomulco", "El Carmen Tequexquitla", "Emiliano Zapata", "Espanita", "Huamantla", "Hueyotlipan", "Ixtacuixtla de Mariano Matamoros", "Ixtenco", "La Magdalena Tlaltelulco", "Lazaro Cardenas", "Mazatecochco de Jose Maria Morelos", "Munoz de Domingo Arenas", "Nanacamilpa de Mariano Arista", "Nativitas", "Panotla", "Papalotla de Xicohtencatl", "San Damian Texoloc", "San Francisco Tetlanohcan", "San Jeronimo Zacualpan", "San Jose Teacalco", "San Juan Huactzinco", "San Lorenzo Axocomanitla", "San Lucas Tecopilco", "San Pablo del Monte", "Sanctorum de Lazaro Cardenas", "Santa Ana Nopalucan", "Santa Apolonia Teacalco", "Santa Catarina Ayometla", "Santa Cruz Quilehtla", "Santa Cruz Tlaxcala", "Santa Isabel Xiloxoxtla", "Tenancingo", "Teolocholco", "Tepetitla de Lardizabal", "Tepeyanco", "Terrenate", "Tetla de la Solidaridad", "Tetlatlahuca", "Tlaxcala", "Tlaxco", "Tocatlan", "Totolac", "Tzompantepec", "Xaloztoc", "Xaltocan", "Xicohtzinco", "Yauhquemehcan", "Zacatelco", "Ziltlaltepec de Trinidad Sanchez Santos" } },
            { "Veracruz", new List<string>{ "Acajete", "Acatlan", "Acayucan", "Actopan", "Acula", "Acultzingo", "Agua Dulce", "Alamo Temapache", "Alpatlahuac", "Alto Lucero de Gutierrez Barrios", "Altotonga", "Alvarado", "Amatitlan", "Amatlan de los Reyes", "Angel R. Cabada", "Apazapan", "Aquila", "Astacinga", "Atlahuilco", "Atoyac", "Atzacan", "Atzalan", "Ayahualulco", "Banderilla", "Benito Juarez", "Boca del Rio", "Calcahualco", "Camaron de Tejeda", "Camerino Z. Mendoza", "Carlos A. Carrillo", "Carrillo Puerto", "Castillo de Teayo", "Catemaco", "Cazones de Herrera", "Cerro Azul", "Chacaltianguis", "Chalma", "Chiconamel", "Chiconquiaco", "Chicontepec", "Chinameca", "Chinampa de Gorostiza", "Chocaman", "Chontla", "Chumatlan", "Citlaltepetl", "Coacoatzintla", "Coahuitlan", "Coatepec", "Coatzacoalcos", "Coatzintla", "Coetzala", "Colipa", "Comapa", "Cordoba", "Cosamaloapan de Carpio", "Cosautlan de Carvajal", "Coscomatepec", "Cosoleacaque", "Cotaxtla", "Coxquihui", "Coyutla", "Cuichapa", "Cuitlahuac", "El Higo", "Emiliano Zapata", "Espinal", "Filomeno Mata", "Fortin", "Gutierrez Zamora", "Hidalgotitlan", "Huatusco", "Huayacocotla", "Hueyapan de Ocampo", "Huiloapan de Cuauhtemoc", "Ignacio de la Llave", "Ilamatlan", "Isla", "Ixcatepec", "Ixhuacan de los Reyes", "Ixhuatlan de Madero", "Ixhuatlan del Cafe", "Ixhuatlan del Sureste", "Ixhuatlancillo", "Ixmatlahuacan", "Ixtaczoquitlan", "Jalacingo", "Jalcomulco", "Jaltipan", "Jamapa", "Jesus Carranza", "Jilotepec", "Jose Azueta", "Juan Rodriguez Clara", "Juchique de Ferrer", "La Antigua", "La Perla", "Landero y Coss", "Las Choapas", "Las Minas", "Las Vigas de Ramirez", "Lerdo de Tejada", "Los Reyes", "Magdalena", "Maltrata", "Manlio Fabio Altamirano", "Mariano Escobedo", "Martinez de la Torre", "Mecatlan", "Mecayapan", "Medellin de Bravo", "Miahuatlan", "Minatitlan", "Misantla", "Mixtla de Altamirano", "Moloacan", "Nanchital de Lazaro Cardenas del Rio", "Naolinco", "Naranjal", "Naranjos Amatlan", "Nautla", "Nogales", "Oluta", "Omealca", "Orizaba", "Otatitlan", "Oteapan", "Ozuluama de Mascarenas", "Pajapan", "Panuco", "Papantla", "Paso de Ovejas", "Paso del Macho", "Perote", "Platon Sanchez", "Playa Vicente", "Poza Rica de Hidalgo", "Pueblo Viejo", "Puente Nacional", "Rafael Delgado", "Rafael Lucio", "Rio Blanco", "Saltabarranca", "San Andres Tenejapan", "San Andres Tuxtla", "San Juan Evangelista", "San Rafael", "Santiago Sochiapan", "Santiago Tuxtla", "Sayula de Aleman", "Sochiapa", "Soconusco", "Soledad Atzompa", "Soledad de Doblado", "Soteapan", "Tamalin", "Tamiahua", "Tampico Alto", "Tancoco", "Tantima", "Tantoyuca", "Tatahuicapan de Juarez", "Tatatila", "Tecolutla", "Tehuipango", "Tempoal", "Tenampa", "Tenochtitlan", "Teocelo", "Tepatlaxco", "Tepetlan", "Tepetzintla", "Tequila", "Texcatepec", "Texhuacan", "Texistepec", "Tezonapa", "Tierra Blanca", "Tihuatlan", "Tlachichilco", "Tlacojalpan", "Tlacolulan", "Tlacotalpan", "Tlacotepec de Mejia", "Tlalixcoyan", "Tlalnelhuayocan", "Tlaltetela", "Tlapacoyan", "Tlaquilpa", "Tlilapan", "Tomatlan", "Tonayan", "Totutla", "Tres Valles", "Tuxpan", "Tuxtilla", "Ursulo Galvan", "Uxpanapa", "Vega de Alatorre", "Veracruz", "Villa Aldama", "Xalapa", "Xico", "Xoxocotla", "Yanga", "Yecuatla", "Zacualpan", "Zaragoza", "Zentla", "Zongolica", "Zontecomatlan de Lopez y Fuentes", "Zozocolco de Hidalgo" } },
            { "Yucatan", new List<string>{ "Abala", "Acanceh", "Akil", "Baca", "Bokoba", "Buctzotz", "Cacalchen", "Calotmul", "Cansahcab", "Cantamayec", "Celestun", "Cenotillo", "Chacsinkin", "Chankom", "Chapab", "Chemax", "Chichimila", "Chicxulub Pueblo", "Chikindzonot", "Chochola", "Chumayel", "Conkal", "Cuncunul", "Cuzama", "Dzan", "Dzemul", "Dzidzantun", "Dzilam Gonzalez", "Dzilam de Bravo", "Dzitas", "Dzoncauich", "Espita", "Halacho", "Hocaba", "Hoctun", "Homun", "Huhi", "Hunucma", "Ixil", "Izamal", "Kanasin", "Kantunil", "Kaua", "Kinchil", "Kopoma", "Mama", "Mani", "Maxcanu", "Mayapan", "Merida", "Mococha", "Motul", "Muna", "Muxupip", "Opichen", "Oxkutzcab", "Panaba", "Peto", "Progreso", "Quintana Roo", "Rio Lagartos", "Sacalum", "Samahil", "San Felipe", "Sanahcat", "Santa Elena", "Seye", "Sinanche", "Sotuta", "Sucila", "Sudzal", "Suma", "Tahdziu", "Tahmek", "Teabo", "Tecoh", "Tekal de Venegas", "Tekanto", "Tekax", "Tekit", "Tekom", "Telchac Pueblo", "Telchac Puerto", "Temax", "Temozon", "Tepakan", "Tetiz", "Teya", "Ticul", "Timucuy", "Tinum", "Tixcacalcupul", "Tixkokob", "Tixmehuac", "Tixpehual", "Tizimin", "Tunkas", "Tzucacab", "Uayma", "Ucu", "Uman", "Valladolid", "Xocchel", "Yaxcaba", "Yaxkukul", "Yobain" } },
            { "Zacatecas", new List<string>{ "Apozol", "Apulco", "Atolinga", "Benito Juarez", "Calera", "Canitas de Felipe Pescador", "Chalchihuites", "Concepcion del Oro", "Cuauhtemoc", "El Plateado de Joaquin Amaro", "El Salvador", "Fresnillo", "Genaro Codina", "General Enrique Estrada", "General Francisco R. Murguia", "General Panfilo Natera", "Guadalupe", "Huanusco", "Jalpa", "Jerez", "Jimenez del Teul", "Juan Aldama", "Juchipila", "Loreto", "Luis Moya", "Mazapil", "Melchor Ocampo", "Mezquital del Oro", "Miguel Auza", "Momax", "Monte Escobedo", "Morelos", "Moyahua de Estrada", "Nochistlan de Mejia", "Noria de Angeles", "Ojocaliente", "Panuco", "Pinos", "Rio Grande", "Sain Alto", "Santa Maria de la Paz", "Sombrerete", "Susticacan", "Tabasco", "Tepechitlan", "Tepetongo", "Teul de Gonzalez Ortega", "Tlaltenango de Sanchez Roman", "Trancoso", "Trinidad Garcia de la Cadena", "Valparaiso", "Vetagrande", "Villa Garcia", "Villa Gonzalez Ortega", "Villa Hidalgo", "Villa de Cos", "Villanueva", "Zacatecas" } }
        };
    }
}
