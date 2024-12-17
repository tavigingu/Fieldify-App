using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldify_App.Models
{
    internal class AddFieldWindowModel
    {
        public static void AddField(string fieldName, string description, int price, string fieldType)
        {
            try
            {
                using (var context = new FieldifyDataDataContext()) // Replace with your DbContext
                {
                    // Găsim Tipul Terenului
                    var tipTeren = context.TipuriTerens.FirstOrDefault(t => t.Nume == fieldType);

                    if (tipTeren == null)
                    {
                        throw new Exception("Field type not found in the database.");
                    }

                    // Adăugăm noul teren
                    var newField = new Teren
                    {
                        Nume = fieldName,
                        TipId = tipTeren.Id,
                        Pret = price,
                        Detalii = description
                    };

                    context.Terens.InsertOnSubmit(newField);
                    context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the field: {ex.Message}");
            }
        }
    }
}
