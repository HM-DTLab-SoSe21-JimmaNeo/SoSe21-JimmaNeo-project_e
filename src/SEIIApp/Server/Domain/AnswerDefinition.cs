using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.Domain {

    public class AnswerDefinition {

        [Key]
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrectAnswer { get; set; }


    }

}
