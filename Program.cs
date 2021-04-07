using System;
using System.Collections.Generic;
using System.Threading;
using DIOSeries.Classes;

namespace DIOSeries
{
    class Program
    {
        static Repositorio repo = new Repositorio();
        static bool listarSeries = false;
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

        static Dictionary<string, Action> opt = new Dictionary<string, Action>
        {
            {"1", ListarSerie},
            {"2", InserirSerie},
            {"3", AtualizarSerie},
            {"4", ExcluirSerie},
            {"5", VisualizarSerie},
            {"C", Limpar},
            {"X", Sair}
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
            Limpar();
            Console.Write("Título: ");
            var titulo = Console.ReadLine();

            Console.Write("Ano: ");
            var ano = int.Parse(Console.ReadLine());

            Console.WriteLine("Gênero");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }
            Console.Write("> ");
            var genero = int.Parse(Console.ReadLine());

            Console.Write("Descrição: ");
            var descricao = Console.ReadLine();

            var id = repo.ProxId();
            var novaSerie = new Serie(id, (Genero)genero, titulo, descricao, ano);

            repo.Inserir(novaSerie);
            Console.WriteLine("Série cadastrada com sucesso.");
            Thread.Sleep(2000);
            Limpar();
        }

        static void AtualizarSerie()
        {
            Limpar();
            if (!listarSeries)
                return;

            ListarSerie();
            Console.WriteLine("Qual série que deseja atualizar?");
            Console.Write("> ");
            var id = Convert.ToInt32(Console.ReadLine());
            var serie = repo.RetornaPorId(id);

            Console.WriteLine($"Id: {serie.Id}");
            Console.WriteLine($"Título Antigo: {serie.Titulo}");
            Console.Write($"Título Novo: ");
            var titulo = Console.ReadLine();

            Console.WriteLine($"\nGenero antigo: {serie.Genero}");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("ID do genero novo: ");
            var genero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nAno antigo: {serie.Ano}");
            Console.Write($"Ano novo: ");
            var ano = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"\nDescrição antiga: {serie.Descricao}");
            Console.Write("Descrição nova: ");
            var descricao = Console.ReadLine();

            var serieAtualiza = new Serie(id, (Genero)genero, titulo, descricao, ano);

            repo.Atualizar(id, serieAtualiza);

            Console.WriteLine("Série atualizada com sucesso!");
            Thread.Sleep(2000);
        }

        static void VisualizarSerie()
        {
            Limpar();
            if (!listarSeries)
                return;

            ListarSerie();
            Console.WriteLine("Qual série deseja visualizar?");
            Console.Write("> ");
            var id = Convert.ToInt32(Console.ReadLine());
            var serie = repo.RetornaPorId(id);
            Limpar();
            Console.WriteLine(serie);
        }

        static void ListarSerie()
        {
            Limpar();

            var a = repo.Lista();
            if (a.Count <= 0)
            {
                Console.WriteLine($"Não há series para serem listadas!{Environment.NewLine}");
                listarSeries = false;
                return;
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
            {
                Console.WriteLine($"Não há series para serem listadas!{Environment.NewLine}");
                listarSeries = false;
                return;
            }

            listarSeries = true;
        }
        static void ExcluirSerie()
        {
            Limpar();

            if (!listarSeries)
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
            Serie s = new Serie(0, Genero.Acao, "Teste", "teste123", 2000);
            repo.Inserir(s);
            Limpar();
            var optionUser = Menu();
            while (optionUser.ToUpper() != "X")
            {
                opt[optionUser].Invoke();
                optionUser = Menu();
            }
        }
    }
}