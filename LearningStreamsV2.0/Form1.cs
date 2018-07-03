using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LearningStreamsV2._0
{
    public partial class Form1 : Form
    {

        private string selectedFolder = "";
        private bool formChanged = false;
        Excuse currentExcuse = new Excuse();
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            buttonEnabled(false);
            currentExcuse.LastUsed = lastUsed.Value;
        }

        private void buttonEnabled(bool yesNo)
        {
            if (yesNo)
            {
                openButton.Enabled = true;
                saveButton.Enabled = true;
                randomButton.Enabled = true;
            }
            else
            {
                openButton.Enabled = false;
                saveButton.Enabled = false;
                randomButton.Enabled = false;
            }
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedFolder = folderBrowserDialog1.SelectedPath;
                buttonEnabled(true);
            }
            dateLabel.Text = lastUsed.Value.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(description.Text) || String.IsNullOrEmpty(results.Text))
            {
                MessageBox.Show("Określ wymówkę i rezultat", "Nie można zapisać pliku",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                saveFileDialog1.InitialDirectory = selectedFolder;
                saveFileDialog1.Filter = "Pliki tekstowe (*.txt)|*.txt |Wszystkie pliki (*.*)|*.*";
                saveFileDialog1.FileName = description.Text + ".txt";
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentExcuse.SaveFile(saveFileDialog1.FileName);
                    UpdateForm(false);
                    MessageBox.Show("Wymówka zapisana");

                }
                dateLabel.Text = lastUsed.Value.ToString();

            }
         
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (CheckChanged())
            { 
            openFileDialog1.InitialDirectory = selectedFolder;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                currentExcuse = new Excuse(openFileDialog1.FileName);
                UpdateForm(false);
            }
            dateLabel.Text = lastUsed.Value.ToString();
            }

        }

        private void UpdateForm(bool changed)
        {
            if (!changed)
            {
                this.description.Text = currentExcuse.Description;
                this.results.Text = currentExcuse.Result;
                this.lastUsed.Text = currentExcuse.LastUsed.ToString();
                if (!String.IsNullOrEmpty(currentExcuse.ExcusePath))
                    dateLabel.Text = File.GetLastWriteTime(currentExcuse.ExcusePath).ToString();
                this.Text = "Program do zarządzania wymówkami";
            }
            else
            {
                this.Text = "Program do zarządzania wymówkami*";
            }
            this.formChanged = changed;
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            currentExcuse.Description = description.Text;
            UpdateForm(true);
        }

        private void result_TextChanged(object sender, EventArgs e)
        {
            currentExcuse.Result = results.Text;
            UpdateForm(true);
        }

        private void lastUsed_ValueChanged(object sender, EventArgs e)
        {
            currentExcuse.LastUsed = lastUsed.Value;
            UpdateForm(true);
        }

        private bool CheckChanged()
        {
            if (formChanged)
            { 
            DialogResult result = MessageBox.Show("Bieżąca wymówka nie zostałą zapisana. Czy kontynuować?", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return false;
            }
            return true;
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            if(CheckChanged())
            {
                currentExcuse = new Excuse(random, selectedFolder);
                UpdateForm(false);
            }
        }
    }
}
