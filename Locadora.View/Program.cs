
using Locadora.Controller;
using Locadora.Models;
using Locadora.Models.Enums;
using Locadora.View;
using Locadora.View.Menus;
using Microsoft.Data.SqlClient;
using Utils;

 void login()
{
    FuncionarioController funcionarioController = new FuncionarioController();
    Console.Clear();
    Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
    Console.WriteLine(" |                  >     Console de Login     <            |");
    Console.WriteLine(" |----------------------------------------------------------|");
    string? login = Validar.ValidarInputString("informe email (Funcionário) || Username (Administrador): ");
    if (login == null) return;

    if( login == "admin")
    {
        
        string? senha = Validar.ValidarInputString("Digite a senha de administrador: ");
        if (senha == null) return;
        if(senha == "admin")
        {
            Console.Clear();
            Console.WriteLine("Login realizado com sucesso!");
            Console.WriteLine("Aperte enter para continuar");
            Console.ReadKey();
            menuGeralAdministrador();
        }
        else
        {
            Console.WriteLine("Senha inválida. Operação Cancelada");
            Console.ReadKey();
            return;
        }

    }
    else
    {
        try
        {
            var funcionario = funcionarioController.BuscarFuncionarioPorEmail(login);
            string? senha = Validar.ValidarInputString("Digite a senha de funcionário: ");
            if (senha == null) return;
            if (senha == funcionario.Senha)
            {
                Console.Clear();
                Console.WriteLine("Login realizado com sucesso!");
                Console.WriteLine("Aperte enter para continuar");
                Console.ReadKey();
                menuGeralFuncionario(funcionario);

            }
            else
            {
                Console.WriteLine("Senha inválida. Operação Cancelada");
                Console.ReadKey();
                return;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

    }

}

void menuGeralAdministrador()
{
    string opcao = "0";

    do
    {

        Console.Clear();
        Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
        Console.WriteLine(" |                  >      Menu Geral      <                |");
        Console.WriteLine(" |----------------------------------------------------------|");
        Console.WriteLine(" | [ 1 ] Clientes              |   [ 2 ] Funcionários       |");
        Console.WriteLine(" | [ 3 ] Veiculos              |   [ 4 ] Categorias Veiculos|");
        Console.WriteLine(" | [ 5 ] Locações              |   [ 6 ] Sair               |");
        Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
        Console.WriteLine();
        Console.Write("  >>> Informe a opção desejada: ");
        opcao = Console.ReadLine()!;

        switch (opcao)
        {
            case "1":
                ClienteMenu clienteMenu = new ClienteMenu();
                clienteMenu.MenuCliente();
                break;
            case "2":
                FuncionarioMenu funcionarioMenu = new FuncionarioMenu();
                funcionarioMenu.MenuFuncionarioAdministrador();
                break;
            case "3":
                VeiculoMenu veiculoMenu = new VeiculoMenu();
                veiculoMenu.MenuVeiculo();
                break;
            case "4":
                CategoriaMenu categoriaMenu = new CategoriaMenu();
                categoriaMenu.MenuCategoria();
                break;
            case "5":
                MenuLocacao locacaoMenu = new MenuLocacao();
                locacaoMenu.LocacaoMenuAdministrador();
                break;
            case "6":
                Console.WriteLine("saindo...");
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Escolha uma opção válida");
                Console.ReadKey();
                break;

        }

    } while (opcao != "6");
}

void menuGeralFuncionario(Funcionario funcionario)
{
    string opcao = "0";

    do
    {

        Console.Clear();
        Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
        Console.WriteLine(" |                  >      Menu Geral      <                |");
        Console.WriteLine(" |----------------------------------------------------------|");
        Console.WriteLine(" | [ 1 ] Clientes              |   [ 2 ] Funcionários       |");
        Console.WriteLine(" | [ 3 ] Veiculos              |   [ 4 ] Categorias Veiculos|");
        Console.WriteLine(" | [ 5 ] Locações              |   [ 6 ] Sair               |");
        Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
        Console.WriteLine();
        Console.Write("  >>> Informe a opção desejada: ");
        opcao = Console.ReadLine()!;

        switch (opcao)
        {
            case "1":
                ClienteMenu clienteMenu = new ClienteMenu();
                clienteMenu.MenuCliente();
                break;
            case "2":
                FuncionarioMenu funcionarioMenu = new FuncionarioMenu();
                funcionarioMenu.MenuFuncionarioFuncionario();
                break;
            case "3":
                VeiculoMenu veiculoMenu = new VeiculoMenu();
                veiculoMenu.MenuVeiculo();
                break;
            case "4":
                CategoriaMenu categoriaMenu = new CategoriaMenu();
                categoriaMenu.MenuCategoria();
                break;
            case "5":
                MenuLocacao locacaoMenu = new MenuLocacao();
                locacaoMenu.LocacaoMenuFuncionario(funcionario);
                break;
            case "6":
                Console.WriteLine("saindo...");
                Console.ReadKey();
                break;
            default:
                Console.WriteLine("Escolha uma opção válida");
                Console.ReadKey();
                break;

        }

    } while (opcao != "6");
}

string opcao = "0";

do
{

    Console.Clear();
    Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
    Console.WriteLine(" |                  >      Login          <                 |");
    Console.WriteLine(" |----------------------------------------------------------|");
    Console.WriteLine(" | [ 1 ] Login                 |   [ 0 ] sair               |");
    Console.WriteLine(" |                             |                            |");
    Console.WriteLine(" |                             |                            |");
    Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
    Console.WriteLine();
    Console.Write("  >>> Informe a opção desejada: ");
    opcao = Console.ReadLine()!;

    switch(opcao){
        case "1":
            login();
            break;
        case "0":
            Console.WriteLine("Saindo...");
            Console.ReadKey();
            break;
        default:
            Console.WriteLine("Escolha uma opção válida");
            Console.ReadKey();
            break;

    }

} while(opcao != "0");