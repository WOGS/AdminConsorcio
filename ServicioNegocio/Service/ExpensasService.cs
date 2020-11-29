using ServicioNegocio.EF;
using ServicioNegocio.Models;
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

        public ExpensaModel calcularExpensas(int id)
        {
            ExpensaModel expensamodel = new ExpensaModel();

            expensamodel = expensasRepository.calcularExpensas(id);

            return expensamodel;
        }
    }
}
