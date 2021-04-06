using System.Collections.Generic;

namespace DIOSeries.Interface
{
    public interface IRepositorio<T>
    {
         List<T> Lista();
         T RetornaPorId(int id);
         void Inserir(T s);
         void Excluir(int id);
         void Atualizar(int id, T s);
         int ProxId();
    }
}