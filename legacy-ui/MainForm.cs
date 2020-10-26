using System;
using System.Windows.Forms;

namespace Legacy.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            var service = new Service();

            Cursor = Cursors.WaitCursor;

            try
            {
                labelResultado.Text = service.Navegamos(textCiudad.Text) ? "SI!!!" : "No, mantenimiento!";
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }

            Cursor = Cursors.Default;
        }
    }
}