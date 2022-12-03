using Inventario_Super.Clases;
using Inventario_Super.Formularios;
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

namespace Inventario_Super
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionBD con = new ConexionBD();
                SqlCommand comando = new SqlCommand();
                comando.Connection = con.abrirConexion();
                comando.CommandText = "select usuario,tipo from Usuarios where usuario=@usuario and contraseña=@contraseña";
                comando.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                comando.Parameters.AddWithValue("@contraseña", txtContraseña.Text);
                SqlDataReader datos = comando.ExecuteReader();
                if (datos.Read())
                {
                    Program.formInicio.Hide();
                    FormMenu fm = new FormMenu();
                    fm.ObtenerInformacionUsuario(datos["usuario"].ToString(), datos["tipo"].ToString());
                    fm.Show();
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    MessageBox.Show("Credenciales Correctas","Login Exitoso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Credenciales Incorrectas o este usuario no existe " + txtUsuario.Text, "Login Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Realizar Consulta \n  Motivo \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.formInicio.Show();
            }
        }
    }
}