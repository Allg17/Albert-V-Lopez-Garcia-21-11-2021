using MiniProyectoALG.Models.SqLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiniProyectoALG
{
    public partial class App : Application
    {
        #region ConexionSqLite
        static ListaDeTareaDataBase Lista;
        public static ListaDeTareaDataBase ListaDeTareaDataBase
        {
            get
            {
                if (Lista == null)
                {
                    Lista = new ListaDeTareaDataBase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ListaTareasDataBase.db3"));

                }
                return Lista;
            }
        }
        #endregion
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
