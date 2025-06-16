using MySql.Data.MySqlClient;
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
    public partial class ChangePassword : Form
    {
        private int userId;
        public ChangePassword(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentPassword = txtcurpass.Text;
            string newPassword = txtnewpass.Text;
            string confirmPassword = txtconpass.Text;

            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string getPasswordQuery = "SELECT pass_us FROM Users WHERE user_id = @userId";
            DataTable result = DatabaseHelper.ExecuteQuery(getPasswordQuery, new MySqlParameter[] {
                new MySqlParameter("@userId", userId)
            });

            if (result.Rows.Count == 1)
            {
                string storedPassword = result.Rows[0]["pass_us"].ToString();
                if (currentPassword != storedPassword)
                {
                    MessageBox.Show("Текущий пароль введен неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string updateQuery = "UPDATE Users SET pass_us = @password, first_us = FALSE WHERE user_id = @userId";
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery, new MySqlParameter[] {
                    new MySqlParameter("@password", newPassword),
                    new MySqlParameter("@userId", userId)
                });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пароль успешно изменен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при изменении пароля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
