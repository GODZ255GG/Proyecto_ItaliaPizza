using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class GestionarCompraInventario
    {
        public List<Logic.Insumos> RecuperarInsumosPorCategoria(string categoria)
        {
            List<Logic.Insumos> listaInsumos = new List<Logic.Insumos>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var insumoPorCategoria = (from pr in context.Insumos
                                              where pr.tipo == categoria
                                              select new Logic.Insumos
                                              {
                                                  IdInsumos = pr.idInsumos,
                                                  Nombre = pr.nombre,
                                              }).ToList();

                    listaInsumos.AddRange(insumoPorCategoria);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información: " + ex.Message);
            }

            return listaInsumos;
        }
    }
}
