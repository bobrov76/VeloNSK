using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.HelpClass
{
    public class Lincs
    {
        public string Folder_Strings()//Ссылка на папку ресурсов
        {        
            return @"C:\Users\Yulia\Desktop\VeloAPI\resours\";
        }
        public string Images_Folder_Strings()//Ссылка на папку С картинеками
        {
            return Folder_Strings() + "ImagesGaleri";
        }
        public string Users_Folder_Strings()//Ссылка на папку Users
        {
            return Folder_Strings() + "Users";
        }
        public string Users_User_Folder_Strings()//Ссылка на папку Users
        {
            return Folder_Strings() + @"Users\User";
        }



        public string Server_Strings()//Ссылка на API
        {
            ////https://localhost:44384/api/
            return "http://90.189.158.10/";
        }
        public string API_Strings()//Ссылка на API
        {
            return Server_Strings() + "api/";
        }
        public string API_Images_Strings()//Ссылка на API Images
        {
            return Server_Strings() + "ImagesGaleri/";
        }
    }
}
