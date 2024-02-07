using ProcesoCrud.Data;
using ProcesoCrud.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesoCrud.Presentation
{
    public partial class Frm_Products : Form
    {
        private DataTable listProducts;

        public Frm_Products()
        {
            InitializeComponent();
        }
        #region "MIS VARIABLES"
        int stateSave = 0;
        int codProduct = 0;

        #endregion

        #region "METODOS"
        private void CleanText()
        {
            txtDescripcion_pr.Text = "";
            txtMarca_pr.Text = "";
            txtStockActual.Text = "0.00";
            cmbMedida.Text = "";
            cmbCategoria.Text = "";

        }

        private void StateButtons(bool lState)
        {
            btnCancelar.Visible= !lState;
            btnGuardar.Visible= !lState;

            btnNuevo.Enabled = lState;
            btnActualizar.Enabled = lState;
            btnEliminar.Enabled = lState;
            btnReporte.Enabled = lState;
            btnSalir.Enabled = lState;

            btnBuscar.Enabled = lState;
            txtBuscar.Enabled= lState;
            dgvListado.Enabled= lState;
        }

        private void StateText(bool lEstado)
        {
            txtDescripcion_pr.Enabled = lEstado;
            txtMarca_pr.Enabled = lEstado;
            txtStockActual.Enabled = lEstado;
            cmbMedida.Enabled = lEstado;
            cmbCategoria.Enabled = lEstado;
        }

        private void UploadMedidas()
        {
            D_Measures data = new D_Measures();
            cmbMedida.DataSource = data.List_me();
            cmbMedida.ValueMember = "codigo_me";
            cmbMedida.DisplayMember = "descripcion_me";
        }
        private void UploadCategories()
        {
            D_Categories data = new D_Categories();
            cmbCategoria.DataSource = data.List_cat();
            cmbCategoria.ValueMember = "codigo_cat";
            cmbCategoria.DisplayMember = "descripciuon_cat";
        }

        private void formatDgv()
        {
            dgvListado.Columns["codigo_me"].Visible = false;
            dgvListado.Columns["codigo_cat"].Visible = false;

        }


        private void LoadDgv(string cText)
        {
            D_Products negocy = new D_Products();
            try
            {
                listProducts = negocy.List_pr(cText);
                dgvListado.DataSource = listProducts;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        private void selectItem_pr()
        {
            if (string.IsNullOrEmpty(dgvListado.CurrentRow.Cells["codigo_pr"].Value.ToString()))
            {
                MessageBox.Show("No hay informacion seleccionada para visualizar",
                    "Aviso del Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                this.codProduct =(int)dgvListado.CurrentRow.Cells["codigo_pr"].Value;
                txtDescripcion_pr.Text= dgvListado.CurrentRow.Cells["descripcion_pr"].Value.ToString();
                txtMarca_pr.Text = dgvListado.CurrentRow.Cells["marca_pr"].Value.ToString();
                cmbMedida.Text = dgvListado.CurrentRow.Cells["descripcion_me"].Value.ToString();
                cmbCategoria.Text = dgvListado.CurrentRow.Cells["descripciuon_cat"].Value.ToString();
                txtStockActual.Text = dgvListado.CurrentRow.Cells["stock_actual"].Value.ToString();

            }
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.stateSave = 1;
            this.codProduct = 0;
            this.CleanText();
            this.StateText(true);
            this.StateButtons(false);
            txtDescripcion_pr.Select();
        }

        private void Frm_Products_Load(object sender, EventArgs e)
        {
            this.UploadMedidas();
            this.UploadCategories();
            this.LoadDgv("%");
            formatDgv();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CleanText();
            this.StateText(false);
            this.StateButtons(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion_pr.Text == string.Empty ||
                txtMarca_pr.Text == string.Empty ||
                cmbMedida.Text == string.Empty ||
                cmbCategoria.Text == string.Empty ||
                txtStockActual.Text == string.Empty)
            {
                MessageBox.Show("Falta ingresar datos requeridos (*)", "Aviso del Sistema",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string Resp = "";
                E_Products aux = new E_Products();
                aux.codigo_pr = this.codProduct;
                aux.descripcion_pr = txtDescripcion_pr.Text;
                aux.marca_pr = txtMarca_pr.Text;
                aux.codigo_me = (int)cmbMedida.SelectedValue;
                aux.codigo_cat = (int)cmbCategoria.SelectedValue;
                aux.stock_actual = Convert.ToDecimal(txtStockActual.Text);

                D_Products dates = new D_Products();
                Resp = dates.Save_pr(this.stateSave, aux);

                if (Resp == "OK")
                {

                    dgvListado.DataSource =  dates.List_pr("%");
                    MessageBox.Show("Los datos han sido guardados correctamente",
                        "Aviso del Sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.codProduct = 0;
                    CleanText();
                    StateButtons(true);
                    StateText(false);
                }

            }
        }

        private void dgvListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.selectItem_pr();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.stateSave = 2;
            this.StateText(true);
            this.StateButtons(false);
            txtDescripcion_pr.Select();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            D_Products dates = new D_Products();
            string filter = txtBuscar.Text;
            dgvListado.DataSource =  dates.List_pr(filter);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListado.Rows.Count <= 0 || string.IsNullOrEmpty(dgvListado.CurrentRow.Cells["codigo_pr"].Value.ToString()))
            {
                MessageBox.Show("No hay informacion para eliminar",
                    "Aviso del sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                string Resp = "";
                D_Products dates = new D_Products();
                Resp = dates.ActiveProduct(codProduct, false);
                if (Resp == "OK")
                {
                    dgvListado.DataSource = dates.List_pr("%");
                    CleanText();
                    codProduct = 0;
                    MessageBox.Show("El registro seleccionado ha sido eliminado",
                        "Aviso del Sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);   
                }
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
