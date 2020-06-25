namespace EnglishHelper.Models.GerundAndInfinitive.VerbsExtractor
{
    using EnglishHelper.Models.GerundAndInfinitive.Model;

    /// <summary>
    /// Интерфейс экстрактора глагололов для помошника.
    /// </summary>
    public interface IVerbsExtractor
    {
        /// <summary>
        /// Получение строки из словаря.
        /// </summary>
        /// <param name="word">Слово, для которого нужно получить строку из словаря.</param>
        /// <returns><see cref="GerundAndInfinitiveItem"/>.</returns>
        GerundAndInfinitiveItem Extract(string word);
    }
}