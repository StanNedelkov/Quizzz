﻿using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Infrastructure.Data.Common.Contracts;
using Quizzz.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IQuizzzRepository repo;
        public AnswerService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task CreateAnswerAsync(AnswerViewModel model)
        {
            await repo.AddAsync(new Answer()
            {
                Content = model.Content,
                Question = model.Question,
                QuestionId = model.QuestionId,
                IsCorrect = model.IsCorrect,
                TimeCreated = DateTime.Now.ToString("F")
            }) ;
            await repo.SaveChangesAsync();
        }

        public IEnumerable<QuestionViewModel> GetAllQuestions()
        {
            return repo.AllReadonly<Question>()
                .Select(x => new QuestionViewModel() { Id = x.Id, Content = x.Content })
                .ToArray();
        }

        public async Task<IEnumerable<AnswerViewModel>> GetAnswersAsync()
        {
            return await repo.AllReadonly<Answer>()
                .Where(x => x.IsActive)
                .Select(x => new AnswerViewModel() {Id = x.Id, Content = x.Content, Question = x.Question, QuestionId = x.QuestionId })
                .ToArrayAsync();
           
        }
    }
}
