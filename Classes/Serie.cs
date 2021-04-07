using DIOSeries;

namespace DIOSeries.Classes
{
    public class Serie : EntidadeBase
    {
        public Genero Genero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Ano { get; set; }
        public bool Excluido { get; private set; }

        public Serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            Id = id;
            Genero = genero;
            Titulo = titulo;
            Descricao = descricao;
            Ano = ano;
            Excluido = false;
        }

        public void Excluir() => this.Excluido = true;

        public override string ToString() => $"#{Id} | Titulo: {Titulo} | Ano: {Ano} | Genero: {Genero} | Descrição: {Descricao}";
    }
}