using EstudoDeCaso.Infra.Context;
using EstudoDeCaso.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace EstudoDeCaso.Infra.Repositories.Base
{
    public abstract class BaseRepository<T>(EstudoDeCasoContext context) : IRepository<T> where T : class
    {
        protected readonly EstudoDeCasoContext _context = context;

        private bool _disposed = false;
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);


        public async Task<T> Alterar(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public async Task<T> Excluir(T entity)
        {
            _context.Remove(entity);

            return await Task.FromResult(entity);
        }

        public async Task<T> Incluir(T entity)
        {
            var entityCreated = await _context.AddAsync(entity);
            if (entityCreated != null) return await Task.FromResult(entity);

            return null;
        }

        public async Task<IEnumerable<T>> Listar() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> ListarPorId(int id) => await _context.Set<T>().FindAsync(id);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ((_disposed))
            {
                return;
            }

            if (disposing)
            {
                _safeHandle.Dispose();
            }
            _disposed = true;
        }
    }
}
