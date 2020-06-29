namespace EnglishHelper.Models.GerundAndInfinitive.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// класс с получением входного глагола и данных для отображения
    /// </summary>
    public class GerundAndInfinitiveModel
    {
        /// <summary>
        /// Получает или задет слово, введенное юзером в веб форму
        /// </summary>
        
        public string Word { get; set; }


        /// <summary>
        /// Получает или задет выходные данные для отображения
        /// </summary>
        public GerundAndInfinitiveItem OutputData { get; set; }
    }
}