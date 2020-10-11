namespace EnglishHelper.Models.DynamicDictionary.WordsExtractor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Npgsql;

    /// <summary>
    /// Экстрактора слов из хранилища.
    /// </summary>
    public class WordsExtractor : IWordsExtractor, IDisposable
    {
        /// <summary>
        /// Имя базы данных, где хранятся слова.
        /// </summary>
        private const string DictionaryDbName = @"dictionary";

        /// <summary>
        /// <see cref="NpgsqlConnection"/>.
        /// </summary>
        private readonly NpgsqlConnection _connection;

        public WordsExtractor(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        
        /// <inheritdoc />
        public IEnumerable<string> ExtractAllTags()
        {
            var result = new List<string>();
            _connection.Open();
            try
            {
                using (var command = new NpgsqlCommand($"SELECT tags FROM {DictionaryDbName}", _connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        result.AddRange(reader.GetFieldValue<string[]>(0));

                    reader.Dispose();
                }
            }
            finally
            {
                _connection.Close();
            }

            return result.Distinct();
        }

        /// <inheritdoc />
        public IEnumerable<DictionaryItem> Extract(string searchingWord)
        {
            _connection.Open();
            try
            {
                using (var command = new NpgsqlCommand(
                    $"SELECT * FROM {DictionaryDbName} WHERE Lower(word) = '{searchingWord.ToLower()}' OR Lower(translate) = '{searchingWord.ToLower()}'",
                    _connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        yield return new DictionaryItem
                            {
                                Word = reader.GetString(0),
                                Translate = reader.GetString(1),
                                Transcription = reader.GetString(2),
                                Tags = reader.GetFieldValue<string[]>(3)
                            };

                    reader.Dispose();
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <inheritdoc />
        public IEnumerable<DictionaryItem> Extract(IEnumerable<string> searchingTags)
        {
            _connection.Open();

            try
            {
                using (var command = new NpgsqlCommand($"SELECT * FROM {DictionaryDbName}", _connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var tags = reader.GetFieldValue<string[]>(3);
                        if (tags.Any(searchingTags.Contains))
                            yield return new DictionaryItem
                                {
                                    Word = reader.GetString(0),
                                    Translate = reader.GetString(1),
                                    Transcription = reader.GetString(2),
                                    Tags = tags
                            };
                    }

                    reader.Dispose();
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}