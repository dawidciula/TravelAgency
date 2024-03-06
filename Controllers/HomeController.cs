using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UbbRentalBike.Models;
using UbbRentalBike.ViewModel;

namespace UbbRentalBike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<TripDetailViewModel> _trips = new List<TripDetailViewModel> 
        {
            new TripDetailViewModel
            {
                Id = 1,
                DestinationCity = "Barcelona",
                TripDate = "23.07.2024 - 30.07.2024",
                CityDeparture = "Warszawa",
                AllInclusive = "Tak",
                Description = "Miasto w północno-wschodniej Hiszpanii, nad Morzem Śródziemnym, około 110 km na południe od grzbietu Pirenejów i granicy hiszpańsko-francuskiej. Stolica prowincji o tej samej nazwie oraz wspólnoty autonomicznej Katalonii. Drugie co do wielkości miasto Hiszpanii, z liczbą mieszkańców wynoszącą 1 620 809 wewnątrz centrum administracyjnego. Zespół miejski Barcelony wykracza poza centrum administracyjne, z liczbą ludności wynoszącą 4 588 000, jest piątym co do wielkości zespołem miejskim w Unii Europejskiej. Cała metropolia ma około 5 milionów mieszkańców.",
                Price = 1999
            },
            new TripDetailViewModel
            {
                Id = 2,
                DestinationCity = "Zakintos",
                TripDate = "10.03.2024 - 15.03.2024",
                CityDeparture = "Katowice",
                AllInclusive = "Nie",
                Description = "Grecka wyspa na Morzu Jońskim, na zachód od Peloponezu, trzecia co do wielkości w archipelagu Wysp Jońskich. Wraz z pobliskimi niemal bezludnymi wysepkami tworzy gminę Zakintos, w jednostce regionalnej Zakintos, w regionie Wyspy Jońskie, w administracji zdecentralizowanej Peloponez, Grecja Zachodnia i Wyspy Jońskie. W 2011 roku liczyła 40 758 mieszkańców.",
                Price = 899
            },
            new TripDetailViewModel
            {
                Id = 3,
                DestinationCity = "Praga",
                TripDate = "15.06.2024 - 25.06.2024",
                CityDeparture = "Kraków",
                AllInclusive = "Nie",
                Description = "Stolica i największe miasto Czech, położone w zachodniej części kraju, w środkowej części krainy Czechy, nad Wełtawą. Jest miastem wydzielonym na prawach kraju, będąc jednocześnie stolicą kraju środkowoczeskiego.",
                Price = 599
            }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_trips);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var trip = _trips.FirstOrDefault(_trips => _trips.Id == id);
            return View(trip);
        }

        public IActionResult Reservations()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
