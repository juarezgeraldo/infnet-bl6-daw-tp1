using infnet_bl6_daw_tp1.Domain.Entities;
using infnet_bl6_daw_tp1.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infnet_bl6_daw_tp1.Domain.Interfaces
{
    public interface IAmigoService
    {
        List<AmigoViewModel> GetAll();
        Amigo Add(Amigo amigo);
        Amigo Save(Amigo amigo);
        Amigo Remove(Amigo amigo);
    }
}
