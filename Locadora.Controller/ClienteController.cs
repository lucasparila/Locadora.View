using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils;

namespace Locadora.Controller
{
    public class ClienteController
    {

        public void AdicionarCliente(Cliente cliente)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.INSERTCLIENTE, connection, transaction);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);

                    // cliente.setClienteID(Convert.ToInt32(command.ExecuteScalar())); versão 1

                    int clienteId = Convert.ToInt32(command.ExecuteScalar());
                    cliente.setClienteID(clienteId);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Cliente> ListarClientes()
        {

            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            using (connection)
            {
                try
                {
                    connection.Open();

                    List<Cliente> clientes = new List<Cliente>();

                    SqlCommand command = new SqlCommand(Cliente.SELECTALLCLIENTES, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var cliente = new Cliente(
                            reader["Nome"].ToString(),
                            reader["Email"].ToString(),
                            reader["Telefone"] != DBNull.Value ? reader["Telefone"].ToString() : null
                        );
                        cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));
                        clientes.Add(cliente);
                    }

                    return clientes;

                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar clientes: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado: " + ex.Message);
                }
          
            }
           

        }


        public Cliente BuscaClientePorEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.SELECTCLIENTEPOREMAIL, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var cliente = new Cliente(
                           reader["Nome"].ToString(),
                           reader["Email"].ToString(),
                           reader["Telefone"] != DBNull.Value ? reader["Telefone"].ToString() : null
                       );
                        cliente.setClienteID(Convert.ToInt32(reader["ClienteID"]));
                        return cliente;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao buscar cliente por email: " + ex.Message);
                }

            }

            
        }


        public void AtualizarTelefoneCliente(string telefone, string email)
        {

            var clienteEncontrado = BuscaClientePorEmail(email);
            if(clienteEncontrado is null)
            {
                throw new Exception("Cliente não encontrado");
            }

            clienteEncontrado.setTelefone(telefone);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.UPDATEFONECLIENTE, connection);
                    command.Parameters.AddWithValue("@Telefone", clienteEncontrado.Telefone);
                    command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao atualizar telefone do clientes: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao atualizar telefone do cliente: " + ex.Message);
                }

            }
           

        }

        public void DeletarCliente(string email)
        {
            var clienteEncontrado = BuscaClientePorEmail(email);
            if (clienteEncontrado is null)
            {
                throw new Exception("Cliente não encontrado");
            }

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection, transaction);
                    command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao deletar o cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao deletar o cliente: " + ex.Message);
                }

            }
        }
    }
}
