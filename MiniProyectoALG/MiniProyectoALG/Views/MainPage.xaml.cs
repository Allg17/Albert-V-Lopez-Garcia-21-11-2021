using MiniProyectoALG.Models;
using MiniProyectoALG.ViewModels;
using MiniProyectoALG.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiniProyectoALG
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            CurrentPageChanged += MainPage_CurrentPageChanged;
        }

        private void MainPage_CurrentPageChanged(object sender, EventArgs e)
        {
            if (((TabbedPage)sender).CurrentPage.Title == ((MainPageViewModel)BindingContext).Page2Name)
            {
                ((MainPageViewModel)BindingContext).RefrescarListaCommand.Execute(null);
            }
        }

        private void Ondelete(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            var id = item.CommandParameter;
            ((MainPageViewModel)BindingContext).DeleteCommand.Execute(id);
        }

        private void OnEdit(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            var id = item.CommandParameter;
            ((MainPageViewModel)BindingContext).EditarCommand.Execute(id) ;
        }
    }
}
