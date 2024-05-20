using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ItaliaPizzaClient.Utilidades
{
    public static class Utilidades
    {
        public static void MostrarMensaje(string mensaje, string titulo, MessageBoxImage icono)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, icono);
        }
        public static void MostrarMensajeCamposVacios()
        {
            MostrarMensaje("Ingrese la información solicitada para continuar", "Campos Vacíos", MessageBoxImage.Warning);
        }
        public static void MostrarMensajeCamposInvalidos()
        {
            MostrarMensaje("Los datos ingresados no son validos. Verifique sus datos", "Datos Invalidos", MessageBoxImage.Warning);
        }
        public static void MostrarMensajeEndpointNotFoundException()
        {
            MostrarMensaje("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxImage.Error);
        }
        public static void MostrarMensajeCommunicationException()
        {
            MostrarMensaje("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxImage.Error);
        }
        public static void MostrarMensajeTimeoutException()
        {
            MostrarMensaje("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxImage.Error);
        }

        public static int GenerarCodigo()
        {
            Random random = new Random();
            int numeroAleatorio = random.Next(1000, 10000);
            return numeroAleatorio;
        }
        public static bool MostrarMensajeConfirmacionRegresar()
        {
            MessageBoxResult resultado = MessageBox.Show("¿Está seguro de querer regresar? Si lo hace los datos ingresados se perderán.", "Regresar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return (resultado == MessageBoxResult.Yes);
        }
    }
}
