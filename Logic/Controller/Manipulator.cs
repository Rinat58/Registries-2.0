using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Logic.Controller
{
    /// <summary>
    /// Манипулирует объектами реестров.
    /// </summary>
    public abstract partial class Manipulator
    {
        #region Методы управления списками реестров.

        /// <summary>
        /// Сохраняет или перезаписывает список реестров в памяти компьютера с заданным именем.
        /// </summary>
        /// /// <param name="name">Имя списка, который нужно сохранить.</param>
        public static void SaveRegistriesList(string name = "Список")
        {
            Serialization.Save(new RegistriesList(name));
        }

        /// <summary>
        /// Запускает метод загрузки для списка реестров с заданным именем.
        /// </summary>
        /// <param name="name">Имя списка, который нужно загрузить</param>
        public static void LoadRegistriesList(string name)
        {
            currRegistriesList = Serialization.Load(name).registriesList;
            CurrName = Serialization.Load(name).Name;
        }
        
        /// <summary>
        /// Удаляет список реестров с заданным именем из списка.
        /// </summary>
        /// <param name="name">Имя списка, который нужно удалить.</param>
        public static void DeleteRegistriesList(string name)
        {
            var overlap = ToDetectOverlapDatFiles(name);
            if (overlap.Count() == 0)
            {
                throw new Exception($"Список \"{name}\" не найден в папке \'SavedRegistry\'!");
            }
            overlap.Single().Delete();
        }

        /// <summary>
        /// Переименовывает нужный список реестров.
        /// </summary>
        /// <param name="currName">Текущее имя</param>
        /// <param name="newName">Новое имя (если по умолчанию, то будет дано "Список i", где i индекс.)</param>
        public static void RenameRegistriesList(string currName, string newName = "Список")
        {
            var overlap = ToDetectOverlapDatFiles(currName);
            if (overlap.Count() == 0)
            {
                throw new Exception($"Список \"{currName}\" не найден в папке \'SavedRegistry\'!");
            }
            overlap.Single().MoveTo(new RegistriesList(currName).Name);
        }

        /// <summary>
        /// Возвращает массив файлов *.dat в папке SavedRegistry.
        /// </summary>
        public static FileInfo[] DatFilesArray()
        {
            // Текущий путь.
            string currDir = Directory.GetCurrentDirectory();

            // Путь к папке проекта.
            string dirProject = Directory.GetParent(currDir).Parent.Parent.FullName;

            // Задаем путь к SavedRegistry.
            string path = Path.Combine(Directory.GetParent(currDir).Parent.Parent.FullName,
                    "SavedRegistriesList");

            // Выбираем каталог и осуществляем в нём поиск.
            DirectoryInfo savedRegistry = new DirectoryInfo(path);
            FileInfo[] registryFiles = savedRegistry.GetFiles("*.dat", SearchOption.TopDirectoryOnly);

            if (registryFiles.Length == 0)
            {
                throw new Exception("В папке \'SavedRegistriesList\' отсутствуют файлы *.dat.");
            }
            return registryFiles;
        }

        /// <summary>
        /// Возвращает коллекцию файлов *.dat у которых имя совпадает с именем в параметре.
        /// Если совпадений нет, то возвращается пустая коллекция.
        /// </summary>
        /// <param name="name">Имя которое будет проверяться.</param>
        /// <returns></returns>
        private static IEnumerable<FileInfo> ToDetectOverlapDatFiles(string name)
        {
            if (DatFilesArray().Count() == 0)
            {
                throw new Exception("В папке \'SavedRegistriesList\' отсутствуют файлы *.dat.");
            }
            return from r in DatFilesArray()
                   where r.Name == name
                   select r;
        }

        #endregion

        #region Методы управления реестрами.

        /// <summary>
        /// Создаёт и добавляет новый реестр с заданным именем в список.
        /// </summary>
        /// <param name="name">Имя реестра</param>
        public static void CreateRegistry(string name = "Реестр")
        {
            currRegistriesList.Add(new Registry(name));
        }

        /// <summary>
        /// Удаляет реестр с заданным именем из списка.
        /// </summary>
        /// <param name="name">Имя реестра который нужно удалить.</param>
        public static void DeleteRegistry(string name)
        {
            var overlap = ToDetectOverlapRegistries(name);
            if (overlap.Count() == 0)
            {
                throw new Exception($"Реестр \"{name}\" не найден в списке!");
            }
            currRegistriesList.Remove(overlap.Single());
        }
        
        /// <summary>
        /// Переименовывает нужный реестр в списке.
        /// </summary>
        /// <param name="currName">Текущее имя</param>
        /// <param name="newName">Новое имя (если по умолчанию, то будет дано "Реестр i", где i индекс.)</param>
        public static void RenameRegistry(string currName, string newName = "Реестр")
        {
            var overlap = ToDetectOverlapRegistries(currName);
            if (overlap.Count() == 0)
            {
                throw new Exception($"Реестр \"{currName}\" не найден в списке!");
            }
            overlap.Single().GiveNameRegistry(newName);
        }

        /// <summary>
        /// Возвращает массив имён реестров из списка в строковой форме по дате создания. 
        /// </summary>
        public static string[] RegistriesNamesArray()
        {
            if (currRegistriesList.Count() == 0)
            {
                throw new Exception("Список реестров пуст!");
            }
            currRegistriesList.Sort(new RegistryComparer());
            int i = 0;
            string[] registriesNamesArray = new string[currRegistriesList.Count];
            foreach (Registry reg in currRegistriesList)
            {
                registriesNamesArray[i] = reg.Name;
                i++;
            }
            return registriesNamesArray;
        }
        
        /// <summary>
        /// Возвращает коллекцию Реестров из списка у которых имя совпадает с именем в параметре.
        /// Если совпадений нет, то возвращается пустая коллекция.
        /// </summary>
        /// <param name="name">Имя которое будет проверяться.</param>
        /// <returns></returns>
        private static IEnumerable<Registry> ToDetectOverlapRegistries(string name)
        {
            if (currRegistriesList.Count() == 0)
            {
                throw new Exception("Список реестров пуст!");
            }
            return from r in currRegistriesList
                   where r.Name == name
                   select r;
        }
        
        
        
        #endregion
    }
}
