using BlockChainP411NEW.Models;
using BlockChainP411NEW.Services;
using System;

namespace BlockChainP411NEW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var blockchainService = new BlockChainService();

            Console.WriteLine("Перевірка цілісності блокчейну при запуску...");
            bool isValid = blockchainService.IsValid();

            if (isValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Блокчейн валідний. Дані не пошкоджено.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Блокчейн НЕВАЛІДНИЙ. Роботу зупинено.");
            }
            Console.ResetColor();

            Console.ReadLine();
        }
    }
}