// Контроллер является центральным компонентом в архитектуре MVC. Контроллер получает ввод пользователя, обрабатывает его и посылает обратно результат обработки, например, в виде представления.

namespace EnglishHelper.Controllers
{
    using System.Configuration;

    using EnglishHelper.Models.DynamicDictionary.WordsExtractor;
    using System.Web.Mvc;

    using EnglishHelper.Models.GerundAndInfinitive.Model;
    using EnglishHelper.Models.GerundAndInfinitive.VerbExtractor;

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
        /// <see cref="VerbsExtractor"/>
        /// </summary>
        private readonly IVerbExtractor _verbExtractor; // видимо, так используется интерфейс 

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="HomeController"/>
        /// </summary>
        public HomeController()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString; // получаем строку подключения
            _verbExtractor=new VerbsExtractor(connectionString); // экземпляр класса?
        }

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

        /// <summary>
        /// Контролл глагольного помощника.
        /// </summary>
        /// <param name="viewData">Сведения, полученные из отображения</param>
        /// <returns></returns>
        //[HttpPost] //атрибут
        public ActionResult GerundAndInfinitiveHelper(GerundAndInfinitiveModel viewData)
        {
            if (string.IsNullOrWhiteSpace(viewData.Word))
                return View(new GerundAndInfinitiveModel { OutputData = null });

            var item = _verbExtractor.Extract(viewData.Word);
            return View(new GerundAndInfinitiveModel {OutputData = item});
        }
    }
}