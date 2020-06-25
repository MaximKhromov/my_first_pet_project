namespace EnglishHelper.Models.GerundAndInfinitive.Model
{
    /// <summary>
    /// Плоский класс представления сведений о глаголе.
    /// </summary>
    public class GerundAndInfinitiveItem
    {
        /// <summary>
        /// Получает или задает глагол.
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// Получает или задает перевод глагола.
        /// </summary>
        public string Translate { get; set; }

        /// <summary>
        /// Получает или задает форму, в которой используется второй глагол.
        /// </summary>
        public VerbForms VerbForm { get; set; }

        /// <summary>
        /// Получает или задает пример применения глагола.
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// Получает или задает объяснение правила.
        /// </summary>
        public string Explanation { get; set; }
    }
}