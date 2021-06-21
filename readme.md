# readme.md

## Press Release


10.Juli 2021
### Neue E-Learning-Plattform NeoLearn verbessert Neugeborenenversorgung in Äthiopien     

Im Rahmen ihres Projektes JimmaNeo setzt die Ludwigs-Maximilian-Universität München auf die neue E-Learning-Plattform NeoLearn  
die durch den Einsatz der wissenschaftlich-fundierten Spaced-Repetition-Lernmethode im Rahmen eines strukturierten Kurses digitales  
Lernen erleichtert. Die Plattform bringt Neonatologie-Spezialisten der LMU mit medizinischem Fachpersonal des Jimma University  
Specialized Hospital zusammen. Das gemeinsame Ziel: Die Verbesserung der Neugeborenenversorgung im äthiopischen Jimma.  
Die Neugeborenensterblichkeit in Äthiopien beträgt 5,8% und gehört damit zu den höchsten der Welt. Um das Leben möglichst vieler Kinder zu retten,  
kooperieren Ärzte des Dr. von HaunerschenKinderspitals der LMU mit medizinischem Fachpersonal in Äthiopien um in diesem Bereich ein  
Weiterbildungsprogramm zu implementieren. 

Dabei setzt das internationale Team zur digitalen Lehre auf die Plattform NeoLearn, entwickelt von einem Studententeam der Hochschule München   
in Kooperation mit Amazon Web Services. Mit dieser E-Learning-Plattform nach Spaced-Repetition-Prinzip, können die Ärzte, Studenten und   
Pflegekräfte wichtige Inhalte der Neonatologie wiederholen und durch Tests abfragen, bis die Informationen im Langzeitgedächtnis verankert sind  
 und in jeder Situation schnell abgerufen werden können. Im Gegensatz zu üblichen Karteikarten-Systemen müssen die Lernenden allerdings   
 ihren Fortschritt durch ein Thema nicht selbst organisieren: Mit der Kurserstellungskomponente der Plattformbereiten Dozenten lineare Lernwege für sie vor.  
  So werden die Lernenden vom Organisationsaufwand des Selbststudiums befreit und können Kurse dennoch flexibel und selbstständig bearbeiten.  
  
„Mit NeoLearn kann ich viel über Neugeborenenversorgung lernen. Die Seite ist einfach zu benutzenund durch häufige Wiederholungen kann  
 man sich alles gut merken und sofort anwenden, wenn man es braucht. So können wir die Arbeit auf unserer Station wirklich verbessern.“,   
 beurteilt Ammanuel Alazar, Pflegekraft auf der Neugeborenenstation des Jimma University Specialized Hospital seine Erfahrung mit der Plattform.  
   
Da das medizinische Fachpersonal die Weiterbildungzusätzlich zu ihrer Arbeit im Krankenhaus absolviert, bietet NeoLearn eine zugängliche und   
motivierende Lernerfahrung, die sich in den Alltag integrieren lässt: Einfacher Einstieg und problemloses Fortfahren sind jederzeit möglich.   
Ein Klick bringt die Lernenden genau dorthin wo sie in ihrem Kursfortschritt stehen geblieben sind. So kann die Plattform immer und überall für   
Lerneinheiten flexibler Größe genutzt werden. Durch eine transparente Fortschrittsanzeige und ein projiziertes Kursabschlussdatum verlieren  
Benutzer dabei ihr Ziel nicht aus den Augen.  

„Aufgrund der Covid-19-Pandemie mussten wir unsere Pläne,das medizinische Personal in Jimma vor Ort auszubilden verschieben, aber dank  
NeoLearn können wir schonjetzt viel wichtiges Wissen durch E-Learning vermitteln um die Versorgungssituation schnell zu verbessern.“,  
sagt Dr. Peter Müller, Leiter des Projekts an der LMU.  

Die Mission der LMU-Ärzteist es,mit ihren Lerninhalten gemeinsam mit dem medizinisches Fachpersonal in Äthiopien ein Fortbildungsprogramm  
zu schaffen, damit mit diesemdie Lage der Neugeborenenversorgung in Jimma verbessertwerden kann. So wird ein Betrag geleistet die  
Sterblichkeit von Säuglingen zu verringern. Das Team der Hochschule München und AWS tragen zu dieser Mission bei, indem sie eine Lernplattform  
zur Verfügung stellen, die so viele technische und organisatorische Hürden abbaut wie möglich, damit sich die Mediziner in Deutschland und  
Äthiopien auf das konzentrieren können, was wirklich zählt:  
 Wissen verbreiten und Neues erlernen um Leben zu retten.  
 
 Erfahren Sie mehr über das Jimma-Neo-Projekt unter https://www.lmu-klinikum.de/aktuelles/projekte/jimmaneooder testen Sie eine Demonstration  
 der NeoLearn-Website unter www.neolearn.et/demo


*Der Press Release verwendet spekulative Nutzerzitate, um den geplannten Nutzermehrwert zu verdeutlichen.  
Die Verlinkungen dienen nur zu Demonstrationszwecken um die formalen Eigenschaften eines Press Release zu erfüllen.*


## Anwendungsbeschreibung

## Softwarearchitektur
Als Blazor Web-Assembly-Projekt ist die Applikation NeoLearn grundlegend zweigeteilt: Der erste Teil ist die WebApp, primär bestehend aus Pages,  
Komponenten und eine Service zum Zugriff auf den Server. Dies stellt den Frontend der Anwendung dar.  
Der zweite Teil ist der Server, bestehend aus Domänen-Datenobjekten, Controllern, Services und Datenbankzugriff. Beide Bestandteile besitzen außerdem  
Zugriff auf die Data-Transfer-Objects, der "Shared"-Komponente. Dies sind gemappte "Versionen" der Domänenobjekte, die zwischen Frontend und Backend  
ausgetauscht werden.  
Kern des Frontend sind die Pages, die jeweils eine Seite der WebApp darstellen, so gibt es Pages für alle Seiten und Funktionalitäten der Applikation, wie  
Login, Learning-page oder Kursübersicht. Die Pages werden ergänzt durch Komponenten, dies sind wiederverwendbare Bestandteile, die auf Seiten  
eingesetzt werden können und Bestandteile und Funktionen einer Seite zur Verfügung stellen. So gibt es etwa eine QuizHelper-Komponente, die das  
Bearbeiten von Fragen ermöglicht und die sowohl bei den Chapterquizzes als auch im Fragenwiederholungsmodus (Repetitionpage) verwendet wird.  
Auch um Seitenelemente dynamisch zu gestalten bieten sich Komponenten an: So können mit ihrer Hilfe auf der Create-Course-Seite leicht beliebig  
viele Kapitel in einem Kurs angelegt werden. 
Pages und Komponenten bestehen aus C#-Code, HTML, CSS und Razor-Syntax.  
Weiterhin gibt es einen BackEndAccessService. Dieser stellt den Zugriffspunkt für das Senden und Empfangen von HTTP-Requests und -Responses an  
und von der serverseitigen API dar. Der BackEndAccessService stellt den Pages und Komponenten C#-Methoden zur Verfügung, die diese dann einfach  
verwenden können um mit der Datenbank und Anwendungslogik zu interagieren.  

Der andere Teil, der Backend stellt mit seinen Controllern API-Controller zur Verfügung, auf die der WebApp-Frontend zugreift. Es existieren Controller  
für die meisten Datenobjekte, einige fassen logisch zusammengehörige in einem Controller und Endpunkt zusammen. Die Controller verfügen über die  
gängigen Befehle einer REST-API (als Methoden) um Objekte abzufragen, anzulegen, zu ändern oder auch zu löschen.  
Diese Controller greifen wiederum auf Services zu um die angestrebten Operationen im Sinne der Anwendungslogik zu bearbeiten und in der Datenbank  
abzulegen (bzw. abzurufen). Auch bei diesen existieren für die meisten, außer logisch zusammengefasste, Objekte einzelne Services.  
Die Services beinhalten die Anwendungslogik und den Datenbankzugriff. Sie geben die über die API nachgefragten Objekte und Informationen wieder,  
ändern bestehende Objekte, legen neue an und löschen sie. Die "Anwendungslogik" besteht dabei im weitesten Sinne daraus, welche Informationen  
sie liefern oder in welchem Zustand sie Objekte ablegen. So liefert die Methode GetAllPendingQuestionStatusOfUser im QuestionStatusService etwa alle  
Fragen, die ein Benutzer in diesem Moment nach dem "Karteikasten-System" beantworten soll.  
Neben diesen Hauptbestandteilen, gibt es weitere Klassen im Server: DatabaseContext stellt eine Session mit der Datenbank dar und wird für den Zugriff  
auf diese durch die Services verwendet. Mithilfe von Startup werden die Applikation und benötigte Dienste konfiguriert. Hier erstellen wir auch den Datenbankkontext, registrieren unsere Services und starten gegebenenfalls unsere Methode CreateTestData, die die Testdaten aufsetzt.
## Team und Ansprechpartner

Ansprechpartner: maximilian.preis@hm.edu
## Anlagen