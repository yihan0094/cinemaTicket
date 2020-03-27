using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace CinemaApp
{
    class Class1
    {
        string username, password, user;
        Guid Id;

        Cinema cinema = new Cinema();
        Account account = new Account();
        Movies movies = new Movies();
        Halls halls = new Halls();
        MovieHall movieHall = new MovieHall();


        public Class1()
        {
            cinema.GenerateMovie();
            cinema.GenerateUser();
            cinema.GenerateMovieHall();
            cinema.GenerateSeats();
        }
       
        public void FirstPage()
        {

            while (true)
            {
                Console.WriteLine("Welcome to Cinema Ticket App");
                Console.WriteLine("1. View All Movies");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit App");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        ShowMovie();
                        break;

                    case "2":
                        Console.Clear();
                        PassCheck();
                        break;

                    case "3":
                        Console.WriteLine("Thank you for using our cinema ticket app.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid Option.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void PassCheck()
        {
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();

            var LinqCheck = (from c in cinema.User
                             where c.Username == username && c.Password == password
                             select c).FirstOrDefault();
            if (LinqCheck != null)
            {
                Console.Clear();
                user = LinqCheck.Username;
                UserInterface();

            }
            else
            {
                Console.WriteLine("Incorrect Username/Password");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMovie()
        {
            var PrintMovie = from p in cinema.Movie
                             select p;

            var table = new ConsoleTable("ID", "Movie Title", "Release Date","Movie Status");
            foreach (var item in PrintMovie)
            {
                string movieStatus = "";

                if (item.MovieAvailability) movieStatus = "Now Showing";
                else movieStatus = "Coming Soon";

                table.AddRow(item.Mid, item.Title, item.ReleaseDate.ToString("dddd, dd MMMM yyyy"), movieStatus);
            }
            table.Write();
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
            Console.Clear();
            
        }

        private void UserInterface()
        {
            var checktrue = true;
            while (checktrue)
            {
                var AccountDetails = (from ad in cinema.User
                                      where ad.Username == username
                                      select ad).FirstOrDefault();

                Console.WriteLine("You login as " + AccountDetails.Username);
                Console.WriteLine("1. Select a movie");
                Console.WriteLine("2. Logout");
                Console.WriteLine("");
                Console.Write("Enter your option: ");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        SelectMovie();
                        break;

                    case "2":
                        user = null;
                        Console.Clear();
                        checktrue = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Option.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void SelectMovie()
        {
            var NowShowing = (from ns in cinema.Movie
                              where ns.MovieAvailability == true
                              select ns).ToList();

            var table = new ConsoleTable("ID", "Movie Title", "Release Date","Movie Status");
            foreach (var item in NowShowing)
            {
                string movieStatus = "";

                if (item.MovieAvailability) movieStatus = "Now Showing";

                table.AddRow(item.Mid, item.Title, item.ReleaseDate.ToString("dddd, dd MMMM yyyy"), movieStatus);
            }
            table.Write();
            ChooseMovie();

        }

        private void ChooseMovie()
        {
            Console.Write("Enter a movie Id: ");
            string movieOpt = Console.ReadLine();
            
            if(movieOpt == "")
            {
                Console.WriteLine("Invalid option. Please try again.");
                Console.ReadKey();
                Console.Clear();
                SelectMovie();
            }
            var checkMovie = (from cm in cinema.MovieHalls
                              where cm.MovieId == Convert.ToInt32(movieOpt)
                              select cm).FirstOrDefault();

            if(checkMovie != null)
            {
                var PrintTitle = (from pt in cinema.Movie
                                  where pt.Mid == checkMovie.MovieId
                                  select pt).FirstOrDefault();

                var movieTitle = "";
                if (PrintTitle != null)
                {
                    movieTitle = PrintTitle.Title;
                    if (checkMovie != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Your movie selection: " + movieTitle);
                        var table = new ConsoleTable("Id", "Showing Time");

                        var chooseTime = (from ct in cinema.MovieHalls
                                          where ct.MovieId == checkMovie.MovieId && ct.ShowTime.Date.Equals(DateTime.Today.Date)
                                          select ct).ToList();

                        foreach (var item in chooseTime)
                        {
                            table.AddRow(item.Id, item.ShowTime.ToString("dddd, dd MMMM yyyy h:mm tt"));
                        }
                        table.Write();
                        PrintSeats();
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again");
                        Console.ReadKey();
                        Console.Clear();

                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again");
                Console.ReadKey();
                Console.Clear();
                SelectMovie();
            }
        }

        private void PrintSeats()
        {
            Console.Write("Enter Id to choose the movie time: ");
            string timeId = Console.ReadLine();

            var selectMovieTime = (from d in cinema.MovieHalls
                                   where d.HallId == Convert.ToInt32(timeId)
                                   select d).FirstOrDefault();

            if (selectMovieTime != null)
            {
                 Console.Clear();
                 Console.WriteLine(" Cinema Hall Seating");
                 Console.WriteLine("");
                 Console.WriteLine(" T: Taken     E:Empty");
                 Console.WriteLine("");

                 var selectMovieSeats = (from d in cinema.Halls
                                         where d.HallNo == selectMovieTime.Id
                                         select d).ToList();


                foreach (var item in selectMovieSeats)
                {
                    
                    Console.Write(" {0},{1} {2}  ", item.TotalRow, item.TotalColumn, item.SeatStatus);
                    item.Seats = item.TotalRow + "," + item.TotalColumn;


                    if (item.TotalColumn == 10)
                    {
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("");
                while (true)
                {
                    Console.Write(" Enter a seat number(row , column): ");
                    string seatNo = Console.ReadLine();

                    var CheckMovieSeat = (from d in selectMovieSeats
                                          where d.Seats == seatNo && d.SeatStatus == EnumSeatStatus.E
                                          select d).FirstOrDefault();

                    if (CheckMovieSeat != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Your ticket has been purchased. ");
                        CheckMovieSeat.SeatStatus = EnumSeatStatus.T;

                        bool checktrue = true;

                        while (checktrue)
                        {
                            Console.WriteLine("Thank you for using our system. Do you wish to continue? (Y/N)");
                            string userCon = Console.ReadLine();

                            if (userCon.ToUpper() == "Y")
                            {
                                checktrue = false;
                                Console.Clear();
                                UserInterface();
                            }
                            else if (userCon.ToUpper() == "N")
                            {
                                Console.Clear();
                                Console.WriteLine("Thank you for using our cinema ticket system");
                                Environment.Exit(0);
                                
                            }
                            else
                            {
                                Console.WriteLine("Invalid Options. Please try again.");
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("This seat is unavailable. Try again ");
                    }
                }
                
            }

        }

    }
}

