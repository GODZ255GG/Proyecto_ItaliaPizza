using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarRecetas : Window
    {
        InsumoManagerClient insumoServer = new InsumoManagerClient();

        public RegistrarRecetas()
        {
            InitializeComponent();
            MostrarInformacionInsumo();

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MostrarInformacionInsumo()
        {
            try
            {
                Insumos[] insumoArray = insumoServer.ObtenerListaInsumos();
                List<Insumos> insumos = insumoArray.ToList();

                dgInsumo.ItemsSource = insumos;
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


        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = tbxNombre.Text;;
            var descripcionPreparación = tbxDescripcion.Text;

            if (CamposVacios())
            {
                if (StringValidos(nombre, descripcionPreparación) && StringLargos(nombre, descripcionPreparación))
                {
                    try
                    {
                        AccionRegistrar();
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

        private void AccionRegistrar()
        {
            var nombre = tbxNombre.Text;
            var descripcionPreparación = tbxDescripcion.Text;


            ItaliaPizzaServer.RecipeManagerClient client = new ItaliaPizzaServer.RecipeManagerClient();

            ItaliaPizzaServer.Recetas nuevoReceta = new ItaliaPizzaServer.Recetas()
            {
                Nombre = nombre,
                DescripcionPreparación = descripcionPreparación,
                
            };

            var result = false;

            if (CamposVacios())
            {

                if (!client.RecetaYaRegistrado(nombre))
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Esta receta ya se encuentra registrado en el sistema, intente con otro", "Receta duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (result)
                {
                    var aux = client.RegistrarReceta(nuevoReceta);
                    if (aux)
                    {
                        MessageBox.Show("La receta se ha registrado exitosamente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la receta", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la receta", "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese la información solicitada para continuar.", "Campos Vacios", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (tbxNombre.Text == string.Empty || tbxDescripcion.Text == string.Empty )
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string nombre, string descripcionPreparación)
        {
            var esValido = false;
            if (Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$")
                && Regex.IsMatch(descripcionPreparación, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                esValido = true;
            }
            return esValido;
        }

        private bool StringLargos(string nombre, string descripcionPreparación)
        {
            var noSonLargos = false;
            if (nombre.Length <= 45 || descripcionPreparación.Length <= 100)
            {
                noSonLargos = true;
            }
            return noSonLargos;
        }
        #endregion
    }
}
