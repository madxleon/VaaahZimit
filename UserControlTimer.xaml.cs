using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace VaaahZemir
{
    /// <summary>
    /// Логика взаимодействия для UserControlTimer.xaml
    /// </summary>
    public partial class UserControlTimer : UserControl
    {
        public UserControlTimer()
        {
            InitializeComponent();
            //------------------------Общий таймер---------------------------------------------
            //---------------------------------------------------------------------------------
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Job);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            int Job_Start = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            //---------------------------------------------------------------------------------
            //---------------------------------------------------------------------------------
            dispatcherTimerTicket = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimerTicket.Tick += new EventHandler(dispatcherTimer_Ticket);
            dispatcherTimerTicket.Interval = new TimeSpan(0, 0, 1);
            
        }
        int Job_Start = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        private static DispatcherTimer dispatcherTimerTicket;
        private static DispatcherTimer dispatcherTimer;
        int sec = 0, min = 0, hour = 0;
        int sectick = 0, mintick = 0, hourtick = 0;
        int i = 0;
        int ticketID;
        int Ticket_Start;
        int Ticket_Pause;
        int Ticket_PauseStop;
        int[] TickPauseStamp = new int[5];
        int[] TickPauseStampStop = new int[5];
        int[] TickPauseStampResult = new int[5];
        string AgentID = "13";
        int Sleep;




        private void CommandBinding_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       

        
        void dispatcherTimer_Job(object sender, EventArgs e)
        {            
            int[] time = new int[3];

            ++sec;

            if (sec > 59)
            {
                ++min;
                sec = 0;
                if (min > 59)
                {
                    ++hour;
                    min = 0;
                }
                
            }
            time[0] = sec;
            time[1] = min;
            time[2] = hour;
            string content = time[2].ToString("00") +":" + time[1].ToString("00")+":" + time[0].ToString("00");
            TextBlock_Job.Text = content;
            CommandManager.InvalidateRequerySuggested();  

        }   //    На работе

        void dispatcherTimer_Ticket(object sender, EventArgs e)
        {
            int[] tick = new int[3];

            ++sectick;

            if (sectick > 59)
            {
                ++mintick;
                sectick = 0;
                if (mintick > 59)
                {
                    ++hourtick;
                    mintick = 0;
                }

            }
            tick[0] = sectick;
            tick[1] = mintick;
            tick[2] = hourtick;
            string content_tick = tick[2].ToString("00") + ":" + tick[1].ToString("00") + ":" + tick[0].ToString("00");
            TextBlock_TicketTime.Text = content_tick;
            CommandManager.InvalidateRequerySuggested();
        }


        void PauseTicket_Click(object sender, EventArgs e)
        {
            
            PauseTicket.Visibility = Visibility.Hidden;
            dispatcherTimerTicket.Stop();
            Ticket_Pause = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            PauseTicketStop.Visibility = Visibility.Visible;
            TickPauseStamp[i] = Ticket_Pause;
        }

        
        void PauseTicketStop_Click(object sender, EventArgs e)
        {

            if (i == 4)
            {
                PauseTicket.Visibility = Visibility.Hidden;
                PauseTicketStop.Visibility = Visibility.Hidden;
                dispatcherTimerTicket.Start();
                i = 0;
            }
            else

            {
                PauseTicket.Visibility = Visibility.Visible;
                dispatcherTimerTicket.Start();
                Ticket_PauseStop = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                PauseTicketStop.Visibility = Visibility.Hidden;
                TickPauseStampStop[i] = Ticket_PauseStop;
                i++;
            }
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }   // RegExp ограничения текстбокса
        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {            
                TextBoxTicketName.MaxLength = 7;
           
        }
       
        public void ButtonStartTimeTicket_Click(object sender, RoutedEventArgs e)
        {
           
            if (TextBoxTicketName.Text == ""|| TextBoxTicketName.Text.Length < 7)
            {
                TextBlock_Check_TecketName.Visibility = Visibility.Visible;

            }
            else
            {
                PauseTicket.Visibility = Visibility.Visible;
                ticketID = Convert.ToInt32(TextBoxTicketName.Text);
                ButtonStartTimeTicket.Visibility = Visibility.Hidden;
                TextBoxTicketName.Visibility = Visibility.Hidden;
                TextBlock_TecketName_2.Text = ticketID.ToString();
                TextBlock_TecketName_2.Visibility = Visibility.Visible;
                ButtonStartTimeTicketDone.Visibility = Visibility.Visible;
                TextBlock_Check_TecketName.Visibility = Visibility.Hidden;
                dispatcherTimerTicket.Start();
                Ticket_Start = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            }
        }
        
        public void ButtonStartTimeTicketDone_Click(object sender, RoutedEventArgs e)
        {
            PauseTicketStop.Visibility = Visibility.Hidden;
            TextBlock_TecketName_2.Visibility = Visibility.Hidden;
            ButtonStartTimeTicketDone.Visibility = Visibility.Hidden;
            ButtonStartTimeTicket.Visibility = Visibility.Visible;
            TextBoxTicketName.Visibility = Visibility.Visible;           
            sectick = 0; mintick = 0; hourtick = 0;
            TextBlock_TicketTime.Text ="00:00:00";            
            TextBoxTicketName.Text = "";
            TextBlock_TecketName_2.Text = "";           
            dispatcherTimerTicket.Stop();
            int Ticket_Finish = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (i < 4)
            {
                Ticket_PauseStop = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                TickPauseStampStop[i] = Ticket_PauseStop;
            }


            Sleep = TickPauseStamp[0] + TickPauseStamp[1] + TickPauseStamp[2] + TickPauseStamp[3] + TickPauseStamp[4];
            int Sleep2 = TickPauseStampStop[0] + TickPauseStampStop[1] + TickPauseStampStop[2] + TickPauseStampStop[3] + TickPauseStampStop[3];
            Sleep = Sleep2 - Sleep;
            string Sleep3 = Sleep.ToString();
           



            //--------------------------------------------sql запрос------------------------------------------------------------------
            string connectionString = "server=192.168.88.227; port=3306;user=root;database=GEN_OCS;password=4959534196;";
           // string connectionString = "server=89.169.16.165; port=3306;user=root;database=GEN_OCS;password=4959534196;";
            // объект для установления соединения с БД
            MySqlConnection connection = new MySqlConnection(connectionString);
            // открываем соединение
            connection.Open();

            string query = "INSERT INTO GEN_OCS.TimeManager (AgentID, ticket, TimeStart, TimeStop, Sleep) VALUES(" + AgentID + ", " + ticketID + "," + Ticket_Start + "," + Ticket_Finish + "," + Sleep + ");";

            MySqlCommand command = new MySqlCommand(query, connection);
            // выполняем запрос
            command.ExecuteNonQuery();
            // закрываем подключение к БД
            connection.Close();

            //-------------------------------------------------------------------------------------------------------------------------


            i = 0;

        }




        
        public void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            
            
            
            int Job_Finish = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            //--------------------------------------------sql запрос------------------------------------------------------------------
             string connectionString = "server=89.169.16.165; port=3306;user=root;database=GEN_OCS;password=4959534196;";
            //string connectionString = "server=192.168.88.227; port=3306;user=root;database=GEN_OCS;password=4959534196;";
            // объект для установления соединения с БД
            MySqlConnection connection = new MySqlConnection(connectionString);
            // открываем соединение
            connection.Open();

            string query = "INSERT INTO GEN_OCS.TimeManager_TimeJob (AgentID, TimeStartWork, TimeFinishWork) VALUES(" + AgentID + ", " + Job_Start + "," + Job_Finish + ");";

            MySqlCommand command = new MySqlCommand(query, connection);
            // выполняем запрос
            command.ExecuteNonQuery();
            // закрываем подключение к БД
            connection.Close();

            //-------------------------------------------------------------------------------------------------------------------------

            Environment.Exit(0);



        }


























    }
}
