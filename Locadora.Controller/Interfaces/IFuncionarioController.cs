using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Controller.Interfaces
{
    public interface IFuncionarioController
    {

        public void AdicionarFuncionario(Funcionario funcionario);
        public List<Funcionario> ListarFuncionarios();
        public Funcionario BuscarFuncionarioPorEmail(string email);
        public void AtualizarSalarioFuncionario(decimal salario,Funcionario funcionario);
        public void AtualizarSenhaFuncionario(string senha, Funcionario funcionario);
        public void DeletarFuncionario(string email);
    }
}
