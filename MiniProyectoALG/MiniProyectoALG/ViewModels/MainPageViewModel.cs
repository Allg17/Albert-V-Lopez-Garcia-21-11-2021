using MiniProyectoALG.Models;
using MiniProyectoALG.Utils;
using System;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace MiniProyectoALG.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        #region Propiedades
        public ListaDeTarea Tarea { get; set; }
        public ObservableCollection<ListaDeTarea> Lista { get; set; }
        public ObservableCollection<string> Estatus { get; set; }
        public int indexEstatus { get; set; }
        public Command CrearTareaCommand { get; set; }
        public Command LimpiarCommand { get; set; }
        public bool Limpiar { get; set; }
        public Command RefrescarListaCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command EditarCommand { get; set; }
        public string Page1Name { get; set; }
        public string Page2Name { get; set; }
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            Page1Name = "Crear Tarea";
            Page2Name = "Ver Tareas";
            Tarea = new ListaDeTarea();
            Estatus = new ObservableCollection<string>();
            Lista = new ObservableCollection<ListaDeTarea>();
            Comandos();
            cargar();
        }




        #endregion

        #region Funciones

        private void cargar()
        {
            Estatus.Add("Todas");
            Estatus.Add("Incompletas");
            Estatus.Add("Completas");
        }
        private void Comandos()
        {
            CrearTareaCommand = new Command(() =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    if (!string.IsNullOrEmpty(Tarea.Descripcion) && !string.IsNullOrEmpty(Tarea.NombreTarea))
                    {
                        Tarea.Fecha = DateTime.Now;
                        App.ListaDeTareaDataBase.Save(Tarea);
                        UserDialogs.Instance.Alert("Tarea Creada!", "Exito!", "Ok");
                        Limpiar = true;
                        LimpiarCommand.Execute(null);
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Llene todos los campos antes de crear la tarea", "Atencion!", "Ok");
                    }

                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, "Error!", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            });
            LimpiarCommand = new Command(async () =>
            {
                if (Limpiar == true)
                {
                    Tarea = new ListaDeTarea();
                }
                else
                {
                    bool Resultado = await UserDialogs.Instance.ConfirmAsync("Seguro que desea limpiar?", "Atencion!", "Si", "No");
                    if (Resultado)
                    {
                        Tarea = new ListaDeTarea();
                    }
                }
            });

            DeleteCommand = new Command<int>(async (id) =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    bool resultado = await UserDialogs.Instance.ConfirmAsync("Desea Borrar esta tarea?", "Atencion", "Si", "No");
                    if (resultado)
                    {
                        await App.ListaDeTareaDataBase.DeleteTableAsync(Lista.FirstOrDefault(x => x.ID == id));
                        UserDialogs.Instance.Alert("Tarea Eliminada!", "Exito!", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, "Error!", "Ok");
                }
                finally
                {
                    IsBusy = false;
                    RefrescarListaCommand.Execute(null);
                }
            });

            EditarCommand = new Command<int>(async (id) =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    bool resultado = await UserDialogs.Instance.ConfirmAsync("Desea marcar esta tarea como completada?", "Atencion", "Si", "No");
                    if (resultado)
                    {
                        ListaDeTarea task = Lista.FirstOrDefault(x => x.ID == id);
                        task.Completada = true;
                        App.ListaDeTareaDataBase.Save(task);

                    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, "Error!", "Ok");
                }
                finally
                {
                    IsBusy = false;
                    RefrescarListaCommand.Execute(null);
                }
            });

            RefrescarListaCommand = new Command(async () =>
            {

                try
                {
                    using (UserDialogs.Instance.Loading("Cargando.."))
                    {
                        List<ListaDeTarea> listaDes = new List<ListaDeTarea>();
                        switch (indexEstatus)
                        {
                            case 0:
                                listaDes = await App.ListaDeTareaDataBase.GetAll();
                                break;
                            case 1:
                                listaDes = await App.ListaDeTareaDataBase.GetLista(false);
                                break;
                            case 2:
                                listaDes = await App.ListaDeTareaDataBase.GetLista(true);
                                break;
                            default:
                                break;
                        }
                        Lista.Clear();
                        foreach (var item in listaDes)

                        {
                            Lista.Add(item);
                        }
                    }

                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, "Error!", "Ok");
                }

            });
        }
        #endregion
    }
}
