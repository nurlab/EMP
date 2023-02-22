using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EMS.Core.Interfaces;
using EMS.DTO.Common;
using EMS.Repositories.Generics;

namespace EMS.Services.Generics
{
    public class GService<D, T, R> : IGService<D, T>
       where R : IGRepository<T>
       where T : class
    {
        #region Private Fields
        protected readonly R _genericRepository;
        protected readonly IMapper _mapper;
        #endregion


        #region Constructor
        public GService(R genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        #endregion

        #region Add Method
        public virtual IResponseDTO Add(D dtoModel)
        {
            IResponseDTO IResponseDTO = new ResponseDTO();
            try
            {
                if (dtoModel is IAuditable auditable)
                {
                    auditable.CreatedOn = DateTime.Now;
                }
                T entityObj = _mapper.Map<T>(dtoModel);

                IResponseDTO.Data = _mapper.Map<D>(_genericRepository.Add(entityObj)) ?? new object();
                IResponseDTO.IsPassed = true;
                IResponseDTO.Message = "Ok";
            }
            catch (Exception ex)
            {
                IResponseDTO.IsPassed = false;
                IResponseDTO.Message = "Internal Error" + ex.Message;
                return IResponseDTO;
            }
            return IResponseDTO;
        }
        public virtual async Task<IResponseDTO> AddAsync(D dtoObject)
        {
            IResponseDTO IResponseDTO = new ResponseDTO();
            try
            {
                if (dtoObject is IAuditable auditable)
                {
                    auditable.CreatedOn = DateTime.Now;
                }
                T entityObj = _mapper.Map<T>(dtoObject);
                await _genericRepository.AddAsync(entityObj);
                IResponseDTO.IsPassed = true;
                IResponseDTO.Message = "Ok";
            }
            catch (Exception ex)
            {
                IResponseDTO.IsPassed = false;
                IResponseDTO.Message = "Internal Error" + ex.Message;
                return IResponseDTO;
            }
            return IResponseDTO;
        }
        #endregion

        #region Count Methods
        public virtual IResponseDTO Count()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.Count()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> CountAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<Task<D>>(await _genericRepository.CountAsync());
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        #endregion

        #region Remove Method
        public virtual IResponseDTO Remove(D entity)
        {
            IResponseDTO IResponseDTO = new ResponseDTO();
            try
            {
                if (entity is IAuditable auditable)
                {
                    auditable.UpdatedOn = DateTime.Now;
                }
                T entityObject = _mapper.Map<T>(entity);

                ((IBaseEntity)entityObject).IsDeleted = true;
                IResponseDTO.Data = _genericRepository.Update(entityObject);


                IResponseDTO.IsPassed = true;
                IResponseDTO.Message = "Ok";
            }
            catch (Exception ex)
            {


                IResponseDTO.IsPassed = false;
                IResponseDTO.Message = "Internal Error" + ex.Message;
                return IResponseDTO;
            }
            return IResponseDTO;
        }
        #endregion

        #region Find Methods
        public virtual IResponseDTO Find(params object[] keys)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.Find(keys)) ?? new object(); 
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> FindAsync(params object[] keys)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<Task<D>>(await _genericRepository.FindAsync(keys));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO Find(Func<T, bool> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.Find(whereCondition)) ?? new object(); 
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> FindAsync(Expression<Func<T, bool>> match)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<Task<D>>(await _genericRepository.GetFirstAsync(match));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        #endregion

        #region Get Methods
        public virtual IResponseDTO GetAll(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(_genericRepository.GetAll(whereCondition));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO GetAll()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(_genericRepository.GetAll());
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO GetAll(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> select)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(_genericRepository.GetAll(whereCondition, select));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> GetAllAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllAsync());
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllAsync(whereCondition));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> select)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllAsync(whereCondition, select));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(_genericRepository.GetAllIncluding(includeProperties));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual async Task<IResponseDTO> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllIncludingAsync(includeProperties));
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO GetFirstOrDefault()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirstOrDefault()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetFirst()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirst()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        public virtual IResponseDTO GetFirstOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirstOrDefault(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetFirst(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirst(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetFirstOrDefaultAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstOrDefaultAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetFirstAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetFirstOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstOrDefaultAsync(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetFirstAsync(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstAsync(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetLastOrDefault()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLastOrDefault()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetLast()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLast()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetLastOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLastOrDefault(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetLast(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLast(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetLastOrDefaultAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastOrDefaultAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetLastAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetLastOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastOrDefaultAsync(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetLastAsync(Expression<Func<T, bool>> whereCondition)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastAsync(whereCondition)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        #endregion


        #region Update Method
        public virtual IResponseDTO Update(D entity)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                T entityToBeUpdated = _mapper.Map<T>(entity);
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> entityEntry = _genericRepository.Update(entityToBeUpdated);
                response.Data = _mapper.Map<D>(entityEntry) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        #endregion

        #region GetMinimum Methods
        public virtual IResponseDTO GetMin()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetMin()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetMinAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetMinAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetMin(Expression<Func<T, object>> selector)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetMin(selector)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetMinAsync(Expression<Func<T, object>> selector)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetMinAsync(selector)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }
        #endregion

        #region GetMaximum Methods
        public virtual IResponseDTO GetMax()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetMax()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetMaxAsync()
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetMaxAsync()) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual IResponseDTO GetMax(Expression<Func<T, object>> selector)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetMax(selector)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        public virtual async Task<IResponseDTO> GetMaxAsync(Expression<Func<T, object>> selector)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetMaxAsync(selector)) ?? new object();
                response.IsPassed = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.IsPassed = false;
                response.Message = "Internal Error" + ex.Message;
            }
            return response;
        }

        #endregion
    }
}
