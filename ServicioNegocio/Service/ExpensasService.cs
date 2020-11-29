using ServicioNegocio.EF;
using ServicioNegocio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Service
{
    public class ExpensasService
    {
        ExpensasRepository expensasRepository;

        public ExpensasService()
        {
            expensasRepository = new ExpensasRepository();
        }

        public List<Expensa> calcularExpensas(int id)
        {
            List<Expensa> expensas = new List<Expensa>();

            expensas = expensasRepository.calcularExpensas(id);

            return expensas;
        }

        public int gastoTotalMes(int id)
        {
            int gastoTotalMes = expensasRepository.gastoTotalMes(id);

            return gastoTotalMes;
        }
    }
}
