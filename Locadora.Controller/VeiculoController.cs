
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
    public class VeiculoController : IVeiculoController
    {
        public void AdicionarVeiculo(Veiculo veiculo)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Veiculo.INSERTVEICULO, connection, transaction);
                    command.Parameters.AddWithValue("@CategoriaID", veiculo.CategoriaID);
                    command.Parameters.AddWithValue("@Placa", veiculo.Placa);
                    command.Parameters.AddWithValue("@Marca", veiculo.Marca);
                    command.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                    command.Parameters.AddWithValue("@Ano", veiculo.Ano);
                    command.Parameters.AddWithValue("@StatusVeiculo", veiculo.StatusVeiculo);
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar o veiculo" + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao adicionar o veiculo" + ex.Message);
                }
            }
        }

        public void AtualizarStatusVeiculo(string statusVeiculo, string placa)
        {
            var veiculoEncontrado = BuscarVeiculoPlaca(placa);
            if (veiculoEncontrado is null)
            {
                throw new Exception("Veiculo não encontrado");
            }

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Veiculo.UPDATESTATUSVEICULO, connection, transaction);
                    command.Parameters.AddWithValue("@StatusVeiculo", statusVeiculo);
                    command.Parameters.AddWithValue("@VeiculoID", veiculoEncontrado.VeiculoID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar status do veículo: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro inesperado ao atualizar status do veículo: " + ex.Message);
                }
            }
        }

        public Veiculo BuscarVeiculoPlaca(string placa)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            Veiculo veiculo = null;
            connection.Open();

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(Veiculo.SELECTVEICULOBYPLACA, connection);
                    command.Parameters.AddWithValue("@Placa", placa);
                    SqlDataReader reader = command.ExecuteReader();
                    var categoriaController = new CategoriaController();
                    if (reader.Read())
                    {
                        veiculo = new Veiculo(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6)
                             );

                        veiculo.setVeiculoId(reader.GetInt32(0));
                        veiculo.setNomeCategoria(categoriaController.BuscaNomeCategoriaPorID(veiculo.CategoriaID));
                        
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar veiculo por id: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao buscar veiculo por id: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return veiculo ?? throw new Exception("Veiculo não encontrado!");
            }
        
        }

        public void DeletarVeiculo(int idVeiculo)
        {
           SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Veiculo.DELETEVEICULO, connection, transaction);
                    command.Parameters.AddWithValue("VeiculoID", idVeiculo);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch(SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar veículo " + ex.Message);
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar veículo " + ex.Message);
                }
            }
        }

        public List<Veiculo> ListarTodosVeiculos()
        {
            var veiculos = new List<Veiculo>();

            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            CategoriaController categoriaController = new CategoriaController();
            using (SqlCommand command = new SqlCommand(Veiculo.SELECTALLVEICULOS, connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var veiculo = new Veiculo(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6)
                             );

                            veiculo.setVeiculoId(reader.GetInt32(0));
                            veiculo.setNomeCategoria(categoriaController.BuscaNomeCategoriaPorID(veiculo.CategoriaID));
                            veiculos.Add(veiculo);
                        }

                        return veiculos;
                    }


                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar veiculos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao listar veiculos: " + ex.Message);
                }
            }
        }
    }
}
