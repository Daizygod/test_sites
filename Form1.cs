using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace test_task_3
{
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            new Presenter(this);
        }
        public event EventHandler updateListboxEvent2, updateEvent, addSiteEvent, updateSiteEvent, deleteSiteEvent, selectListEvent, updateListboxEvent;

        private void InitializeComponent()
        {
            this.lstbox = new System.Windows.Forms.ListBox();
            this.updatebtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            this.urlTextbox = new System.Windows.Forms.TextBox();
            this.delayTextbox = new System.Windows.Forms.TextBox();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstbox
            // 
            this.lstbox.AccessibleName = "lstbox";
            this.lstbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstbox.FormattingEnabled = true;
            this.lstbox.HorizontalScrollbar = true;
            this.lstbox.ItemHeight = 20;
            this.lstbox.Location = new System.Drawing.Point(16, 23);
            this.lstbox.Margin = new System.Windows.Forms.Padding(5);
            this.lstbox.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.lstbox.Name = "lstbox";
            this.lstbox.ScrollAlwaysVisible = true;
            this.lstbox.Size = new System.Drawing.Size(1168, 344);
            this.lstbox.TabIndex = 0;
            this.lstbox.SelectedIndexChanged += new System.EventHandler(this.lstbox_SelectedIndexChanged);
            this.lstbox.Leave += new System.EventHandler(this.lstbox_Leave);
            // 
            // updatebtn
            // 
            this.updatebtn.Location = new System.Drawing.Point(377, 442);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(94, 29);
            this.updatebtn.TabIndex = 5;
            this.updatebtn.Text = "Изменить";
            this.updatebtn.UseVisualStyleBackColor = true;
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.Location = new System.Drawing.Point(277, 442);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(94, 29);
            this.addbtn.TabIndex = 4;
            this.addbtn.Text = "Добавить";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(477, 443);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(94, 29);
            this.deletebtn.TabIndex = 6;
            this.deletebtn.Text = "Удалить";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // urlTextbox
            // 
            this.urlTextbox.Location = new System.Drawing.Point(16, 443);
            this.urlTextbox.Name = "urlTextbox";
            this.urlTextbox.Size = new System.Drawing.Size(255, 27);
            this.urlTextbox.TabIndex = 3;
            this.urlTextbox.Enter += new System.EventHandler(this.urlTextbox_Enter);
            this.urlTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlTextbox_KeyDown);
            // 
            // delayTextbox
            // 
            this.delayTextbox.Location = new System.Drawing.Point(182, 390);
            this.delayTextbox.Name = "delayTextbox";
            this.delayTextbox.Size = new System.Drawing.Size(89, 27);
            this.delayTextbox.TabIndex = 2;
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(16, 390);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(160, 27);
            this.nameTextbox.TabIndex = 1;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(16, 367);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(116, 20);
            this.lbl1.TabIndex = 7;
            this.lbl1.Text = "Наименование";
            this.lbl1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 367);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Задержка";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "URL";
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1198, 507);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.nameTextbox);
            this.Controls.Add(this.delayTextbox);
            this.Controls.Add(this.urlTextbox);
            this.Controls.Add(this.deletebtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.updatebtn);
            this.Controls.Add(this.lstbox);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void urlTextbox_Enter(object sender, EventArgs e)
        {
            
        }

        private void urlTextbox_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    addSiteEvent.Invoke(sender, e);
                }
        }

        private void labelUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {
           
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            updateEvent.Invoke(sender, e);
        }

        private void lstbox_Leave(object sender, EventArgs e)
        {
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            addSiteEvent.Invoke(sender, e);
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            updateSiteEvent.Invoke(sender, e);
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteSiteEvent.Invoke(sender, e);
        }

        private void lstbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectListEvent.Invoke(sender, e);
        }
    }
}
