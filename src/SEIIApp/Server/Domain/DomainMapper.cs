using AutoMapper;
using SEIIApp.Shared.DomainTdo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.Domain {
    public class DomainMapper : Profile {

        public DomainMapper() {

            CreateMap<QuizDefinition, QuizDefinitionDto>()
                .ForMember(quizDto => quizDto.Questions, opts => opts.MapFrom(obj => obj.Questions.ToArray()));
            CreateMap<QuizDefinitionDto, QuizDefinition>()
                .ForMember(quizObj => quizObj.Questions, opts => opts.MapFrom(dto => dto.Questions.ToList()));

            CreateMap<QuizDefinition, QuizDefinitionBaseDto>();
            CreateMap<QuizDefinition, QuizDefinition>();

            CreateMap<QuestionDefinition, QuestionDefinitionDto>()
                .ForMember(questionDto => questionDto.Answers, opt => opt.MapFrom(obj => obj.Answers.ToArray()));
            CreateMap<QuestionDefinitionDto, QuestionDefinition>()
                .ForMember(questionObj => questionObj.Answers, opt => opt.MapFrom(obj => obj.Answers.ToList()));

            CreateMap<AnswerDefinition, AnswerDefinitionDto>();
            CreateMap<AnswerDefinitionDto, AnswerDefinition>();

        }

    }
}
