using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProyectoALG.Models
{
    [Table("ListaDeTarea")]
   public class ListaDeTarea
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string NombreTarea { get; set; }
        public string Descripcion { get; set; }
        public bool Completada { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaVence { get; set; }
        public ListaDeTarea()
        {
            Fecha = DateTime.Now;
            FechaVence = DateTime.Now;
        }
    }
}
