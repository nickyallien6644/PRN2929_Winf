using PRN292_LapTopSaleSystemWF_Group4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN292_LapTopSaleSystemWF_Group4.Validate
{
    class AuthenticationValidate
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        public Boolean checkEmail(String input)
        {
            try
            {
                var format = new System.Net.Mail.MailAddress(input);
                return format.Address == input;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public User checkUserExist(String email)
        {
            var users = db.Users.Where(u => u.Email == email);
            foreach(var user in users)
            {
                if(user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }

        public Boolean checkPhone(String input)
        {
                String phonematch = @"^(\d{3})[ -]?(\d{3})[ -]?(\d{4}) x(\d*)";
                if(Regex.Match(input, phonematch).Success)
                {
                    return true;
                }
                return false;
        }

        public Boolean checkPassword(String password)
        {       
            string rex = @"^(?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[^\da - zA - Z]).{ 8,15}$"; //match digits

            if(Regex.Match(password, rex).Success)
            {
                return true;
            }
            return false;
           
        }
    }
}
