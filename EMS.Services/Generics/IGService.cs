using System;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EMS.Core.Interfaces;

namespace EMS.Services.Generics
{
    public interface IGService<D, T>
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
        IResponseDTO GetAll(Expression<Func<T, bool>> whereCondition);
        IResponseDTO GetAll(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> select);

        Task<IResponseDTO> GetAllAsync();
        Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> whereCondition);
        Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> select);
        IResponseDTO GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IResponseDTO> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        IResponseDTO GetFirstOrDefault();
        IResponseDTO GetFirstOrDefault(Expression<Func<T, bool>> whereCondition);

        Task<IResponseDTO> GetFirstOrDefaultAsync();
        Task<IResponseDTO> GetFirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition);

        IResponseDTO GetFirst();
        IResponseDTO GetFirst(Expression<Func<T, bool>> whereCondition);

        IResponseDTO GetLastOrDefault(Expression<Func<T, bool>> whereCondition);
        IResponseDTO GetLastOrDefault();

        Task<IResponseDTO> GetLastOrDefaultAsync(Expression<Func<T, bool>> whereCondition);
        Task<IResponseDTO> GetLastOrDefaultAsync();

        IResponseDTO GetLast(Expression<Func<T, bool>> whereCondition);
        IResponseDTO GetLast();

        #endregion
        Task<IResponseDTO> GetFirstAsync();
        Task<IResponseDTO> GetFirstAsync(Expression<Func<T, bool>> whereCondition);

        Task<IResponseDTO> GetLastAsync();
        Task<IResponseDTO> GetLastAsync(Expression<Func<T, bool>> whereCondition);


        #region Find Methods
        IResponseDTO Find(params object[] keys);
        IResponseDTO Find(Func<T, bool> whereCondition);
        Task<IResponseDTO> FindAsync(params object[] keys);
        Task<IResponseDTO> FindAsync(Expression<Func<T, bool>> match);
        #endregion


    }
}
