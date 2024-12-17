using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fieldify_App.Models
{
    internal class MyBookingsPageModel
    {

        public static List<Programare> GetBookingsByUserEmail(string email, bool isAdmin)
        {
            using (var dbContext = new FieldifyDataDataContext())
            {
                if (isAdmin)
                {

                    var bookingsData = dbContext.Programares
                        .ToList();

                    StringBuilder message = new StringBuilder();

                    // Adăugăm informațiile terenului și username-ul direct la programări
                    foreach (var programare in bookingsData)
                    {
                        // Căutăm terenul asociat pentru fiecare programare
                        var teren = dbContext.Terens
                            .FirstOrDefault(t => t.Id == programare.TerenId);

                        // Căutăm utilizatorul asociat programării pentru a adăuga username-ul
                        var userName = dbContext.Users
                            .Where(u => u.Id == programare.UserId)
                            .Select(u => u.Username)
                            .FirstOrDefault();

                        // Dacă găsim terenul, îl asociem cu programarea
                        if (teren != null)
                        {
                            programare.Teren = teren;
                            programare.Pret = teren.Pret;

                            // Adăugăm terenul la programare
                        }

                        // Dacă găsim username-ul, îl adăugăm la programare
                        if (!string.IsNullOrEmpty(userName))
                        {
                            programare.UserName = userName; // Adăugăm username-ul la programare
                        }


                    }

                    return bookingsData;
                }
                else
                {
                    // Căutăm utilizatorul pe baza emailului
                    var user = dbContext.Users.FirstOrDefault(u => u.Email == email);

                    if (user == null)
                    {
                        Console.WriteLine($"Nu există niciun utilizator cu email-ul: {email}");
                        return new List<Programare>();
                    }

                    // Obținem toate programările pentru utilizatorul respectiv
                    var bookingsData = dbContext.Programares
                        .Where(p => p.UserId == user.Id)
                        .ToList();

                    StringBuilder message = new StringBuilder();

                    // Adăugăm informațiile terenului direct la programări (fără a crea un DTO)
                    foreach (var programare in bookingsData)
                    {
                        // Căutăm terenul asociat pentru fiecare programare
                        var teren = dbContext.Terens
                            .FirstOrDefault(t => t.Id == programare.TerenId);

                        //message.AppendLine($"Programare Id: {programare.Id}, OraInceput: {programare.OraInceput}, OraSfarsit: {programare.OraSfarsit}");

                        if (teren != null)
                        {
                            programare.Teren = teren; // Adăugăm terenul la programare
                            programare.Pret = teren.Pret;
                        }

                        if (programare.OraInceput == TimeSpan.MinValue)
                            programare.OraInceput = TimeSpan.Zero; // sau altă valoare implicită
                        if (programare.OraSfarsit == TimeSpan.MinValue)
                            programare.OraSfarsit = TimeSpan.Zero; // sau altă valoare implicită
                    }

                     return bookingsData;
                }
            }
        }



        public static bool SetBookingStatusToCancelled(int bookingId)
        {
            try
            {
                using (var dbContext = new FieldifyDataDataContext())
                {
                    // Găsește programarea după ID
                    var programare = dbContext.Programares.FirstOrDefault(p => p.Id == bookingId);

                    if (programare == null)
                        return false;

                    // Actualizează statusul
                    programare.StatusProgramare = "Anulata";

                    // Salvează modificările
                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la anularea programării: {ex.Message}");
                return false;
            }
        }

    }
}
