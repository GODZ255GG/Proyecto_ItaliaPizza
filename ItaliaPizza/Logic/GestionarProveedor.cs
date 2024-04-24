﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GestionarProveedor
    {
        public bool RegistrarProveedor(Proveedor proveedor)
        {
            bool resultado = NuevoRegistroProveedor(proveedor);
            return resultado;
        }

        public bool NuevoRegistroProveedor(Proveedor proveedor)
        {
            var resultado = false;
            using (var context = new BDItaliaPizzaEntities())
            {
                DataAccess.Proveedores nuevoProveedor = new DataAccess.Proveedores()
                {
                    nombreCompañia = proveedor.NombreCompañia,
                    nombreDelContacto = proveedor.NombreContacto,
                    telefono = proveedor.Telefono,
                    ciudad = proveedor.Ciudad,
                    direccion = proveedor.Direccion
                };
                context.Proveedores.Add(nuevoProveedor);
                resultado = context.SaveChanges() > 0;
            }
            return resultado;
        }

        public List<Proveedor> CargarProveedores()
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var proveedoresDetalle = (from proveedor in context.Proveedores
                                              select new Proveedor
                                              {
                                                  IdProveedores = proveedor.idProveedores,
                                                  NombreCompañia = proveedor.nombreCompañia,
                                                  NombreContacto = proveedor.nombreDelContacto,
                                                  Telefono = proveedor.telefono,
                                                  Ciudad = proveedor.ciudad,
                                                  Direccion = proveedor.direccion
                                              });
                    listaProveedores.AddRange(proveedoresDetalle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listaProveedores;
        }

        public bool EliminarProveedor(int idProveedor)
        {
            var resultado = false;
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var proveedor = context.Proveedores.FirstOrDefault(p => p.idProveedores == idProveedor);
                    if (proveedor != null)
                    {
                        context.Proveedores.Remove(proveedor);
                        context.SaveChanges();
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado = false;
            }
            return resultado;
        }

        public bool ActualizarProveedor(int idProveedor, string nuevoNombreCompañia, string nuevoNombreContacto, string nuevoTelefono, string nuevaCiudad, string nuevaDireccion)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var proveedor = context.Proveedores.FirstOrDefault(p => p.idProveedores == idProveedor);
                    if (proveedor != null)
                    {
                        proveedor.nombreCompañia = nuevoNombreCompañia;
                        proveedor.nombreDelContacto = nuevoNombreContacto;
                        proveedor.telefono = nuevoTelefono;
                        proveedor.ciudad = nuevaCiudad;
                        proveedor.direccion = nuevaDireccion;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool CompañiaYaRegistrada(string nombreCompañia)
        {
            bool resultado = false;
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var cantidad = context.Proveedores.Count(p => p.nombreCompañia == nombreCompañia);
                    if (cantidad > 0)
                    {
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
    }
}