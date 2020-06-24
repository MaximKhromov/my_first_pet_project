namespace EnglishHelper.Controllers
{
    using EnglishHelper.Models.DynamicDictionary.WordsExtractor;
    using System.Web.Mvc;

    /// <summary>
    /// Основной контроллер.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// <see cref="WordsExtractor"/>.
        /// </summary>
        private IWordsExtractor _wordsExtractor = new WordsExtractor();

        /// <summary>
        /// Контролл домашней страницы.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Контролл динамического словаря.
        /// </summary>
        /// <returns></returns>
        public ActionResult DynamicDictionary()
        {
            var words = _wordsExtractor.Extract();
            return View(words);
        }
    }
}