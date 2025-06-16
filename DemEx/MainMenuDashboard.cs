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
    public partial class MainMenuDashboard : Form
    {
        public MainMenuDashboard()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void валидацияФИОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidationFullName validForm = new ValidationFullName();
            validForm.Show();
        }

        private void отчетОЗагруженностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zapros zaprosForm = new Zapros();
            zaprosForm.Show();
        }

        private void гостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Гости guestsForm = new Гости();
            guestsForm.Show();
        }
    }
}
