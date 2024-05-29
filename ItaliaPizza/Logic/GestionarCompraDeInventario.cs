using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class GestionarCompraDeInventario
    {

        public void RegistrarNuevaCompra(int idProveedor, List<int> idInsumos)
        {
            using (var context = new BDItaliaPizzaEntities())
            { 
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DataAccess.CompraDeInventario nuevaCompra = new DataAccess.CompraDeInventario
                        {
                            Proveedores_idProveedores = idProveedor,
                            fechaDeCompra = DateTime.Now,
                        };
                        context.CompraDeInventario.Add(nuevaCompra);
                        context.SaveChanges();

                        int idCompra = nuevaCompra.idCompraDeInventario;
                        foreach (int idInsumo in idInsumos)
                        {
                            CompraInsumo compraInsumo = new CompraInsumo
                            {
                                Insumos_idInsumos = idInsumo,
                                CompraDeInventario_idCompraDeInventario = idCompra,
                            };
                            context.CompraInsumo.Add(compraInsumo);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al registrar el nuevo pedido", ex);
                    }
                }
            }
        }

        public List<CompraDeInventario> RecuperarCompras()
        {
            List<CompraDeInventario> listaCompra = new List<CompraDeInventario>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var ComprasConInsumos = (from c in context.CompraDeInventario
                                             join pr in context.Proveedores on c.Proveedores_idProveedores equals pr.idProveedores
                                             group new { c, pr } by new
                                             {
                                                 c.idCompraDeInventario,
                                                 c.fechaDeCompra,
                                                 pr.idProveedores,
                                             } into g
                                             select new
                                             {
                                                 CompraId = g.Key.idCompraDeInventario,
                                                 FechaDeCompra = g.Key.fechaDeCompra,
                                                 ProveedorId = g.Key.idProveedores,
                                                 Insumos = (from ci in context.CompraInsumo
                                                            join ins in context.Insumos on ci.Insumos_idInsumos equals ins.idInsumos
                                                            where ci.CompraDeInventario_idCompraDeInventario == g.Key.idCompraDeInventario
                                                            select new
                                                            {
                                                                Nombre = ins.nombre,
                                                            }).ToList()
                                             }).ToList();
                    foreach (var compraDetalle in ComprasConInsumos)
                    {
                        var insumoDetalle = compraDetalle.Insumos
                            .GroupBy(x => new { x.Nombre })
                            .Select(grp => new
                            {
                                Nombre = grp.Key.Nombre,
                                Cantidad = grp.Count()
                            });

                        string insumosConcatenados = string.Join("", insumoDetalle.Select(x => $"{x.Nombre} x{x.Cantidad}\n"));

                        listaCompra.Add(new Logic.CompraDeInventario
                        {
                            IdCompraDeInventario = compraDetalle.CompraId,
                            FechaDeCompra = (DateTime)compraDetalle.FechaDeCompra,
                            InsumosConcatenados = insumosConcatenados
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información" + ex.Message);
            }
            return listaCompra;
        }

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
