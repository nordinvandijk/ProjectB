using System;
using static System.Console;
using CinemaApp.Screens; // Using namespace folder Screens
using System.Globalization;

namespace CinemaApp
{
    class Application
    {
        //Fields
        public HomeScreen homeScreen;
        public FilmOverviewScreen filmOverviewScreen;
        public AccountCreationScreen accountCreationScreen;
        public AddMoviesScreen addMoviesScreen;
        public AddToOrderScreen addToOrderScreen;
        public AdminPanelScreen adminPanelScreen;
        public EventScreen eventScreen;
        public FilmInfoScreen filmInfoScreen;
        public LogInScreen logInScreen;
        public MovieAgendaScreen movieAgendaScreen;
        public OrderConfirmationScreen orderConfirmationScreen;
        public OrderOverviewScreen orderOverviewScreen;
        public OverviewCinemaScreen overviewCinemaScreen;
        public ReservationOverviewScreen reservationOverviewScreen;
        public SeatsOverviewScreen seatsOverviewScreen;
        public SubscriptionScreen subscriptionScreen;

        public MovieManager movieManager;
        public UserManager userManager;
        public FilteredFilmScreen filteredFilmScreen;
        public FilmFilter FilmFilter;
        public KijkwijzerFilmFilter kijkwijzerFilmFilter;
        public KijkwijzerFilter kijkwijzerFilter;
        public Time time;
        public FilmAgenda filmAgenda;
        public OrderManager orderManager;



        //Constructor
        public Application()
        {
            homeScreen = new HomeScreen(this);
            filmOverviewScreen = new FilmOverviewScreen(this);
            accountCreationScreen = new AccountCreationScreen(this);
            addMoviesScreen = new AddMoviesScreen(this);
            addToOrderScreen = new AddToOrderScreen(this);
            adminPanelScreen = new AdminPanelScreen(this);
            eventScreen = new EventScreen(this);
            filmInfoScreen = new FilmInfoScreen(this);
            logInScreen = new LogInScreen(this);
            movieAgendaScreen = new MovieAgendaScreen(this);
            orderConfirmationScreen = new OrderConfirmationScreen(this);
            orderOverviewScreen = new OrderOverviewScreen(this);
            overviewCinemaScreen = new OverviewCinemaScreen(this);
            reservationOverviewScreen = new ReservationOverviewScreen(this);
            seatsOverviewScreen = new SeatsOverviewScreen(this);
            subscriptionScreen = new SubscriptionScreen(this);
            filteredFilmScreen = new FilteredFilmScreen(this);
            FilmFilter = new FilmFilter(this);
            movieManager = new MovieManager();
            userManager = new UserManager();
            kijkwijzerFilmFilter = new KijkwijzerFilmFilter(this);
            kijkwijzerFilter = new KijkwijzerFilter(this);
            time = new Time(this);
            filmAgenda = new FilmAgenda(this);
            orderManager = new OrderManager(this);

        }

        //Methods
        public void Start()
        {
            //filmAgenda.ClearMovieItems();
            time.UpdateAgenda();
            homeScreen.run();
            Clear();
            ConsoleUtils.WaitForKeyPress();
        }
    }
}