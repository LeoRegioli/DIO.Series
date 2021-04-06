using System.Collections.Generic;
using DIOSeries.Interface;

namespace DIOSeries.Classes
{
    public class Repositorio : IRepositorio<Serie>
    {
        private List<Serie> list = new List<Serie>();

        public void Atualizar(int id, Serie s) => list[id] = s;
        public void Excluir(int id) => list[id].Excluir();
        public void Inserir(Serie s) => list.Add(s);
        public List<Serie> Lista() => list;
        public int ProxId() => list.Count;
        public Serie RetornaPorId(int id) => list[id];
    }
}