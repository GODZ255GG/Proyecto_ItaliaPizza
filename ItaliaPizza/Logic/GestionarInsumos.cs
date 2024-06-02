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
                    cantidadDeEmpaque = insumos.CantidadDeEmpaque,
                    unidadDeMedida = insumos.UnidadDeMedida
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

        public bool RegistrarStockInsumo(int idInsumo, int cantidad)
        {
            bool resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                // Busca el producto en la base de datos
                var insumo = context.Insumos.FirstOrDefault(p => p.idInsumos == idInsumo);

                if (insumo != null)
                {
                    // Verifica si ya existe un registro en InventarioDeProductos para este producto
                    var inventarioInsumo = context.InventarioDeInsumo.FirstOrDefault(i => i.Insumos_idInsumos == idInsumo);

                    if (inventarioInsumo != null)
                    {
                        // Si ya existe, actualiza la cantidad
                        inventarioInsumo.cantidadTotal = cantidad;
                    }
                    else
                    {
                        // Verifica si existe un idInventario válido
                        var inventarioExistente = context.Inventario.FirstOrDefault();
                        if (inventarioExistente == null)
                        {
                            // Si no existe un idInventario válido, crea uno
                            inventarioExistente = new DataAccess.Inventario()
                            {
                                cantidadMaxima = 100, // Asigna un valor adecuado
                                cantidadMinima = 0   // Asigna un valor adecuado
                            };
                            context.Inventario.Add(inventarioExistente);
                            context.SaveChanges();  // Guarda los cambios para obtener el idInventario
                        }

                        // Ahora crea el nuevo registro en InventarioDeProductos
                        var nuevoInventarioInsumo = new DataAccess.InventarioDeInsumo()
                        {
                            Insumos_idInsumos = idInsumo,
                            cantidadTotal = cantidad,
                            Inventario_idInventario = inventarioExistente.idInventario // Asigna el idInventario válido
                        };
                        context.InventarioDeInsumo.Add(nuevoInventarioInsumo);
                    }
                    context.SaveChanges();
                    resultado = true;
                }
            }
            return resultado;
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
                                              UnidadDeMedida = insumos.unidadDeMedida
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

        public List<InventarioDelInsumo> InventarioDeInsumos()
        {
            List<InventarioDelInsumo> listaStock = new List<InventarioDelInsumo>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var insumosConInventario = (from insumo in context.Insumos
                                                  join inventario in context.InventarioDeInsumo
                                                  on insumo.idInsumos equals inventario.Insumos_idInsumos
                                                  select new InventarioDelInsumo
                                                  {
                                                      IdInsumos = insumo.idInsumos,
                                                      CantidadTotal = inventario.cantidadTotal ?? 0
                                                  }).ToList();

                    listaStock.AddRange(insumosConInventario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaStock;
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

        public InventarioDelInsumo ObtenerInventarioDeInsumo(int idInsumo)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var inventario = context.InventarioDeInsumo
                        .FirstOrDefault(i => i.Insumos_idInsumos == idInsumo);

                    if (inventario != null)
                    {
                        return new InventarioDelInsumo
                        {
                            IdInsumos = inventario.Insumos_idInsumos,
                            CantidadTotal = inventario.cantidadTotal ?? 0
                        };
                    }
                    return null; // Retorna null si no hay inventario para el producto
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar el inventario del producto: " + ex.Message);
                return null;
            }
        }

        public bool ActualizarInsumo(int idInsumo, string nuevoNombre, string nuevoCodigo, string nuevaMarca, string nuevoTipo, string nuevoEmpaque, string nuevaUnidad)
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
                    query.unidadDeMedida = nuevaUnidad;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}