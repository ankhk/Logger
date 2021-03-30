using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    interface ILog
    {
        /// <summary>
        /// Критичная ошибка: приложение не может далее функционировать
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void FatalError(string message);

        /// <summary>
        /// критичная ошибка: приложение не может далее функционировать
        /// </summary>
        /// <param name="message">сообщение.</param>
        /// <param name="e">Исключение.</param>
        void Fatal(string message, Exception e);

        /// <summary>
        /// Ошибка в работе приложения: операция расчета завершается, но приложение продолжает работу.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Error(string message);

        /// <summary>
        /// Ошибка в работе приложения: операция расчета завершается, но приложение продолжает работу.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="e">Исключение.</param>
        void Error(string message, Exception e);

        /// <summary>
        /// Ошибка в работе приложения: операция расчета завершается, но приложение продолжает работу.
        /// </summary>
        /// <param name="ex">Исключение.</param>
        void Error(Exception ex);

        /// <summary>
        /// Запись уникальных ошибок
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e">Исключение.</param>
        void ErrorUnique(string message, Exception e);

        /// <summary>
        /// Предупреждение: на работу приложения не влияет, но может сообщать о потенциальных проблемах в расчете.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Warning(string message);

        /// <summary>
        /// Предупреждение: на работу приложения не влияет, но может сообщать о потенциальных проблемах в расчете.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="e">Исключение.</param>
        void Warning(string message, Exception e);


        /// <summary>
        /// Записывает в лог уникальные в течение дня ошибки.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <remarks>
        /// Если в течении дня поступают сообщения с одинаковым содержанием, то в лог попадут только первые вхождения. По прошествию дня уникальность возобновляется.
        /// </remarks>>
        void WarningUnique(string message);

        /// <summary>
        /// Информирование: не влияет на работу приложения, является инструментом информирования.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Info(string message);

        /// <summary>
        /// Информирование: не влияет на работу приложения, является инструментом информирования.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="e">Исключение.</param>
        void Info(string message, Exception e);

        /// <summary>
        /// Информирование: не влияет на работу приложения, является инструментом информирования.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="args">Аргументы.</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Трассирует и отлаживает.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Debug(string message);

        /// <summary>
        /// Трассирует и отлаживает.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="e">Исключение.</param>
        void Debug(string message, Exception e);

        /// <summary>
        /// Трассирует и отлаживает.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args">Аргументы.</param>
        void DebugFormat(string message, params object[] args);

        /// <summary>
        /// Запись системных логов информационного характера.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        void SystemInfo(string message, Dictionary<object, object> properties = null);
    }
}
