using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Log: ILog
    {
        // [мс]*[с]*[мин] Каждые 30 минут проверка.
        const double CheckInterval = 1000 * 60 * 30;
        private int nowDay;
        private Dictionary<string, string> _dayErrorCollection = new Dictionary<string, string>();
        private Dictionary<string, string> _dayWarningCollection = new Dictionary<string, string>();
        private string _logFilePath;
        private readonly string _applicationPath = Directory.GetCurrentDirectory();
        public Log()
        {
            this._logFilePath = _applicationPath + "\\logs\\" + DateTime.UtcNow.Day + "." + DateTime.UtcNow.Month + "." + DateTime.UtcNow.Year + ".txt";
            Init();
            nowDay = DateTime.UtcNow.Day;
            var inspector = new Timer(CheckInterval);
            inspector.Elapsed += new ElapsedEventHandler(Check);
            inspector.Start();
        }

        private void Check(object sender, ElapsedEventArgs e)
        {
            if (nowDay != DateTime.UtcNow.Day)
            {
                Init();
                nowDay = DateTime.UtcNow.Day;
            }
        }

        private void Init()
        {
            try
            {
                _dayErrorCollection.Clear();
                _dayWarningCollection.Clear();

                if (!File.Exists(_logFilePath))
                {
                    if (!Directory.Exists(_applicationPath + "\\logs"))
                        Directory.CreateDirectory(_applicationPath + "\\logs");
                    using (File.Create(_logFilePath));
                    StreamWriter sw = new StreamWriter(_logFilePath);
                    sw.WriteLine(DateTime.UtcNow + " Логгер запущен.");
                    sw.Close();
                }
                else
                {
                    using (StreamReader sr = new StreamReader(_logFilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] array = str.Split(' ');
                            if (array[1] == "ОШИБКА")
                                _dayErrorCollection.Add(array[0], array[2]);
                            if (array[1] == "ПРЕДУПРЕЖДЕНИЕ")
                                _dayWarningCollection.Add(array[0], array[2]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex);
            }
        }

        private void SendToFile(string message, string e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.UtcNow}. {message}: {e}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void FatalError(string message)
        {
            //Process.GetCurrentProcess().Kill();
            SendToFile("Критическая ошибка", message);
        }

        public void Fatal(string message, Exception e)
        {
            //Process.GetCurrentProcess().Kill();
            SendToFile("Критическая ошибка", message + " " + e.Message);
        }

        public void Error(string message)
        {
            try
            {
                SendToFile("ОШИБКА", message);
                _dayErrorCollection.Add(message, "ОШИБКА");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void Error(string message, Exception e)
        {
            try
            {
                SendToFile("ОШИБКА", message + " " + e.Message);
                _dayErrorCollection.Add(message, e.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void Error(Exception e)
        {
            try
            {
                SendToFile("ОШИБКА", e.Message);
                _dayErrorCollection.Add(e.Message, "ОШИБКА");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void ErrorUnique(string message, Exception e)
        {
            try
            {
                string value;
                if (!_dayErrorCollection.TryGetValue(message + " " + e.Message, out value))
                {
                    SendToFile("ОШИБКА", message + " " + e.Message);
                    _dayErrorCollection.Add(message + " " + e.Message, "ОШИБКА");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void Warning(string message)
        {
            try
            {
                SendToFile("Предупреждение", message);
                _dayWarningCollection.Add(message, "Предупреждение");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void Warning(string message, Exception e)
        {
            try
            {
                SendToFile("Предупреждение", message + " " + e.Message);
                _dayWarningCollection.Add(message, e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void WarningUnique(string message)
        {
            try
            {
                string value;
                if (!_dayWarningCollection.TryGetValue(message, out value))
                {
                    SendToFile("Предупреждение", message);
                    _dayWarningCollection.Add(message, "Предупреждение");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }

        public void Info(string message)
        {
            SendToFile("Информирование", message);
        }

        public void Info(string message, Exception e)
        {
            SendToFile("Информирование", message + " " + e.Message);
        }

        public void Info(string message, params object[] args)
        {
            SendToFile("Информирование", message + " " + args);
        }

        public void Debug(string message)
        {
            SendToFile("ОТЛАДКА", message);
        }

        public void Debug(string message, Exception e)
        {
            SendToFile("ОТЛАДКА", message + " " + e.Message);
        }
        public void DebugFormat(string message, params object[] args)
        {
            SendToFile("ОТЛАДКА", message + " " + args);
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            try
            {
                if (properties != null)
                    foreach (var x in properties)
                        SendToFile("Информация о системе", message + " (" + x.Key + "): " + x.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Сообщение: " + ex.Message);
            }
        }
    }
}
