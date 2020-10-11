namespace EnglishHelper.Models.DynamicDictionary
{
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Плоский класс со сведениями из словаря.
    /// </summary>
    public class DictionaryData
    {
        /// <summary>
        /// Получает или задает коллекцию словаря. 
        /// </summary>
        public IEnumerable<DictionaryItem> Items { get; set; }

        /// <summary>
        /// Получает или задает коллекцию выделенных тегов.
        /// </summary>
        public BindingList<string> SelectedTags { get; set; }

        /// <summary>
        /// Получает значение, показывающее, что искать нужно по слову.
        /// </summary>
        public bool SearchingByWord { get; set; }

        /// <summary>
        /// Получает или задает искомое слово.
        /// </summary>
        public string SearchingWord { get; set; }

        /// <summary>
        /// Получает или задает коллекцию всех существующих тегов.
        /// </summary>
        public IEnumerable<string> AllTags { get; set; }

        /// <summary>
        /// Получает или задает текст сообщения.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}