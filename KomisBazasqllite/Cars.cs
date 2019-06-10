using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomisBazasqllite
{
    class Car
    {

        public Car()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Marka { get; set; }
        [MaxLength(30)]
        public string Model { get; set; }
        [MaxLength(5)]
        public int Rok { get; set; }
    }
}
