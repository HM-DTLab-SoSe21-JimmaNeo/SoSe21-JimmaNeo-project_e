using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEIIApp.Shared.DomainTdo {
    public class QuestionDefinitionDto {

        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string QuestionText { get; set; }
        [ValidateComplexType]
        public AnswerDefinitionDto[] Answers { get; set; }


    }
}
