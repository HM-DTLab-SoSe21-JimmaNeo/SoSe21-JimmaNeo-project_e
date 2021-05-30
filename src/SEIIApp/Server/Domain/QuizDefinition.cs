using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.Domain {

    public class QuizDefinition {

        [Key]
        public int Id { get; set; }

        public string QuizName { get; set; }

        public List<QuestionDefinition> Questions { get; set; }

    }
}
