//Andrii Balakhtin 2^IOT 20/05/2024 VERIFICA DI MQTT

using System.Data.SqlClient;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Text;

class Program
{
    public class Dato
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }

    static async Task Main(string[] args)
    {
        String topic = "bortolanim";

        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
        .WithTcpServer("broker.hivemq.com", 1883)
        .WithCleanSession()
        .Build();

        var connectResult = await mqttClient.ConnectAsync(options);

        if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
        {
            Console.WriteLine("Connected to MQTT Broker successfull!!!");
            await mqttClient.SubscribeAsync(topic);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Dato dato = new Dato();
                String message = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                string[] vel = message.Split(' ');
                dato.Message = vel[1];

                Console.WriteLine($"I received a message: {dato.Message}");
                scriviDato(dato);

                return Task.CompletedTask;
            };
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;

            await mqttClient.UnsubscribeAsync(topic);
            await mqttClient.DisconnectAsync();
        }
        else
        {
            Console.WriteLine($"Failed to connect to MQTT broker: {connectResult.ResultCode}");
        }
    }
    public static void scriviDato(Dato x)
    {
        String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=C:\\Users\\balakhtina\\Desktop\\AndriiBalakhtin2IOT-MQTT\\TableRicevedDate.mdf;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;";

        try
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                String queryInsert = $"INSERT INTO Dati (Message) VALUES ('{x.Message}')";
                
                using (SqlCommand cmd = new SqlCommand(queryInsert, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}