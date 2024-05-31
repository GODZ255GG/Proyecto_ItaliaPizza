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

                string turnoActual = lbTurno.Content.ToString();
                DateTime inicioTurno, finTurno;

                if (turnoActual == "Matutino")
                {
                    inicioTurno = DateTime.Today.AddHours(8);
                    finTurno = DateTime.Today.AddHours(15);
                }
                else
                {
                    inicioTurno = DateTime.Today.AddHours(15);
                    finTurno = DateTime.Today.AddHours(22);
                }

                var pedidosDelTurno = pedidosDesdeBD.Where(p => p.FechaPedido >= inicioTurno && p.FechaPedido < finTurno).ToList();

                dgPedidos.ItemsSource = pedidosDelTurno;

                decimal sumaTotal = (decimal)pedidosDelTurno.Sum(p => p.Total);
                lbVentasTotales.Content = $"${sumaTotal:F2}";
            }
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
            }
        }

        private void CargarPedidos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();

                string turnoActual = lbTurno.Content.ToString();
                DateTime inicioTurno, finTurno;

                if (turnoActual == "Matutino")
                {
                    inicioTurno = DateTime.Today.AddHours(8);
                    finTurno = DateTime.Today.AddHours(15);
                }
                else 
                {
                    inicioTurno = DateTime.Today.AddHours(15);
                    finTurno = DateTime.Today.AddHours(22);
                }

                var pedidosDelTurno = pedidosDesdeBD.Where(p => p.FechaPedido >= inicioTurno && p.FechaPedido < finTurno).ToList();

                dgPedidos.ItemsSource = pedidosDelTurno;
            }
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
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
            double totalIngresos = double.Parse(lbVentasTotales.Content.ToString().Replace("$", "").Trim());
            double dineroRestante = double.Parse(tbxDineroEnCaja.Text);

            try
            {
                var cortesDesdeBD = cortesDeCajaServer.RecuperarInformacionDeCortesDeCaja();

                bool corteExistente = cortesDesdeBD.Any(corte => corte.FechaCorteDeCaja.Date == fechaCorteDeCaja.Date && corte.Turno == turno);

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
            catch (EndpointNotFoundException ex)
            {
                Utilidades.Utilidades.MostrarMensajeEndpointNotFoundException();
                this.Close();

            }
            catch (CommunicationException ex)
            {
                Utilidades.Utilidades.MostrarMensajeCommunicationException();
                this.Close();

            }
            catch (TimeoutException ex)
            {
                Utilidades.Utilidades.MostrarMensajeTimeoutException();
                this.Close();
            }
            catch (Exception ex)
            {
                Utilidades.Utilidades.MostrarMensaje($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxImage.Error);
                this.Close();
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