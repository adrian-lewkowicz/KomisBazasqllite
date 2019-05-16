using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomisBazasqllite
{
    class Cars
    {
        
        private int Id;
        private string marka;
        private string model;
        private int rok;

        public Cars()
        {
        }

        public int Id1 { get => Id; set => Id = value; }
        public string Marka { get => marka; set => marka = value; }
        public string Model { get => model; set => model = value; }
        public int Rok { get => rok; set => rok = value; }
    }
}
