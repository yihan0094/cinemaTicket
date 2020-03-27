using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaApp
{
    class Cinema
    {
        public ICollection<Account> User { get; set; }
        public ICollection<Movies> Movie { get; set; }
        public ICollection<Halls> Halls { get; set; }
        public ICollection<MovieHall> MovieHalls { get; set; }

        public Cinema()
        {
            User = new List<Account>();
            Movie = new List<Movies>();
            MovieHalls = new List<MovieHall>();
            Halls = new List<Halls>();
        }

        public void GenerateUser()
        {
            User.Add(new Account() {
                Id = Guid.NewGuid(),
                Username = "nicholas",
                Password = "123123"
            });

            User.Add(new Account()
            {
                Id = Guid.NewGuid(),
                Username = "alex",
                Password = "456456"
            });
        }

        public void GenerateMovie()
        {
            Movie.Add(new Movies() {
                Id = Guid.NewGuid(),
                Mid = 101,
                Title = "Bruce Almighty",
                ReleaseDate = new DateTime(2020, 3, 10, 0, 0, 0),
                MovieAvailability = true
            });

            Movie.Add(new Movies()
            {
                Id = Guid.NewGuid(),
                Mid = 102,
                Title = "Ashfall",
                ReleaseDate = new DateTime(2020, 3, 15, 0, 0, 0),
                MovieAvailability = true 
            });

            Movie.Add(new Movies()
            {
                Id = Guid.NewGuid(),
                Mid = 103,
                Title = "Train to Busan",
                ReleaseDate = new DateTime(2020, 3, 30, 0, 0, 0),
                MovieAvailability = false
            });

            Movie.Add(new Movies()
            {
                Id = Guid.NewGuid(),
                Mid = 104,
                Title = "Tom & Jerry",
                ReleaseDate = new DateTime(2020, 3, 26, 0, 0, 0),
                MovieAvailability = true
            });

        }


        public void GenerateMovieHall()
        {
            MovieHalls.Add(new MovieHall(){
                Id = 1,
                HallId = 001,
                MovieId = 101,
                ShowTime = new DateTime(2020,3,26,08,00,00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 2,
                HallId = 002,
                MovieId = 101,
                ShowTime = new DateTime(2020, 3, 27, 10, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 3,
                HallId = 003,
                MovieId = 101,
                ShowTime = new DateTime(2020, 3, 27, 12, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 4,
                HallId = 004,
                MovieId = 102,
                ShowTime = new DateTime(2020, 3, 27, 08, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 5,
                HallId = 005,
                MovieId = 102,
                ShowTime = new DateTime(2020, 3, 27, 13, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 6,
                HallId = 005,
                MovieId = 104,
                ShowTime = new DateTime(2020, 3, 27, 16, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 7,
                HallId = 006,
                MovieId = 104,
                ShowTime = new DateTime(2020, 3, 27, 16, 00, 00)
            });

            MovieHalls.Add(new MovieHall()
            {
                Id = 8,
                HallId = 007,
                MovieId = 104,
                ShowTime = new DateTime(2020, 3, 27, 19, 00, 00)
            });
        }

        public void GenerateSeats()
        {
            EnumSeatStatus seatstatus;

            foreach (var item in MovieHalls)
            {
                for (int i = 1; i <= 3; i++)
                {
                    for (int x = 1; x <= 10; x++)
                    {
                        Random rng = new Random();
                        Thread.Sleep(1);
                        int random = rng.Next(0, 10);

                        if (random > 3)
                        {
                            seatstatus = EnumSeatStatus.E;
                        }
                        else
                        {
                            seatstatus = EnumSeatStatus.T;
                        }

                        Thread.Sleep(1);
                        Halls.Add(new Halls() { HallNo = item.HallId, TotalRow = i, TotalColumn = x, Seats = i + "," + x, SeatStatus = seatstatus });
                    }
                }
            }
        }
    }
}
