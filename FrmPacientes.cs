using System;
using System.Windows.Forms;

namespace Medico
{
    public partial class FrmPacientes : Form
    {
        private int contadorId = 1;

        public FrmPacientes()
        {
            InitializeComponent();
            this.Load += FrmPacientes_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmPacientes_Load(object sender, EventArgs e)
        {
            cbSexo.Items.AddRange(new string[] { "Masculino", "Femenino", "Prefiero no decirlo", "Otro" });

            if (dgvDatosPaciente.Columns.Count == 0)
            {
                dgvDatosPaciente.Columns.Add("IdPaciente", "ID");
                dgvDatosPaciente.Columns.Add("Nombre", "Nombre");
                dgvDatosPaciente.Columns.Add("FechaNacimiento", "Fecha de Nacimiento");
                dgvDatosPaciente.Columns.Add("Dni", "DNI");
                dgvDatosPaciente.Columns.Add("Sexo", "Sexo");
                dgvDatosPaciente.Columns.Add("Direccion", "Dirección");
                dgvDatosPaciente.Columns.Add("Telefono", "Teléfono");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Agrega a la grilla
            dgvDatosPaciente.Rows.Add(
                contadorId,
                txtNombre.Text,
                dtpNacimiento.Value.ToShortDateString(),
                txtDni.Text,
                cbSexo.Text,
                txtDireccion.Text,
                txtTelefono.Text
            );

            // Guardar en la base de datos con DAO
            try
            {
                PacienteDAO dao = new PacienteDAO();
                dao.InsertarPaciente(
                    txtNombre.Text,
                    dtpNacimiento.Value.Date,
                    txtDni.Text,
                    cbSexo.Text,
                    txtDireccion.Text,
                    txtTelefono.Text
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la base de datos: " + ex.Message);
            }

            contadorId++;
            LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDatosPaciente.CurrentRow != null)
            {
                DataGridViewRow fila = dgvDatosPaciente.CurrentRow;

                fila.Cells[1].Value = txtNombre.Text;
                fila.Cells[2].Value = dtpNacimiento.Value.ToShortDateString();
                fila.Cells[3].Value = txtDni.Text;
                fila.Cells[4].Value = cbSexo.Text;
                fila.Cells[5].Value = txtDireccion.Text;
                fila.Cells[6].Value = txtTelefono.Text;

                LimpiarCampos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatosPaciente.CurrentRow != null)
            {
                dgvDatosPaciente.Rows.RemoveAt(dgvDatosPaciente.CurrentRow.Index);
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDni.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cbSexo.SelectedIndex = -1;
            dtpNacimiento.Value = DateTime.Today;
        }

        // Métodos vacíos (puedes usarlos si necesitas lógica futura)
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void dtpNacimiento_ValueChanged(object sender, EventArgs e) { }
        private void txtDni_TextChanged(object sender, EventArgs e) { }
        private void cbSexo_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void dgvDatosPaciente_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
