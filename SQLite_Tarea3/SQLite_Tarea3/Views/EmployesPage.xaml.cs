using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite_Tarea3.Models;
using SQLite_Tarea3.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLite_Tarea3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployesPage : ContentPage
    {
        public EmployesPage(Employees employee)
        {
            InitializeComponent();
            BindingContext = new EmployeesViewModel(this, employee);
        }
    }
}