using AutoMapper;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Service.Infrastructure
{
    public interface IService<T, V> where T : class
    {
        Task<ResponseEntity> InsertAsync(V modelVm);

        Task<ResponseEntity> UpdateAsync(dynamic id, V modelVm);

        Task<ResponseEntity> GetAllAsync();
        Task<ResponseEntity> GetPagingAsync(int pageIndex, int pageSize, string keywords);
        Task<ResponseEntity> GetByIdAsync(dynamic id);

        Task<ResponseEntity> DeleteByIdAsync(dynamic id);
    }

    public abstract class ServiceBase<T, V> : IService<T, V> where T : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        public ServiceBase(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ResponseEntity> GetAllAsync()
        {
            try
            {
                IEnumerable<T> entities = await _repository.GetAllAsync();
                var modelVm = _mapper.Map<IEnumerable<V>>(entities);
                return new ResponseEntity(StatusCodeConstants.OK, modelVm);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }
        }

        public virtual async Task<ResponseEntity> GetPagingAsync(int pageIndex, int pageSize, string keywords)
        {
            try
            {
                PagingData<T> entity = await _repository.GetPagingAsync(pageIndex, pageSize, keywords);
                var modelVm = new PagingData<V>();
                modelVm.Data = _mapper.Map<IEnumerable<V>>(entity.Data);
                modelVm.PageIndex = entity.PageIndex;
                modelVm.PageSize = entity.PageSize;
                modelVm.Keywords = entity.Keywords;
                modelVm.TotalRow = entity.TotalRow;

                return new ResponseEntity(StatusCodeConstants.OK, modelVm);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }
        }

        public virtual async Task<ResponseEntity> GetByIdAsync(dynamic id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if(entity == null)
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND);

                V modelVm = _mapper.Map<V>(entity);
                return new ResponseEntity(StatusCodeConstants.OK, modelVm);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }
        }

        public virtual async Task<ResponseEntity> InsertAsync(V modelVm)
        {
            try
            {
                T entity = _mapper.Map<T>(modelVm);
                entity = await _repository.InsertAsync(entity);

                modelVm = _mapper.Map<V>(entity);
                return new ResponseEntity(StatusCodeConstants.CREATED, modelVm, MessageConstants.INSERT_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }

        }

        public virtual async Task<ResponseEntity> UpdateAsync(dynamic id, V modelVm)
        {
            try
            {
                T entity = await _repository.GetByIdAsync(id);
                if(entity == null)
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND, modelVm);

                entity = _mapper.Map<T>(modelVm);
                await _repository.UpdateAsync(id, entity);

                return new ResponseEntity(StatusCodeConstants.OK, modelVm, MessageConstants.UPDATE_SUCCESS);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }
        }

        public virtual async Task<ResponseEntity> DeleteByIdAsync(dynamic id)
        {
            try
            {
                if (await _repository.DeleteByIdAsync(id) != 0)
                    return new ResponseEntity(StatusCodeConstants.OK, null, MessageConstants.DELETE_SUCCESS);
                return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, null, MessageConstants.DELETE_ERROR);
            }
            catch (Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER, ex.Message);
            }
        }
    }
}