using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoALG.Models.SqLite
{
    public class ListaDeTareaDataBase
    {
        public readonly SQLiteAsyncConnection database;

        public ListaDeTareaDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ListaDeTarea>().Wait();
            database.ExecuteScalarAsync<string>("PRAGMA auto_vacuum = FULL");
        }

        public void Save(ListaDeTarea tarea)
        {
            if (tarea.ID > 0)
            {
                database.UpdateAsync(tarea);
            }
            else
                database.InsertAsync(tarea);
        }

        public async Task<List<ListaDeTarea>> GetLista(bool completado)
        {
            return await database.Table<ListaDeTarea>().Where(x => x.Completada == completado).ToListAsync();
        }

        public async Task<List<ListaDeTarea>> GetAll()
        {
            return await database.Table<ListaDeTarea>().ToListAsync();
        }

        public async Task<int> DeleteTableAsync(ListaDeTarea Tarea)
        {
            var res = await database.DeleteAsync(Tarea);
            await database.ExecuteAsync($"VACUUM");
            return res;
        }

        public async Task<int> DeleteTableAsync()
        {
            var res = await database.ExecuteAsync($"DELETE FROM ListaDeTarea");
            await database.ExecuteAsync($"VACUUM");
            return res;
        }
    }
}
