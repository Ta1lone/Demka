using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemEx
{
    public partial class Гости : Form
    {
        public Гости()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadTableData();
        }

        private void LoadTableData()
        {
            try
            {
                // Выбираем таблицу для отображения (можно изменить на любую другую)
                string tableName = "Guests";
                string query = $"SELECT * FROM `{tableName}`";

                DataTable data = DatabaseHelper.ExecuteQuery(query);
                dataGridView1.DataSource = data;

                // Настройка отображения столбцов (опционально)
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewColumns()
        {
            // Настройка заголовков столбцов
            if (dataGridView1.Columns.Contains("idGuests"))
                dataGridView1.Columns["idGuests"].HeaderText = "ID";

            if (dataGridView1.Columns.Contains("Lastname"))
                dataGridView1.Columns["Lastname"].HeaderText = "Фамилия";

            if (dataGridView1.Columns.Contains("Firstname"))
                dataGridView1.Columns["Firstname"].HeaderText = "Имя";

            if (dataGridView1.Columns.Contains("Othername"))
                dataGridView1.Columns["Othername"].HeaderText = "Отчество";

            if (dataGridView1.Columns.Contains("Contact_num"))
                dataGridView1.Columns["Contact_num"].HeaderText = "Телефон";

            // Автоматическое изменение ширины столбцов
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void Обновить_Click(object sender, EventArgs e)
        {
            LoadTableData();
        }
    }
}
