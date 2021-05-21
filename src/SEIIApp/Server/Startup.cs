using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SEIIApp.Server.DataAccess;
using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace SEIIApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Diese Anweisung führt dazu, dass eine In-Memory-Test-Datenbank erstellt wird.
            //Möchte man hier z.B. einen konkreten Datenbank-Server verwenden, muss man nur
            //ein Paket des Datenbank-Treibers (NuGet-Package) laden und hier die Zeile ersetzen.
            //Eventuell muss noch eine Datenbank-Verbindungszeichenfolge angegeben werden, die
            //z.B. den Servernamen, IP-Adresse, Benutzername, Kennwort usw. enthält.
            services.AddDbContext<DatabaseContext>(options => { options.UseInMemoryDatabase("InMemoryDb"); });


            //Swagger -- OpenAPI UI
            //Diese Zeilen fügen die Verwendung einer generierten API-Beschreibung
            //dem Backend hinzu. Es wird aus der Dokumentation des Codes automatisch
            //eine OpenAPI-Specification erstellt und daraus die UI.
            services.AddSwaggerGen(options =>
            {
                //Generiert die Dokumentation für die Version 1 unserer Backend-Anwendung
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Backend Server API", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //Wenn hier ein Fehler auftritt, dann aktivieren Sie in den Einstellungen des 
                //Projektes → Rechte Maustaste auf SEIIApp.Server → Eigenschaften
                //im Tab "Build" die Option "XML-Dokumentationsdatei" und geben dort ein:
                //$(MSBuildProjectDirectory)\SEIIApp.Server.xml
                options.IncludeXmlComments(xmlPath);

                //Die API-UI können Sie dann aufrufen unter:
                //https://localhost:44311/swagger
                //wobei die URL bei Ihnen abweichend sein kann.
            });


            //AutoMapper
            //https://docs.automapper.org/en/latest/Getting-started.html
            //Der AutoMapper wird verwendet, um die Klassen, die zum Transfer der Daten
            //bestimmt sind und die Klassen, die für das Speichern der Daten in der Datenbank
            //bestimmt sind aufeinander zu mappen.
            services.AddAutoMapper(typeof(Domain.Mapper));
            //Diese Zeile fügt die Konfiguration des Mappers zum Mapper


            //Services
            //*********************************************************************************
            //Wir registrieren hier Services, die wir in der Anwendung verwenden.
            //Services sind also Klassen, die Logik kapseln, z.B. Logik um 
            //Daten in die Datenbank hinzuzufügen oder zu ändern.
            //Aber auch irgendwelche anderen Arten von Berechnungen.
            services.AddScoped<Services.QuizService>();
            services.AddScoped<Services.UserService>();
            services.AddScoped<Services.CourseService>();
            services.AddScoped<Services.QuestionService>();


            //Allgemeines zu Warnungen in der Visual Studio Fehlerliste
            //******************************************************************************
            //werden XML-Kommentare erstellt, dann möchte VisualStudio gerne, dass wir
            //den gesamten Code dokumentieren. Damit wir hier keine Warnungen bekommen, gehen Sie bitte auf
            //Rechte Maustaste auf SEIIApp.Server → Eigenschaften → Build
            //bei "Warnungen unterdrücken" fügen Sie 1591 hinzu (das ist die Kommentar-Warnung).
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            Services.QuizService quizService, Services.UserService userService, Services.CourseService courseService, Services.QuestionService questionService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //Swagger Part II
            //Hier legen wir fest, dass wir die generierte API-Spezifikation als 
            //Webseite verwenden möchten. Hier verweisen wir auch auf die 
            //genierte Datei im OpenAPI-Format
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //Legt fest, dass die generierte Oberfläche die Version 1 unserer
                //generierten API verwendet
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Spezifikation");
            });


            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
            
#if DEBUG
            TestData.CreateTestData(quizService, userService, courseService, questionService);
#endif
        }
    }
}