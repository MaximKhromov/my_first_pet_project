namespace EnglishHelper.Models.GerundAndInfinitive.VerbsExtractor
{
    using System;
    using System.Data.SqlTypes;

    using EnglishHelper.Models.GerundAndInfinitive.Model;
    using Npgsql;

    /// <summary>
    /// Экстрактор глагололов для помошника.
    /// </summary>
    public class VerbsExtractor : IVerbsExtractor, IDisposable
    {
        /// <summary>
        /// Имя базы данных, где хранятся глаголы, для которых нужно определить формы смыслового глагола.
        /// </summary>
        private const string InfinitiveAndGerundDb = @"infinitive_or_gerund";

        /// <summary>
        /// Имя базы данных, где хранятся пояснения.
        /// </summary>
        private const string ExplanationsDb = @"explanations";

        /// <summary>
        /// <see cref="NpgsqlConnection"/>.
        /// </summary>
        private readonly NpgsqlConnection _connection;

        /// <summary>
        /// Инициализирует новый экземпляр 
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public VerbsExtractor(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection.Dispose();
        }

        /// <inheritdoc />
        public GerundAndInfinitiveItem Extract(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word), "Слово не может быть пустой строкой!");

            var item = new GerundAndInfinitiveItem ();
            var explanationId = -1;

            _connection.Open();
            try
            {
                using (var command = new NpgsqlCommand($"SELECT * FROM {InfinitiveAndGerundDb} WHERE Lower(verb) = '{word.ToLower()}'", _connection))
                {
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        item.Verb = reader.GetString(0);
                        item.VerbForm = ConvertForm(reader.GetString(1));
                        item.Translate = reader.GetString(3);
                        item.Example = reader.GetString(4);
                        explanationId = reader.GetInt32(5);
                    }

                    reader.Dispose();
                }

                if (explanationId == -1)
                    throw new SqlNullValueException($"Не удалось получить объяснения для случаев применения слова {word}");

                using (var command = new NpgsqlCommand($"SELECT * FROM {ExplanationsDb} WHERE explanation_id = '{explanationId}'", _connection))
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
        private VerbForms ConvertForm(string stringForm)
        {
            switch (stringForm)
            {
                case "infinitive":
                    return VerbForms.Infinitive;
                case "both":
                    return VerbForms.Both;
                case "gerund":
                    return VerbForms.Gerund;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringForm), "неизвестная форма применения глагола");
            }
        }
    }
}
