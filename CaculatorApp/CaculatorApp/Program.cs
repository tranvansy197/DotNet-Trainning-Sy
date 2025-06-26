// See https://aka.ms/new-console-template for more information

using CaculatorApp;

Console.WriteLine("Hello, World!");

//primary constructors
var person = new Person("Sy");
person.SayHello();

//Collection Expressions
int[] numbers = [1, 2, 3, 4, 5];
Console.WriteLine(string.Join(", ", numbers));

//Improved Interpolated Strings
const string name = "Triet";
Console.WriteLine($"Welcome, {name}");

//Calculator


while (true)
{
    Console.WriteLine("Calculator");
    Console.WriteLine("1. Start");
    Console.WriteLine("2. Stop");
    Console.Write("Enter your choice: ");
    var input = Convert.ToInt32(Console.ReadLine());
    if (input == 2)
    {
        Console.WriteLine("Exits!");
        break;
    }
    
    Console.Write("Enter first number: ");
    var number1 = Convert.ToDouble(Console.ReadLine());

    Console.Write("Enter operator(+ - * /): ");
    var op = Console.ReadLine();

    Console.Write("Enter second number: ");
    var number2 = Convert.ToDouble(Console.ReadLine());

    var result = op switch
    {
        "+" => number1 + number2,
        "-" => number1 - number2,
        "*" => number1 * number2,
        "/" => number1 / number2,
        _ => throw new InvalidOperationException("Invalid operator")
    };

    Console.WriteLine($"Result: {result}");

}






