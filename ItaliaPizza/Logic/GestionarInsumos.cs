using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class GestionarInsumos
    {
        public bool Registrar(Logic.Insumos insumos)
        {
            bool resultado = NuevoRegistro(insumos);
            return resultado;
        }

        public bool NuevoRegistro(Logic.Insumos insumos)
        {
            var resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Insumos nuevoInsumo = new DataAccess.Insumos()
                {
                    nombre = insumos.Nombre,
                    codigoInsumo = insumos.CodigoInsumo,
                    marca = insumos.Marca,
                    tipo = insumos.Tipo,
                    cantidadDeEmpaque = insumos.CantidadDeEmpaque
                };
                context.Insumos.Add(nuevoInsumo);
                resultado = context.SaveChanges() > 0;
            }
            return resultado;
        }

        public bool InsumoYaRegistrado(string nombre)
        {
           using (var context = new BDItaliaPizzaEntities())
            {
                var insumos = (from insumo in context.Insumos
                                where insumo.nombre == nombre
                                select insumo).Count();
                if (insumos > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Insumos> CargarInsumos()
        {
            List<Insumos> listaInsumos = new List<Insumos>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var insumosDetalle = (from insumos in context.Insumos
                                            select new Insumos
                                            {
                                                IdInsumos = insumos.idInsumos,
                                                Nombre = insumos.nombre,
                                                CodigoInsumo = insumos.codigoInsumo,
                                                Marca = insumos.marca,
                                                Tipo = insumos.tipo,
                                                CantidadDeEmpaque = insumos.cantidadDeEmpaque,
                                            });
                    listaInsumos.AddRange(insumosDetalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaInsumos;
        }

        public bool EliminarInsumoSeleccionado(int idInsumo)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                var insumo = context.Insumos.FirstOrDefault(i => i.idInsumos == idInsumo);
                if (insumo != null)
                {
                    context.Insumos.Remove(insumo);
                    context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
        }

        public bool ActualizarInsumo(int idInsumo, string nuevoNombre, string nuevoCodigo, string nuevaMarca, string nuevoTipo, string nuevoEmpaque)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                var query = (from insumos in context.Insumos
                             where insumos.idInsumos == idInsumo
                             select insumos).FirstOrDefault();
                if (query != null)
                {
                    query.nombre = nuevoNombre;
                    query.codigoInsumo = nuevoCodigo;
                    query.marca = nuevaMarca;
                    query.tipo = nuevoTipo;
                    query.cantidadDeEmpaque = nuevoEmpaque;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
