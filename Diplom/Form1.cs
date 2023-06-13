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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Form2.lastName;
            dataGridView1.ReadOnly = true; // Запретить редактирование ячеек
            dataGridView1.Visible = false;
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
            dataGridView1.Visible = true;
            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            richTextBox1.Clear();
            button2.Enabled = false;
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

            if (selectedRowData.Count > 4)
            {
                // Получаем данные строки из источника данных (DataTable)
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                if (rowView != null)
                {
                    string selectedID = selectedRow.Cells["ID"].Value.ToString();
                    idelete = Convert.ToInt32(selectedID);
                    DataRow row = rowView.Row;

                    // Выводим все данные строки в richTextBox1
                    StringBuilder sb = new StringBuilder();
                    foreach (object item in row.ItemArray)
                    {
                        sb.AppendLine(item.ToString());
                    }
                    richTextBox1.Text += sb.ToString() + Environment.NewLine;
                }

                // Очищаем dataGridView1 и скрываем его
                dataGridView1.DataSource = null;
                dataGridView1.Visible = false;
                button2.Enabled = true;
                button3.Enabled = true;
                richTextBox1.Visible = true;
            }
            else
            {
                string selectedValue = selectedRowData[0];

                string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";
                string query = "SELECT id, fio AS 'ФИО', dob AS 'Дата рожд.', dol AS 'Жел. должность', resume AS 'Резюме', gender AS 'Пол', nazvanieOrg AS 'Назв. Орг.', dolzhnost AS 'Прошл. Должн.', periodRaboti AS 'Период работы', urObrazov AS 'Ур. Обр.', uchZaved AS 'Уч. Завед.', specialnost AS 'Специальность', perObuch AS 'Пер. Обуч.', dopObrazovanie AS 'Доп. Образ.', telephone AS 'Тел.', email AS 'Почта' FROM rezume WHERE dol = @dol";

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
        int idelete;
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Visible = false;
            button2.Enabled=false;
            MessageBox.Show("Кандидатура была отклонена!");
            int iddd = idelete;
            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";

            string deleteCommandText = "DELETE FROM rezume WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(deleteCommandText, connection))
                {
                    command.Parameters.AddWithValue("@id", iddd);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=chuc.sdlik.ru;Port=33333;Database=VKR_OtdelenovD;Uid=VKR_OtdelenovD;Pwd=dalshd2893890Yds;";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT fio, telephone, email FROM rezume WHERE id = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", idelete);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string telephone = reader.GetString("telephone");
                            string email = reader.GetString("email");
                            string fio = reader.GetString("fio");

                            MessageBox.Show($"ФИО: {fio}\nТелефон: {telephone}\nПочта: {email}");
                        }
                        else
                        {
                            MessageBox.Show("Запись с указанным идентификатором не найдена.");
                        }
                    }
                }
            }
        }
    }
}
