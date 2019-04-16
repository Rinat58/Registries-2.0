using System;
using Logic.Controller;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Accounts 2.0\n***** Добро пожаловать в программу учёта записей! *****\n");
            
            Manipulator.LoadRegistriesList("Базовый список");

            Console.ReadLine();
        }
    }
}
