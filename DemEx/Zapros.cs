using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using MySql.Data.MySqlClient;
namespace DemEx
{
    public partial class Zapros : Form
    {
        public Zapros()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Load1();
        }
        private void Load1()
        {
            string query = @"
                SELECT 
    (SUM(
        CASE 
            WHEN g.`Exit` IS NULL THEN DATEDIFF(CURDATE(), g.`Entry`)
            ELSE DATEDIFF(g.`Exit`, g.`Entry`)
        END
    ) * 100.0) / 
    ( (SELECT COUNT(*) FROM Room_stock) *  -- Всегда 10 номеров
      (DATEDIFF(
          COALESCE( (SELECT MAX(`Exit`) FROM Guests_currently_living_in_the_hotel), CURDATE() ),
          (SELECT MIN(`Entry`) FROM Guests_currently_living_in_the_hotel)
      ) + 1)
    ) AS overall_occupancy_percentage
FROM 
    Guests_currently_living_in_the_hotel g;";

            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    // Безопасное преобразование с проверкой на DBNull
                    object result = dt.Rows[0]["overall_occupancy_percentage"];
                    double occupancyRate = (result == DBNull.Value) ? 0 : Convert.ToDouble(result);

                    // Отображаем результат
                    lblOccupancyRate.Text = $"{occupancyRate:F2}%";

                    // Устанавливаем значение progressBar (ограничиваем от 0 до 100)
                    int progressValue = (int)Math.Round(Math.Min(100, Math.Max(0, occupancyRate)));
                    progressBar1.Value = progressValue;

                    // Устанавливаем цвет в зависимости от уровня загрузки
                    if (occupancyRate < 30)
                        progressBar1.ForeColor = Color.Red;
                    else if (occupancyRate < 70)
                        progressBar1.ForeColor = Color.Orange;
                    else
                        progressBar1.ForeColor = Color.Green;
                }
                else
                {
                    lblOccupancyRate.Text = "Нет данных";
                    progressBar1.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblOccupancyRate.Text = "Ошибка";
                progressBar1.Value = 0;
            }
        }
        private void Zapros_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load1();
        }

        private void Zapros_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
