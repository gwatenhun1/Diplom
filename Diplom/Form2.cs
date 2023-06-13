using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;


namespace Diplom
{
    public partial class Form2 : Form
    {
        public static string lastName;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (AuthenticateUser(login, password))
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private bool AuthenticateUser(string login, string password)
        {
            MySqlConnection connection;
            string server = "chuc.sdlik.ru";
            string database = "VKR_OtdelenovD";
            string uid = "VKR_OtdelenovD";
            string passwords = "dalshd2893890Yds";
            string port = "33333";

            string hashedPassword = GetSHA256Hash(password);
            string query = "SELECT * FROM user WHERE Mail = @login AND Password = @password";

            using (connection = new MySqlConnection($"server={server};database={database};uid={uid};password={passwords};port={port};"))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", hashedPassword);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            reader.Read(); // Переходим к первой строке результата

                            // Получаем значение поля "LastName" и присваиваем его переменной "lastName"
                            lastName = reader.GetString("LastName");

                            return true;
                        }  
                    }
                }
            }

            return false;
        }
        private string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }




        








        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пожалуйста, обратитесь в отдел информационной поддержки по адрессу г. Челябинск, ул. Блюхера 42а, 206 кабинет. Или по телефону +795646977");
        }
       
        private void label4_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false; // Отображаем текст в виде обычных символов
            }
            else
            {
                textBox2.UseSystemPasswordChar = true; // Отображаем текст в виде паролей
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
