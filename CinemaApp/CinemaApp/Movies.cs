using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp
{
    public partial class Movies
    {
        public Guid Id { get; set; }
        public int Mid { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool MovieAvailability { get; set; }
    }
}
