using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Model;

namespace Logic.Controller
{
    public partial class Manipulator
    {
        /// <summary>
        /// Класс для работы со списками реестров. Сериализуется в *.dat.
        /// </summary>
        [Serializable]
        private class RegistriesList
        {
            /// <summary>
            /// Список реестров куда запишется текущий список.
            /// </summary>
            public List<Registry> registriesList = new List<Registry>();

            /// <summary>
            /// Имя списка реестров.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Дата создание списка реестров.
            /// </summary>
            public readonly DateTime DateCreate;
            
            /// <summary>
            /// Инициализирует экземпляр класса RegistriesList с заданным именем. 
            /// </summary>
            /// <param name="name">Имя списка.</param>
            public RegistriesList(string name)
            {
                DateCreate = DateTime.Now;
                registriesList = currRegistriesList;
                GiveNameRegistriesList(name);
            }
            private RegistriesList() { }

            /// <summary>
            /// Вспомогательный метод. Проверяет название списка реестров.
            /// </summary>
            public void GiveNameRegistriesList(string name = "Список")
            {
                // Стандартное именование списков по нумерации.
                if (name == "Список" || name == "")
                {
                    if (DatFilesArray().Length != 0)
                    {
                        string tempName;
                        int index = 0;
                        do
                        {
                            index++;
                            tempName = string.Format($"Список {index}");
                            // while работает до тех пор, пока совпадения "Список {index}" будут не найдены,
                            // если совпадение есть, то index++ и цикл продолжит работу.
                        } while (ToDetectOverlapDatFiles(tempName).Count() > 0);
                        Name = tempName;
                    }
                    else
                    {
                        Name = "Список 1";
                    }
                }
                // Проверка на некорректный ввод.
                else if (string.IsNullOrWhiteSpace(name) || (name.Length <= 2))
                {
                    throw new Exception("Некорректный ввод! Название не должно быть пустым, её длина должна быть больше 2 символов или название не должно содеражть только символы-разделители!");
                }
                // Проверка на совпадения.
                else if ((from r in currRegistriesList where r.Name == name select r).Count() > 0)
                {
                    throw new Exception($"Список \"{name}\" уже существует, выберите пожалуйста другое название!");
                }
                else
                {
                    Name = name;
                }

            }
        }

        /// <summary>
        /// Текущий список реестров.
        /// </summary>
        private static List<Registry> currRegistriesList = new List<Registry>();
        /// <summary>
        /// Текущее имя списка реестров.
        /// </summary>
        public static string CurrName { get; set; } = "Новый список";

        /// <summary>
        /// Реестр.
        /// </summary>
        [Serializable]
        private class Registry
        {
            #region Поля реестра.

            /// <summary>
            /// Имя реестра.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Дата создание реестра.
            /// </summary>
            public readonly DateTime DateCreate;

            /// <summary>
            /// Коллекция аккаунтов.
            /// </summary>
            public List<Account> accountList = new List<Account>();

            /// <summary>
            /// Инициализирует экземпляр класса Registry с заданным именем. 
            /// </summary>
            /// <param name="name">Имя реестра.</param>
            public Registry(string name)
            {
                DateCreate = DateTime.Now;
                GiveNameRegistry(name);
            }
            private Registry() { }

            /// <summary>
            /// Вспомогательный метод. Проверяет название реестра.
            /// </summary>
            public void GiveNameRegistry(string name = "Реестр")
            {
                // Стандартное именование реестров по нумерации.
                if (name == "Реестр" || name == "")
                {
                    if (currRegistriesList.Count != 0)
                    {
                        string tempName;
                        int index = 0;
                        do
                        {
                            index++;
                            tempName = string.Format($"Реестр {index}");
                        // while работает до тех пор, пока совпадения "Реестр {index}" будут не найдены,
                        // если совпадение есть, то index++ и цикл продолжит работу.
                        } while (ToDetectOverlapRegistries(tempName).Count() > 0);
                        Name = tempName;
                    }
                    else
                    {
                        Name = "Реестр 1";
                    }
                }
                // Проверка на некорректный ввод.
                else if (string.IsNullOrWhiteSpace(name) || (name.Length <= 2))
                {
                    throw new Exception("Некорректный ввод! Название не должно быть пустым, её длина должна быть больше 2 символов или название не должно содеражть только символы-разделители!");
                }
                // Проверка на совпадения.
                else if ((from r in currRegistriesList where r.Name == name select r).Count() > 0)
                {
                    throw new Exception($"Реестр \"{name}\" уже существует, выберите пожалуйста другое название!");
                }
                else
                {
                    Name = name;
                }
                
            }
            #endregion

            #region Методы управления аккаунтами.

            /// <summary>
            /// Регистрация аккаунта.
            /// </summary>
            public void Register()
            {

            }

            /// <summary>
            /// Вход в аккаунт.
            /// </summary>
            public void Join()
            {

            }

            /// <summary>
            /// Удаление аккаунта.
            /// </summary>
            public void Delete()
            {

            }
            #endregion
        }

        /// <summary>
        /// Компаратор объектов Registry.
        /// </summary>
        private class RegistryComparer : IComparer<Registry>
        {
            /// <summary>
            /// Сортирует реестры по дате создания.
            /// </summary>
            public RegistryComparer() { }
            
            public int Compare(Registry x, Registry y)
            {
                if (x.DateCreate > y.DateCreate)
                    return 1;
                else if (x.DateCreate < y.DateCreate)
                    return -1;
                else
                    return 0;
            }
        }
    }
}
