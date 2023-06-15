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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
            string oldPassword = textBox1.Text;
            string newPassword = textBox2.Text;
            string confirmNewPassword = textBox3.Text;

            // Хэширование SHA256 введенного старого пароля для сравнения с хешированным паролем в базе данных
            string hashedOldPassword = ComputeSha256Hash(oldPassword);

            string Lastname = Form2.lastName;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Получение хешированного пароля из базы данных для текущего пользователя
                string query = "SELECT Password FROM user WHERE LastName = @LastName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@LastName", Lastname); // Замените на имя пользователя

                object result = command.ExecuteScalar();
                string hashedPasswordFromDB = (result != null) ? result.ToString() : string.Empty;
                // Проверка соответствия старого пароля
                if (hashedOldPassword == hashedPasswordFromDB)
                {
                    // Проверка соответствия нового пароля и его подтверждения
                    if (newPassword == confirmNewPassword)
                    {
                        // Хэширование SHA256 нового пароля перед обновлением в базе данных
                        string hashedNewPassword = ComputeSha256Hash(newPassword);

                        // Обновление пароля в базе данных
                        string updateQuery = "UPDATE user SET Password = @NewPassword WHERE Lastname = @LastName";
                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                        updateCommand.Parameters.AddWithValue("@LastName", Lastname); // Замените на имя пользователя

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Пароль успешно изменен!");
                    }
                    else
                    {
                        MessageBox.Show("Новый пароль и подтверждение нового пароля не совпадают!");
                    }
                }
                else
                {
                    MessageBox.Show("Введен неверный старый пароль!");
                }
            }

            // Функция для хеширования SHA256 пароля
            string ComputeSha256Hash(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
