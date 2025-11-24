using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Locadora.Controller
{
    public class LocacaoFuncionarioController : ILocacaoFuncionarioController
    {
        public void AdicionarLocacaoFuncionario(LocacaoFuncionario locacaoFuncionario, Funcionario funcionario)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(LocacaoFuncionario.INSERTLOCACAOFUNCIONARIO, connection, transaction);
                    command.Parameters.AddWithValue("@LocacaoID", locacaoFuncionario.locacao.LocacaoID);
                    command.Parameters.AddWithValue("@FuncionarioID", funcionario.FuncionarioID);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao adicionar ao adicionar na tabela LocacaoFuncionarios: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao adicionar ao adicionar na tabela LocacaoFuncionarios: " + ex.Message);
                }
            }
        }

        public LocacaoFuncionario BuscaLocacaoFuncionarioPorId(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                connection.Open();

                LocacaoController controllerLocacao = new LocacaoController();
                FuncionarioController controllerFuncionario = new FuncionarioController();

                using (SqlCommand command = new SqlCommand(LocacaoFuncionario.SELECTLOCACAOFUNCIONARIOPORID, connection))
                {
                    try
                    {
                        command.Parameters.Add("@LocacaoID", SqlDbType.UniqueIdentifier).Value = id;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            var locacao = controllerLocacao.BuscarLocacaoPorId(reader.GetGuid(1));

                            var locacaoFuncionario = new LocacaoFuncionario(
                                reader.GetInt32(0),
                                locacao
                            );

                            var funcionarioPrimeiro = controllerFuncionario.BuscarFuncionarioPorId(reader.GetInt32(2));
                            locacaoFuncionario.addFuncionario(funcionarioPrimeiro);

                            while (reader.Read())
                            {
                                var funcionario = controllerFuncionario.BuscarFuncionarioPorId(reader.GetInt32(2));
                                locacaoFuncionario.addFuncionario(funcionario);
                            }

                            return locacaoFuncionario;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Locação com funcionário não encontrada: {ex.Message}");
                    }
                }
            }
        }

        public List<LocacaoFuncionario> ListarLocaoesFuncionarios()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            List<LocacaoFuncionario> locacoesFuncionario = new List<LocacaoFuncionario>();
            LocacaoController controllerLocacao = new LocacaoController();
            FuncionarioController controllerFuncionario = new FuncionarioController();
            using (SqlCommand command = new SqlCommand(LocacaoFuncionario.SELECTLOCACAOFUNCIONARIOALL, connection))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        Locacao locacao = controllerLocacao.BuscarLocacaoPorId(reader.GetGuid(1));
                        Funcionario funcionario = controllerFuncionario.BuscarFuncionarioPorId(reader.GetInt32(2));

                        var locFunc = locacoesFuncionario.FirstOrDefault(p => p.locacao.LocacaoID == reader.GetGuid(1));


                        if (locFunc == null)
                        {
                            locFunc = new LocacaoFuncionario(reader.GetInt32(0), locacao);
                            locacoesFuncionario.Add(locFunc);
                        }
                        locFunc.addFuncionario(funcionario);
                                                          
                    }
                    return locacoesFuncionario;

                }
                catch (SqlException ex)
                {

                    throw new Exception($"Locacao não encontrada " + ex.Message);
                }
                catch (Exception ex)
                {

                    throw new Exception($"Locacao não encontrada " + ex.Message);
                }
            }
        }
    }
}
