using System;
using SEIIApp.Shared.DomainDto;
using System.Linq;
using AutoMapper;
using SEIIApp.Server.Domain.CourseDomain;
using SEIIApp.Server.Domain.UserDomain;

namespace SEIIApp.Server.Domain
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Quiz, QuizDto>()
                .ForMember(quizDto => quizDto.Questions,
                    opts => opts.MapFrom(obj => obj.Questions.ToArray()));
            CreateMap<QuizDto, Quiz>()
                .ForMember(quizObj => quizObj.Questions,
                    opts => opts.MapFrom(dto => dto.Questions.ToList()));


            CreateMap<Question, QuestionDto>()
                .ForMember(questionDto => questionDto.Answers,
                    opt => opt.MapFrom(obj => obj.Answers.ToArray()));
            CreateMap<QuestionDto, Question>()
                .ForMember(questionObj => questionObj.Answers,
                    opt => opt.MapFrom(dto => dto.Answers.ToList()));

            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();

            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}