﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArenasProyect3.Modulos.Admin
{
    public partial class MenuPrincipal : Form
    {
        //VARIABLES GENERALES
        string maquina = Environment.MachineName;

        //CONSTRUCTOR DEL MANTENIMIENTO - MENU ADMINISTRADOR
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        //CÓDIGO PARA PODER MOSTRAR LA HORA EN VIVO
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHoraVivo.Text = DateTime.Now.ToString("H:mm:ss tt");
            lblFechaVivo.Text = DateTime.Now.ToLongDateString();
        }

        //Drag Form - LIBRERIA PARA PODER MOVER EL FORMULARIO PRINCIPAL
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int LParam);

        //EVENTO PARA TRAER LAS LIBRERIAS PARA PODER MOVER
        private void panelPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //MINIMIZAR EL MENÚ PRINCIPAL
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //CERRAR EL MENÚ PRINCIPAÑ
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //EVENTO DE INICIO Y DE CARGA DEL MENÚ PRINCIPAL
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            //FUNCION PARA CARGAR DATOS DEL USUARIO
            DatosUsuario();
            CargarNovedades();

            //AJUSTAR FECHAS DESDE EL PRIEMRO AL ULTIMO DEIA DEL MES
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            DesdeFecha.Value = oPrimerDiaDelMes;
            HastaFecha.Value = oUltimoDiaDelMes;

            //FUNCION PARA COLOCAR DATOS RELEVANTES
            //ReporteMenuComercial(DesdeFecha.Value, HastaFecha.Value);
        }

        //EVENTOS DE ACCIONES  DEL MENÚ RPINCIPAL------------------------------------------------------------------
        //ABRIR EL PEQUEÑO PANEL DE CIERRE DE SESIÓN Y CONFIGURACIÓN
        private void imgUsuario_Click(object sender, EventArgs e)
        {
            if (panelConfiguracionUsuario.Visible == true)
            {
                panelConfiguracionUsuario.Visible = false;
            }
            else
            {
                panelConfiguracionUsuario.Visible = true;
            }
        }

        //ABRIR EL PANEL DE CONFIGURACIÓN DE LA APLICACIÓN
        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            panelConfiguracion.Visible = true;
            txtMaquinaHabilitada.Text = maquina;
            txtUsuarioActual.Text = datalistadoBusquedaUusario.SelectedCells[1].Value.ToString() + " " + datalistadoBusquedaUusario.SelectedCells[2].Value.ToString();
        }

        //ABRIR EL PANEL DE LOS DATOS DEL USUARIO ACTUAL
        private void btnDetalleUsuario_Click(object sender, EventArgs e)
        {
            panelDetallesUsuario.Visible = true;
            VisualizarDatosUsuario();
        }

        //ABRIR EL PANEL DE LOS DATOS DEL USUARIO ACTUAL
        private void btnInformacionUsuario_Click(object sender, EventArgs e)
        {
            panelDetallesUsuario.Visible = true;
            panelConfiguracionUsuario.Visible = false;
            VisualizarDatosUsuario();
        }

        //ABRIR EL PANEL DE PERSONALIZACION DE BARRA DE ACCESIBILIDAD
        private void btnPersonalizacionAccesibilidad_Click(object sender, EventArgs e)
        {
            panelProhibicion.Visible = true;
        }

        //ABRIR EL PANEL DE GESTION DE GRAFICOS
        private void btnAreaGraficos_Click(object sender, EventArgs e)
        {
            panelProhibicion.Visible = true;
        }

        //ABRIRI EL PANEL CON LOS DATOS DE LA TABLA AUDITORA
        private void btnAuditora_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL MANTENIMIENTO PARA ADMINISTRAR LAS NOVEDADES
        private void btnEstadoNovedades_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL MANTENIMIENTO DE ESTADO DE LICENCIAS
        private void btnEstadoLicencias_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL MANTENIMIENTO PARA ADMINISTRAR EL ESTADO DE SISTEMA
        private void btnEstadoSistema_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL PANEL PAR ADMINISTRAR LAS NOTIFICACIONES
        private void btnAdministrarNotificaciones_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL PANLE PARA ADMINISTRAR USUARIOS
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR ULTIMOS CAMBIOS REALOZADPS
        private void btnUltimosCambios_Click(object sender, EventArgs e)
        {
            if (panelPrincipalAdministrador.Controls.Count == 1)
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            else
            {
                panelPrincipalAdministrador.Controls.Clear();
                AbrirFormularios(new MenuAdministracion());
            }
            panelDatos.Visible = false;
        }

        //ABRIR EL PANEL DE CONFIRMACIÓN DE VISUALIZACIÓN DE CONTRASEÑA
        private void btnVisualizarContrasena_Click(object sender, EventArgs e)
        {
            panelConfirmacioncontrasena.Visible = true;
        }

        //MOSTRA CONTRASEÑA EN LA ETAPA DE CONFIRMACIÓN
        private void btnConfirmarContrasenaF_Click(object sender, EventArgs e)
        {
            txtCOntrasenaUsuario.PasswordChar = '\0';
            panelConfirmacioncontrasena.Visible = false;
        }

        //CANCELAR MUESTRA DE LA CONTRASEÑA EN LA ETAPA DE CONFIRMACIÓN
        private void btnCancelarContrasenaF_Click(object sender, EventArgs e)
        {
            panelConfirmacioncontrasena.Visible = false;
        }

        //CERRAR VENTANA DE DETALLES DEL USUARIO QUE INICIO SESIÓN
        private void btnCerrarDetallesUsuario_Click(object sender, EventArgs e)
        {
            panelDetallesUsuario.Visible = false;
            txtCOntrasenaUsuario.PasswordChar = '*';
        }

        //CERRAR VENTANA DE DETALLES DEL USUARIO QUE INICIO SESIÓN
        private void btnAceptarF_Click(object sender, EventArgs e)
        {
            panelDetallesUsuario.Visible = false;
            txtCOntrasenaUsuario.PasswordChar = '*';
        }

        //CERRAR PANEL DE CONFIGURACIÓN DEL MENÚ DEL SISTEMA
        private void btnCerrarConfiguracion_Click(object sender, EventArgs e)
        {
            panelConfiguracion.Visible = false;
        }

        //CERRAR PANEL DE NOVEDADES EL MENÚ DEL SISTEMA
        private void btnCerrarPanelNovedades_Click(object sender, EventArgs e)
        {
            panelNovedades.Visible = false;
        }

        //LINK QUE ABRE LA PÁGINA DE LA EMPRESA HACIENDO CLICK EN EL LOGO
        private void btnPaginaArenas_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.arenassrl.com.pe/");
        }

        //REGRESAR AL INCIO DEL MENÚ Y CERRAR TODAS LOS SUBMENUS
        private void btnHome_Click(object sender, EventArgs e)
        {
            this.panelPrincipalAdministrador.Controls.Clear();
            panelDatos.Visible = true;
        }

        //CERRAR SESIÓN DEL SISTEMA
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            Modulos.Login frm = new Modulos.Login();
            frm.Show();
        }

        //BUSQUEDA DE REPORTES SEGÚN LA FECH ASELECCIONADA - DESDE
        private void DesdeFecha_ValueChanged(object sender, EventArgs e)
        {
            //ReporteMenuComercial(DesdeFecha.Value, HastaFecha.Value);
        }

        //BUSQUEDA DE REPORTES SEGÚN LA FECH ASELECCIONADA - HASTA
        private void HastaFecha_ValueChanged(object sender, EventArgs e)
        {
            //ReporteMenuComercial(DesdeFecha.Value, HastaFecha.Value);
        }

        //BLOQUEO DE ACCESOS------------------------------------------------------
        //BLOQUEO DE ACCESO A REPORTE GENERAL DEL ÁREA
        //SALIR DE LA VENTANA DE NOTIFICACIÓN DE PROHIBICIÓN DE ACCESO
        private void btnSalirNotificacionProhibicionF_Click(object sender, EventArgs e)
        {
            panelProhibicion.Visible = false;
        }

        //CARGA DE METODOS - GENERAL----------------------------------------------------------------------------------
        //CARGA DE DATOS DEL USUARIO QUE INICIO SESIÓN
        public void VisualizarDatosUsuario()
        {
            imgUsuario2.BackgroundImage = null;
            byte[] b = (Byte[])datalistadoBusquedaUusario.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            imgUsuario2.Image = Image.FromStream(ms);

            txtNombreusuario.Text = datalistadoBusquedaUusario.SelectedCells[1].Value.ToString();
            txtApellidousuario.Text = datalistadoBusquedaUusario.SelectedCells[2].Value.ToString();
            txtCorreoUsuario.Text = datalistadoBusquedaUusario.SelectedCells[3].Value.ToString();
            txtCOntrasenaUsuario.Text = datalistadoBusquedaUusario.SelectedCells[4].Value.ToString();
            txtAreaUsuario.Text = datalistadoBusquedaUusario.SelectedCells[7].Value.ToString();
            txtRolusuario.Text = datalistadoBusquedaUusario.SelectedCells[9].Value.ToString();
        }

        //BUSQUEDA DE USUARIO
        public void DatosUsuario()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Conexion.ConexionMaestra.conexion;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("BuscarUsuarioPorCodigo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idusuario", Program.IdUsuario);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            datalistadoBusquedaUusario.DataSource = dt;
            con.Close();

            imgUsuario.BackgroundImage = null;
            byte[] b = (Byte[])datalistadoBusquedaUusario.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            imgUsuario.Image = Image.FromStream(ms);

            lblusuarioActual.Text = datalistadoBusquedaUusario.SelectedCells[1].Value.ToString() + " " + datalistadoBusquedaUusario.SelectedCells[2].Value.ToString();
            Program.NombreUsuarioCompleto = datalistadoBusquedaUusario.SelectedCells[1].Value.ToString() + " " + datalistadoBusquedaUusario.SelectedCells[2].Value.ToString();
        }

        //CARGA DE LAS NOVEDADES Y VALIDACIÓN DEL TIEMPO DE APARICIÓN
        public void CargarNovedades()
        {
            //CARGA DE PROCEDIMIENTO ALMACENADO
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Conexion.ConexionMaestra.conexion;
            con.Open();
            da = new SqlDataAdapter("SELECT IdEstadoSistemaInicio, Descripcion, VersionSsitema, FechaInstalacionSsitema, NuevasFuncionesNovedades, FechaAparicion FROM EstadoSistemaInicio WHERE Estado = 1", con);
            da.Fill(dt);
            datalistadoNovedades.DataSource = dt;
            con.Close();
            //SI NO HAY FILAS O SI LA CONSULTA NO TIENE RESULTADO
            if (datalistadoNovedades.RowCount > 0)
            {
                //SE CAPTURA LA FECHA DE TÉRMINO DEL MENSAJE DE MI LISTADO YA CARGADO GRACIAS A LA CONSULTA
                DateTime fechaTerminoNotificacion = Convert.ToDateTime(datalistadoNovedades.SelectedCells[5].Value.ToString());
                //SI LA FECHA DE TÉRMINO ES MENOR A LA FECHA ACTUAL
                if (fechaTerminoNotificacion > DateTime.Now)
                {
                    //CAPTURA DE VARIABLES PARA LA MUESTRA DE ESTAS
                    string versionSistema = datalistadoNovedades.SelectedCells[2].Value.ToString();
                    string fechaInstalacion = datalistadoNovedades.SelectedCells[3].Value.ToString();
                    string mensajeNovedades = datalistadoNovedades.SelectedCells[4].Value.ToString();
                    //HABILITAR LA VISIBILIDAD DEL PANEL
                    panelNovedades.Visible = true;
                    //ASIGNAR LAS VARIABLES CAPTURADAS A LOS ELEMNETOS DEL PANEL DE NOVEDADES
                    lblVersionSistema.Text = versionSistema;
                    lblFechaInstalacion.Text = fechaInstalacion;
                    lblNovedadesNuevasFunciones.Text = mensajeNovedades;
                }
                else
                {
                    //OCULTAR EL PANEL DE NOVEDADES
                    panelNovedades.Visible = false;
                }
            }
            else
            {
                //OCULTAR EL PANEL DE NOVEDADES
                panelNovedades.Visible = false;
            }
        }

        //ACIONES DE ENTRAR Y SALIR DE LOS FORMULARIOS Y DE CIERRE DE SESION
        public void AbrirFormularios(object formFormulario)
        {
            Form frm = formFormulario as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelPrincipalAdministrador.Controls.Add(frm);
            this.panelPrincipalAdministrador.Tag = frm;
            frm.Show();
        }

        //BOTON PARA CAMBIAR DE ESTILO O TEMA
        private void btnCambiarEstilo_Click(object sender, EventArgs e)
        {
            if(rbClaro.Checked == true)
            {
                AplicarTemaClaro(this);
            }
            else
            {
                AplicarTemaOscuro(this);
            }
        }

        void AplicarTemaClaro(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.BackColor = SystemColors.Control;
                ctrl.ForeColor = SystemColors.ControlText;

                if (ctrl.HasChildren)
                    AplicarTemaClaro(ctrl);
            }
        }

        void AplicarTemaOscuro(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.BackColor = Color.FromArgb(32, 32, 32);
                ctrl.ForeColor = Color.White;

                if (ctrl.HasChildren)
                    AplicarTemaOscuro(ctrl);
            }
        }
    }
}
