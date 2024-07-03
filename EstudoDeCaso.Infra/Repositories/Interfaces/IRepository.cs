using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoDeCaso.Infra.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Incluir(T entity);
        Task<T> Alterar(T entity);
        Task<T> Excluir(T entity);
        Task<IEnumerable<T>> Listar();
        Task<T> ListarPorId(int id);
        Task SaveAsync();
    }
}
