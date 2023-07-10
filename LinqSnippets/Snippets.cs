using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;


namespace LinqSnippets
{
    public class Snippets
    {
        public static void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            //1. SELECT *  de todos los cars
            var CarList = from car in cars select car;

            foreach (var car in CarList)
            {
                Console.WriteLine(car);
            }

            //2. SELECT WHERE car is Audi
            var AudiList = from car in cars where car.Contains("Audi") select car;

            foreach (var Audi in AudiList)
            {
                Console.WriteLine(Audi);
            }

        }

        public static void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Multiplicar todos los numeros por 3
            // tomar todos menos el 9
            // ordenar de forma ascendente

            var ProcessedNumberList =
                numbers
                    .Select(num => num * 3)
                    .Where(num => num != 9)
                    .OrderBy(num => num);

        }

        public static void SearchExample()
        {
            List<string> testList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "cj",
                "f",
                "c"
            };

            //1. Encontrar el primero de los elementos
            var first = testList.First();

            //2. First element that is "c"
            var cText = testList.First(text => text.Equals("c"));

            //3. First element that contains "j"
            var jText = testList.First(text => text.Contains("j"));

            //4. First elements that contains "z" or default
            var firstOrDefaultText = testList.FirstOrDefault(text => text.Contains("z"));

            //5. Last elements that contains "z" or default
            var lastOrDefaultText = testList.LastOrDefault(text => text.Contains("z"));

            //6.Single Values
            var singleText = testList.Single();
            var singleOrDefaultText = testList.SingleOrDefault();



            //OTHER EXAMPLE
            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            //Obtain ( 4, 8)
            var myEvenNumber = evenNumbers.Except(otherEvenNumbers);
        }

        public static void MultipleSelects() 
        {
            // SELECT MANY
            string[] myOptions =
            {
                "Option 1, Text 1",
                "Option 2, Text 2",
                "Option 3, Text 3",
            };

            var myOptionSelection = myOptions.SelectMany(Option => Option.Split(","));


            var enterprise = new[]
            {
                new Enterprise ()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Martin",
                            Email = "martinlora@example.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "Pepe@example.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Juanjo",
                            Email = "juanjo@example.com",
                            Salary = 2000
                        }

                    }
                },
                new Enterprise ()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "ana@example.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Maria",
                            Email = "maria@example.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "Marta",
                            Email = "marta@example.com",
                            Salary = 4000
                        }

                    }
                }

            };

            //Obtain all employees of all Enterprise

            var employeList = enterprise.SelectMany(enterprise => enterprise.Employees);

            //Know if ana is empty (Saber si cualquier lista esta vacia)

            bool hasEnterprises = enterprise.Any();

            bool hasEmployees = enterprise.Any(enterprise => enterprise.Employees.Any());

            //All enterprises at least has an employee wit more than $1000 of salary
            bool hasEmployeWithSalaryMoreThanOrEqual1000 =
                enterprise.Any(enterprise =>
                enterprise.Employees.Any(employe => employe.Salary >= 1000));

        }


        public static void LinqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "c", "d" };

            //INNER JOIN 

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );

            //OUTER JOIN - LEFT

            var leftOuterJoin = from element in firstList
                                join seconElement in secondList
                                on element equals seconElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { element, temporalElement };

            //OUTER JOIN - RIGHT

            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { secondElement, temporalElement };

            //UNION 
            //var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        public static void SkipTakeLinq()
        {
            var myList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            // SKIP

            var skipTwoFirstValue = myList.Skip(2); //{ 3, 4, 5, 6, 7, 8, 9, 10 };

            var skipTwoLastValue = myList.SkipLast(2); // { 1, 2, 3, 4, 5, 6, 7, 8 };

            var skipWhile = myList.SkipWhile( num => num < 4); // {5, 6, 7, 8, 9, 10 };

            //TAKE

            var takeTwoFirstValue = myList.Take(2); //{ 1, 2 };

            var takeTwoLastValue = myList.TakeLast(2); // { 9, 10 };

            var takeWhile = myList.TakeWhile(num => num < 4); // { 1, 2, 3 };

        }

        //paging
        public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);

        }


        //variables
        public static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var aboveAverage = from number in numbers 
                               let average = numbers.Average() // Con esto saca la media de un numero
                               let nSquared = Math.Pow(number, 2) // al numero lo elevo a potencia de 2 osea elevo al cuadrado
                               where nSquared > average       // osea que aqui digo que cuando el numero al cuadrado este por encima de la media
                               select number;                // obtengo el numero

            Console.WriteLine("Average: {0}", numbers.Average()); // Muestra la media del numero

            foreach (var number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square {1}",number,Math.Pow(number, 2)); // Al cuadrado
            }

        }

        //ZIP
        public static void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "One", "Two", "Three", "Four", "Five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);

            // { "1 = One", "2 = Two", "3 = Three", "4 = Four", "5 = Five" }
        }

        //Repeat & Range
        public static void repeatRangeLinq()
        {
            // Generar collection from 1 - 1000 --> Range
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); //Repite 5 veces la X --> X, X, X, X, X
        }

        public static void StudentLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true
                }
            };

            // Selecciona los alumnos que estan certificados
            var certifiedStudent = from student in classRoom
                                   where student.Certified
                                   select student;

            // selecciona alumnos no certificados
            var noCertifiedStudent = from student in classRoom
                                   where student.Certified == false
                                   select student;


            // Selecciona a los alumnos aprovados y certificados por su nombre
            var aprovedStudentsNames =  from student in classRoom
                                        where student.Grade >= 50 && student.Certified == true
                                        select student.Name;
        }

        //ALL
        public static void AllLinq()
        {
            var numbers = new List<int>() {1, 2, 3, 4, 5};

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualsThan2 = numbers.All(x => x >= 2); // false
        }

        //aggregate
        public static void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all Numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            /*
            0+1=1
            1+1=2
            2+2=4
            4+3=7 
            7+4=11 ... asi sucesivamente
             */

            string[] words = { "Hello,", "my", "name", "is", "Martin" };
            string greeting = words.Aggregate((prevGreeting, current)  => prevGreeting + current);
            /*
            "","Hello,"=  hello,
            "Hello,", "my"= Hello, my
            ... asi sucesivamente
             */
        }

        //Disctinct
        public static void disctinnctValue()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };

            IEnumerable<int> disctinctValues = numbers.Distinct();
        }

        //GroupBy
        static public void GroupByExample()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // obteer agrupacion de pares
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9 ..... 2,4,6,8
                }
            }

            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martin",
                    Grade = 90,
                    Certified = true
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            //ahora puedo hacer un foreach y obtener los certificados y los no certificados

        }



        public static void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first Post",
                    Content = "My first Content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 1,
                            Title = "My first comment",
                            Content = "My content",
                            Created = DateTime.Now
                        },
                         new Comment
                        {
                            Id = 2,
                            Title = "My second comment",
                            Content = "My other content",
                            Created = DateTime.Now
                        }
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second Post",
                    Content = "My second Content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 3,
                            Title = "My other new comment",
                            Content = "My new content",
                            Created = DateTime.Now
                        },
                         new Comment
                        {
                            Id = 2,
                            Title = "My other comment",
                            Content = "My new content",
                            Created = DateTime.Now
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(post => post.Comments,
                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }


    }
}