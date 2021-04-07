using System;
using System.Collections.Generic;
using System.Threading;
using DIOSeries.Classes;

namespace DIOSeries
{
    class Program
    {
        static Repositorio repo = new Repositorio();
        static Dictionary<string, string> dic = new Dictionary<string, string>
        {
            {"1", "Listar Séries"},
            {"2", "Inserir uma nova Serie"},
            {"3", "Atualizar Série"},
            {"4", "Excluir Série"},
            {"5", "Visualizar Série"},
            {"C", "Limpar Tela"},
            {"X", "Sair"}
        };

        static Dictionary<string, Delegate> opt = new Dictionary<string, Delegate>
        {
            {"1", new Func<string, bool>(ListarSerie)}, //continuar
            {"2", new Action(InserirSerie)},
            {"3", new Action(AtualizarSerie)},
            {"4", new Action(ExcluirSerie)},
            {"5", new Action(VisualizarSerie)},
            {"C", new Action(Limpar)},
            {"X", new Action(Sair)}
        };

        static string Menu()
        {
            Console.WriteLine("DIO Séries a seu dispor");
            Console.WriteLine("Informe a opção desejada:");
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.Write("> ");
            return Console.ReadLine().ToUpper();
        }

        static void InserirSerie()
        {
            //TODO
        }

        static void AtualizarSerie()
        {
            Limpar();
            if (!ListarSerie())
                return;

            Console.WriteLine("Qual série que deseja atualizar?");
            var id = Convert.ToInt32(Console.ReadLine());
            var serie = repo.RetornaPorId(id);

            Console.WriteLine($"Id: {serie.Id}");
            Console.WriteLine($"Título Antigo: {serie.Titulo}");
            Console.Write($"Título Novo: ");
            var titulo = Console.ReadLine();

            Console.WriteLine($"Genero antigo: {serie.Genero}");
            foreach (var item in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine($"{item} - {Enum.GetName(typeof(Genero), item)}");
            }

            Console.Write("ID do genero novo: ");
            var genero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Ano antigo: {serie.Ano}");
            Console.Write($"Ano novo: ");
            var ano = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Descrição antiga: {serie.Descricao}");
            Console.Write("Descrição nova: ");
            var descricao = Console.ReadLine();

            var serieAtualiza = new Serie((Genero)genero, titulo, descricao, ano);

            repo.Atualizar(id, serieAtualiza);

            Console.WriteLine("Série atualizada com sucesso!");
            Console.ReadKey();
        }

        static void VisualizarSerie()
        {
            Limpar();
            if (!ListarSerie())
                return;

            Console.WriteLine("Qual série deseja visualizar?");
            Console.Write("> ");
            var id = Convert.ToInt32(Console.ReadLine());
            var serie = repo.RetornaPorId(id);
            Console.WriteLine(serie);
        }

        static bool ListarSerie()
        {
            Limpar();

            var a = repo.Lista();
            if (a.Count <= 0)
            {
                Console.WriteLine($"Não há series para serem listadas!{Environment.NewLine}");
                return false;
            }

            int aux = 0;
            foreach (var item in a)
            {
                if (!item.Excluido)
                {
                    Console.WriteLine(item);
                    aux++;
                }
            }

            if (aux <= 0)
                return false;

            return true;
        }
        static void ExcluirSerie()
        {
            Limpar();

            if (!ListarSerie())
                return;

            ListarSerie();
            Console.WriteLine("> Qual série deseja excluir?");
            int id = int.Parse(Console.ReadLine());
            repo.Excluir(id);
            Console.WriteLine("> Excluído com sucesso!");
            Thread.Sleep(2000);
        }
        static void Limpar() => Console.Clear();
        static void Sair() => Environment.Exit(-1);
        static void Main(string[] args)
        {
            Limpar();
            var optionUser = Menu();
            while (optionUser.ToUpper() != "X")
            {
                opt[optionUser].DynamicInvoke();
                optionUser = Menu();
            }
        }
    }
}
