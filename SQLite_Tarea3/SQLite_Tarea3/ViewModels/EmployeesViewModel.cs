using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite_Tarea3.Config;
using SQLite_Tarea3.Models;
using Xamarin.Forms;

namespace SQLite_Tarea3.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }

        int idEmployee;
        string nombre;
        string apellido;
        string edad;
        string correo;
        string direccion;
        string titlePicker;

       
        public int IdEmployee { get => idEmployee; set { SetProperty(ref idEmployee, value); } }
        public string Nombre { get => nombre; set { SetProperty(ref nombre, value); } }
        public string Apellido { get => apellido; set { SetProperty(ref apellido, value); } }
        public string Edad { get => edad; set { SetProperty(ref edad, value); } }
        public string Correo { get => correo; set { SetProperty(ref correo, value); } }
        public string Direccion { get => direccion; set { SetProperty(ref direccion, value); } }
        public string TitlePicker { get => titlePicker; set { SetProperty(ref titlePicker, value); } }

        Page Page;
        Employees Employee;
        public EmployeesViewModel(Page page, Employees employee)
        {

            Page = page;
            Employee = employee;

            LoadData();
            SaveCommand = new Command(async () => {
                if (Validate())
                {
                    CargarDatos();
                    UserDialogs.Instance.ShowLoading("Guardando");
                    int respuesta = await DataBase.CurrentDB.SaveEmployee(Employee);
                    if (respuesta == Constants.SUCCESS)
                    {
                        Constants.WasChange = true; //Variable bandera para determinar si se realizo un cambio
                        await Page.DisplayAlert("Información", "Guardado con éxito.", "Aceptar");
                        await Page.Navigation.PopAsync();
                    }
                    else
                        await Page.DisplayAlert("Información", "Error al guardar.", "Aceptar");
                    UserDialogs.Instance.HideLoading();
                }
                else
                    await Page.DisplayAlert("Advertensia", "Debe llenar todos los campos.", "Aceptar");
            });
        }

        void LoadData()
        {

            if (Employee.IdEmployee > 0)
            {
                Title = "Actualizar empleado";
                Nombre = Employee.Nombre;
                Apellido = Employee.Apellido;
                Edad = Employee.Edad;
                Correo = Employee.Correo;
                Direccion = Employee.Direccion;
            }
            else
            {
                Title = "Añadir persona";
            }
        }

        bool Validate()
        {
            if (!string.IsNullOrEmpty(Nombre)
                && !string.IsNullOrEmpty(Apellido) 
                && !string.IsNullOrEmpty(Edad) 
                && !string.IsNullOrEmpty(Correo) 
                && !string.IsNullOrEmpty(Direccion))
                return true;
            else
                return false;
        }
        void CargarDatos()
        {
            Employee.Nombre = Nombre;
            Employee.Apellido = Apellido;
            Employee.Edad = Edad;
            Employee.Correo = Correo; 
            Employee.Direccion = Direccion;
        }
    }

}
