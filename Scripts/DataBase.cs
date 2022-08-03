using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW_Manager.Scripts
{
    public class DataBase
    {
        Crypto crypto = new Crypto();
        string mainPath = Environment.ExpandEnvironmentVariables("%AppData%\\PwManager");
        string[] paths = { Environment.ExpandEnvironmentVariables("%AppData%\\PwManager") + @"\Passwords.dat", Environment.ExpandEnvironmentVariables("%AppData%\\PwManager") + @"\Notes.dat", Environment.ExpandEnvironmentVariables("%AppData%\\PwManager") + @"\Contacts.dat" };
        string settingPath = Environment.ExpandEnvironmentVariables("%AppData%\\PwManager") + @"\User.dat";

        public void FolderExists(){
            
            if (!Directory.Exists(mainPath)) Directory.CreateDirectory(mainPath);
        }


        public async void Save2d(int _pathIndex, List<List<String>> toSave, string _key)
        {
            string path = paths[_pathIndex];
            List<String> _temp = new List<String>();

            for (int i = 0; i < toSave.Count(); i++)
            {
                for (int j = 0; j < toSave[i].Count; j++)
                {
                    _temp.Add(crypto.EncryptData(toSave[i][j], _key));
                }
                _temp.Add("");
            }
            await File.WriteAllLinesAsync(path, _temp);
        }

        public List<List<String>> Load2d(int _pathIndex, string _key)
        {
            string path = paths[_pathIndex];
            if (!File.Exists(path))
            {
                File.Create(path);
                return new List<List<string>>();
            }

            List<List<String>> _list = new List<List<string>>();
            String[] pws = File.ReadAllLines(path);
            List<String> _temp = new List<String>();

            for (int i = 0; i < pws.Length; i++)
            {
                if (pws[i] != String.Empty)
                {
                    _temp.Add(crypto.DecryptData(pws[i], _key));
                }
                else
                {
                    _list.Add(_temp);
                    _temp = new List<String>();
                }
            }
            return _list;
        }

        public List<String> InitialLoad()
        {
            if (!File.Exists(settingPath))
            {
                File.Create(settingPath);
                return new List<String>();
            }
            return File.ReadAllLines(settingPath).ToList();
        }

        public async void saveUser(List<String> _user)
        {
            await File.WriteAllLinesAsync(settingPath, _user);
        }
    }

}