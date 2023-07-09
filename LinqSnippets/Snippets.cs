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

    }
}