using Inventario_Super.Clases;
using Inventario_Super.Formularios.EntradaSalidaProductos;
using Inventario_Super.Formularios.FormsProductos;
using Inventario_Super.Formularios.FormsUsuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario_Super.Formularios
{
    public partial class FormMenu : Form , InterfaceInfoU
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        public bool cerrar = false;

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cerrar == false)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    MessageBox.Show("Para salir de la Plataforma debe cerrar sesión", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ObtenerInformacionUsuario(string usuario, string tipo)
        {
            lblUsuario.Text = usuario;
            lblTipo.Text = tipo;
            switch (tipo)
            {
                case "Jefe":
                    //No tiene acceso a la Administracion de empleados
                    btnConsultarE.Enabled = false;
                    btnAgregarE.Enabled = false;
                    btnEditarE.Enabled = false;
                    btnEliminarE.Enabled = false;
                    break;
                case "Supervisor":
                    //No tiene acceso a la Administracion de empleados
                    btnConsultarE.Enabled = false;
                    btnAgregarE.Enabled = false;
                    btnEditarE.Enabled = false;
                    btnEliminarE.Enabled = false;
                    //No tiene acceso a la Administracion de usuarios
                    btnConsultarU.Enabled = false;
                    btnAgregarU.Enabled = false;
                    btnEditarU.Enabled = false;
                    btnEiminarU.Enabled = false;
                    break;
                case "Encargado":
                    //No tiene acceso a la Administracion de empleados
                    btnConsultarE.Enabled = false;
                    btnAgregarE.Enabled = false;
                    btnEditarE.Enabled = false;
                    btnEliminarE.Enabled = false;
                    //No tiene acceso a la Administracion de usuarios
                    btnConsultarU.Enabled = false;
                    btnAgregarU.Enabled = false;
                    btnEditarU.Enabled = false;
                    btnEiminarU.Enabled = false;
                    //No tiene acceso a la Administracion de Productos
                    btnConsultarP.Enabled = false;
                    btnAgregarP.Enabled = false;
                    btnEditarP.Enabled = false;
                    btnEliminarP.Enabled = false;
                    break;
                default:
                    //Tiene acceso a todas las opciones
                    break;
            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnConsultaG_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "Select idProducto,codigo,nombre,descripcion,cantidad from Productos";
                SqlDataReader datos = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(datos);
                dataGridView1.DataSource = tabla;
                con.cerrarConexion();
                MessageBox.Show("Consulta exitosa con la Base de Datos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar consulta con la Base de Datos" + ex.Message, "ATENCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btnLimpiarC_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estimado usuario... Desea Cerrar Sesión", "Salir de la plataforma", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                cerrar = true;
                this.Close();
                Program.formInicio.Show();
            }
        }

        private void btnConsultarE_Click(object sender, EventArgs e)
        {
            FormConsultarE fce = new FormConsultarE();
            fce.Show();
        }

        private void btnAgregarE_Click(object sender, EventArgs e)
        {
            FormAgregarE fae = new FormAgregarE();
            fae.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fae.Show();
        }

        private void btnEditarE_Click(object sender, EventArgs e)
        {
            FormEditarE fee = new FormEditarE();
            fee.Show();
        }

        private void btnEliminarE_Click(object sender, EventArgs e)
        {
            FormEliminarE fee = new FormEliminarE();
            fee.Show();
        }

        private void btnConsultarU_Click(object sender, EventArgs e)
        {
            FormConsultarU fcu = new FormConsultarU();
            fcu.Show();
        }

        private void btnAgregarU_Click(object sender, EventArgs e)
        {
            FormAgregarU fau = new FormAgregarU();
            fau.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fau.Show();
        }

        private void btnEditarU_Click(object sender, EventArgs e)
        {
            FormEditarU feu = new FormEditarU();
            feu.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            feu.Show();
        }

        private void btnEiminarU_Click(object sender, EventArgs e)
        {
            FormEliminarU feu = new FormEliminarU();
            feu.Show();
        }

        private void btnConsultarP_Click(object sender, EventArgs e)
        {
            FormConsultarP fcp = new FormConsultarP();
            fcp.Show();
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            FormAgregarP fap = new FormAgregarP();
            fap.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fap.Show();
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            FormEliminarP fep = new FormEliminarP();
            fep.Show();
        }

        private void btnEditarP_Click(object sender, EventArgs e)
        {
            FormEditarP fep = new FormEditarP();
            fep.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fep.Show();
        }

        private void btnAgregarEn_Click(object sender, EventArgs e)
        {
            FormEntrada fae = new FormEntrada();
            fae.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fae.Show();
        }

        private void btnAgregarSa_Click(object sender, EventArgs e)
        {
            FormSalida fas = new FormSalida();
            fas.ObtenerInformacionUsuario(lblUsuario.Text,lblTipo.Text);
            fas.Show();
        }
    }
}