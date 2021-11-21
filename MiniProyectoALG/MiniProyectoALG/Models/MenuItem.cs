using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProyectoALG.Models
{
    public enum MenuItemType
    {
        Home,
        Tarea,
        VerTareas
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
