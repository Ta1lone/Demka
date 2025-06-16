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

namespace DemEx
{
    public partial class Aautorization : Form
    {
        public Aautorization()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Aautorization_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtlog_us.Text.Trim();
            string password = txtpass_us.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string checkLockQuery = "SELECT locked_us, last_us FROM Users WHERE username = @username";
            DataTable lockResult = DatabaseHelper.ExecuteQuery(checkLockQuery, new MySqlParameter[] {
                new MySqlParameter("@username", username)
            });

            if (lockResult.Rows.Count > 0)
            {
                bool isLocked = Convert.ToBoolean(lockResult.Rows[0]["locked_us"]);
                DateTime? lastLoginDate = lockResult.Rows[0]["last_us"] as DateTime?;

                if (isLocked)
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (lastLoginDate.HasValue && (DateTime.Now - lastLoginDate.Value).TotalDays > 30)
                {
                    string lockUserQuery = "UPDATE Users SET locked_us = TRUE WHERE username = @username";
                    DatabaseHelper.ExecuteNonQuery(lockUserQuery, new MySqlParameter[] {
                        new MySqlParameter("@username", username)
                    });
                    MessageBox.Show("Ваша учетная запись заблокирована из-за отсутствия активности в течение месяца. Обратитесь к администратору", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string query = "SELECT user_id, username, pass_us, role, first_us FROM Users WHERE username = @username";
            DataTable result = DatabaseHelper.ExecuteQuery(query, new MySqlParameter[] {
                new MySqlParameter("@username", username)
            });

            if (result.Rows.Count == 1 && password == result.Rows[0]["pass_us"].ToString())
            {
                int userId = Convert.ToInt32(result.Rows[0]["user_id"]);
                string role = result.Rows[0]["role"].ToString();
                bool isFirstLogin = Convert.ToBoolean(result.Rows[0]["first_us"]);

                UpdateUserLoginInfo(userId, true);

                MessageBox.Show("Вы успешно авторизовались", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (isFirstLogin)
                {
                    ChangePassword changePasswordForm = new ChangePassword(userId);
                    if (changePasswordForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenMainForm(role);
                    }
                }
                else
                {
                    OpenMainForm(role);
                }
            }
            else
            {
                string updateAttemptsQuery = "UPDATE Users SET attem_us = attem_us + 1 WHERE username = @username";
                DatabaseHelper.ExecuteNonQuery(updateAttemptsQuery, new MySqlParameter[] {
                    new MySqlParameter("@username", username)
                });

                string checkAttemptsQuery = "SELECT attem_us FROM Users WHERE username = @username";
                DataTable attemptsResult = DatabaseHelper.ExecuteQuery(checkAttemptsQuery, new MySqlParameter[] {
                    new MySqlParameter("@username", username)
                });

                if (attemptsResult.Rows.Count > 0 && Convert.ToInt32(attemptsResult.Rows[0]["attem_us"]) >= 3)
                {
                    string lockUserQuery = "UPDATE Users SET locked_us = TRUE WHERE username = @username";
                    DatabaseHelper.ExecuteNonQuery(lockUserQuery, new MySqlParameter[] {
                        new MySqlParameter("@username", username)
                    });
                    MessageBox.Show("Вы ввели неверный логин или пароль 3 раза. Ваша учетная запись заблокирована. Обратитесь к администратору", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введенные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateUserLoginInfo(int userId, bool resetAttempts)
        {
            string query = "UPDATE Users SET last_us = NOW(), attem_us = @attem_us WHERE user_id = @userId";
            MySqlParameter[] parameters = new MySqlParameter[] {
                new MySqlParameter("@attem_us", resetAttempts ? 0 : (object)DBNull.Value),
                new MySqlParameter("@userId", userId)
            };
            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }


        private void OpenMainForm(string role)
        {
            if (role == "admin")
            {
                AdminDashboard adminForm = new AdminDashboard();
                adminForm.Show();
            }
            else
            {
                MainMenuDashboard userForm = new MainMenuDashboard();
                userForm.Show();
            }
        }

       
    }
    
}
