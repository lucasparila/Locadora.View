using Locadora.Controller;
using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.View.Menus
{
    public class CategoriaMenu
    {
        private CategoriaController Controller = new CategoriaController();

        private void InsertService()
        {
            string? name = Validar.ValidarInputString("Categoria: ");
            if (name == null) return;

            decimal daily = Validar.ValidarInputDecimal("Valor da Diária: R$ ");
            if (daily == 0) return;

            string? description = Validar.ValidarInputOpcional("Descrição (opcional): ");

            Categoria category = new Categoria(name, daily, description);

            try
            {
                Controller.AdicionarCategoria(category);
                Console.WriteLine("\n >>>  Categoria inserido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void FindService()
        {
            
            try
            {
                string? name = Validar.ValidarInputString("Informe o nome da categoria para busca: ");
                if (name == null) return;
                var category = Controller.BuscaCategoriaPorNome(name);

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\n=-=-=   > Categoria <   =-=-=\n");

                Console.WriteLine(category);
                Console.WriteLine("---------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SelectAllService()
        {
            Console.Clear();
            Console.WriteLine();

            try
            {
                var list = Controller.ListarCategorias();
                Console.WriteLine("\n=-=-=   > Categorias de Veículos <   =-=-=\n");
                foreach (var category in list)
                {
                    Console.WriteLine(category);
                    Console.WriteLine("------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateCategoryService()
        {
            string? name = Validar.ValidarInputString("Informe o nome da categoria para busca: ");
            if (name == null) return;

            try
            {
                var vlr = Controller.BuscaCategoriaPorNome(name);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe categoria com esse nome cadastrado!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Categoria  <   =-=-=\n");
                Console.WriteLine(vlr + "\n");

                string? nameNovo = Validar.ValidarInputString("Categoria: ");
                if (name == null) return;

                decimal daily = Validar.ValidarInputDecimal("Valor da Diária: R$ ");
                if (daily == 0) return;

                string? description = Validar.ValidarInputOpcional("Descrição (opcional): ");

                Categoria category = new Categoria(name, daily, description);

                Controller.AtualizarCategoria(category);

                Console.WriteLine("\n >>>  Categoria atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteService()
        {
            string? name = Validar.ValidarInputString("Informe o nome da categoria para busca: ");
            if (name == null) return;

            try
            {
                var vlr = Controller.BuscaCategoriaPorNome(name);
                if (vlr is null)
                {
                    Console.WriteLine("\nNão existe categoria com esse nome cadastrado!");
                    return;
                }

                Console.WriteLine("\n=-=-=   >  Categoria  <   =-=-=\n");
                Console.WriteLine(vlr);

                Console.Write("Tem certeza que deseja deletar a categoria? [S/N]: ");
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

                Controller.DeletarCategoria(name);
                Console.WriteLine("\n >>>  Categoria deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void MenuCategoria()
        {
            int opcao = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
                Console.WriteLine(" |                  >      Categoria      <                 |");
                Console.WriteLine(" |----------------------------------------------------------|");
                Console.WriteLine(" | [ 1 ] Cadastrar Categoria   |   [ 2 ] Exibir Categorias  |");
                Console.WriteLine(" | [ 3 ] Atualizar Categoria   |   [ 4 ] Deletar Categoria  |");
                Console.WriteLine(" | [ 5 ] Buscar Categoria      |   [ 6 ] Voltar             |");
                Console.WriteLine(" |-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=|");
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
                        UpdateCategoryService();
                        break;
                    case 4:
                        DeleteService();
                        break;
                    case 5:
                        FindService();
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
