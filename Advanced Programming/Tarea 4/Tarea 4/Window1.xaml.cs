using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Tarea_4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string arma;
        public Window1()
        {
            InitializeComponent();
            Bazooka.Click += Bazooka_Click;
            Bat.Click += Bat_Click;
            Air_Strike.Click += Air_Strike_Click;
            Teletransportar.Click += Teletransportar_Click;
            
        }

        void Teletransportar_Click(object sender, RoutedEventArgs e)
        {
            arma = "Teletransportar";
            Window_Armas.Close();
        }

        void Air_Strike_Click(object sender, RoutedEventArgs e)
        {
            arma = "Air Strike";
            Window_Armas.Close();
        }

        void Bat_Click(object sender, RoutedEventArgs e)
        {
            arma = "Bat";
            Window_Armas.Close();
        }

        void Bazooka_Click(object sender, RoutedEventArgs e)
        {
            arma = "Bazooka";
            Window_Armas.Close();
        }


    }
}
