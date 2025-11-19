
using Locadora.Controller;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils;

//Cliente cliente = new Cliente("Joana", "jo@gmail.com");
////Documento documento = new Documento(1, "RG", "12356789", new DateOnly(2020, 1, 1), new DateOnly(2025, 1, 1));

var clienteController = new ClienteController();
//try
//{
//    clienteController.AdicionarCliente(cliente);

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

 
//try
//{
//    List<Cliente> clientes = clienteController.ListarClientes();


//    foreach (Cliente client in clientes)
//    {
//        Console.WriteLine(client);
//    }
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}



//try
//{
//    clienteController.AtualizarTelefoneCliente("999999999", "l@gmail.com");
//    Console.WriteLine(clienteController.BuscaClientePorEmail("l@gmail.com"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

try
{
    clienteController.BuscaClientePorEmail("l@gmail.com");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}


