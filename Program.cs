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
            //TODO
        }

        static void AtualizarSerie()
        {
            //TODO
        }

        static void VisualizarSerie()
        {
            Limpar();
            ListarSerie();
            Console.WriteLine("> Qual série deseja visualizar?");
            //terminar
        }

        static void ListarSerie()
        {
            var a = repo.Lista();
            foreach (var item in a)
            {
                if (!item.Excluido)
                    Console.WriteLine(item);
            }
        }
        static void ExcluirSerie()
        {
            Limpar();
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
            var optionUser = Menu();
            while (optionUser.ToUpper() != "X")
            {
                opt[optionUser].Invoke();
                optionUser = Menu();
            }
        }
    }
}
