using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WpfApp1
{
    public class DataClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string opis { get; set; }
        public DateTime timeEnding { get; set; }

        public int broj { get; set; }

        public override string ToString()
        {
            return $"{opis} - {timeEnding}";
        }
    }
}
