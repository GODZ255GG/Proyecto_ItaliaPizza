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
    public partial class ConsultarReceta : Window
    {
        RecipeManagerClient client = new RecipeManagerClient();

        private int idRecetas;
        private string nombre;
        private string descripcionPreparación;
        

        public ConsultarReceta(Recetas receta)
        {
            InitializeComponent();


            idRecetas = receta.IdRecetas;
            nombre = receta.Nombre;
            descripcionPreparación = receta.DescripcionPreparación;
            ;

            tbxNombre.Text = nombre;
            tbxDescripcion.Text = descripcionPreparación;
            
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Quieres eliminar la receta?", "Confirmar eliminación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    client.EliminarReceta(idRecetas);
                    MessageBox.Show("Receta eliminada exitosamente", "Eliminación exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Title = "Modificar Receta";
            lbTitulo.Content = "Modificar Receta";

            btnModificar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = false;
            tbxDescripcion.IsReadOnly = false;
            
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre;
            string nuevaDescripcion;
            

            nuevoNombre = tbxNombre.Text;
            nuevaDescripcion = tbxDescripcion.Text;
            

            if (CamposVacios())
            {
                if (StringValidos(nombre, descripcionPreparación) && StringLargos(nombre, descripcionPreparación))
                {
                    try
                    {
                        client.ActualizarReceta(idRecetas, nuevoNombre, nuevaDescripcion);
                        MessageBox.Show("La receta se ha actualizado exitosamente", "Actualización exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Title = "Consultar Receta";
            lbTitulo.Content = "Información de la Receta";

            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;

            tbxNombre.IsReadOnly = true;
            tbxDescripcion.IsReadOnly = true;
            

            tbxNombre.Text = nombre;
            tbxDescripcion.Text = descripcionPreparación;
            
        }

        #region Validaciones
        public bool CamposVacios()
        {
            if (tbxNombre.Text == string.Empty || tbxDescripcion.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool StringValidos(string nombre, string descripcionPreparación)
        {
            if (Regex.IsMatch(nombre, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$")
                && Regex.IsMatch(descripcionPreparación, @"^[a-zA-Z\s\-.,'()ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                return true;
            }
            return false;
        }

        private bool StringLargos(string nombre, string descripcionPreparación)
        {
            if (nombre.Length <= 45 || descripcionPreparación.Length <= 100)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
