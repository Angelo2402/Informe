using System;
using System.Windows.Forms;

namespace Medico
{
    public partial class FRMLOGIN : Form
    {
        public FRMLOGIN()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e) { }

        private void txtContraseña_TextChanged(object sender, EventArgs e) { }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtContraseña.Text.Trim();

            // Ahora usamos UsuarioDAO
            UsuarioDAO dao = new UsuarioDAO();

            try
            {
                if (dao.ValidarUsuario(usuario, clave))
                {
                    MessageBox.Show("BIENVENIDO AL SISTEMA MEDICO!!", "Acceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FrmPrincipal ventana = new FrmPrincipal();
                    ventana.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
