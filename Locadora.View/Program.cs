
using Locadora.Controller;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils;

//Cliente cliente = new Cliente("Judas", "ju@gmail.com");
//Documento documento = new Documento( "CPF", "178158325", new DateOnly(2024, 1, 1), new DateOnly(2025, 5, 9));


//var clienteController = new ClienteController();
//try
//{
//    clienteController.AdicionarCliente(cliente, documento);

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    clienteController.DeletarCliente("ka@gmail.com");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}


//try
//{
//    clienteController.AtualizarDocumentoCliente("lu@gmail.com", documento);
//    Console.WriteLine(clienteController.BuscaClientePorEmail("lu@gmail.com"));

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
//Console.WriteLine(ex.Message);
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


#region AdicionarCategoria
//Categoria categoria = new Categoria("Picap", 200, "Veiculo compacto com carroceria espaçosa");
var categoriaController = new CategoriaController();
//try
//{
//    categoriaController.AdicionarCategoria(categoria);
//    Console.WriteLine("Categoria adicionada com sucesso");
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region ListarCategorias

//try
//{    List<Categoria> categorias = categoriaController.ListarCategorias();


//    foreach (Categoria cat in categorias)
//   {
//       Console.WriteLine(cat);
//        Console.WriteLine("-------");
//    }
//}
//catch(Exception ex)
//{
//Console.WriteLine(ex.Message);
//}
#endregion
#region BuscarCategoria

//try
//{
//    var categoria = categoriaController.BuscaCategoriaPorNome("SUV");
//    Console.WriteLine(categoria);
//}catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region DeletarCategoria

//try
//{
//    categoriaController.DeletarCategoria("Picap");
//    Console.WriteLine("Categoria removida com sucesso");

//}catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion


#region AtualizarCategoria

try
{
    Categoria categoria = new Categoria("Picape", 150, "Veiculo compacto com carroceria espaçosa");
    categoriaController.AtualizarCategoria("Picape", categoria);
    Console.WriteLine("Categoria atualizada com sucesso");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

#endregion