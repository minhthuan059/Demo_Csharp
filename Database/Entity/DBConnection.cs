
//using MySqlConnector;


//namespace Database.Entity
//{
//    public class DBConnection
//    {

//        private static DBConnection _instance = null;
//        public string ConnectionString { get; set; } = "";

//        private MySqlConnection _connection = null;
//        public MySqlConnection Connection
//        {
//            get
//            {
//                if (_connection == null)
//                {
//                    _connection = new MySqlConnection(ConnectionString); ;
//                    _connection.Open();
//                }
//                return _connection;
//            }
//        }

//        public static DBConnection Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new DBConnection();
//                }
//                return _instance;
//            }
//        }
//    }
//}
