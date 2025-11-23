using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Funcionario
    {
        public readonly static string INSERTFUNCIONARIO = @"INSERT INTO tblFuncionarios(Senha, Nome, CPF, Email, Salario)
                                                           VALUES (@Nome, @Senha, @CPF, @Email, @Salario);";

        public readonly static string SELECTALLFUNCIONARIO = @"SELECT FuncionarioID, Senha, Nome, CPF, Email, Salario FROM tblFuncionarios";

        public readonly static string SELECTFUNCIONARIOPOREMAIL = @"SELECT FuncionarioID, Senha, Nome, CPF, Email, Salario 
                                                                   FROM tblFuncionarios
                                                                    WHERE Email = @Email";
        public readonly static string SELECTFUNCIONARIOPORID = @"SELECT FuncionarioID, Senha, Nome, CPF, Email, Salario 
                                                                   FROM tblFuncionarios
                                                                    WHERE FuncionarioID = @FuncionarioID";

        public readonly static string UPDATESALARIOFUNCIONARIO = @"UPDATE tblFuncionarios
                                                                 SET Salario = @Salario
                                                                  WHERE FuncionarioID = @FuncionarioID";

        public readonly static string UPDATESENHAFUNCIONARIO = @"UPDATE tblFuncionarios
                                                                 SET Senha = @Senha
                                                                  WHERE FuncionarioID = @FuncionarioID";

        public readonly static string DELETEFUNCIONARIO = @"DELETE FROM tblFuncionarios
                                                           WHERE FuncionarioID = @FuncionarioID";


        public int FuncionarioID { get; set; }
        public string Nome {  get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public decimal? Salario { get; set; }

        public Funcionario(string senha,string nome, string cPF, string email)
        {
            Nome = nome;
            CPF = cPF;
            Email = email;
            Senha = senha;
            this.Salario = null;
        }

        public Funcionario(string senha, string nome, string cPF, string email,  decimal? salario) : this(senha, nome, cPF, email)
        {
            this.Salario = salario;
        }

        public void setId(int id)
        {
            this.FuncionarioID = id;
        }

        public void setSalario (decimal salario)
        {
            this.Salario = salario;
        }

        public void setSenha(string senha)
        {
            this.Senha = senha;
        }
        public override string ToString()
        {
            return $"Nome: {this.Nome}\n" +
                $"CPF: {this.CPF}\n" +
                $"Email: {this.Email}\n" +
                $"Salario: {(this.Salario != null ? this.Salario: "-")}\n";
        }
    }
}
