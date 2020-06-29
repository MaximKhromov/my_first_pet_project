namespace EnglishHelper.Models.GerundAndInfinitive.VerbExtractor
{
    using System;
    using System.Data.SqlTypes;

    using EnglishHelper.Models.GerundAndInfinitive.Model;
    using Npgsql;

    /// <summary>
    /// Экстрактор глагололов для помошника.
    /// </summary>
    public class VerbsExtractor : IVerbExtractor, IDisposable // создаем класс, реализующий интрефейсы IVerbExtractor и IDisposable
    {
        /// <summary>
        /// <see cref="NpgsqlConnection"/>.
        /// </summary>
        private readonly NpgsqlConnection _connection; // класс, представляющий подключение к серверу БД

        /// <summary>
        /// Инициализирует новый экземпляр 
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public VerbsExtractor(string connectionString) // конструктор
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        /// <inheritdoc />
        public void Dispose() //метод 
        {
            _connection.Dispose();
        }

        public GerundAndInfinitiveItem Extract(string word) // метод, возвращающий
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word), "Слово не может быть пустой строкой!");

            var item = new GerundAndInfinitiveItem(); // ?
            var explanationId = -1;

            _connection.Open();
            try
            {
                using (var command = new NpgsqlCommand($"SELECT * FROM infinitive_or_gerund WHERE verb = '{word}'", _connection))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        item.Verb = reader.GetString(0);
                        item.VerbForms = ConvertForm(reader.GetString(1));
                        item.Translate = reader.GetString(3);
                        item.Example = reader.GetString(4);
                        explanationId = reader.GetInt32(5);
                    }

                    reader.Dispose();
                }

                //if (explanationId == -1)
                    //throw new SqlNullValueException($"Не удалось получить объяснения для случаев применения слова {word}");

                using (var command = new NpgsqlCommand($"SELECT * FROM explanations WHERE explanation_id = '{explanationId}'", _connection))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                        item.Explanation = reader.GetString(1);

                    reader.Dispose();
                }
            }
            finally
            {
                _connection.Close();
            }


            return item;
        }

        /// <summary>
        /// Преобразование формы применения глагола из строки в перечисление.
        /// </summary>
        /// <param name="stringForm">Строковое представление формы применения глагола.</param>
        /// <returns>Форма применения глагола.</returns>
        private Verb ConvertForm(string stringForm)
        {
            switch (stringForm)
            {
                case "infinitive":
                    return Verb.Infinitive;
                case "both":
                    return Verb.Both;
                case "gerund":
                    return Verb.Gerund;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringForm), "неизвестная форма применения глагола");
            }
        }
    }
}