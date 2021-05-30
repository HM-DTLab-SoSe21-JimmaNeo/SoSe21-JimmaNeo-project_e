using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEIIApp.Shared.DomainTdo {

    public class QuizDefinitionBaseDto {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string QuizName { get; set; }
    }

    public class QuizDefinitionDto : QuizDefinitionBaseDto {
        
        [ValidateComplexType]
        public QuestionDefinitionDto[] Questions { get; set; }

    }

}
