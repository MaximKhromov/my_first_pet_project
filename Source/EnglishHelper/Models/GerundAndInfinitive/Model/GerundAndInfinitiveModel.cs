namespace EnglishHelper.Models.GerundAndInfinitive.Model
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Модель для отображения страницы.
    /// </summary>
    public class GerundAndInfinitiveModel
    {
        /// <summary>
        /// Получает или задает слово, введенное пользователем в web форму.
        /// </summary>
        [Required (ErrorMessage = "Строка не должна быть пустой.")]
        public string Word { get; set; }

        /// <summary>
        /// Получает или ззадает выходные данные для отображени.
        /// </summary>
        public GerundAndInfinitiveItem OutputData { get; set; }
    }
}