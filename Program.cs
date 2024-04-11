using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpOS
{
    class Program
    {
        static string DeveloperName = "Oliver Hiivola";
        static string BuildNumber = "0.5 Dev Beta";
        static string BuildDate = "10.4.2024";

        static HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("C#OS Loading...");
            await Task.Delay(4000); // Delay to simulate loading

            Console.WriteLine("C#OS - Type 'help' for available commands.");

            while (true)
            {
                Console.Write("C#Input: ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                switch (command)
                {
                    case "help":
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("help - Display available commands");
                        Console.WriteLine("echo <message> - Print message to the console");
                        Console.WriteLine("calc <operand1> <operator> <operand2> - Perform arithmetic operations (+, -, *, /)");
                        Console.WriteLine("cls - Clear the console screen");
                        Console.WriteLine("buildinfo - Display developer name and build number");
                        Console.WriteLine("get - Retrieve a list from https://nookgit12.github.io/get.txt");
                        Console.WriteLine("thanks - Display a thank you message");
                        Console.WriteLine("exit - Exit C#OS");
                        break;

                    case "echo":
                        if (parts.Length > 1)
                        {
                            string message = string.Join(" ", parts, 1, parts.Length - 1);
                            Console.WriteLine(message);
                        }
                        else
                        {
                            Console.WriteLine("Usage: echo <message>");
                        }
                        break;

                    case "calc":
                        if (parts.Length == 4)
                        {
                            double operand1, operand2;
                            if (double.TryParse(parts[1], out operand1) && double.TryParse(parts[3], out operand2))
                            {
                                char op = parts[2][0];
                                double result = 0;
                                switch (op)
                                {
                                    case '+':
                                        result = operand1 + operand2;
                                        break;
                                    case '-':
                                        result = operand1 - operand2;
                                        break;
                                    case '*':
                                        result = operand1 * operand2;
                                        break;
                                    case '/':
                                        result = operand2 != 0 ? operand1 / operand2 : double.NaN;
                                        break;
                                    default:
                                        Console.WriteLine($"Invalid operator: {op}");
                                        break;
                                }
                                Console.WriteLine($"Result: {result}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid operands.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usage: calc <operand1> <operator> <operand2>");
                        }
                        break;

                    case "cls":
                        Console.Clear();
                        break;

                    case "buildinfo":
                        Console.WriteLine("C#OS, Made in C# entirely");
                        Console.WriteLine($"Developer: {DeveloperName}");
                        Console.WriteLine($"Build Version: {BuildNumber}");
                        Console.WriteLine($"Build Date: {BuildDate}");
                        break;

                    case "get":
                        await RetrieveListFromUrl("https://nookgit12.github.io/CSos.txt");
                        break;

                    case "thanks":
                        Console.WriteLine("Thanks for using C#OS!");
                        break;

                    case "exit":
                        Console.WriteLine("Exiting C#OS...");
                        return;

                    default:
                        Console.WriteLine($"Unknown command: {command}. Type 'help' for available commands.");
                        break;
                }
            }
        }

        static async Task RetrieveListFromUrl(string url)
        {
            try
            {
                string list = await httpClient.GetStringAsync(url);
                Console.WriteLine("List retrieved successfully:");
                Console.WriteLine(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving list from {url}: {ex.Message}");
            }
        }
    }
}
