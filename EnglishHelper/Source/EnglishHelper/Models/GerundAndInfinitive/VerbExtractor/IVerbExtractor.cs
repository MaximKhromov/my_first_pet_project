namespace EnglishHelper.Models.GerundAndInfinitive.VerbExtractor
{
    using EnglishHelper.Models.GerundAndInfinitive.Model;

    /// <summary>
    /// Интерфейс экстрактора глаголов для помощника
    /// </summary>
    public interface IVerbExtractor
    {
        /// <summary>
        /// Получение строки из словаря
        /// </summary>
        /// <param name="word">Слово для которого нужно получить слово из словаря</param>
        /// <returns></returns>
        GerundAndInfinitiveItem Extract(string word);
    }
}