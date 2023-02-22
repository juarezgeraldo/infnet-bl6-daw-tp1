using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace infnet_bl6_daw_tp1.Domain.Entities
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }


        [Display(Name = "Nome completo")]
        public string NomeCompleto
        {
            get { return Nome + " " + Sobrenome; }
        }
        [Display(Name = "Próximo aniversário")]
        [DataType(DataType.Date)]
        public DateTime ProximoAniversario
        {
            get { return ProximoAniversarioFuncao(); }
        }
        [Display(Name = "Dias para aniversário")]
        public int DiasFaltantes
        {
            get { return CalculaDiasFaltantesFuncao(); }
        }

        public DateTime ProximoAniversarioFuncao()
        {
            DateTime dataProximoAniversario = new(DateTime.Now.Year, Nascimento.Month, Nascimento.Day, 0, 0, 0);
            if (DateTime.Compare(dataProximoAniversario, DateTime.Today) < 0)
            {
                dataProximoAniversario = dataProximoAniversario.AddYears(1);
            }
            return dataProximoAniversario;
        }
        public int CalculaDiasFaltantesFuncao()
        {
            DateTime dataAtual = new(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime dataAniversario = ProximoAniversarioFuncao();
            DateTime dataProximoAniversario = dataAniversario;

            if (dataAtual.Month == dataAniversario.Month &&
                dataAtual.Day == dataAniversario.Day)
            {
                return 0;
            }
            int difDatas = (int)dataAtual.Subtract(dataProximoAniversario).TotalDays;
            if (difDatas < 0) { difDatas *= -1; }

            return difDatas;
        }
        public bool NomeCompletoPossui(string nomePesquisa)
        {
            return NomeCompleto.ToLowerInvariant().Contains(nomePesquisa.Trim().ToLowerInvariant());
        }
    }
}
