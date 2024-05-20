using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;

namespace WindowsForm_Grafic
{
    public partial class Form1 : Form
    {
        public static int samples = 10;
        public class Dato
        {
            public int Id { get; set; }
            public DateTime DateTime { get; set; }
            public string Message { get; set; }
        }

        public static List<Dato> dati;

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                   
                    Values = new ChartValues<double>()
                }
            };


            for (int i = 0; i < samples; i++)
            {
                cartesianChart.Series[0].Values.Add(0.00);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadDB();
            List<double> mes = new List<double>();
            foreach (var dato in dati)
            {
                mes.Add(Convert.ToDouble(dato.Message.Replace('.', ',')));
            }
            cartesianChart.Series[0].Values.Clear();

            for(int i = 0; i < Math.Min(samples, mes.Count);i++)
            {
                cartesianChart.Series[0].Values.Add(mes[i]);
            }
        }
        public static void ReadDB()
        {
            String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                "AttachDbFilename=C:\\Users\\balakhtina\\Desktop\\AndriiBalakhtin2IOT-MQTT\\TableRicevedDate.mdf;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;";
            dati = new List<Dato>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"SELECT TOP {samples} * FROM Dati ORDER BY Id DESC";

                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                Dato dato = new Dato();
                                dato.Id = read.GetInt32(0);
                                dato.DateTime = read.GetDateTime(1);
                                dato.Message = read.GetString(2);
                                dati.Add(dato);
                            }
                        }
                    }
                    dati.Reverse();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
