using infnet_bl6_daw_tp1.Domain.Entities;
using infnet_bl6_daw_tp1.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_tp1.Service.Services
{
    public class AmigoService : IAmigoService
    {
        public readonly infnet_bl6_daw_tp1DbContext _dbContext;
        public AmigoService(infnet_bl6_daw_tp1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

/*        public IList<TipoInvestimentoViewModel> GetAll()
        {
            var tipoInvestimentos = _dbContext.tipoInvestimento.ToList();

            return TipoInvestimentoViewModel.GetAll(tipoInvestimentos);
        }
*/

        public List<Amigo> GetAll()
        {
             return _dbContext.Amigo.ToList();
        }
        Amigo IAmigoService.Add(Amigo amigo)
        {
            return _dbContext.Amigo.Add(amigo).Entity;
            
        }

    }
}
