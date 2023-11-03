using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading; // Add this namespace for DispatcherTimer

namespace Text_Edit
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = null;
        private bool autoSaveEnabled = false;
        private DispatcherTimer autoSaveTimer;

        public MainWindow()
        {
            InitializeComponent();

            saveFiltBtr.Click += SaveFileButton_Click;
            selectFileBtr.Click += SelectFileButton_Click;
            autoSaveCheckBox.Checked += AutoSaveCheckBox_Checked;
            autoSaveCheckBox.Unchecked += AutoSaveCheckBox_Unchecked;

            autoSaveTimer = new DispatcherTimer();
            autoSaveTimer.Interval = TimeSpan.FromSeconds(1); 
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                currentFilePath = openFileDialog.FileName;

                try
                {
                    string fileContent = File.ReadAllText(currentFilePath);
                    textBox.Text = fileContent;

                    fileLocationtxt.Text = currentFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath != null)
            {
                try
                {
                    File.WriteAllText(currentFilePath, textBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No file is currently selected. Please use the 'Select File' button to choose a file.");
            }
        }

        private void AutoSaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            autoSaveEnabled = true;
            autoSaveTimer.Start();
        }

        private void AutoSaveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            autoSaveEnabled = false;
            autoSaveTimer.Stop();
        }

        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {

            if (autoSaveEnabled && currentFilePath != null)
            {
                try
                {
                    File.WriteAllText(currentFilePath, textBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void CopyBtr_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }

        private void PasteBtr_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
        }

        private void SelectAllBtr_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectAll();
        }


    }
}
