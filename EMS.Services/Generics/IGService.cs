using System;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EMS.Core.Interfaces;

namespace EMS.Services.Generics
{
    public interface IGService<D, T, R>
    {
        #region Add Method
        IResponseDTO Add(D dtoModel);
        Task<IResponseDTO> AddAsync(D dtoObject);
        #endregion

        #region Delete Method
        IResponseDTO Remove(D entity);
        #endregion

        #region Update Method
        IResponseDTO Update(D entity);

        #endregion

        #region Get Methods
        IResponseDTO GetAll();
        Task<IResponseDTO> GetAllAsync();
        IResponseDTO GetAll(Expression<Func<T, bool>> where);
        Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> where);
        IResponseDTO GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
        Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
        IResponseDTO GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IResponseDTO> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        IResponseDTO GetFirstOrDefault();
        Task<IResponseDTO> GetFirstOrDefaultAsync();
        IResponseDTO GetFirst();
        Task<IResponseDTO> GetFirstAsync();
        IResponseDTO GetLastOrDefault();
        Task<IResponseDTO> GetLastOrDefaultAsync();
        IResponseDTO GetLast();
        Task<IResponseDTO> GetLastAsync();
        IResponseDTO GetFirstOrDefault(Expression<Func<T, bool>> where);
        Task<IResponseDTO> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where);
        IResponseDTO GetFirst(Expression<Func<T, bool>> where);
        Task<IResponseDTO> GetFirstAsync(Expression<Func<T, bool>> where);
        IResponseDTO GetLastOrDefault(Expression<Func<T, bool>> where);
        Task<IResponseDTO> GetLastOrDefaultAsync(Expression<Func<T, bool>> where);
        IResponseDTO GetLast(Expression<Func<T, bool>> where);
        Task<IResponseDTO> GetLastAsync(Expression<Func<T, bool>> where);
        #endregion

        #region Find Methods
        IResponseDTO Find(params object[] keys);
        Task<IResponseDTO> FindAsync(params object[] keys);
        IResponseDTO Find(Func<T, bool> where);
        Task<IResponseDTO> FindAsync(Expression<Func<T, bool>> match);
        #endregion


    }
}
