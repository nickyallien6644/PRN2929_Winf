using PRN292_LapTopSaleSystemWF_Group4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN292_LapTopSaleSystemWF_Group4.DAO
{
    public class UserDAO
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();

        public User login(String email, String input)
        {
            String password = MD5Hash(input);
            var users = db.Users.Where(u => u.Email == email && u.Password == password && u.Role == "Admin");
            foreach (User u in users)
            {
                return u;
            }
            return null;
        }

        

        public Boolean registration(String fullname, String password, String email, String phone, String address, String img, String role)
        {
            try
            {
                User user = new User
                {
                    Fullname = fullname,
                    Password = MD5Hash(password),
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Image = img,
                    Role = role,
                    Active = true
                };

                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }
        public Boolean update(int id, String name, String password, string email, string phone, string address, string image, string role, bool active)
        {
            try
            {
                var users = db.Users.First(a => a.ID == id);
                users.Fullname = name;
                users.Password = password;
                users.Email = email;
                users.Phone = phone;
                users.Address = address;
                users.Image = image;
                users.Role = role;
                users.Active = active;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        // delte user 
        public Boolean delete(int id)
        {
            bool check = false;
            var users = db.Users.Where(a => a.ID == id);
            foreach (var user in users)
            {
                if (user.ID == id)
                {
                    db.Users.Remove(user);
                    check = true;
                }
            }
            db.SaveChanges();
            return check;
        }
        public String MD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
