namespace EnglishHelper.Models.DynamicDictionary
{
    using System.Collections.Generic;

    /// <summary>
    /// Модель представления элемента в словаре.
    /// </summary>
    public class DictionaryItem
    {
        /// <summary>
        /// Получает или задает слово.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Получает или задает перевод слова.
        /// </summary>
        public string Translate { get; set; }

        /// <summary>
        /// Получает или задает транскрипцию слова.
        /// </summary>
        public string Transcription { get; set; }

        /// <summary>
        /// Получает или задает теги, с которыми связаны эти слова.
        /// </summary>
        public ICollection<string> Tags { get; set; }
    }
}