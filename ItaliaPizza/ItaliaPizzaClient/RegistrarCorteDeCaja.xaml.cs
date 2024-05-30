using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for RegistrarCorteDeCaja.xaml
    /// </summary>
    public partial class RegistrarCorteDeCaja : Window
    {
        ItaliaPizzaServer.OrderManagerClient pedidosServer = new ItaliaPizzaServer.OrderManagerClient();
        ItaliaPizzaServer.CashRecordClient cortesDeCajaServer = new ItaliaPizzaServer.CashRecordClient();

        public RegistrarCorteDeCaja()
        {
            InitializeComponent();
            EstablecerFechaYHora();
            EstablecerTurno();
            CargarPedidos();
            CalcularVentasTotales();
        }

        private void EstablecerFechaYHora()
        {
            DateTime fechaActual = DateTime.Now;
            lbFechaDeCorte.Content = fechaActual; //.ToString("f");
        }

        private void EstablecerTurno()
        {
            DateTime horaActual = DateTime.Now;
            string turnoActual;

            if (horaActual.TimeOfDay >= new TimeSpan(8, 0, 0) && horaActual.TimeOfDay < new TimeSpan(15, 0, 0))
            {
                turnoActual = "Matutino de 8am a 3pm";
            }
            else if (horaActual.TimeOfDay >= new TimeSpan(15, 0, 0) && horaActual.TimeOfDay < new TimeSpan(22, 0, 0))
            {
                turnoActual = "Vespertino de 3pm a 10pm";
            }
            else
            {
                turnoActual = "Fuera de horario de turnos";
            }

            lbTurno.Content = turnoActual;
        }

        private void CalcularVentasTotales()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();
                dgPedidos.ItemsSource = pedidosDesdeBD;

                decimal sumaTotal = (decimal)pedidosDesdeBD.Sum(p => p.Total);
                lbVentasTotales.Content = $"${sumaTotal:F2}";
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

        private void CargarPedidos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();
                dgPedidos.ItemsSource = pedidosDesdeBD;
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

        private void btnRegistrarCorteDeCaja_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(tbxDineroEnCaja.Text))
            {
                MessageBox.Show("Ingrese el dinero en caja antes de registrar el corte.", "Dinero en caja vacío", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidDecimal(tbxDineroEnCaja.Text))
            {
                MessageBox.Show("Ingrese un número válido en el campo de dinero en caja.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AccionRegistrarCorteDeCaja();
        }

        private void AccionRegistrarCorteDeCaja()
        {
            DateTime fechaCorteDeCaja = (DateTime)lbFechaDeCorte.Content;
            string turno = lbTurno.Content.ToString();
            float totalIngresos = (float)Math.Round(float.Parse(lbVentasTotales.Content.ToString().Replace("$", "").Trim()), 2);
            float dineroRestante = (float)Math.Round(float.Parse(tbxDineroEnCaja.Text), 2);

            try
            {
                var cortesDesdeBD = cortesDeCajaServer.RecuperarInformacionDeCortesDeCaja();

                bool corteExistente = cortesDesdeBD.Any(corte => corte.FechaCorteDeCaja.Date == fechaCorteDeCaja.Date);


                if (totalIngresos <= 0)
                {
                    MessageBox.Show("No hay ventas realizadas", "Ingreso inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (corteExistente)
                {
                    MessageBox.Show("Ya existe un corte de caja registrado el día de hoy.", "Corte de caja duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ItaliaPizzaServer.CashRecordClient corteDeCajaClient = new ItaliaPizzaServer.CashRecordClient();
                corteDeCajaClient.RegistrarNuevoCorteDeCaja(fechaCorteDeCaja, totalIngresos, dineroRestante, turno);
                MessageBox.Show("El corte de caja se ha registrado exitosamente.", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde.", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo.", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo.", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error inesperado: " + ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TbxDineroCaja_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsValidDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsValidDecimal(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[0-9]*\.?[0-9]{0,2}$");
        }
    }
}