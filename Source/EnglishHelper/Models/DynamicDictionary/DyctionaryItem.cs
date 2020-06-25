namespace EnglishHelper.Models.DynamicDictionary
{
    /// <summary>
    /// Модель представления элемента в словаре.
    /// </summary>
    public class DyctionaryItem
    {
        /// <summary>
        /// Получает или задает слово.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Получает или задает перевод слова.
        /// </summary>
        public string Translate { get; set; }
    }
}