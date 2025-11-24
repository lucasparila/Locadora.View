using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class LocacaoFuncionario
    {
        public readonly static string INSERTLOCACAOFUNCIONARIO = "INSERT INTO tblLocacaoFuncionarios (LocacaoID, FuncionarioID) " +
                                                                  " VALUES " +
                                                                   " (@LocacaoID, @FuncionarioID);";

        public readonly static string SELECTLOCACAOFUNCIONARIOPORID = "SELECT LocacaoFuncionarioID, LocacaoID, FuncionarioID " +
                                                                       "FROM tblLocacaoFuncionarios " +
                                                                        "WHERE LocacaoID = @LocacaoID;";

        public readonly static string SELECTLOCACAOFUNCIONARIOALL = "SELECT LocacaoFuncionarioID, LocacaoID, FuncionarioID " +
                                                                       "FROM tblLocacaoFuncionarios;";

        public int ID { get; private set; }
        public Locacao locacao { get; private set; }
        public List<Funcionario> funcionarios { get; private set; }

        public LocacaoFuncionario(Locacao locacao)
        {
            this.locacao = locacao;
            this.funcionarios = new List<Funcionario>();
        }
        public LocacaoFuncionario(int id, Locacao locacao) : this(locacao)
        {
            this.ID = id;
        }

        public void addFuncionario(Funcionario funcionario)
        {
            if (!funcionarios.Contains(funcionario))
            {
                funcionarios.Add(funcionario);
            }
        }

        public override string ToString()
        {
            string dados = $"{this.locacao.ToString()}";
            if (this.funcionarios.Count > 0)
            {
                foreach(var funcionario in this.funcionarios)
                {
                    dados += $"Funcionario: {funcionario.Nome}\n";
                }
            }
            return dados;

        }
    }
}
