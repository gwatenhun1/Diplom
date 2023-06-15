using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Diplom
{
    public partial class Form1 : Form
    {
        string Id, Fio, Dob, Dol, Resume, Gender, NazvanieOrg, Dolzhnost, PeriodRaboti, UrObrazov, UchZaved, Specialnost, PerObuch, DopObrazovanie, Telephone, Email;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Form2.lastName;
            dataGridView1.ReadOnly = true; // Запретить редактирование ячеек
            comboBox1.FlatStyle = FlatStyle.Flat;           

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
         Application.Exit(); // Завершить работу приложения
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "-Изменить данные-")
            {
                Form4 form4 = new Form4();
                form4.StartPosition = FormStartPosition.CenterScreen;
                form4.ShowDialog();
            }
            else if (comboBox1.Text == "-Выход-")
            {
                Form2 form2 = new Form2();
                form2.StartPosition = FormStartPosition.CenterScreen;
                form2.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }
        private void LoadData()
        {
            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
            string query = "SELECT DISTINCT dol as 'Должность' FROM rezume";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                ResizeColumns();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Enabled = true;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return; // Если индексы недействительны, выходим из метода
            }

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            List<string> selectedRowData = new List<string>();


            // Получаем значения всех ячеек выбранной строки
            foreach (DataGridViewCell cell in selectedRow.Cells)
            {
                selectedRowData.Add(cell.Value?.ToString() ?? string.Empty);
            }

            if (selectedRowData.Count > 3)
            {
                //string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
                //MySqlCommand command = new MySqlCommand(query, connectionString);
                //using (MySqlConnection connection = new MySqlConnection(connectionString))
                //{

                //}
                if (e.RowIndex >= 0)
                {
                    // Получение значения ячейки с кодом
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    Id = row.Cells["Код"].Value.ToString();

                }

                string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
                string query = "SELECT * FROM rezume WHERE id = @Id";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", Id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id = reader.GetString(0);  // Первый столбец
                            Fio = reader.GetString(1);  // Второй столбец
                            Dob = reader.GetString(2);  // Третий столбец
                            Dol = reader.GetString(3);  // Четвертый столбец
                            Resume = reader.GetString(4);  // Пятый столбец
                            Gender = reader.GetString(5);  // Шестой столбец
                            NazvanieOrg = reader.GetString(6); 
                            Dolzhnost = reader.GetString(7); 
                            PeriodRaboti = reader.GetString(8); 
                            UrObrazov = reader.GetString(9); 
                            UchZaved = reader.GetString(10); 
                            Specialnost = reader.GetString(11);
                            PerObuch = reader.GetString(12); 
                            DopObrazovanie = reader.GetString(13); 
                            Telephone = reader.GetString(14); 
                            Email = reader.GetString(15); 
                        }
                    }
                }
                MessageBox.Show($"{Id} + {Fio} + {Dob} + {Dol} + {Resume} + {Gender} + {NazvanieOrg} + {Dolzhnost} + {PeriodRaboti} + {UrObrazov} + {UchZaved} + {Specialnost} + {PerObuch} + {DopObrazovanie} + {Telephone} + {Email}");







                // Очищаем dataGridView1 и скрываем его
                button2.Enabled = true;
                button3.Enabled = true;

            }
            else
            {
                string selectedValue = selectedRowData[0];

                string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
                string query = "SELECT id AS 'Код', fio AS 'ФИО', dob AS 'Дата рожд.', dol AS 'Жел. должность' FROM rezume WHERE dol = @dol";


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@dol", selectedValue);
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    ResizeColumns();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            richTextBox1.Clear();
            button2.Enabled = false;
            button4.Enabled = false;
        }
        private void ResizeColumns()
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void button2_Click(object sender, EventArgs e)
        {

            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
            string query = "INSERT INTO rezume_otkl (fio, dob, dol, resume, gender, nazvanieOrg, dolzhnost, periodRaboti, urObrazov, uchZaved, specialnost, perObuch, dopObrazovanie, telephone, email) VALUES (@Fio, @Dob, @Dol, @Resume, @Gender, @NazvanieOrg, @Dolzhnost, @PeriodRaboti, @UrObrazov, @UchZaved, @Specialnost, @PerObuch, @DopObrazovanie, @Telephone, @Email)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                // Замените переменные в соответствии с вашими данными
                command.Parameters.AddWithValue("@fio", Fio);
                command.Parameters.AddWithValue("@dob", Dob);
                command.Parameters.AddWithValue("@dol", Dol);
                command.Parameters.AddWithValue("@resume", Resume);
                command.Parameters.AddWithValue("@gender", Gender);
                command.Parameters.AddWithValue("@nazvanieOrg", NazvanieOrg);
                command.Parameters.AddWithValue("@dolzhnost", Dolzhnost);
                command.Parameters.AddWithValue("@periodRaboti", PeriodRaboti);
                command.Parameters.AddWithValue("@urObrazov", UrObrazov);
                command.Parameters.AddWithValue("@uchZaved", UchZaved);
                command.Parameters.AddWithValue("@specialnost", Specialnost);
                command.Parameters.AddWithValue("@perObuch", PerObuch);
                command.Parameters.AddWithValue("@dopObrazovanie", DopObrazovanie);
                command.Parameters.AddWithValue("@telephone", Telephone);
                command.Parameters.AddWithValue("@email", Email);

                command.ExecuteNonQuery();
            }

            button2.Enabled=false;
            MessageBox.Show("Кандидатура была отклонена!");

            string deleteCommandText = "DELETE FROM rezume WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(deleteCommandText, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
            string query = "INSERT INTO rezume_otmet (fio, dob, dol, resume, gender, nazvanieOrg, dolzhnost, periodRaboti, urObrazov, uchZaved, specialnost, perObuch, dopObrazovanie, telephone, email) VALUES (@Fio, @Dob, @Dol, @Resume, @Gender, @NazvanieOrg, @Dolzhnost, @PeriodRaboti, @UrObrazov, @UchZaved, @Specialnost, @PerObuch, @DopObrazovanie, @Telephone, @Email)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                // Замените переменные в соответствии с вашими данными
                command.Parameters.AddWithValue("@fio", Fio);
                command.Parameters.AddWithValue("@dob", Dob);
                command.Parameters.AddWithValue("@dol", Dol);
                command.Parameters.AddWithValue("@resume", Resume);
                command.Parameters.AddWithValue("@gender", Gender);
                command.Parameters.AddWithValue("@nazvanieOrg", NazvanieOrg);
                command.Parameters.AddWithValue("@dolzhnost", Dolzhnost);
                command.Parameters.AddWithValue("@periodRaboti", PeriodRaboti);
                command.Parameters.AddWithValue("@urObrazov", UrObrazov);
                command.Parameters.AddWithValue("@uchZaved", UchZaved);
                command.Parameters.AddWithValue("@specialnost", Specialnost);
                command.Parameters.AddWithValue("@perObuch", PerObuch);
                command.Parameters.AddWithValue("@dopObrazovanie", DopObrazovanie);
                command.Parameters.AddWithValue("@telephone", Telephone);
                command.Parameters.AddWithValue("@email", Email);

                command.ExecuteNonQuery();
            }

            button2.Enabled = false;
            MessageBox.Show("Кандидатура была отмечена!");

            string deleteCommandText = "DELETE FROM rezume WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(deleteCommandText, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

        }
    }
}
