using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Logic.Controller
{
    public abstract partial class Manipulator
    {
        /// <summary>
        /// Сериализация списка реестров.
        /// </summary>
        private abstract class Serialization
        {
            // Текущий путь.
            private static readonly string currDir = Directory.GetCurrentDirectory();
            // Путь к папке проекта.
            private static readonly string dirProject = Directory.GetParent(currDir).Parent.Parent.FullName;

            /// <summary>
            /// Сохраняет список реестров в память компьютера, переданный в качестве параметра.
            /// </summary>
            /// <param name="regList">Список реестров который нужно сохранить на память.</param>
            /// <param name="name">Каким именем сохранить список в памяти.</param>
            public static void Save(RegistriesList regList)
            {
                // Создать бинарный форматтер.
                BinaryFormatter formatter = new BinaryFormatter();

                // Создать папку с сохранениями в папке проекта.
                Directory.CreateDirectory(Path.Combine(dirProject, "SavedRegistriesList"));

                // Задаем новый путь.
                string path = Path.Combine(Directory.GetParent(currDir).Parent.Parent.FullName,
                    "SavedRegistriesList",
                    string.Format("{0}.dat", regList.Name));

                // Получаем поток из файла *.dat.
                using (var fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    // Сериализовать.
                    formatter.Serialize(fStream, regList);
                }
            }

            /// <summary>
            /// Загружает и возвращает список реестров с заданным именем, если таковой не найден, то вызывает исключение.
            /// </summary>
            /// <param name="name">Имя списка, который нужно загрузить.</param>
            public static RegistriesList Load(string name)
            {
                // Создать бинарный форматтер.
                BinaryFormatter formatter = new BinaryFormatter();

                // Создать папку с сохранениями в папке проекта.
                Directory.CreateDirectory(Path.Combine(dirProject, "SavedRegistriesList"));

                // Задаем новый путь.
                string path = Path.Combine(Directory.GetParent(currDir).Parent.Parent.FullName,
                    "SavedRegistriesList",
                    string.Format("{0}.dat", name));

                // Получаем поток из файла *.dat.
                using (var fStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    // Десериализовать и возвратить.
                    return (RegistriesList)formatter.Deserialize(fStream);
                }
            }
        }
    }
}
