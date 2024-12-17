using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldify_App.Models
{
    internal class MyProfilePageModel
    {
        public static void SaveUserDetails(User updatedUser)
        {
            using (var context = new FieldifyDataDataContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == updatedUser.Username);

                if (user != null)
                {
                    user.Email = updatedUser.Email;
                    user.Nume = updatedUser.Nume;
                    user.Prenume = updatedUser.Prenume;
                    user.Description = updatedUser.Description;
                    user.NumarTelefon = updatedUser.NumarTelefon;

                    context.SubmitChanges(); // Salvează modificările în baza de date
                }
            }
        }
    }
}
