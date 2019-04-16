using System;
using Logic.Controller;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Используйте класс Manipulator, мне было лень делать интерфейс!

            Console.WriteLine("Registries 2.0\n***** Добро пожаловать в программу для создания реестров! *****\n");

            Manipulator.SaveRegistriesList("Базовый список");
            Manipulator.LoadRegistriesList("Базовый список");

            // Дальше можете создавать реестры используя методы класса Manipulator!!!
            // Не забудьте сохранить потом измененния используя "Manipulator.SaveRegistriesList"!!!
            

            Console.ReadLine();
        }
    }
}
