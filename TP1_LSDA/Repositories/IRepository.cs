using System.Collections.Generic;
using System.Data;

namespace TP1_LSDA.Repositories
{
    /// <summary>
    /// Contrat générique d'accès aux données.
    /// Mode connecté : GetAll, GetById, Add, Update, Delete (SqlCommand + SqlDataReader).
    /// Mode déconnecté : GetDataSet, SaveChanges (SqlDataAdapter + DataSet + SqlCommandBuilder).
    /// </summary>
    public interface IRepository<T> where T : class
    {
        // ----- Mode connecté -----
        List<T> GetAll();
        T GetById(int id);
        int Add(T entity);
        bool Update(T entity);
        bool Delete(int id);

        // ----- Mode déconnecté -----
        DataSet GetDataSet();
        int SaveChanges(DataSet dataSet);
    }
}
