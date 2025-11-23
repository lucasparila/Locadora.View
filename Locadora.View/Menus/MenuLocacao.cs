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

        
        private void InsertService()
        {
            Cliente cliente = null;
            Veiculo veiculo = null;
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
                if(veiculo.StatusVeiculo != EstatusVeiculo.Disponivel.ToString())
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

            try
            {
                controllerLocacao.AdicionarLocacao(new Locacao (cliente, veiculo, veiculo.categoria.Diaria, diarias));

                controllerVeiculo.AtualizarStatusVeiculo(EstatusVeiculo.Alugado.ToString(), veiculo.Placa);
                Console.WriteLine("\n >>>  Locação realizada com sucesso!");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }
        }


        private void SelectAllService()
        {
            Console.Clear();
            Console.WriteLine();

            try
            {
                var list = controllerLocacao.ListarLocacoes();

                foreach (var locacao in list)
                {
                    Console.WriteLine(locacao);
                }
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
                    Console.WriteLine($"\nStatus da locacao: {locacao.Status}");
                    Console.WriteLine("\nSó é possível finalizar uma localização ativa. Operação cancelada!");
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

                Console.WriteLine($"Valor total da locação: R$ {valorTotal}");
                
                Console.WriteLine("\n >>>  Locacao finalizada com sucesso!\n");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LocacaoMenu()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine(" |                   >      Cliente      <                   |");
                Console.WriteLine(" |-----------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Realizar Locação     |   [ 2 ] Exibir Locações      |");
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
                        InsertService();
                        break;
                    case 2:
                        SelectAllService();
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
