using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace test_task_3
{
    public class Presenter
    {
        Model myModel;
        Form1 form1;
        public Presenter(Form1 m) {
            form1 = m;
            myModel = new Model();
            form1.updateEvent += update_handler;
            form1.addSiteEvent += addSite_handler;
            form1.updateSiteEvent += updateSite_handler;
            form1.deleteSiteEvent += deleteSite_handler;
            form1.selectListEvent += selectList_handler;
        }
        // вызов метода при загузке формы
        async void update_handler(object sender, EventArgs e)
        {
            update_list();
            updateListbox_handler(null, null);
        }
        // метод обновл€ющий глобальные переменные и начинает асинхронное обновление доступности
        public async void update_list()
        {
            // обновление глобальных переменных
            myModel.ViewListBoxText = myModel.update_sites_list();
            myModel.sitesToListbox = myModel.sites_list();

            // вызов асинхронных методов дл€ обновлени€ доступности
            async_updating(myModel.sitesToListbox, myModel.ViewListBoxText);
        }
        // метод обновл€ющий список в элементе listbox
        void updateListbox_handler(object sender, EventArgs e)
        {
            form1.lstbox.DataSource = null;
            form1.lstbox.DataSource = myModel.ViewListBoxText;
        }
        // метод добавлени€ сайта в список
        void addSite_handler(object sender, EventArgs e)
        {
            change_file();
            myModel.add_site(form1.nameTextbox.Text.ToString(), form1.urlTextbox.Text.ToString(), form1.delayTextbox.Text.ToString());
            form1.nameTextbox.Text = null;
            form1.delayTextbox.Text = null;
            form1.urlTextbox.Text = null;
            change_file();
            after_update_file();
        }
        // мметод обновл€ющий элемент в списке
        void updateSite_handler(object sender, EventArgs e)
        {
            change_file();
            myModel.update_site(myModel.sitesToListbox, form1.lstbox.SelectedIndex, form1.nameTextbox.Text.ToString(), form1.urlTextbox.Text.ToString(), form1.delayTextbox.Text.ToString());
            MessageBox.Show(form1.lstbox.SelectedIndex.ToString());
            change_file();
            after_update_file();
        }
        // метод обновл€ющий listbox и начинающий асинхронное обновление
        void after_update_file()
        {
            myModel.ViewListBoxText = myModel.update_sites_list();
            myModel.sitesToListbox = myModel.sites_list();
            updateListbox_handler(null, null);
            async_updating(myModel.sitesToListbox, myModel.ViewListBoxText);
        }
        // метод удалени€ элемента из списка
        void deleteSite_handler(object sender, EventArgs e)
        {
            change_file();
            myModel.delete_site(myModel.sitesToListbox, form1.lstbox.SelectedIndex);
            change_file();
            after_update_file();
        }
        void selectList_handler(object sender, EventArgs e)
        {
            
        }
        // метод вызывающий асинхронные методы дл€ обновлени€ доступности каждого сайта
        public async void async_updating(List<Sites> sites, List<string> listsites)
        {
            int countOfIndex = sites.Count - 1;
            int list_index = sites.Count;
            MessageBox.Show("‘оновое обновление успешно запущено");
            while (countOfIndex >= 0)
            {
                await Task.Run(() => {

                    myModel.bw = new BackgroundWorker();
                    Sites site = sites.ElementAt(countOfIndex);
                    countOfIndex--;
                    list_index--;
                    string urls = site.siteURL;
                    int delay = site.siteDelay;

                    myModel.bw.DoWork += (obj, ea) => TasksAsync(listsites, urls, list_index, delay);
                    myModel.bw.RunWorkerAsync();
                });
            }
        }
        // метод измен€ющий переменную котора€ содержит информацию измен€етс€ ли сейчас файл
        public void change_file()
        {
            myModel.not_changed = !myModel.not_changed;
        }
        // асинхонный метод который обновл€ет доступность сайта пока файл не изменитс€
        private async void TasksAsync(List<string> sitesListBox, string url, int index, int delay) 
        {
            await Task.Run(() =>
            {
                while (myModel.not_changed)
                {
                    bool enable;
                    string prev_string = sitesListBox[index];
                    Uri uri = new Uri(url);
                    try
                    {
                        HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        enable = true;
                    }
                    catch
                    {
                        enable = false;
                    }

                    if (prev_string.Contains("True") && enable == false)
                    {
                        prev_string = sitesListBox[index].Replace("True", "False");
                        myModel.ViewListBoxText[index] = prev_string;
                        form1.lstbox.Invoke((MethodInvoker)delegate {
                            form1.lstbox.DataSource = null;
                            form1.lstbox.DataSource = myModel.ViewListBoxText;
                        });
                    }
                    else if (prev_string.Contains("False") && enable == true)
                    {
                        prev_string = sitesListBox[index].Replace("False", "True");
                        myModel.ViewListBoxText[index] = prev_string;
                        form1.lstbox.Invoke((MethodInvoker)delegate {
                            form1.lstbox.DataSource = null;
                            form1.lstbox.DataSource = myModel.ViewListBoxText;
                        });
                    }
                    System.Threading.Thread.Sleep(delay * 1000);
                }
            });
        }
    }
}
