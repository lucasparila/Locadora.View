using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller.Interfaces
{
    public interface ILocacaoFuncionarioController
    {

        public void AdicionarLocacaoFuncionario(LocacaoFuncionario locacaoFuncionario);
        public List<LocacaoFuncionario> ListarLocaoesFuncionarios();
        public LocacaoFuncionario BuscaLocacaoFuncionarioPorId(int id);
    }
}
