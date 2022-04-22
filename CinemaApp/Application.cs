using System;
using static System.Console;
using CinemaApp.Screens; // Using namespace folder Screens

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

        }

        //Methods
        public void Start()
        {
            DateTime startMovie1 = new DateTime(2000,1,1,12,0,0);
            DateTime endMovie1 = new DateTime(2000,1,1,14,0,0);

            DateTime startMovie2 = new DateTime(2000,1,1,13,0,0);
            DateTime endMovie2 = new DateTime(2000,1,1,14,0,0);

            WriteLine(time.CheckTimeOverlap(startMovie1,endMovie1,startMovie2,endMovie2));
            
            // homeScreen.run();
            // Clear();
            // userManager.Test();
            // ConsoleUtils.WaitForKeyPress();
        }
    }
}