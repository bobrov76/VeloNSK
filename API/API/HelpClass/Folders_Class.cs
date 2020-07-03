using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.HelpClass
{
    public class Folders_Class
    {
        public string Create_Directory(string path, string subpath)//Создание директории
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    return "Create_Directory_False";
                }
                else
                {
                    dirInfo.CreateSubdirectory(subpath);
                    return "Create_Directoryrue_True";
                }
            }
            catch { return "Create_Directory_Error"; }
        }
        public string Delete_Directory(string dirName)//Удаление директории 
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                dirInfo.Delete(true);
                return "Delete_Directory_True";
            }
            catch { return "Delete_Directory_Error"; }  
        }
    }
}
