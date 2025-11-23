using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Locadora.Controller
{
    public class FuncionarioController : IFuncionarioController
    {
        public void AdicionarFuncionario(Funcionario funcionario)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.INSERTFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    command.Parameters.AddWithValue("@Senha", funcionario.Senha);
                    command.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    command.Parameters.AddWithValue("@Email", funcionario.Email);
                    command.Parameters.AddWithValue("@Salario", funcionario.Salario ?? (object)DBNull.Value);

                    
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao adicionar o funcionário {funcionario.Nome}" + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro inesperado ao adicionar o funcionário {funcionario.Nome}" + ex.Message);
                }
            }
        }
        public void AtualizarSenhaFuncionario(string senha, Funcionario funcionario)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            funcionario.setSenha(senha);
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.UPDATESENHAFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@Senha", funcionario.Senha);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionario.FuncionarioID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar senha do funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado senha do funcionario: " + ex.Message);
                }
            }
        }

        public void AtualizarSalarioFuncionario(decimal salario, Funcionario funcionario)
        {
           

            funcionario.setSalario(salario);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.UPDATESALARIOFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@Salario", funcionario.Salario);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionario.FuncionarioID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar o salario do funcionario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado o salario do funcionario: " + ex.Message);
                }

            }
        }

        public Funcionario BuscarFuncionarioPorEmail(string email)
        {

            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlCommand command = new SqlCommand(Funcionario.SELECTFUNCIONARIOPOREMAIL, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        decimal? salario = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5);
                        var funcionario = new Funcionario(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            salario
                        );

                        funcionario.setId(reader.GetInt32(0));
                        return funcionario;
                    }
                    throw new Exception($"Funcionario não encontrado");

                }
                catch (SqlException ex)
                {
                    
                    throw new Exception($"Funcionario não encontrado " + ex.Message);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception($"Funcionario não encontrado " + ex.Message);
                }
            }
        }

        public void DeletarFuncionario(string email)
        {
            var funcionario = BuscarFuncionarioPorEmail(email);
            if (funcionario is null)
            {
                throw new Exception("Funcionario não encontrado");
            }

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Funcionario.DELETEFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionario.FuncionarioID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar o funcionário: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao deletar o funcionário: " + ex.Message);
                }

            }
        }

        public List<Funcionario> ListarFuncionarios()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            using (connection)
            {
                try
                {
                    connection.Open();

                    List<Funcionario> funcionarios = new List<Funcionario>();

                    SqlCommand command = new SqlCommand(Funcionario.SELECTALLFUNCIONARIO, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        decimal? salario = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5);

                        var funcionario = new Funcionario(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            salario
                        );

                        funcionario.setId(reader.GetInt32(0));
                        funcionarios.Add(funcionario);
                    }

                    return funcionarios;

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar funcionarios: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao listar funcionarios: " + ex.Message);
                }
            }
        }
    }
}
