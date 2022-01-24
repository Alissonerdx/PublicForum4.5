using AutoMapper;
using FluentValidation.Results;
using PublicForum2.Application.Configurations;
using PublicForum2.Application.Interfaces;
using PublicForum2.Application.ViewModels;
using PublicForum2.Domain.Entities;
using PublicForum2.Domain.Interfaces.Repository;
using PublicForum2.Domain.Validations.Topic;
using PublicForum2.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Application.Services
{
    public class TopicService : BaseService, ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        public TopicService(IUnitOfWork unitOfWork, ITopicRepository topicRepository)
            : base(unitOfWork)
        {
            _topicRepository = topicRepository;
        }

        public TopicViewModel Create(TopicViewModel data)
        {
            data.Created = DateTime.Now;
            var model = Mapper.Map<Topic>(data);

            data.Validation = new CreateTopicValidation().Validate(model);
            if (!data.Validation.IsValid)
                return data;

            BeginTransaction();

            data = Mapper.Map<TopicViewModel>(_topicRepository.Add(model));

            Commit();

            return data;
        }

        public async Task<TopicViewModel> Delete(Guid id, Guid ownerId)
        {
            var result = await _topicRepository.GetByIdAndOwnerId(id, ownerId);
            if (result != null)
            {
                result.IsDeleted = true;
                BeginTransaction();
                result = _topicRepository.Update(result);
                Commit();

                return Mapper.Map<TopicViewModel>(result);
            }

            return null;
        }

        public async Task<TabulatorViewModel> GetPaginated(int page = 1, int pageSize = 30, List<Dictionary<string, string>> filters = null, string ownerId = null, string orderBy = "Id", bool ascending = true)
        {
            var results = await _topicRepository.GetPaginated(page, pageSize, filters, orderBy, ascending);
            var count = await _topicRepository.GetCountFiltered(filters);
            var mapped = Mapper.Map<List<TopicViewModel>>(results);

            return new TabulatorViewModel
            {
                data = mapped != null && mapped.Count > 0 ? mapped.Select(m => PrepareDataTabulator(m, ownerId)) : null,
                last_page = page,
                row_count = count
            };
        }

        private Object PrepareDataTabulator(TopicViewModel topic, string ownerId)
        {
            return new
            {
                Id = topic.Id,
                Title = topic.Title,
                Date = topic.Created.ToString("MM/dd/yyyy HH:mm:ss"),
                OwnerId = topic.OwnerId,
                Owner = $"{topic.Owner.FirstName} {topic.Owner.LastName}",
                IsOwner = ownerId != null ? topic.OwnerId == ownerId : false
            };
        }

        public async Task<TopicViewModel> Update(TopicViewModel data)
        {
            var model = Mapper.Map<Topic>(data);
            data.Validation = new UpdateTopicValidation().Validate(model);
            if (!data.Validation.IsValid)
                return data;

            var modelDb = await _topicRepository.GetById(data.Id);

            if (modelDb.OwnerId == data.OwnerId)
            {
                modelDb.Title = model.Title;
                modelDb.Description = model.Description;

                BeginTransaction();

                data = Mapper.Map<TopicViewModel>(_topicRepository.Update(modelDb));

                Commit();

                return data;
            }

            data.Validation.Errors.Add(new ValidationFailure("OwnerId", "Topic can only be changed by its owner"));
            return data;
        }

        public async Task<TopicViewModel> GetById(Guid id)
        {
            return Mapper.Map<TopicViewModel>(await _topicRepository.GetById(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
