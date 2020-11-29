using ServicioNegocio.EF;
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

        public List<Expensa> calcularExpensas(int id)
        {
            List<CalcularExpensa_Result> resultado = contexto.CalcularExpensa(id).ToList();

            List<Expensa> expensas = new List<Expensa>();

            foreach (CalcularExpensa_Result item in resultado)
            {
                Expensa expensa = new Expensa();
                expensa.AnioExpensa = item.AnioExpensa;
                expensa.MesExpensa = item.MesExpensa;
                expensa.GastoTotal = item.GastoTotal;
                expensa.ExpensaUnidad = item.ExpensaUnidad;
                expensas.Add(expensa);
            }
            return expensas;
        }

        public int gastoTotalMes(int id)
        {
            int gastoTotalMes = 800;

            return gastoTotalMes;
        }
    }
}
