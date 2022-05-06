using Application.Dto;
using Domain.Aggregates;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Constants;
using Application.Exceptions;

namespace Application.Services
{
    public class ServiceBase<TRepository, TDocument, TDto> : IService<TDto>
        where TDocument : IDocument
        where TDto : DtoBase
        where TRepository : IRepository<TDocument>
    {
        protected readonly IUnitOfWork Uow;
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        public ServiceBase(IUnitOfWork uow, ILogger logger, IMapper mapper)
        {
            Uow = uow;
            Repository = Uow.GetRepository<TRepository, TDocument>();
            Logger = logger;
            Mapper = mapper;
        }

        public virtual async Task<TDto?> GetByIdAsync(string id)
        {
            var document = await Repository.GetByIdAsync(id);

            if (document == null)
            {
                return null;
            }

            return Mapper.Map<TDocument, TDto>(document);
        }

        //public virtual async Task<ListDtoResponse<TDto>> ListAsync(int offset, int limit)
        //{
        //    var resp = await Repository.ListAsync(offset, limit);

        //    return Mapper.Map<ListDocumentResponse<TDocument>, ListDtoResponse<TDto>>(resp);
        //}

        public virtual Task AddAsync(TDto dto)
        {
            var document = Mapper.Map<TDto, TDocument>(dto);

            //set new members
            dto.Id = document.Id;
            dto.CreatedAt = document.CreatedAt;

            return Repository.InsertOneAsync(document);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var document = await GetByIdAsync(dto.Id);

            if (document == null)
            {
                throw new ValidationException(ErrorCode.RecordNotFound);
            }

            var createdAt = document.CreatedAt;
            dto.CreatedBy = document.CreatedBy;

            var updatedDocument = Mapper.Map<TDto, TDocument>(dto);

            updatedDocument.SetCreatedAt(createdAt);

            await Repository.ReplaceOneAsync(updatedDocument);
        }

        public virtual Task DeleteByIdAsync(string id)
        {
            return Repository.DeleteByIdAsync(id);
        }
    }
}
