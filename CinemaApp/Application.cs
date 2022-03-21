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
        }

        //Methods
        public void Start()
        {
            homeScreen.run();
            ConsoleUtils.WaitForKeyPress();
        }
    }
}