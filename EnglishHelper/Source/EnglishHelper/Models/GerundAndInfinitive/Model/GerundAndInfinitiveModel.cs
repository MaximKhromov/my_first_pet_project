namespace EnglishHelper.Models.GerundAndInfinitive.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// плоский класс с входным глаголом
    /// </summary>
    public class GerundAndInfinitiveModel
    {
        /// <summary>
        /// Получает или задет слово, введенное юзером в веб форму
        /// </summary>
        [Required (ErrorMessage = "Строка не должна быть пустой.")]
        public string Word { get; set; }


        /// <summary>
        /// Получает или задет выходные данные для отображения
        /// </summary>
        public GerundAndInfinitiveItem OutputData { get; set; }
    }
}