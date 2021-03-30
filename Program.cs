using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Log();
            logger.FatalError("Критическая ошибка.");
            logger.Error(new IndexOutOfRangeException());
            logger.ErrorUnique("Критическая ошибка:", new IndexOutOfRangeException());
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add(1, "Какая-то информация о системе.");
            dict.Add(2, "Еще одна информация.");
            dict.Add(3, "И еще одна.");
            logger.SystemInfo("Информация о системе", dict);
            logger.Warning("Какое-то предупреждение.");
            logger.WarningUnique("Еще одно какое-то предупреждение.");
            Console.ReadLine();
        }
    }
}
