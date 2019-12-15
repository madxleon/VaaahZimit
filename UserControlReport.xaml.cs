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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace VaaahZemir
{
    /// <summary>
    /// Логика взаимодействия для UserControlReport.xaml
    /// </summary>
    public partial class UserControlReport : UserControl
    {
        public UserControlReport()
        {
            InitializeComponent();                     
                        
            Employee alex = new Employee();
                        
            // Day.ColumnCount = 4;
            // dataGridView1.Columns[0].Name = "ID";
            //dataGridView1.Columns[1].Name = "Name";
            //dataGridView1.Columns[2].Name = "Position";
            //dataGridView1.Columns[3].Name = "Team";
            alex.Day = "001";
            dataGrid.Items.Add(alex);

        }


        public void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            List<String> Sleeping = new List<String>();
            int[] AgentInfo = new int[50];
            int[] AgentID = new int[50];
            int[] Sleep = new int[50];
            int[] ticket = new int[50];
            //--------------------------------------------sql запрос------------------------------------------------------------------
            string connectionString = "server=192.168.88.227; port=3306;user=root;database=GEN_OCS;password=4959534196;";
            // string connectionString = "server=89.169.16.165; port=3306;user=root;database=GEN_OCS;password=4959534196;";
            // объект для установления соединения с БД
            MySqlConnection connection = new MySqlConnection(connectionString);
            // открываем соединение
            connection.Open();
            string query = "Select TimeStart FROM GEN_OCS.TimeManager;";


            MySqlCommand command = new MySqlCommand(query, connection);


        }





        public class Employee
        {
            public string Day { get; set; }
            public string TicketID { get; set; }
            public string Work { get; set; }
            public string Sleep { get; set; }
        }

















    }
}
