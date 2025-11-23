using Locadora.Controller;
using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.View.Menus
{
    public class FuncionarioMenu
    {

        private FuncionarioController Controller = new FuncionarioController();

        private void InsertService()
        {
            string? name = Validar.ValidarInputString("Nome: ");
            if (name == null) return;

            string? cpf = Validar.ValidarInputString("CPF: ");
            if (cpf == null) return;
            string? senha = Validar.ValidarInputString("senha: ");
            if (senha == null) return;
            string? email = Validar.ValidarInputString("email: ");
            if (email == null) return;

            decimal? salario = Validar.ValidarInputDecimalOpcional("Salario (opcional): ");

            Funcionario funcionario = new Funcionario(senha, name, cpf, email, salario);

            try
            {
                Controller.AdicionarFuncionario(funcionario);
                Console.WriteLine("\n   >>>   Funcionario inserido com sucesso!");
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

            try
            {
                var list = Controller.ListarFuncionarios();

                foreach (var funcionario in list)
                {
                    Console.WriteLine(funcionario);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdatePasswordService()
        {
            string? email = Validar.ValidarInputString("Informe o email para busca do funcionario: ");
            if (email == null) return;

            try
            {
                var vlr = Controller.BuscarFuncionarioPorEmail(email);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe funcionario cadastrado com esse email!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Funcionario  <   =-=-=\n");
                Console.WriteLine(vlr + "\n");

                string? senha = Validar.ValidarInputString("Informe o telefone atualizado: ");
                if (senha == null) return;

                Controller.AtualizarSenhaFuncionario(senha, vlr);

                Console.WriteLine("\n >>>  Senha atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateSalaryService()
        {
            string? email = Validar.ValidarInputString("Informe o email para busca do cliente: ");
            if (email == null) return;


            try
            {
                var vlr = Controller.BuscarFuncionarioPorEmail(email);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe funcionario cadastrado com esse email!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Funcionario  <   =-=-=\n");
                Console.WriteLine(vlr + "\n");

                decimal salario = Validar.ValidarInputDecimal("Gidite o novo salário do funcionário: ");
                if (salario == null) return;

                Controller.AtualizarSalarioFuncionario(salario, vlr);

                Console.WriteLine("\n >>>  Salario atualizado com sucesso!\n");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void DeleteService()
        {
            string? email = Validar.ValidarInputString("Informe o email para busca do funcionário: ");
            if (email == null) return;

            try
            {
                var vlr = Controller.BuscarFuncionarioPorEmail(email);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe funcionario cadastrado com esse email!");
                    return;
                }

                Console.WriteLine("\n=-=-=-=   >   Funcionário   <   =-=-=-=\n");
                Console.WriteLine(vlr + "\n");

                Console.Write("Tem certeza que deseja deletar o funcionário? [S/N]: ");
                string res = Console.ReadLine()!.ToUpper();

                while (res is not "S" && res is not "N")
                {
                    Console.Write("Error! Informe apenas [S/N] pra continuar: ");
                    res = Console.ReadLine()!.ToUpper();
                }

                if (res == "N")
                {
                    Console.WriteLine("\nEncerrando a operação de deletar...");
                    return;
                }

                Controller.DeletarFuncionario(email);
                Console.WriteLine("\n >>>  Funcionário deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void MenuFuncionario()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-|");
                Console.WriteLine(" |                   >      Cliente      <                   |");
                Console.WriteLine(" |-----------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Cadastrar Funcionario|   [ 2 ] Exibir Funcionarios  |");
                Console.WriteLine(" | [ 3 ] Atualizar Senha      |   [ 4 ] Atualizar Salario    |");
                Console.WriteLine(" | [ 5 ] Deletar Funcionario  |   [ 6 ] Voltar               |");
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
                        UpdatePasswordService();
                        break;
                    case 4:
                        UpdateSalaryService();
                        break;
                    case 5:
                        DeleteService();
                        break;
                    case 6:
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
