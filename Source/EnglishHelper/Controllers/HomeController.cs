// Контроллер является центральным компонентом в архитектуре MVC. Контроллер получает ввод пользователя, обрабатывает его и посылает обратно результат обработки, например, в виде представления.

namespace EnglishHelper.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Web.Mvc;

    using EnglishHelper.Models.DynamicDictionary;
    using EnglishHelper.Models.DynamicDictionary.WordsExtractor;
    using EnglishHelper.Models.GerundAndInfinitive.Model;
    using EnglishHelper.Models.GerundAndInfinitive.VerbsExtractor;

    /// <summary>
    /// Основной контроллер.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// <see cref="WordsExtractor"/>.
        /// </summary>
        private readonly IWordsExtractor _wordsExtractor;

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
            _wordsExtractor = new WordsExtractor(connectionString);
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
        /// <param name="inputData"><see cref="DictionaryData"/>.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        public ActionResult DynamicDictionary(DictionaryData inputData)
        {
            const string AnyWordsNotFound = @"Не удалось найти ни одного слова по указанному запросу.";
            var allTags = _wordsExtractor.ExtractAllTags();
            var data = new DictionaryData { Items = new List<DictionaryItem>(), AllTags = allTags, SearchingByWord = true, ErrorMessage = string.Empty };
            if (!string.IsNullOrWhiteSpace(inputData.SearchingWord))
            {
                data.Items = _wordsExtractor.Extract(inputData.SearchingWord).ToList();
                if (!data.Items.Any())
                    data.ErrorMessage = AnyWordsNotFound;

                return View(data);
            }

            if (inputData.SelectedTags == null || !inputData.SelectedTags.Any())
                return View(data);

            data.Items = _wordsExtractor.Extract(inputData.SelectedTags).ToList();
            if (!data.Items.Any())
                data.ErrorMessage = AnyWordsNotFound;

            data.SearchingByWord = false;
            return View(data);

        }

        /// <summary>
        /// Контролл глагольного помошника.
        /// </summary>
        /// <param name="viewData">Сведения, полученные из отображения.</param>
        /// <returns><see cref="ActionResult"/>.</returns>
        public ActionResult GerundAndInfinitiveHelper(GerundAndInfinitiveModel viewData)
        {
            if (string.IsNullOrWhiteSpace(viewData.Word))
                return View(new GerundAndInfinitiveModel { OutputData = null });

            try
            {
                var item = _verbsExtractor.Extract(viewData.Word);
                return View(new GerundAndInfinitiveModel { OutputData = item });
            }
            catch (SqlNullValueException)
            {
                return View(new GerundAndInfinitiveModel
                    {
                        OutputData = null
                    });
            }
        }
    }
}
