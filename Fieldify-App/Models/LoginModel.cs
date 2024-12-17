using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldify_App.Models
{
    internal class LoginModel
    {
        
        public static User ValidateUser(string email, string password)
        {
            using (FieldifyDataDataContext context = new FieldifyDataDataContext())
            {
                // Caută utilizatorul în baza de date
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Parola == password);

                if (user != null)
                {
                    return user; // Populează CurrentUser dacă utilizatorul este găsit
                    
                }

                return null; // Returnează false dacă utilizatorul nu este găsit
            }
        }
    }
}
