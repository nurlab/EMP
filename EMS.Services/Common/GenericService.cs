using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Core.Interfaces;
using EMS.DTO.Common;
using EMS.Repositories.Generics;
using EMS.Services.Generics;

namespace EMS.Services.Common
{
    public class GenericService<D, T, R> : IGService<D, T>
       where R : IGRepository<T>
       where T : class
    {
        #region Private Fields
        protected readonly R _genericRepository;
        protected readonly IMapper _mapper;
        #endregion


        #region Constructor
        public GenericService(R genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        #endregion

        #region Add Method
        public virtual IResponseDTO Add(D dtoObject)
        {
            IResponseDTO IResponseDTO = new ResponseDTO();
            try
            {
                if (dtoObject is IAuditable)
                {
                    ((IAuditable)dtoObject).CreatedOn = DateTime.Now;
                }
                T entityObj = _mapper.Map<T>(dtoObject);

                object addedModel = _genericRepository.Add(entityObj);

                IResponseDTO.Data = _mapper.Map<D>(addedModel);
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
                if (dtoObject is IAuditable)
                {
                    ((IAuditable)dtoObject).CreatedOn = DateTime.Now;
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

        #region Remove Method
        public virtual IResponseDTO Remove(D dtoObject)
        {
            IResponseDTO IResponseDTO = new ResponseDTO();
            try
            {
                if (dtoObject is IAuditable)
                {
                    ((IAuditable)dtoObject).UpdatedOn = DateTime.Now;
                }
                T entityObject = _mapper.Map<T>(dtoObject);

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
                response.Data = _mapper.Map<D>(_genericRepository.Find(keys));
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
        public virtual IResponseDTO Find(Func<T, bool> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.Find(where));
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
        public virtual IResponseDTO GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(_genericRepository.GetAll(where, select));
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
        public virtual async Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllAsync(where));
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
        public virtual async Task<IResponseDTO> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<IQueryable<D>>(await _genericRepository.GetAllAsync(where, select));
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
                response.Data = _mapper.Map<D>(_genericRepository.GetFirstOrDefault());
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
                response.Data = _mapper.Map<D>(_genericRepository.GetFirst());
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
        public virtual IResponseDTO GetFirstOrDefault(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirstOrDefault(where));
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

        public virtual IResponseDTO GetFirst(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetFirst(where));
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
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstOrDefaultAsync());
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
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstAsync());
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

        public virtual async Task<IResponseDTO> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstOrDefaultAsync(where));
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

        public virtual async Task<IResponseDTO> GetFirstAsync(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetFirstAsync(where));
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
                response.Data = _mapper.Map<D>(_genericRepository.GetLastOrDefault());
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
                response.Data = _mapper.Map<D>(_genericRepository.GetLast());
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

        public virtual IResponseDTO GetLastOrDefault(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLastOrDefault(where));
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

        public virtual IResponseDTO GetLast(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(_genericRepository.GetLast(where));
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
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastOrDefaultAsync());
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
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastAsync());
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

        public virtual async Task<IResponseDTO> GetLastOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastOrDefaultAsync(where));
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

        public virtual async Task<IResponseDTO> GetLastAsync(Expression<Func<T, bool>> where)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                response.Data = _mapper.Map<D>(await _genericRepository.GetLastAsync(where));
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
        public virtual IResponseDTO Update(D dtoObject)
        {
            IResponseDTO response = new ResponseDTO();
            try
            {
                T entityToBeUpdated = _mapper.Map<T>(dtoObject);
                response.Data = _mapper.Map<D>(_genericRepository.Update(entityToBeUpdated));
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