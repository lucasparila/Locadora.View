
using Locadora.Controller;
using Locadora.Models;
using Locadora.Models.Enums;
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
//Categoria categoria = new Categoria("Picape", 200.00m, "Veiculo compacto com carroceria espaçosa");
//var categoriaController = new CategoriaController();
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

//try
//{
//    Categoria categoria = new Categoria("Picape", 150, "Veiculo compacto com carroceria espaçosa");
//    categoriaController.AtualizarCategoria("Picape", categoria);
//    Console.WriteLine("Categoria atualizada com sucesso");
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion


#region AdicionarVeiculo

var veiculoController = new VeiculoController();
//var veiculo = new Veiculo(2002, "XYZ-9876", "Chevrolet", "S10", 2025, EstatusVeiculo.Disponivel.ToString());

//try
//{
//   veiculoController.AdicionarVeiculo(veiculo);
//    Console.WriteLine("Veiculo adicionado com sucesso");
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region ListarVeiculos

//List<Veiculo> veiculo = new List<Veiculo>();
//try
//{
//    veiculo = veiculoController.ListarTodosVeiculos();

//    foreach(var veiculoItem in veiculo)
//    {
//        Console.WriteLine(veiculoItem);
//        Console.WriteLine();
//    }

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region BuscarVeiculoPorId

//try
//{
//    Console.WriteLine(veiculoController.BuscarVeiculoPlaca("XYZ-9876"));

//}catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region DeletarVeiculo

//try
//{

//    var veiculo = veiculoController.BuscarVeiculoPlaca("XYZ-9876");
//    veiculoController.DeletarVeiculo(veiculo.VeiculoID);
//    Console.WriteLine("Veículo excluído com sucesso");

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region AtualizarVeiculo

//try
//{

//    veiculoController.AtualizarStatusVeiculo(EstatusVeiculo.Manutencao.ToString(), "MNO7890");
//    Console.WriteLine("Veículo atualizado com sucesso");

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

var controllerFuncionar = new FuncionarioController();
#region InserirFuncionario

//try
//{
//    var funcionario1 = new Funcionario("123senha", "Lucas", "424293823809", "long@gamil.com", 7.500m);
//    var funcionario2 = new Funcionario("123senha", "Joao", "4444293923509", "j@gamil.com");
//    //controllerFuncionar.AdicionarFuncionario(funcionario1);
//    controllerFuncionar.AdicionarFuncionario(funcionario2);

//    Console.WriteLine("Funcionario adicionado com sucesso");

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region ListarFuncionarios

//try
//{
//    List<Funcionario> funcionarios = controllerFuncionar.ListarFuncionarios();

//    foreach (var funcionario in funcionarios)
//    {
//        Console.WriteLine(funcionario);
//        Console.WriteLine("---------");
//    }

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region BuscarFuncionario

//try
//{
//    var funcionario = controllerFuncionar.BuscarFuncionarioPorEmail("ana.costa@locadora.com");

//    Console.WriteLine(funcionario);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region AtualizarSalarioFuncionario

//try
//{
//    controllerFuncionar.AtualizarSalarioFuncionario("ana.costa@locadora.com", 7500m);

//    Console.WriteLine("Salario atualizado com sucesso"); 
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region AtualizarSenhaFuncionario

//try
//{
//    var funcionario = controllerFuncionar.BuscarFuncionarioPorEmail("an.costa@locadora.com");
//    if(funcionario != null)
//    {
//        controllerFuncionar.AtualizarSenhaFuncionario("ana123", funcionario);
//        Console.WriteLine("Senha atualizada com sucesso");
//    }
//    else
//    {

//        //Console.WriteLine("funcionario não encontrado");

//    }


//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion


#region InserirFuncionario

//try
//{
//    controllerFuncionar.DeletarFuncionario("j@gamil.com");
//    Console.WriteLine("Funcionario deletado com sucesso");

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region InserirLocacao
//var clienteController = new ClienteController();
//var locacaoController = new LocacaoController();

//try
//{
//    var cliente = clienteController.BuscaClientePorId(1);
//    if( cliente == null)
//    {
//        Console.WriteLine("Cliente não encontrado");
//        return;
//    }
//    var veiculo = veiculoController.BuscarVeiculoId(2);
//    if( veiculo == null)
//    {
//        Console.WriteLine("Veiculo não encontrado");
//        return;
//    }

//    var locacao = new Locacao(cliente, veiculo, veiculo.categoria.Diaria, 5);

//    locacaoController.AdicionarLocacao(locacao);

//    Console.WriteLine("Locacao criada com sucesso");
//    Console.WriteLine(locacao);

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

#endregion

#region InserirLocacao

var locacaoController = new LocacaoController();
List<Locacao> locacoes = new List<Locacao>();
try
{
    var idParaBuscar = Guid.Parse("E1C7423E-F36B-1410-820C-00C896CF73F1");
   locacaoController.AtualizarStatusLocacao(idParaBuscar, "Concluida");
    
       
 

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

#endregion
