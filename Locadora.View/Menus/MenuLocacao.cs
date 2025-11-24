using Locadora.Controller;
using Locadora.Models;
using Locadora.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.View.Menus
{
    public class MenuLocacao
    {

        private LocacaoController controllerLocacao = new LocacaoController();
        private VeiculoController controllerVeiculo = new VeiculoController();
        private ClienteController clienteController = new ClienteController();

        

        private void InsertServiceFuncionario(Funcionario funcionario)
        {
            Cliente cliente = null;
            Veiculo veiculo = null;
            FuncionarioController controllerFuncionario = new FuncionarioController();
            int diarias;

            string? email = Validar.ValidarInputString("Digite o email do cliente: ");
            if (email == null) return;

            try
            {
                cliente = clienteController.BuscaClientePorEmail(email);
                if (cliente is null)
                {
                    Console.WriteLine("\nNão existe cliente com esse email cadastrado!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Cliente  <   =-=-=\n");
                Console.WriteLine(cliente + "\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            string? placa = Validar.ValidarInputString("Digite a placa do veículo: ");
            if (placa == null) return;

            try
            {
                veiculo = controllerVeiculo.BuscarVeiculoPlaca(placa);
                if (veiculo is null)
                {
                    Console.WriteLine("\nNão existe veículo cadastrado com essa placa!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Veículo  <   =-=-=\n");
                Console.WriteLine(veiculo + "\n");
                if (veiculo.StatusVeiculo != EstatusVeiculo.Disponível.ToString())
                {
                    Console.WriteLine($"Status do veículo: {veiculo.StatusVeiculo}. Não disponível para locação. Por favor, escolha um veículo disponível.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            diarias = Validar.ValidarInputInt("Diarias: ");
            if (diarias == 0) return;


            List<Funcionario> funcionarios = new List<Funcionario>();
            funcionarios.Add(funcionario);
            Console.Write("Adicionar outro funcionário envolvido nesta locação? [S/N]: ");
            string op = Console.ReadLine()!.ToUpper();

            while (op is not "S" && op is not "N")
            {
                Console.Write("Error! Informe apenas [S/N] pra continuar: ");
                op = Console.ReadLine()!.ToUpper();
            }
            
            while(op == "S")
            {
                
               
                    string? emailFuncionario = Validar.ValidarInputString("Digite o email do funcionário: ");
                    if (!string.IsNullOrWhiteSpace(emailFuncionario))
                    {
                        try
                        {
                            Funcionario funcionarioEncontrado = controllerFuncionario.BuscarFuncionarioPorEmail(emailFuncionario);
                            funcionarios.Add(funcionarioEncontrado);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    Console.Write("Adicionar outro funcionário envolvido nesta locação? [S/N]: ");
                    op = Console.ReadLine()!.ToUpper();

                    while (op is not "S" && op is not "N")
                    {
                        Console.Write("Error! Informe apenas [S/N] pra continuar: ");
                        op = Console.ReadLine()!.ToUpper();
                    }   
            }

            try
            {
                var locacao = new Locacao(cliente, veiculo, veiculo.categoria.Diaria, diarias);
                Guid idLocacao = controllerLocacao.AdicionarLocacao(locacao);
                locacao.setId(idLocacao);

                controllerVeiculo.AtualizarStatusVeiculo(EstatusVeiculo.Alugado.ToString(), veiculo.Placa);

                var locacaoFuncionario = new LocacaoFuncionario(locacao);
                var controllerLocacaoFunconario = new LocacaoFuncionarioController();

                foreach(var f in funcionarios)
                {
                    locacaoFuncionario.addFuncionario(f);
                    controllerLocacaoFunconario.AdicionarLocacaoFuncionario(locacaoFuncionario, f);

                }
                
                Console.WriteLine("\n >>>  Locação realizada com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void SelectAllService()
        {
            Console.Clear();
            Console.WriteLine();
            LocacaoFuncionarioController controllerLocacaoFuncionario = new LocacaoFuncionarioController ();
            try
            {
                var list = controllerLocacaoFuncionario.ListarLocaoesFuncionarios();
                Console.WriteLine("\n=-=-=   >  Locações  <   =-=-=\n");
                foreach (var locacao in list)
                {
                    Console.WriteLine(locacao);
                    Console.WriteLine("------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SelectAllServiceFuncionario(Funcionario funcionario)
        {
            Console.Clear();
            Console.WriteLine();
            LocacaoFuncionarioController controllerLocacaoFuncionario = new LocacaoFuncionarioController();
            try
            {
                var list = controllerLocacaoFuncionario.ListarLocaoesFuncionarios();
                Console.WriteLine("\n=-=-=   >  Locações  <   =-=-=\n");
                foreach (var locacao in list)
                {
                    if (locacao.funcionarios.Any(f => f.FuncionarioID == funcionario.FuncionarioID))
                    {
                        Console.WriteLine(locacao);
                        Console.WriteLine("---------------------------");

                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FindService()
        {
            Console.Clear();
            Console.WriteLine();
            LocacaoFuncionarioController controllerLocacaoFuncionario = new LocacaoFuncionarioController();
            try
            {
                Guid id = Validar.ValidarInputGuid("Informe o id para busca da locação: ");
                if (id == Guid.Empty) return;
                var funcionarioLocacao = controllerLocacaoFuncionario.BuscaLocacaoFuncionarioPorId(id);

                    Console.WriteLine("\n=-=-=   >  Locação  <   =-=-=\n");  
                    Console.WriteLine(funcionarioLocacao);
                    Console.WriteLine("\n");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateCancelLocacaoService()
        {
            Guid id = Validar.ValidarInputGuid("Informe o id para busca da locação: ");
            if (id == Guid.Empty) return;


            try
            {
                var locacao = controllerLocacao.BuscarLocacaoPorId(id);
                if (locacao is null)
                {
                    Console.WriteLine("\nNão existe locação cadastrada com esse id!");
                    return;
                }

                if (locacao.Status != "Ativa")
                {
                    Console.WriteLine($"\nStatus da locacao: {locacao.Status}");
                    Console.WriteLine("\nSó é possível cancelar uma localização ativa. Operação cancelada!");
                    return;
                }

                if(locacao.DataLocacao.Date < DateTime.Now.Date)
                {
                    Console.WriteLine("\nNão é possível cancelar: essa locação já está ativa a mais de um dia. Por favor, finalize a locação.");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Locação  <   =-=-=\n");
                Console.WriteLine(locacao + "\n");

                locacao.setStatus(EStatusLocacao.Cancelada.ToString());
                locacao.setDataDevolucaoReal(null);
                locacao.setValorTotal(0m);
                controllerLocacao.AtualizarDataDevolucaoRealLocacao(locacao, null);
                controllerLocacao.AtualizarStatusLocacao(locacao, EStatusLocacao.Cancelada.ToString());
                controllerLocacao.AtualizarValorTotalLocacao(locacao);
                var controllerVeiculo = new VeiculoController();
                controllerVeiculo.AtualizarStatusVeiculo(EstatusVeiculo.Disponível.ToString(), locacao.veiculo.Placa);
                Console.WriteLine($"Valor total da locação: R$ 0");

                Console.WriteLine("\n >>>  Locacao cancelada com sucesso!\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateLocacaoService()
        {
            Guid id = Validar.ValidarInputGuid("Informe o id para busca da locação: ");
            if (id == Guid.Empty) return;


            try
            {
                 var locacao = controllerLocacao.BuscarLocacaoPorId(id);
                if (locacao is null)
                {
                    Console.WriteLine("\nNão existe locação cadastrada com esse id!");
                    return;
                }

                if(locacao.Status != "Ativa")
                {
                    Console.WriteLine($"\nStatus da locação: {locacao.Status}");
                    Console.WriteLine("\nSó é possível finalizar uma locação ativa. Operação cancelada!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Locação  <   =-=-=\n");
                Console.WriteLine(locacao + "\n");

                locacao.setStatus(EStatusLocacao.Concluida.ToString());
                locacao.setDataDevolucaoReal(DateTime.Now);
                decimal valorTotal = locacao.CalcularValorFinal();
                controllerLocacao.AtualizarDataDevolucaoRealLocacao(locacao, DateTime.Now);
                controllerLocacao.AtualizarStatusLocacao(locacao, EStatusLocacao.Concluida.ToString());
                controllerLocacao.AtualizarValorTotalLocacao(locacao);
                var controllerVeiculo = new VeiculoController();
                controllerVeiculo.AtualizarStatusVeiculo(EstatusVeiculo.Disponível.ToString(), locacao.veiculo.Placa);
                Console.WriteLine($"Valor total da locação: R$ {valorTotal}");
                
                Console.WriteLine("\n >>>  Locacao finalizada com sucesso!\n");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LocacaoMenuAdministrador()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine(" |                   >      Locação      <                   |");
                Console.WriteLine(" |-----------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Exibir Locações      |   [ 2 ] Finalizar Locação    |");
                Console.WriteLine(" | [ 3 ] Cancelar Locação     |   [ 4 ] Buscar Locação       |");
                Console.WriteLine(" | [ 5 ] Voltar               |                              |");
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine();
                Console.Write("  >>> Informe o menu desejado: ");
                string entrada = Console.ReadLine()!;
                bool conversao = int.TryParse(entrada, out opcao);
                Console.WriteLine("---------------------------------------");

                switch (opcao)
                {
                    case 1:
                        SelectAllService();
                        break;
                    case 2:
                        UpdateLocacaoService();
                        break;
                    case 3:
                        UpdateCancelLocacaoService();
                        break;
                    case 4:
                        FindService();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("\nOpção Inválida. Tente novamente.");
                        break;
                }

                Console.Write("\n  >  Pressione qualquer Tecla para prosseguir ");
                Console.ReadLine();

            } while (true);
        }


            public void LocacaoMenuFuncionario(Funcionario funcionario)
             {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine(" |                   >      Locaçao      <                   |");
                Console.WriteLine(" |-----------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Realizar Locação     |   [ 2 ] Minhas Locações      |");
                Console.WriteLine(" | [ 3 ] Finalizar Locação    |   [ 4 ] Cancelar Locação     |");
                Console.WriteLine(" | [ 5 ] Voltar               |                              |");
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine();
                Console.Write("  >>> Informe o menu desejado: ");
                string entrada = Console.ReadLine()!;
                bool conversao = int.TryParse(entrada, out opcao);
                Console.WriteLine("---------------------------------------");

                switch (opcao)
                {
                    case 1:
                        InsertServiceFuncionario( funcionario);
                        break;
                    case 2:
                        SelectAllServiceFuncionario(funcionario);
                        break;
                    case 3:
                        UpdateLocacaoService();
                        break;
                    case 4:
                        UpdateCancelLocacaoService();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("\nOpção Inválida. Tente novamente.");
                        break;
                }

                Console.Write("\n  >  Pressione qualquer Tecla para prosseguir ");
                Console.ReadLine();

            } while (true);

        }
    }
}
