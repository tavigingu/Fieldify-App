using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldify_App.Models
{
    internal class BookingDetailsPageModel
    {
        public static List<string> GetBookedHours(int fieldId, DateTime selectedDate)
        {
            try
            {
                using (var context = new FieldifyDataDataContext())
                {
                    var bookedHours = context.Programares
                        .Where(p => p.TerenId == fieldId &&
                                    p.DataProgramare == selectedDate.Date &&
                                    p.StatusProgramare == "Confirmata" 
                                    )
                        .Select(p => new
                        {
                            p.OraInceput,
                            p.OraSfarsit
                        })
                        .ToList()
                        .Select(p => $"{p.OraInceput:hh\\:mm} - {p.OraSfarsit:hh\\:mm}") // Format în memorie
                        .ToList();

                    return bookedHours;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la obținerea orelor rezervate: {ex.Message}");
                return new List<string>();
            }
        }

        // Salvează o programare nouă
        public static bool SaveBooking(int fieldId, DateTime bookingDate, TimeSpan startTime, TimeSpan endTime, int userId)
        {
            try
            {
                using (var context = new FieldifyDataDataContext())
                {
                    var programare = new Programare
                    {
                        TerenId = fieldId,
                        DataProgramare = bookingDate.Date,
                        OraInceput = startTime,
                        OraSfarsit = endTime,
                        UserId = userId,
                        StatusProgramare = "Confirmata"
                    };

                    context.Programares.InsertOnSubmit(programare);
                    context.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea programării: {ex.Message}");
                return false;
            }
        }
    }
}
