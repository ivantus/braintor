using Microsoft.AspNetCore.Mvc;

namespace web.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private static readonly List<Quiz> Quizzes = new()
    {
        new Quiz(
            "angular-fundamentals",
            "Angular Fundamentals",
            new List<QuizQuestion>
            {
                new(
                    "angular-fundamentals-q1",
                    "What is an Angular component?",
                    new List<QuizAnswer>
                    {
                        new("A reusable UI building block with a template and logic.", true),
                        new("A database table.", false),
                        new("A CSS file only.", false),
                        new("A server process.", false)
                    }),
                new(
                    "angular-fundamentals-q2",
                    "What does property binding do?",
                    new List<QuizAnswer>
                    {
                        new("Binds a component value to an element property.", true),
                        new("Creates a new Angular project.", false),
                        new("Deletes unused CSS.", false),
                        new("Runs backend migrations.", false)
                    }),
                new(
                    "angular-fundamentals-q3",
                    "What does event binding listen for?",
                    new List<QuizAnswer>
                    {
                        new("User or browser events like clicks.", true),
                        new("Only database events.", false),
                        new("Only build errors.", false),
                        new("Only CSS changes.", false)
                    }),
                new(
                    "angular-fundamentals-q4",
                    "What is a service commonly used for?",
                    new List<QuizAnswer>
                    {
                        new("Sharing logic or data access between components.", true),
                        new("Writing HTML tags.", false),
                        new("Replacing TypeScript.", false),
                        new("Changing the browser engine.", false)
                    }),
                new(
                    "angular-fundamentals-q5",
                    "What does dependency injection help with?",
                    new List<QuizAnswer>
                    {
                        new("Providing class dependencies without manually creating them.", true),
                        new("Drawing images.", false),
                        new("Renaming files.", false),
                        new("Changing JSON into CSS.", false)
                    }),
                new(
                    "angular-fundamentals-q6",
                    "What is HttpClient used for?",
                    new List<QuizAnswer>
                    {
                        new("Making HTTP requests to APIs.", true),
                        new("Styling buttons.", false),
                        new("Compiling C# code.", false),
                        new("Creating SQL indexes.", false)
                    }),
                new(
                    "angular-fundamentals-q7",
                    "What is Angular routing used for?",
                    new List<QuizAnswer>
                    {
                        new("Navigating between application views.", true),
                        new("Encrypting passwords.", false),
                        new("Formatting dates only.", false),
                        new("Running unit tests.", false)
                    }),
                new(
                    "angular-fundamentals-q8",
                    "What does the @for block render?",
                    new List<QuizAnswer>
                    {
                        new("A repeated template for each item in a collection.", true),
                        new("A single CSS class.", false),
                        new("A backend controller.", false),
                        new("An npm package.", false)
                    }),
                new(
                    "angular-fundamentals-q9",
                    "What does the @if block do?",
                    new List<QuizAnswer>
                    {
                        new("Conditionally renders part of a template.", true),
                        new("Installs Angular CLI.", false),
                        new("Creates a REST endpoint.", false),
                        new("Sorts arrays automatically.", false)
                    }),
                new(
                    "angular-fundamentals-q10",
                    "Where should API call logic usually live?",
                    new List<QuizAnswer>
                    {
                        new("In an Angular service.", true),
                        new("Inside index.html.", false),
                        new("Inside a CSS selector.", false),
                        new("Inside package-lock.json.", false)
                    })
            }),
        new Quiz(
            "csharp-basics",
            "C# Basics",
            new List<QuizQuestion>
            {
                new(
                    "csharp-basics-q1",
                    "What is a class in C#?",
                    new List<QuizAnswer>
                    {
                        new("A blueprint for creating objects.", true),
                        new("A package manager command.", false),
                        new("A browser window.", false),
                        new("A JSON-only file.", false)
                    }),
                new(
                    "csharp-basics-q2",
                    "What is an interface used for?",
                    new List<QuizAnswer>
                    {
                        new("Defining a contract that classes can implement.", true),
                        new("Changing image size.", false),
                        new("Running npm scripts.", false),
                        new("Storing CSS variables only.", false)
                    }),
                new(
                    "csharp-basics-q3",
                    "What is a property in C#?",
                    new List<QuizAnswer>
                    {
                        new("A member that exposes data with get and set accessors.", true),
                        new("A folder in Visual Studio only.", false),
                        new("An HTML attribute.", false),
                        new("A NuGet package.", false)
                    }),
                new(
                    "csharp-basics-q4",
                    "What does async/await help with?",
                    new List<QuizAnswer>
                    {
                        new("Writing asynchronous code that is easier to read.", true),
                        new("Changing class names automatically.", false),
                        new("Creating CSS animations.", false),
                        new("Disabling compiler checks.", false)
                    }),
                new(
                    "csharp-basics-q5",
                    "What is LINQ used for?",
                    new List<QuizAnswer>
                    {
                        new("Querying and transforming data collections.", true),
                        new("Opening browser tabs.", false),
                        new("Drawing icons.", false),
                        new("Serving static images only.", false)
                    }),
                new(
                    "csharp-basics-q6",
                    "What is a record in C#?",
                    new List<QuizAnswer>
                    {
                        new("A type commonly used for immutable data models.", true),
                        new("A database server.", false),
                        new("A CSS reset.", false),
                        new("An npm command.", false)
                    }),
                new(
                    "csharp-basics-q7",
                    "What does nullable reference types help detect?",
                    new List<QuizAnswer>
                    {
                        new("Possible null reference problems.", true),
                        new("Slow network requests.", false),
                        new("Incorrect image dimensions.", false),
                        new("Missing CSS classes only.", false)
                    }),
                new(
                    "csharp-basics-q8",
                    "What is a List<T>?",
                    new List<QuizAnswer>
                    {
                        new("A generic collection of items.", true),
                        new("A single string value only.", false),
                        new("A controller action.", false),
                        new("A database migration.", false)
                    }),
                new(
                    "csharp-basics-q9",
                    "What does an exception represent?",
                    new List<QuizAnswer>
                    {
                        new("An error or unexpected condition during execution.", true),
                        new("A successful HTTP response only.", false),
                        new("A CSS selector.", false),
                        new("A package restore command.", false)
                    }),
                new(
                    "csharp-basics-q10",
                    "What is a namespace used for?",
                    new List<QuizAnswer>
                    {
                        new("Organizing and grouping related types.", true),
                        new("Changing monitor resolution.", false),
                        new("Writing HTML forms.", false),
                        new("Compressing images.", false)
                    })
            }),
        new Quiz(
            "aspnet-core-web-api",
            "ASP.NET Core Web API",
            new List<QuizQuestion>
            {
                new("aspnet-core-web-api-q1", "What is a controller action?", new List<QuizAnswer> { new("A method that handles an HTTP request.", true), new("A CSS rule.", false), new("A database column.", false), new("A frontend route only.", false) }),
                new("aspnet-core-web-api-q2", "What does [HttpGet] mean?", new List<QuizAnswer> { new("The action responds to HTTP GET requests.", true), new("The action deletes a record.", false), new("The action runs npm install.", false), new("The action writes CSS.", false) }),
                new("aspnet-core-web-api-q3", "What does NotFound() return?", new List<QuizAnswer> { new("An HTTP 404 response.", true), new("An HTTP 200 response.", false), new("A JavaScript file.", false), new("A database connection.", false) }),
                new("aspnet-core-web-api-q4", "What is dependency injection used for?", new List<QuizAnswer> { new("Providing services to classes that need them.", true), new("Drawing the UI.", false), new("Changing TypeScript types.", false), new("Opening Swagger only.", false) }),
                new("aspnet-core-web-api-q5", "What does CORS control?", new List<QuizAnswer> { new("Which browser origins can call the API.", true), new("Which database tables exist.", false), new("Which CSS files are loaded.", false), new("Which IDE is used.", false) }),
                new("aspnet-core-web-api-q6", "What is Swagger useful for?", new List<QuizAnswer> { new("Exploring and testing API endpoints.", true), new("Compiling Angular templates.", false), new("Creating images.", false), new("Running SQL backups.", false) }),
                new("aspnet-core-web-api-q7", "What is model binding?", new List<QuizAnswer> { new("Mapping request data to action parameters or models.", true), new("Binding CSS files together.", false), new("Installing NuGet packages.", false), new("Changing route names automatically.", false) }),
                new("aspnet-core-web-api-q8", "What does Ok(value) return?", new List<QuizAnswer> { new("An HTTP 200 response with a body.", true), new("An HTTP 500 response.", false), new("A deleted file.", false), new("A frontend component.", false) }),
                new("aspnet-core-web-api-q9", "What is middleware?", new List<QuizAnswer> { new("Code that runs in the HTTP request pipeline.", true), new("A TypeScript interface.", false), new("A CSS layout system.", false), new("A browser bookmark.", false) }),
                new("aspnet-core-web-api-q10", "What does app.MapControllers() do?", new List<QuizAnswer> { new("Maps controller routes into the app.", true), new("Creates Angular components.", false), new("Deletes old builds.", false), new("Formats JSON files.", false) })
            }),
        new Quiz(
            "typescript-essentials",
            "TypeScript Essentials",
            new List<QuizQuestion>
            {
                new("typescript-essentials-q1", "What is an interface in TypeScript?", new List<QuizAnswer> { new("A shape that describes an object type.", true), new("A server endpoint.", false), new("A CSS class.", false), new("A database table.", false) }),
                new("typescript-essentials-q2", "What does string[] mean?", new List<QuizAnswer> { new("An array of strings.", true), new("One string only.", false), new("A number list.", false), new("A CSS selector.", false) }),
                new("typescript-essentials-q3", "What is a union type?", new List<QuizAnswer> { new("A type that can be one of several types.", true), new("A package lock file.", false), new("A controller route.", false), new("An HTML element.", false) }),
                new("typescript-essentials-q4", "What does optional property syntax use?", new List<QuizAnswer> { new("A question mark after the property name.", true), new("A hash symbol.", false), new("A dollar sign only.", false), new("A CSS dot.", false) }),
                new("typescript-essentials-q5", "What are generics used for?", new List<QuizAnswer> { new("Writing reusable typed code.", true), new("Starting a web server.", false), new("Creating PNG files.", false), new("Running database scripts.", false) }),
                new("typescript-essentials-q6", "What does import do?", new List<QuizAnswer> { new("Brings exported code from another module into the file.", true), new("Deletes a folder.", false), new("Changes browser zoom.", false), new("Creates an API endpoint.", false) }),
                new("typescript-essentials-q7", "What does async function return?", new List<QuizAnswer> { new("A Promise.", true), new("A CSS rule.", false), new("A database table.", false), new("An image file.", false) }),
                new("typescript-essentials-q8", "What is type narrowing?", new List<QuizAnswer> { new("Refining a variable type through checks.", true), new("Making text smaller.", false), new("Removing routes.", false), new("Compressing JSON.", false) }),
                new("typescript-essentials-q9", "What does readonly prevent?", new List<QuizAnswer> { new("Reassigning a property after it is set.", true), new("HTTP requests.", false), new("HTML rendering.", false), new("CSS loading.", false) }),
                new("typescript-essentials-q10", "What does Observable<string[]> describe?", new List<QuizAnswer> { new("A stream that emits arrays of strings.", true), new("A single boolean value.", false), new("A CSS animation.", false), new("A backend controller.", false) })
            }),
        new Quiz(
            "html-and-css",
            "HTML and CSS",
            new List<QuizQuestion>
            {
                new("html-and-css-q1", "What is semantic HTML?", new List<QuizAnswer> { new("HTML that uses elements according to their meaning.", true), new("HTML with no tags.", false), new("CSS inside JavaScript only.", false), new("A database schema.", false) }),
                new("html-and-css-q2", "What is the box model?", new List<QuizAnswer> { new("Content, padding, border, and margin.", true), new("A TypeScript type.", false), new("A C# class.", false), new("A browser API only.", false) }),
                new("html-and-css-q3", "What is flexbox useful for?", new List<QuizAnswer> { new("One-dimensional layout.", true), new("Database queries.", false), new("API routing.", false), new("Package installation.", false) }),
                new("html-and-css-q4", "What is CSS grid useful for?", new List<QuizAnswer> { new("Two-dimensional layout.", true), new("HTTP status codes.", false), new("C# records.", false), new("Angular services.", false) }),
                new("html-and-css-q5", "What does a CSS selector target?", new List<QuizAnswer> { new("Elements to style.", true), new("Database rows.", false), new("NuGet packages.", false), new("Controller actions.", false) }),
                new("html-and-css-q6", "What does responsive design support?", new List<QuizAnswer> { new("Layouts that work across different screen sizes.", true), new("Only desktop screens.", false), new("Only API calls.", false), new("Only JSON files.", false) }),
                new("html-and-css-q7", "What is alt text used for?", new List<QuizAnswer> { new("Describing images for accessibility.", true), new("Changing server ports.", false), new("Running tests.", false), new("Creating services.", false) }),
                new("html-and-css-q8", "What does display: block usually do?", new List<QuizAnswer> { new("Makes an element start on a new line and fill available width.", true), new("Deletes an element.", false), new("Creates a controller.", false), new("Returns HTTP 404.", false) }),
                new("html-and-css-q9", "What does margin control?", new List<QuizAnswer> { new("Space outside an element.", true), new("Text content only.", false), new("API response body.", false), new("TypeScript imports.", false) }),
                new("html-and-css-q10", "What is a form element used for?", new List<QuizAnswer> { new("Collecting user input.", true), new("Compiling backend code.", false), new("Installing Angular.", false), new("Serving Swagger.", false) })
            }),
        new Quiz(
            "football-legends-achievements",
            "Football Legends and Their Achievements",
            new List<QuizQuestion>
            {
                new(
                    "football-legends-achievements-q1",
                    "Which player is the only footballer to have won three FIFA World Cups?",
                    new List<QuizAnswer>
                    {
                        new("Pelé", true),
                        new("Diego Maradona", false),
                        new("Lionel Messi", false),
                        new("Zinedine Zidane", false)
                    }),
                new(
                    "football-legends-achievements-q2",
                    "Which major international trophy did Lionel Messi win with Argentina in 2022?",
                    new List<QuizAnswer>
                    {
                        new("FIFA World Cup", true),
                        new("UEFA European Championship", false),
                        new("Africa Cup of Nations", false),
                        new("CONCACAF Gold Cup", false)
                    }),
                new(
                    "football-legends-achievements-q3",
                    "How many UEFA Champions League titles did Cristiano Ronaldo win as a player?",
                    new List<QuizAnswer>
                    {
                        new("Five", true),
                        new("Two", false),
                        new("Three", false),
                        new("Seven", false)
                    }),
                new(
                    "football-legends-achievements-q4",
                    "Which player captained Argentina to victory at the 1986 FIFA World Cup?",
                    new List<QuizAnswer>
                    {
                        new("Diego Maradona", true),
                        new("Gabriel Batistuta", false),
                        new("Javier Zanetti", false),
                        new("Alfredo Di Stéfano", false)
                    }),
                new(
                    "football-legends-achievements-q5",
                    "Who scored Spain's winning goal in the 2010 FIFA World Cup final?",
                    new List<QuizAnswer>
                    {
                        new("Andrés Iniesta", true),
                        new("Xavi Hernández", false),
                        new("David Villa", false),
                        new("Fernando Torres", false)
                    }),
                new(
                    "football-legends-achievements-q6",
                    "Which player scored twice for France in the 1998 FIFA World Cup final?",
                    new List<QuizAnswer>
                    {
                        new("Zinedine Zidane", true),
                        new("Thierry Henry", false),
                        new("Didier Deschamps", false),
                        new("David Trezeguet", false)
                    }),
                new(
                    "football-legends-achievements-q7",
                    "Which Brazilian striker won the Golden Boot at the 2002 FIFA World Cup with eight goals?",
                    new List<QuizAnswer>
                    {
                        new("Ronaldo Nazário", true),
                        new("Ronaldinho", false),
                        new("Rivaldo", false),
                        new("Romário", false)
                    }),
                new(
                    "football-legends-achievements-q8",
                    "Which Dutch legend won the Ballon d'Or three times?",
                    new List<QuizAnswer>
                    {
                        new("Johan Cruyff", true),
                        new("Dennis Bergkamp", false),
                        new("Arjen Robben", false),
                        new("Ruud van Nistelrooy", false)
                    }),
                new(
                    "football-legends-achievements-q9",
                    "Which player captained West Germany to the 1974 FIFA World Cup title?",
                    new List<QuizAnswer>
                    {
                        new("Franz Beckenbauer", true),
                        new("Gerd Müller", false),
                        new("Lothar Matthäus", false),
                        new("Jürgen Klinsmann", false)
                    }),
                new(
                    "football-legends-achievements-q10",
                    "Which player appeared in three consecutive FIFA World Cup finals from 1994 to 2002?",
                    new List<QuizAnswer>
                    {
                        new("Cafu", true),
                        new("Roberto Carlos", false),
                        new("Paolo Maldini", false),
                        new("Lilian Thuram", false)
                    })
            })
    };

    [HttpGet]
    public ActionResult<List<QuizSummary>> GetAll()
    {
        var summaries = Quizzes
            .Select(quiz => new QuizSummary(quiz.Id, quiz.Name))
            .ToList();

        return Ok(summaries);
    }

    [HttpGet("{id}")]
    public ActionResult<Quiz> GetById(string id)
    {
        var quiz = Quizzes.FirstOrDefault(quiz => quiz.Id == id);

        if (quiz is null)
            return NotFound();

        return Ok(quiz);
    }
}

public record QuizSummary(string Id, string Name);

public record Quiz(string Id, string Name, List<QuizQuestion> Questions);

public record QuizQuestion(string Id, string Text, List<QuizAnswer> Answers);

public record QuizAnswer(string Text, bool IsCorrect);
