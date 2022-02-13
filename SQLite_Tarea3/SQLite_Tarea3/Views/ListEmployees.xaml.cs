using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite_Tarea3.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLite_Tarea3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEmployees : ContentPage
    {
        ListEmployeesViewModel EmployeesListViewModel;
        public ListEmployees()
        {
            InitializeComponent();
            EmployeesListViewModel = new ListEmployeesViewModel(this);
            BindingContext = EmployeesListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EmployeesListViewModel.Load();
        }
    }
}