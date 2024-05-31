using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GestionarCortesDeCaja
    {
        public List<CorteDeCaja> RecuperarInformacionDeCortesDeCaja()
        {
            List<CorteDeCaja> listaCortes = new List<CorteDeCaja>();

            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    var cortesDeCaja = (from c in context.CorteDeCaja
                                        select new CorteDeCaja
                                        {
                                            IdCorteDeCaja = c.idCorteDeCaja,
                                            FechaCorteDeCaja = (DateTime)c.fechaDeCorte,
                                            TotalIngresos = (double)c.totalDeIngresos,
                                            DineroRestante = (double)c.dineroRestante,
                                            Turno = c.turno
                                        }).ToList();
                    listaCortes.AddRange(cortesDeCaja);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recuperar la información de los cortes de caja: " + ex.Message);
            }

            return listaCortes;
        }

        public void RegistrarNuevoCorteDeCaja(DateTime fechaCorteDeCaja, double totalIngresos, double dineroRestante, string turno)
        {
            try
            {
                using (var context = new BDItaliaPizzaEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            DataAccess.CorteDeCaja nuevoCorteDeCaja = new DataAccess.CorteDeCaja
                            {
                                fechaDeCorte = fechaCorteDeCaja,
                                totalDeIngresos = totalIngresos,
                                dineroRestante = dineroRestante,
                                turno = turno
                            };
                            context.CorteDeCaja.Add(nuevoCorteDeCaja);
                            context.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al registrar el nuevo corte de caja.", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el nuevo corte de caja: " + ex.Message);
            }
        }
    }
}
