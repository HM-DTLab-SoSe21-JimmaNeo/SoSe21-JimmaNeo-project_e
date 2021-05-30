using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEIIApp.Server.DataAccess {
    public class DatabaseContext : DbContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            //Hier werden Einstellungen/Optionen zur Datenbank und zu den Tabellen erfasst
            //die sich nicht durch Annotationen abbilden lassen (z.B. multiple primäre Schlüssel).
        }

        //Eine Tabelle kann der Datenbank hinzugefügt werden, indem 
        //eine Eigenschaft (Property) erstellt wird, die als generisches Argument die
        //abzubildende Domänen-Klasse hat, z.B.
        //public DbSet<Customer> Customers { get; set; }
        //Diese Zeile genügt bereits, um eine Tabelle "Customers", die Objekte der Domänen-Klasse
        //Customer aufnehmen kann, zu erstellen.


        //Hier legen wir eine Tabellen für die Quizze an.
        public DbSet<Domain.QuizDefinition> QuizDefinitions { get; set; }
        //Wir legen, obwohl wir könnten, keine Tabellen für 
        //QuestionDefinitions and AnswerDefinitions an.
        //Diese Abhängigkeiten zu Quiz werden automatisch erkannt.



    }
}
