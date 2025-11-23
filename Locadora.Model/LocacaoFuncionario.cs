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
                                                                        "WHERE LocacaoFuncionarioID = @LocacaoFuncionarioID;";

        public readonly static string SELECTLOCACAOFUNCIONARIOALL = "SELECT LocacaoFuncionarioID, LocacaoID, FuncionarioID " +
                                                                       "FROM tblLocacaoFuncionarios;";
                                                                       
        public int ID {  get; private set; }
        public Locacao locacao {  get; private set; }
        public Funcionario funcionario { get; private set; }

        public LocacaoFuncionario(Locacao locacao, Funcionario funcionario)
        {
            this.locacao = locacao;
            this.funcionario = funcionario;
        }
        public LocacaoFuncionario( int id, Locacao locacao, Funcionario funcionario): this(locacao, funcionario)
        {
            this.ID = id;
        }

        public override string ToString()
        {
            return $"{this.locacao.ToString()}" +
                $"Modelo do veículo: {this.locacao.veiculo.Modelo}\n" +
                $"Placa do veículo: {this.locacao.veiculo.Placa}\n" +
                $"Nome do Funcionário: {this.funcionario.Nome}\n";
                
        }
    }
}
