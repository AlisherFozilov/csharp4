using System;

namespace _4
{
    class Program
    {
        static void Main(string[] args)
        {
            try {

                // DZ 1
                Console.WriteLine("=============================================");
                Console.WriteLine("task 1");
                Console.WriteLine();
                Console.WriteLine("Enter 2 numbers (Rectangle sides):");
                var a = inputNumber();
                var b = inputNumber();
                var r = new Rectangle(a, b);
                Console.WriteLine($"Area {r.Area}");
                Console.WriteLine($"Perimeter {r.Perimeter}");

                Console.WriteLine("=============================================");
                Console.WriteLine("task 2");
                Console.WriteLine();
                var book = new Book(
                    new Author("Homer Simpson"),
                    new Title("Thoughts about Doughnut"),
                    new Content("IT'S GREEEAAAAAAT!")
                );

                Console.WriteLine("Info about book:");
                Console.WriteLine();
                book.Show();
                
                Console.WriteLine("=============================================");
                Console.WriteLine("task 3");
                Console.WriteLine();
                var computer = new Computer(){
                    RAMmb = 4096,
                    Processor = "intel core i5",
                    Model = "Lenovo IdeaPad L340"
                };

                computer.PrintToConsole();

                Console.WriteLine();
                Console.WriteLine("Turning on...");
                Console.WriteLine();

                computer.TurnOn();
                computer.PrintToConsole();

                Console.WriteLine();
                Console.WriteLine("Upgrading RAM...");
                Console.WriteLine();

                computer.UpgradeRAMmb(4096);

                computer.PrintToConsole();

                Console.WriteLine();
                Console.WriteLine("Turning off...");
                Console.WriteLine();

                computer.TurnOff();

                computer.PrintToConsole();
                
                // DZ 2

                Console.WriteLine("=============================================");
                Console.WriteLine("task 4 Converter");
                Console.WriteLine();

                var converter = new Converter(
                    usd: 0.088,
                    rub: 6.69,
                    eur: 0.072
                );

                Console.WriteLine($"Input amount in somoni: ");
                var somoni = inputDouble();

                var eur = converter.SOMToEUR(somoni);
                var rub = converter.SOMToRUB(somoni);
                var usd = converter.SOMToUSD(somoni);

                Console.WriteLine($"somoni: {somoni}");
                Console.WriteLine("EUR: {0:0.00}", eur);
                Console.WriteLine("RUB: {0:0.00}", rub);
                Console.WriteLine("USD: {0:0.00}", usd);

                somoni = converter.EURToSOM(eur);
                Console.WriteLine($"SOM from EUR {somoni}");
                somoni = converter.USDToSOM(usd);
                Console.WriteLine($"SOM from USD {somoni}");
                somoni = converter.RUBToSOM(rub);
                Console.WriteLine($"SOM from RUB {somoni}");

                Console.WriteLine("=============================================");
                Console.WriteLine("task 5");
                Console.WriteLine();

                var position = new Position(){
                    Title = "Marketing Project Manager",
                    Description = "Manages marketing",
                    BasicSalary = 10_000,
                };

                var employee = new Employee(
                    name: "Chester",
                    surname: "Bennington",
                    position: position,
                    experienceMonths: 27
                );

                var salary = employee.CalculateSalary();
                Console.WriteLine("Info about employee:");
                Console.WriteLine();
                employee.PrintToConsole();
                Console.WriteLine();
                Console.WriteLine($"Salary for the employer is {salary}");
                Console.WriteLine("Taxes per salary:");
                var taxes = employee.CalculateTaxes();
                Console.WriteLine(taxes);
            } catch (BadInputException)
            {
                Console.WriteLine("Bad input. Program terminated. Try again.");
                return;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Unknown program error");
                return;
            }
        }

        static int inputNumber()
        {
            var input = Console.ReadLine();
            var ok = Int32.TryParse(input, out var number);
            if (!ok)
            {
                throw new BadInputException("not a number");
            }
            return number;
        }
        static double inputDouble()
        {
            var input = Console.ReadLine();
            var ok = Double.TryParse(input, out var number);
            if (!ok)
            {
                throw new BadInputException("not a number");
            }
            return number;
        }
    }

    class Computer
    {
        public bool PowerOn { get; private set; }
        public string Model { get; set; }
        public string Processor { get; set; }
        public int RAMmb { get; set; }

        public double RAMgb { get => Math.Round(Convert.ToDouble(RAMmb) / 1024, 2); }

        public void PrintToConsole()
        {
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Processor is {Processor}");
            Console.WriteLine($"RAM is {RAMgb} Gigabytes");
            string state;
            if (PowerOn)
            {
                state = "on";
            }
            else
            {
                state = "off";
            }
            Console.WriteLine($"State: {state}");
        }
        public void TurnOn()
        {
            PowerOn = true;
        }

        public void TurnOff()
        {
            PowerOn = false;
        }
        
        public void UpgradeRAMmb(int megabytes)
        {
            RAMmb += megabytes;
        }

        public void Downgrade(int megabytes)
        {
            RAMmb -= megabytes;
        }
    }

    class Employee
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Salary { get; set; }
        public Position Position { get; set; }
        public int ExperienceMonths { get; set; }
        public Employee(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Employee(string name, string surname, Position position, int experienceMonths)
        {
            Name = name;
            Surname = surname;
            Position = position;
            ExperienceMonths = experienceMonths;
        }

        public double CalculateSalary()
        {
            var c = experienceCoefficient();
            return Position.BasicSalary * c;
        }

        public void PrintToConsole()
        {
            Console.WriteLine($"{Name} {Surname}");
            Console.WriteLine($"Experience in monthes: {ExperienceMonths}");
            Console.WriteLine($"Position title: {Position.Title}");
            Console.WriteLine($"Position description: {Position.Description}");
        }
        public double CalculateTaxes()
        {
            var salary = CalculateSalary();
            var workTax = salary * 13 / 100;
            var pensionFundTax = salary * 1 / 100;
            var totalTax = workTax + pensionFundTax;
            return totalTax;
        }
        private int experienceCoefficient()
        {
            var years = ExperienceMonths / 12;
            return years;
        }
    }

    class Position
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BasicSalary { get; set; }
    }

    class Converter
    {
        public double rateUSD { get; private set; }
        public double rateRUB { get; private set; }
        public double rateEUR { get; private set; }

        public Converter(double usd, double eur, double rub)
        {
            rateUSD = usd;
            rateRUB = rub;
            rateEUR = eur;
        }

        public double SOMToRUB(double amount)
        {
            return amount * rateRUB;
        }
        
        public double SOMToUSD(double amount)
        {
            return amount * rateUSD;
        }
        
        public double SOMToEUR(double amount)
        {
            return amount * rateEUR;
        }

        public double RUBToSOM(double amount)
        {
            return amount / rateRUB;
        }
        
        public double USDToSOM(double amount)
        {
            return amount / rateUSD;
        }
        
        public double EURToSOM(double amount)
        {
            return amount / rateEUR;
        }

    }

    class Rectangle
    {
        private double side1;
        private double side2;

        public Rectangle(double side1, double side2)
        {
            this.side1 = side1;
            this.side2 = side2;
        }
        
        double AreaCalculator()
        {
            return side1 * side2;
        }

        double PerimeterCalculator()
        {
            return (side1 + side2) * 2;
        }

        public double Area { 
            get
            {
                return AreaCalculator();
            }
        }
        public double Perimeter{ 
            get
            {
                return PerimeterCalculator();
            }
        }
    }

    class Book
    {
        public Title Title { get; set; }
        public Author Author { get; set; }
        public Content Content { get; set; }

        public Book(Author author, Title title, Content content)
        {
            Title = title;
            Author = author;
            Content = content;
        }

        public Book()
        {

        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Title.Show();
            Console.ForegroundColor = ConsoleColor.Red;
            Author.Show();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Content.Show();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Title
    {
        private string data;
        public void Show()
        {
            Console.WriteLine($"\"{data}\"");
        }

        public Title(string data)
        {
            this.data = data;
        }
    }

    class Author
    {
        private string data;
        public void Show()
        {
            Console.WriteLine($"Author: {data}");
        }

        public Author(string data)
        {
            this.data = data;
        }
    }

    class Content
    {
        private string data;
        public void Show()
        {
            Console.WriteLine(data);
        }

        public Content(string data)
        {
            this.data = data;
        }
    }



    [System.Serializable]
    public class BadInputException : System.Exception
    {
        public BadInputException() { }
        public BadInputException(string message) : base(message) {}
        public BadInputException(string message, System.Exception inner) : base(message, inner) { }
        protected BadInputException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}

