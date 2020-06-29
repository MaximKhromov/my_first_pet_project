//Тип перечисления(или тип enum) — это тип значения, определенный набором именованных констант применяемого целочисленного типа.
//Чтобы определить тип перечисления, используйте ключевое слово enum и укажите имена элементов перечисления.

namespace EnglishHelper.Models.GerundAndInfinitive.Model
{
    /// <summary>
    /// Перечисление с видами форм второго грагола.
    /// </summary>
    public enum Verb
    {
        /// <summary>
        /// Герундий.
        /// </summary>
        Gerund,

        /// <summary>
        /// Инфинитив.
        /// </summary>
        Infinitive,

        /// <summary>
        /// Обе формы возможны.
        /// </summary>
        Both
    }
}