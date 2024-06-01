using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GestionarReceta
    {
        public bool Registrar(Logic.Recetas receta)
        {
            bool status = NuevoRegistro(receta);
            return status;
        }

        public bool NuevoRegistro(Logic.Recetas receta)
        {
            var status = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Receta nuevoReceta = new DataAccess.Receta()
                {
                    nombre = receta.Nombre,
                    descripcionPreparacion = receta.DescripcionPreparación


                };
                context.Receta.Add(nuevoReceta);
                status = context.SaveChanges() > 0;
            }
            return status;
        }

        public bool RecetaYaRegistrado(string nombre)
        {
            bool result = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var usuario = (from Receta in context.Receta
                               where Receta.nombre == nombre
                               select Receta).Count();
                if (usuario > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool ActualizarReceta(int idRecetas, string nombre, string descripcionPreparación)
        {
            using (var context = new BDItaliaPizzaEntities())
            {
                var query = (from Receta in context.Receta
                             where Receta.idRecetas == idRecetas
                             select Receta).FirstOrDefault();
                if (query != null)
                {
                    query.nombre = nombre;
                    query.descripcionPreparacion = descripcionPreparación;

                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public List<Recetas> CargarReceta()
        {
            List<Recetas> listaReceta = new List<Recetas>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var recetaDetalle = (from receta in context.Receta
                                         select new Recetas
                                         {
                                             IdRecetas = receta.idRecetas,
                                             Nombre = receta.nombre,
                                             DescripcionPreparación = receta.descripcionPreparacion,

                                         });
                    listaReceta.AddRange(recetaDetalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaReceta;
        }

        public bool EliminarReceta(int idRecetas)
        {
            var status = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                var receta = context.Receta.FirstOrDefault(p => p.idRecetas == idRecetas);
                if (receta != null)
                {
                    context.Receta.Remove(receta);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }
    }
}
