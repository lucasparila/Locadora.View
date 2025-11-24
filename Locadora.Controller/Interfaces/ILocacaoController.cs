using Locadora.Models;
using Locadora.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoController
    {
        public Guid AdicionarLocacao(Locacao locacao);
        public List<Locacao> ListarLocacoes();
        public Locacao BuscarLocacaoPorId(Guid id);

        public void AtualizarStatusLocacao(Locacao locacao, string status);
        public void AtualizarDataDevolucaoRealLocacao(Locacao locacao, DateTime? devolucao);
        public void AtualizarValorTotalLocacao(Locacao locacao);


    }
}
