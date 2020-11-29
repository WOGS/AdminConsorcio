using ServicioNegocio.EF;
using ServicioNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    public class ExpensasRepository
    {
        Contexto contexto;

        public ExpensasRepository()
        {
            contexto = new Contexto();
        }

        public ExpensaModel calcularExpensas(int id)
        {
            int first = 0;
            ExpensaModel expensaModel = new ExpensaModel();
            Expensa expensaActual = new Expensa();
            List<CalcularExpensa_Result> resultado = contexto.CalcularExpensa(id).ToList();

            List<Expensa> expensas = new List<Expensa>();

            foreach (CalcularExpensa_Result item in resultado)
            {
                if (first == 0)
                {
                    expensaActual.AnioExpensa = item.AnioExpensa;
                    expensaActual.MesExpensa = item.MesExpensa;
                    expensaActual.GastoTotal = item.GastoTotal;
                    expensaActual.ExpensaUnidad = item.ExpensaUnidad;
                    first = 1;
                }
                else
                {
                    Expensa expensa = new Expensa();
                    expensa.AnioExpensa = item.AnioExpensa;
                    expensa.MesExpensa = item.MesExpensa;
                    expensa.GastoTotal = item.GastoTotal;
                    expensa.ExpensaUnidad = item.ExpensaUnidad;
                    expensas.Add(expensa);
                }
            }
            expensaModel.expensas = expensas;
            expensaModel.gastosActuales = expensaActual.GastoTotal;
            expensaModel.montoPorUnidad = expensaActual.ExpensaUnidad;
            expensaModel.expensaMes = expensaActual.MesExpensa;

            return expensaModel;
        }

    }
}
