using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite_Tarea3.Models;
using SQLite;

namespace SQLite_Tarea3.Config
{

    public class DataBase
    {
        readonly SQLiteAsyncConnection dataBase;
        private static DataBase instance { get; set; }
        private DataBase(string _dbPath)
        {
            dataBase = new SQLiteAsyncConnection(_dbPath);
            dataBase.CreateTableAsync<Employees>().Wait();
        }

        //Se crea un metodo para poder crear una sola instancia de la base de datos 
        public static DataBase CurrentDB
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "persona.db3"));
                }
                return instance;
            }
        }

        public Task<List<Employees>> GetAllEmployees()
        {
            return dataBase.Table<Employees>().ToListAsync();
        }
        public Task<int> GetEmpleyeeCount()
        {
            return dataBase.Table<Employees>().CountAsync();
        }

        public Task<Employees> GetEmployeById(int id)
        {
            return dataBase.Table<Employees>().Where(i => i.IdEmployee == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveEmployee(Employees employe)
        {
            if (employe.IdEmployee != 0)
            {
                return dataBase.UpdateAsync(employe);
            }
            else
            {
                return dataBase.InsertAsync(employe);
            }
        }

        public Task<int> DeleteEmployee(Employees employee)
        {
            return dataBase.DeleteAsync(employee);
        }
    }

}
