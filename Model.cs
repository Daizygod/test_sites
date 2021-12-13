using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.ComponentModel;
//using Microsoft.VisualBasic.Devices;


namespace test_task_3
{
    public class Model
    {
        // глобальная переменная которая хранит последний изменённый список сайтов в строков типе
        public List<string> ViewListBoxText;
        // глобальная переменная которая хранит последний изменённый список сайтов в типе класса sites
        public List<Sites> sitesToListbox;

        public bool not_changed = true; // переменная для хранения статуса изменения файла
        public BackgroundWorker bw;
        public List<String> update_sites_list() // метод получения списка сайтов в строковом формате из файла
        {
            List<String> lstSites = new List<String>();
            int count = 1;
            String line;

            try
            {
                StreamReader sr = new StreamReader("files/sites.txt");
                line = sr.ReadLine();

                while (line != null)
                {
                    string phrase = line;
                    bool siteCheck;
                    string[] words = phrase.Split(' ');
                    bool delay_parse = int.TryParse(words[2], out int delayint);
                    if (!delay_parse)
                    {
                        delayint = 60;

                    }
                    if (words[1].StartsWith("http://") || words[1].StartsWith("https://"))
                    {
                        siteCheck = testSite(words[1]);
                    } else
                    {
                        siteCheck = false;
                    }
                    

                    lstSites.Add("id: " + count.ToString() + " | Наименование: " + words[0] + " | Доступность: " + siteCheck.ToString() + " | Интервал проверки: " + words[2] + " | URL: " + words[1]);
                    
                    line = sr.ReadLine();
                    count++;
                }
                sr.Close();
                return lstSites;
            }
            catch
            {
                MessageBox.Show("Ошибка c обновлением данных из текстового файла");
                return lstSites;
            }
        }
        public List<Sites> sites_list() // метод получения списка сайтов в фомате класса sites из файла
        {
            List<Sites> model_sites = new List<Sites>();
            Sites St = new Sites();
            int count = 1;
            String line, name, url; 
            int delayed;
            bool checks; 
            try
            {
                StreamReader sr = new StreamReader("files/sites.txt");

                line = sr.ReadLine();

                while (line != null)
                {
                    string phrase = line; 
                    string[] words = phrase.Split(' ');
                    name = words[0];
                    url = words[1];//----------------------------------------
                    bool delay_parse = int.TryParse(words[2], out int delayint);
                    if (delay_parse)
                    {
                        delayed = delayint; //----------------------------------------
                    }
                    else
                    {
                        delayed = 60; //----------------------------------------
                    }

                    if (words[1].StartsWith("http://") || words[1].StartsWith("https://"))
                    {
                        checks = testSite(url); //----------------------------------------
                    }
                    else
                    {
                        checks = false; //----------------------------------------
                    }
                    //model_sites.Add(St);
                    model_sites.Add(new Sites() { siteID = count, siteName = name, siteURL = url, siteDelay = delayed, siteCheck = checks});
                    //parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });

                    line = sr.ReadLine();
                    count++;
                }
                sr.Close();
                return model_sites;
            }
            catch
            {
                MessageBox.Show("Ошибка c обновлением данных из текстового файла");
                return model_sites;
            }
        }
        public bool testSite(string url) // метод для поверки доступности сайта
        {
            Uri uri = new Uri(url);
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }
        

        public void delete_site(List<Sites> sites, int index) // метод для удаления сайта из текстового файла
        {
            try
            {
                List<string> pastFile = read_file();
                pastFile.RemoveAt(index);
                System.IO.File.WriteAllLines("files/sites.txt", pastFile);
                MessageBox.Show("Удалено");
            }
            catch
            {
                MessageBox.Show("Ошибка удаления из файла");
            }

        }
        public bool add_site(string name, string url, string delay) // метод добавления сайта в текстовый файл
        {
            try
            {
                List<string> pastFile = read_file();
                bool parsed = int.TryParse(delay, out int delayParse);
                if (parsed)
                {
                    if (delayParse > 30000 || delayParse <= 9)
                    {
                        delay = "60";
                    } else
                    {
                        delay = delayParse.ToString();
                    }
                } else delay = "60";

                if (url != "")
                {
                    if (!(url.StartsWith("http://") || url.StartsWith("https://") || url == null))
                    {
                        url = "https://google.com";
                    }
                } else
                {
                    url = "https://google.com";
                }

                pastFile.Add(name + " " + url + " " + delay);
                System.IO.File.WriteAllLines("files/sites.txt", pastFile);
                return true;
            }
            catch 
            {
                MessageBox.Show("Ошибка записи в файл");
                return false;
                
            }
        }
        List<string> read_file() { // метод для получения списка сайтов в строковом типе

            List<String> lstSites = new List<String>();
            String line;

            try
            {
                StreamReader sr = new StreamReader("files/sites.txt");
                line = sr.ReadLine();

                while (line != null)
                {
                    lstSites.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                return lstSites;
            }
            catch
            {
                MessageBox.Show("Ошибка c чтением данных из текстового файла");
                return lstSites;
            }

        }
        // метод изменения сайта в текстовом файле
        public bool update_site(List<Sites> sites, int index, string name, string url, string delay) 
        {
            try
            {
                List<string> pastFile = read_file();
                Sites site = sites.ElementAt(index);
                if (delay != "")
                {
                    bool parsed = int.TryParse(delay, out int delayParse);
                    if (parsed)
                    {
                        if (delayParse > 30000 || delayParse <= 9)
                        {
                            delay = "60";
                        }
                        else delay = delayParse.ToString();
                    }
                    else delay = "60";
                }
                else delay = site.siteDelay.ToString();
                if (url != "")
                {
                    if (!(url.StartsWith("http://") || url.StartsWith("https://") || url == null))
                    {
                        url = "https://google.com";
                    }
                } else
                {
                    url = site.siteURL.ToString();
                }
                if (name == "")
                {
                    name = site.siteName;
                }
                pastFile[index] = name + " " + url + " " + delay;
                System.IO.File.WriteAllLines("files/sites.txt", pastFile);
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка записи в файл");
                return false;

            }
        }
    }
}
