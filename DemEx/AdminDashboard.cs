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
using Microsoft.VisualBasic;

namespace DemEx
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadUsers();
        }
        private void LoadUsers()
        {
            string query = "SELECT user_id, username, role, locked_us, last_us FROM Users";
            DataTable users = DatabaseHelper.ExecuteQuery(query);
            dataGridViewUsers.DataSource = users;
            ConfigureDataGridView();
        }
        private void ConfigureDataGridView()
        {
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewUsers.Columns["user_id"].Visible = false;
            dataGridViewUsers.Columns["username"].HeaderText = "Логин";
            dataGridViewUsers.Columns["role"].HeaderText = "Роль";
            dataGridViewUsers.Columns["locked_us"].HeaderText = "Заблокирован";
            dataGridViewUsers.Columns["last_us"].HeaderText = "Последний вход";
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = Interaction.InputBox("Введите логин нового пользователя:", "Добавление пользователя");
            if (string.IsNullOrWhiteSpace(username)) return;

            // Проверка на существование пользователя
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE username = @username";
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery,
                new MySqlParameter[] { new MySqlParameter("@username", username) }));

            if (count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = Interaction.InputBox("Введите пароль:", "Добавление пользователя");
            if (string.IsNullOrWhiteSpace(password)) return;

            string role = Interaction.InputBox("Введите роль (user/admin):", "Добавление пользователя", "user");
            if (string.IsNullOrWhiteSpace(role)) return;

            string insertQuery = "INSERT INTO Users (username, pass_us, role, first_us, locked_us, attem_us) " +
                               "VALUES (@username, @pass_us, @role, TRUE, FALSE, 0)";

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery,
                new MySqlParameter[] {
                    new MySqlParameter("@username", username),
                    new MySqlParameter("@pass_us", password),
                    new MySqlParameter("@role", role)
                });

            if (rowsAffected > 0)
            {
                MessageBox.Show("Пользователь успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridViewUsers.SelectedRows[0];
            int userId = Convert.ToInt32(row.Cells["user_id"].Value);
            string currentUsername = row.Cells["username"].Value.ToString();
            string currentRole = row.Cells["role"].Value.ToString();

            string newUsername = Interaction.InputBox("Введите новый логин:",
                "Редактирование пользователя", currentUsername);
            if (string.IsNullOrWhiteSpace(newUsername)) return;

            // Проверка на существование пользователя, если имя изменилось
            if (newUsername != currentUsername)
            {
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE username = @username";
                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery,
                    new MySqlParameter[] { new MySqlParameter("@username", newUsername) }));

                if (count > 0)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string newRole = Interaction.InputBox("Введите новую роль (user/admin):",
                "Редактирование пользователя", currentRole);
            if (string.IsNullOrWhiteSpace(newRole)) return;

            string newPassword = Interaction.InputBox("Введите новый пароль (оставьте пустым, чтобы не менять):",
                "Редактирование пользователя");

            int rowsAffected;
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                string updateQuery = "UPDATE Users SET username = @username, role = @role WHERE user_id = @userId";
                rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery,
                    new MySqlParameter[] {
                        new MySqlParameter("@username", newUsername),
                        new MySqlParameter("@role", newRole),
                        new MySqlParameter("@userId", userId)
                    });
            }
            else
            {
                string updateQuery = "UPDATE Users SET username = @username, pass_us = @pass_us, role = @role WHERE user_id = @userId";
                rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery,
                    new MySqlParameter[] {
                        new MySqlParameter("@username", newUsername),
                        new MySqlParameter("@pass_us", newPassword),
                        new MySqlParameter("@role", newRole),
                        new MySqlParameter("@userId", userId)
                    });
            }

            if (rowsAffected > 0)
            {
                MessageBox.Show("Данные пользователя обновлены!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["user_id"].Value);
            string query = "UPDATE Users SET locked_us = FALSE, attem_us = 0 WHERE user_id = @userId";
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query,
                new MySqlParameter[] { new MySqlParameter("@userId", userId) });

            if (rowsAffected > 0)
            {
                MessageBox.Show("Пользователь разблокирован", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Ошибка при разблокировке пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminDashboardForm_FormClosing(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
