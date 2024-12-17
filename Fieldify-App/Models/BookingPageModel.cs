using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldify_App.Models
{
    internal class BookingPageModel
    {
        public static List<Teren> GetFields()
        {

                using (var dbContext = new FieldifyDataDataContext())
                {
                    // Executăm interogarea pentru a obține date brute
                    var fieldsData = dbContext.Terens.ToList();

                    // Construim obiectele `Teren` manual în memorie
                    var fields = fieldsData.Select(t => new Teren
                    {
                        Id = t.Id,
                        Nume = t.Nume,
                        Pret = t.Pret,
                        Detalii = t.Detalii,
                        TipuriTeren = new TipuriTeren
                        {
                            Nume = t.TipuriTeren.Nume
                        }
                    }).ToList();

                    return fields;
                }
        }

        public static bool DeleteField(int fieldId)
        {
            try
            {
                using (var context = new FieldifyDataDataContext())
                {
                    var field = context.Terens.FirstOrDefault(f => f.Id == fieldId);
                    if (field == null) return false;

                    context.Terens.DeleteOnSubmit(field);
                    context.SubmitChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
