using System;
using System.Windows.Forms;

namespace Legacy.UI
{
    public partial class MainForm : Form
    {
        private readonly Service _service;

        public MainForm()
        {
            InitializeComponent();

            _service = Service.WithDefaultProxy();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                labelResultado.Text = _service.Navegamos(textCiudad.Text) ? "SI!!!" : "No, mantenimiento!";
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }

            Cursor = Cursors.Default;
        }
    }
}