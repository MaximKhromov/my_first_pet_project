namespace EnglishHelper.Models.DynamicDictionary.WordsExtractor
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс экстактора слов из хранилища.
    /// </summary>
    public interface IWordsExtractor
    {
        /// <summary>
        /// Получает коллекцию всех тегов.
        /// </summary>
        /// <returns>Коллекция всех тегов.</returns>
        IEnumerable<string> ExtractAllTags();

        /// <summary>
        /// Извлекает все слова из словаря.
        /// </summary>
        /// <param name="searchingWord">Искомое слово</param>
        /// <returns>Коллекция слов из словаря.</returns>
        IEnumerable<DictionaryItem> Extract(string searchingWord);

        /// <summary>
        /// Извлекает все слова из словаря.
        /// </summary>
        /// <param name="searchingTags">Выбранные теги.</param>
        /// <returns>Коллекция слов из словаря.</returns>
        IEnumerable<DictionaryItem> Extract(IEnumerable<string> searchingTags);
    }
}
