﻿namespace EnglishHelper.Controllers
{
    using System.Configuration;

    using EnglishHelper.Models.DynamicDictionary.WordsExtractor;
    using EnglishHelper.Models.GerundAndInfinitive.Model;
    using EnglishHelper.Models.GerundAndInfinitive.VerbsExtractor;
    using System.Web.Mvc;

    /// <summary>
    /// Основной контроллер.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// <see cref="WordsExtractor"/>.
        /// </summary>
        private readonly IWordsExtractor _wordsExtractor = new WordsExtractor();

        /// <summary>
        /// <see cref="VerbsExtractor"/>.
        /// </summary>
        private readonly IVerbsExtractor _verbsExtractor;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="HomeController"/>.
        /// </summary>
        public HomeController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            _verbsExtractor = new VerbsExtractor(connectionString);
        }

        /// <summary>
        /// Контролл домашней страницы.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Контролл динамического словаря.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        public ActionResult DynamicDictionary()
        {
            var words = _wordsExtractor.Extract();
            return View(words);
        }

        /// <summary>
        /// Контролл глагольного помошника.
        /// </summary>
        /// <param name="viewData">Сведения, полученные из отображения.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        // [HttpPost]
        public ActionResult GerundAndInfinitiveHelper(GerundAndInfinitiveIModel viewData)
        {
            if (string.IsNullOrWhiteSpace(viewData.Word))
                return View(new GerundAndInfinitiveIModel { OutputData = null });

            var item = _verbsExtractor.Extract(viewData.Word);
            return View(new GerundAndInfinitiveIModel { OutputData = item });
        }
    }
}