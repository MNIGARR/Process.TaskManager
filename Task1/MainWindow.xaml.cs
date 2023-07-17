using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ProcessName
        {
            get { return (string)GetValue(ProcessesProperty); }
            set { SetValue(ProcessesProperty, value); }
        }
        public Process SelectProcess { get; set; }

        public ObservableCollection<Process> Processes
        {
            get { return (ObservableCollection<Process>)GetValue(ProcessesProperty); }
            set { SetValue(ProcessesProperty, value); }
        }

        public static readonly DependencyProperty ProcessesProperty =
            DependencyProperty.Register("Processes", typeof(ObservableCollection<Process>), typeof(MainWindow));

        public MainWindow()
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            DataContext = this; 
        }

        private void Create_Button_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo processinfo = new();
                processinfo.FileName = Textbox.Text;
                Process.Start(processinfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("INVALID PROCESS NAME");
            }
        }


        private void End_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectProcess.Kill();
                Processes.Remove(SelectProcess);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PROCESS IS NOT ENDED");
            }

        }

    }
}
