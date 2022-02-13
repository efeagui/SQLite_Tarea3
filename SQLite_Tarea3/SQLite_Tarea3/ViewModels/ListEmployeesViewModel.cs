using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite_Tarea3.Config;
using SQLite_Tarea3.Models;
using SQLite_Tarea3.Views;
using Xamarin.Forms;

namespace SQLite_Tarea3.ViewModels
{

    class ListEmployeesViewModel : BaseViewModel
    {
        public Command DeleteCommand { get; }
        public Command EditCommand { get; }
        public Command AddCommand { get; }
        ObservableCollection<Employees> listEmployees;

        bool isEmpty;
        bool isNotEmpty;
        string foto;
        public bool IsEmpty { get => isEmpty; set { SetProperty(ref isEmpty, value); } }
        public bool IsNotEmpty { get => isNotEmpty; set { SetProperty(ref isNotEmpty, value); } }

        public ObservableCollection<Employees> ListEmployees { get => listEmployees; set { SetProperty(ref listEmployees, value); } }

        public string Foto { get => foto; set { SetProperty(ref foto, value); } }

        Page Page;
        public ListEmployeesViewModel(Page page)
        {
            Page = page;
            Constants.WasChange = true;//Se inicializa como true para que cargue los datos de la lista

            DeleteCommand = new Command(async (employeeSelected) => {
                var employee = employeeSelected as Employees;
                bool canDelete = await Page.DisplayAlert("Advertencia", "¿Seguro desea eliminar a " + employee.Nombre + " " + employee.Apellido + "?", "Aceptar", "Cancelar");
                if (canDelete)
                {
                    int res = await DataBase.CurrentDB.DeleteEmployee((Employees)employeeSelected);
                    if (res == Constants.SUCCESS)
                        ListEmployees.Remove((Employees)employeeSelected);
                    Load();
                }

            });
            EditCommand = new Command(async (employeeSelected) => {
                var employee = employeeSelected as Employees;
                UserDialogs.Instance.ShowLoading("Cargando");
                await Page.Navigation.PushAsync(new EmployesPage(employee));
                UserDialogs.Instance.HideLoading();
            });
            AddCommand = new Command(async () => {
                await Page.Navigation.PushAsync(new EmployesPage(new Employees()));
            });
            //Este metodo se manda a llamar en el on appearing del ListEmployees.xaml.cs
            //Load();

        }


        public async void Load()
        {
            Title = "Personas";
            Foto = "lista_vacia.png";

            int count = await DataBase.CurrentDB.GetEmpleyeeCount();
            if (count > 0)
            {
                if (Constants.WasChange)//Cargara la lista solo cuanda hay sucesido un cambio
                {
                    var list = await DataBase.CurrentDB.GetAllEmployees();
                    ListEmployees = new ObservableCollection<Employees>(list);
                    Constants.WasChange = false;//Se resetean los cambios
                }
                IsEmpty = false;
                IsNotEmpty = !IsEmpty;
            }
            else
            {

                IsEmpty = true;
                IsNotEmpty = !IsEmpty;

            }



        }

    }
}
