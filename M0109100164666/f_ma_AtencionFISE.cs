using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatos;
using AncestrosOT;
using Ancestros;
using SIELSEControles;
using SIELSEUtil;
using System.Runtime.InteropServices;
using M0109100164666.Services;
using M0101100123000;
using System.Runtime.Remoting;
using System.Diagnostics;

namespace M0109100164666
{
    public partial class f_ma_AtencionFISE : Ancestros.SIELSEBaseMantenimiento
    {
        AccesoDatos.clsAccesoDatos objComercial;
        AccesoDatos.clsDatosRetorno objDatosRetorno;
        protected long lngCodigoSolicitudFISEBeneficiarioFiltro;

        public f_ma_AtencionFISE(Guid guiHandle, string strContextoSeguridad, string strLoginUsuario, string strCodigoModulo)
        : base(guiHandle, strContextoSeguridad, strLoginUsuario, strCodigoModulo)
        {
            InitializeComponent();
        }
        public f_ma_AtencionFISE(Guid guiHandle, string strContextoSeguridad, string strLoginUsuario, string strCodigoModulo, long lngCodigoSolicitudFISEBeneficiarioFiltro_)
        : this (guiHandle, strContextoSeguridad, strLoginUsuario, strCodigoModulo)
        {
            //InitializeComponent();
            lngCodigoSolicitudFISEBeneficiarioFiltro = lngCodigoSolicitudFISEBeneficiarioFiltro_;
        }
        protected override void prInicializarVariables()
        {
            base.prInicializarVariables();
            prInicializarOrigenesDatos(bsotaSolicitudFISEBeneficiario, bsotaSolicitudFISEBeneficiarioPorFiltro , components.Components);

            dgvPrincipal = dgvSolicitudesFISEFiltro;
            dgvPrincipal.AutoGenerateColumns = false;

            dstAtencionesFISE.taSolicitudFISEBeneficiario.TableNewRow += TaSolicitudFISEBeneficiario_TableNewRow;
                

            strTituloInicialVentana = "Solicitudes de Atenciones FISE";

            objComercial = new AccesoDatos.clsAccesoDatos(SIELSESeguridad.fnObtenerContexto(eSistemas.Comercial), SIELSESeguridad.fnObtenerUsuarioConexion());

            string strContextoConexion = SIELSESeguridad.fnObtenerContexto(eSistemas.Comercial);
            string strUsuarioConexion = SIELSESeguridad.fnObtenerUsuarioConexion();

            sielsePadronRUCBeneficiario.ContextoConexion = strContextoConexion;
            sielsePadronRUCBeneficiario.UsuarioConexion = strUsuarioConexion;
            sielsePadronRUCBeneficiario.CodigoSucursal = SIELSESeguridad.Sesion.CodigoSucursal;
            sielsePadronRUCBeneficiario.LoginUsuario = SIELSESeguridad.Sesion.StrLoginUsuario;

            sielsePadronRUCSolicitante.ContextoConexion = strContextoConexion;
            sielsePadronRUCSolicitante.UsuarioConexion = strUsuarioConexion;
            sielsePadronRUCSolicitante.CodigoSucursal = SIELSESeguridad.Sesion.CodigoSucursal;
            sielsePadronRUCSolicitante.LoginUsuario = SIELSESeguridad.Sesion.StrLoginUsuario;

            
        }

        private void TaSolicitudFISEBeneficiario_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row["CodigoSolicitudFISEBeneficiario"] = 0;
        }

        protected override void prEjecutarFiltros()
        {

            objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.smtaSolicitudFISEBeneficiarioPorFiltro", 
                0,
                chkSucursal.Checked ? 1 : 0, cboSucursal.SelectedValue,
                chkZonaAdministrativa.Checked ? 1 : 0, cboZonaAdministrativa.SelectedValue,
                chkEstadoSolicitudFISE.Checked ? 1 : 0, cboEstadoSolicitudFISE.SelectedValue,
                ckSubEstadoSolicitudFISE.Checked ? 1 : 0, cboSubEstadoSolicitudFISE.SelectedValue,
                Convert.ToDateTime(dtpFechaInicial.Text),
                Convert.ToDateTime(dtpFechaFinal.Text)
                );
            if (objDatosRetorno.Datos.Tables.Count > 0)
            {
                SIELSEUtil.SIELSEUtil.CopiarTabla(objDatosRetorno.Datos.Tables[0], dstAtencionesFISE.taSolicitudFISEBeneficiarioPorFiltro);
            }
            else
            {
                SIELSEUtil.SIELSEUtil.MostrarError("La consulta a la base de datos no devolvió ninguna tabla.");
            }
        }
        protected override void prActualizarDatosDetalle()
        {
            base.prActualizarDatosDetalle();
            eprControlError.Clear();

            objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.taSolicitudFISEBeneficiarioRegistroPorCodigo", bsotaSolicitudFISEBeneficiarioPorFiltro.Current != null ? (long)(((DataRowView)bsotaSolicitudFISEBeneficiarioPorFiltro.Current).Row["CodigoSolicitudFISEBeneficiario"])
            : (long)(((DataRowView)bsotaSolicitudFISEBeneficiario.Current).Row["CodigoSolicitudFISEBeneficiario"]));

            if (objDatosRetorno.Datos.Tables.Count > 0)
            {
                SIELSEUtil.SIELSEUtil.CopiarTabla(objDatosRetorno.Datos.Tables[0], dstAtencionesFISE.taSolicitudFISEBeneficiario);
            }
        }
        protected override void prActualizarControlesDetalle()
        {
            base.prActualizarControlesDetalle();
            SIELSEUtil.SIELSEUtil.MostrarAdvertencia("Actualizacion de Controles");


        }

        protected override void prNuevoRegistro(object sender, EventArgs e)
        {
            base.prNuevoRegistro(sender, e);
            if (bsoBase != bsoPrincipal) return;
            dstAtencionesFISE.taSolicitudFISEBeneficiario.Clear(); 
            bsotaSolicitudFISEBeneficiario.AddNew();
            tcoAtencionFISE.SelectedIndex = 0;
            tcoRegistroFISE.SelectedIndex = 0;
        }
        protected override void prGestionarControles()
        {
            base.prGestionarControles();

            var tlsButton = new ToolStripButton();
            //aclBase.SetAction(tlsRegistrado,actGuardar);

            tlsOpcionesFISE.Items.Add(tlsButton);
        }
        protected override void prAbrirDataSets()
        {
            base.prAbrirDataSets();
            DataSet dstAux;
            dstAux = objComercial.fnObtenerDatosProcedimientoDataSetN("dbo.smResumenSolicitudFISE");
            if (dstAux.Tables[0].Rows.Count > 0)
            {
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[0], dstAtencionesFISE.gnEstadoSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[1], dstAtencionesFISE.gnSubEstadoSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[2], dstAtencionesFISE.gnSucursal);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[3], dstAtencionesFISE.gnZonaAdministrativa);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[4], dstAtencionesFISE.gnTipoSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[5], dstAtencionesFISE.gnClaseSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[6], dstAtencionesFISE.gnMotivoSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[7], dstAtencionesFISE.gnRelacionSuministroViviendaSolicitudFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[8], dstAtencionesFISE.gnAccionVerificacionFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[9], dstAtencionesFISE.gnMotivoVerificacionFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[10], dstAtencionesFISE.gnMaterialPisoVerificacionFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[11], dstAtencionesFISE.gnRelacionFamiliarVerificacionFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[12], dstAtencionesFISE.gnDepartamentoFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[13], dstAtencionesFISE.gnProvinciaFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[14], dstAtencionesFISE.gnDistritoFISE);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[15], dstAtencionesFISE.gnDepartamentoFISESo);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[16], dstAtencionesFISE.gnProvinciaFISESo);
                SIELSEUtil.SIELSEUtil.CopiarTabla(dstAux.Tables[17], dstAtencionesFISE.gnDistritoFISESo);
            }            
        }
       

        private void btnAgregarVerificacion_Click(object sender, EventArgs e)
        {
            f_VerificacionFISE oFormVerificacion = new f_VerificacionFISE(objComercial, dstAtencionesFISE.gnAccionVerificacionFISE, dstAtencionesFISE.gnMotivoVerificacionFISE, dstAtencionesFISE.gnMaterialPisoVerificacionFISE, dstAtencionesFISE.gnRelacionFamiliarVerificacionFISE);
            oFormVerificacion.ShowDialog();
        }

        private void cboTipoSolicitud_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboTipoSolicitud.SelectedValue != null)
            {
                bsognClaseSolicitudFISE.Filter = "CodigoTipoSolicitudFISE=" + cboTipoSolicitud.SelectedValue;
                cboClaseSolicitudFISE.Focus();
                bsognMotivoSolicitudFISE.ResetBindings(false);
            }
        }

        private void cboClaseSolicitudFISE_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboClaseSolicitudFISE.SelectedValue != null && cboTipoSolicitud.SelectedValue != null)
            {
                bsognMotivoSolicitudFISE.Filter = "CodigoTipoSolicitudFISE=" + cboTipoSolicitud.SelectedValue + " AND "+ "CodigoClaseSolicitudFISE=" + cboClaseSolicitudFISE.SelectedValue;
                cboMotivoSolicitudOsinergmin.Focus();
                bsognMotivoSolicitudFISE.ResetBindings(false);
            }
        }

        private void cboSucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSucursal.SelectedValue != null)
            {
                bsognZonaAdministrativo.Filter = "CodigoSucursal=" + cboSucursal.SelectedValue;                
            }
        }

        private void cboEstadoSolicitudFISE_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboEstadoSolicitudFISE.SelectedValue != null)
            {
                bsognSubEstadoSolicitudFISE.Filter = "CodigoEstadoSolicitudFISE=" + cboEstadoSolicitudFISE.SelectedValue;              
            }
        }

        


        private void sielsePadronRUCBeneficiario_UserSelectedValueChanged(object sender, UserSelectedValuePropertyArgs e)
        {
            //sielsePadronRUCBeneficiario.TextoBusqueda = string.Empty;

            string NumeroDocumento;
            byte TipoDocumentoIdentidad;
        
            if (Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]) == 2)
            {
                NumeroDocumento = sielsePadronRUCBeneficiario.Registro["NumeroRUC"].ToString();
                TipoDocumentoIdentidad = Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]);
                txtNombreRazonSocialA.Text = sielsePadronRUCBeneficiario.Registro["RazonSocial"].ToString();
                txtApellidoPaternoA.Enabled = false;
                txtApellidoPaternoA.Clear();
                txtApellidoMaternoA.Enabled = false;
                txtApellidoMaternoA.Clear();
            }
            else
            {
                NumeroDocumento = sielsePadronRUCBeneficiario.Registro["NumeroDNI"].ToString();
                TipoDocumentoIdentidad = Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]);
                txtApellidoPaternoA.Enabled = true;
                txtApellidoMaternoA.Enabled = true;
                //txtApellidoPaternoA.Text = sielsePadronRUCBeneficiario.Registro["RazonSocial"].ToString();
                //txtApellidoMaternoA.Text = sielsePadronRUCBeneficiario.Registro["RazonSocial"].ToString();
                txtNombreRazonSocialA.Text = sielsePadronRUCBeneficiario.Registro["RazonSocial"].ToString();
            }

            txtCelularOtros.Text = sielsePadronRUCBeneficiario.Registro["Telefono"].ToString();
            txtPadronRUCBeneficiario.Text = sielsePadronRUCBeneficiario.Registro["CodigoPadronRUC"].ToString();
            txtPadronRUCBeneficiario.Enabled = false;
            DatosSuministroBeneficiario(TipoDocumentoIdentidad, NumeroDocumento);
            btnBuscarCodigoSuministroU_Click(sender, e);
            //DataSet dstAux;
            //dstAux = objComercial.fnObtenerDatosProcedimientoDataSetN("dbo.spFISE_Buscar_Suministro", TipoDocumentoIdentidad, NumeroDocumento);

            //if (dstAux.Tables[0].Rows.Count > 0)
            //{
            //    txtCodigoSuministroBeneficiario.Text = dstAux.Tables[0].Rows[0]["CodigoSuministro"].ToString();
            //    lblLatitudSuministroBeneficiario.Text = dstAux.Tables[0].Rows[0]["Latitud"].ToString();
            //    lblLongitudSuministroBeneficiario.Text = dstAux.Tables[0].Rows[0]["Longitud"].ToString();
            //    txtCorreoOtros.Text = dstAux.Tables[0].Rows[0]["CorreoElectronico"].ToString();
            //    txtCelularOtros.Text = dstAux.Tables[0].Rows[0]["Telefono"].ToString();
            //    txtNombreTitularU.Text = dstAux.Tables[0].Rows[0]["NombreSuministro"].ToString();
            //    cboDepartamentoBeneficiario.SelectedValue = dstAux.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString();
            //    //cboProvinciaBeneficiario.SelectedValue = dstAux.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
            //    bsognProvincia.Filter = "CodigoDepartamento=" + dstAux.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString() + " AND " + "CodigoProvincia=" + dstAux.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
            //    bsognDistrito.Filter = "CodigoDepartamento=" + cboDepartamentoBeneficiario.SelectedValue + " AND " + "CodigoProvincia=" + cboProvinciaBeneficiario.SelectedValue + " AND " + "CodigoDistrito=" + dstAux.Tables[0].Rows[0]["CodigoDistritoPredio"].ToString();
            //    txtDireccionBeneficiario.Text = dstAux.Tables[0].Rows[0]["DireccionPredio"].ToString();
            //}
        }
        private void DatosSuministroBeneficiario(byte TipoDocumentoIdentidad, string NumeroDocumento)
        {
            //DataSet dstAux;
            //bool rAplicaDetraccion = false;
            objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.spFISE_Buscar_Suministro", TipoDocumentoIdentidad, NumeroDocumento);

            if (objDatosRetorno.IdRetorno == 0)
            {
                if (objDatosRetorno.Datos.Tables.Count > 0)
                {
                    txtApellidoPaternoA.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["ApellidoPaterno"].ToString();
                    txtApellidoMaternoA.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["ApellidoMaterno"].ToString();
                    txtNombresBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["NombreCliente"].ToString();
                    txtCodigoSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoSuministro"].ToString();
                    lblLatitudSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Latitud"].ToString();
                    lblLongitudSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Longitud"].ToString();
                    txtCorreoOtros.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["CorreoElectronico"].ToString();
                    txtCelularOtros.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Telefono"].ToString();
                    txtNombreTitularU.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["NombreSuministro"].ToString();
                    cboDepartamentoBeneficiario.SelectedValue = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString();
                    //cboProvinciaBeneficiario.SelectedValue = dstAux.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
                    bsognProvincia.Filter = "CodigoDepartamento=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString() + " AND " + "CodigoProvincia=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
                    bsognDistrito.Filter = "CodigoDepartamento=" + cboDepartamentoBeneficiario.SelectedValue + " AND " + "CodigoProvincia=" + cboProvinciaBeneficiario.SelectedValue + " AND " + "CodigoDistrito=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDistritoPredio"].ToString();
                    txtDireccionBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["DireccionPredio"].ToString();

                }
                else SIELSEUtil.SIELSEUtil.MostrarError("No se cuenta con información de Suministro");
                //return rAplicaDetraccion;
            }
            else
                SIELSEUtil.SIELSEUtil.MostrarError("No se cuenta con información de Suministro");
        }
        private void cboDepartamentoBeneficiario_SelectionChangeCommitted(object sender, EventArgs e)
        {         
            if (cboDepartamentoBeneficiario.SelectedValue != null)
            {
                bsognProvincia.Filter = "CodigoDepartamento=" + cboDepartamentoBeneficiario.SelectedValue;               
            }
        }
        private void cboProvinciaBeneficiario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboDepartamentoBeneficiario.SelectedValue != null && cboProvinciaBeneficiario.SelectedValue != null)
            {
                bsognDistrito.Filter = "CodigoDepartamento=" + cboDepartamentoBeneficiario.SelectedValue + " AND " + "CodigoProvincia=" + cboProvinciaBeneficiario.SelectedValue;

            }
        }

        private void chkMismaPersonaSolicitante_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigoPadronRucSolicitante.Enabled = false;
            if ((chkMismaPersonaSolicitante.Checked) && (txtPadronRUCBeneficiario .Text!= ""))
            {
                if (Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]) == 2)
                {
                    txtDNISolicitante.Text = sielsePadronRUCBeneficiario.Registro["NumeroRUC"].ToString();
                }
                else
                {
                    if (base.fnValidarCamposRequeridos() == true)
                    {
                        sielsePadronRUCSolicitante.Visible = false;
                        sielsePadronRUCSolicitante.Focus();
                        txtDNISolicitante.Visible = true;
                        txtDNISolicitante.Text = sielsePadronRUCBeneficiario.Registro["NumeroDNI"].ToString();
                        txtApellidoPaternoSolicitante.Text = txtApellidoPaternoA.Text;
                        txtApellidoMaternoSolicitante.Text = txtApellidoMaternoA.Text;
                        txtNombresSolicitante.Text = txtNombresBeneficiario.Text;
                        txtCorreoSolicitante.Text = txtCorreoOtros.Text;
                        txtCelularSolicitante.Text = txtCelularOtros.Text;
                        txtDireccionSolicitante.Text = txtDireccionBeneficiario.Text;
                        cboDepartamentoSolicitante.SelectedValue = Convert.ToInt32(cboDepartamentoBeneficiario.SelectedValue.ToString());
                        bsognProvinciaFISESo.Filter = "CodigoDepartamentoSo=" + cboDepartamentoSolicitante.SelectedValue + " AND " + "CodigoProvinciaSo=" + cboProvinciaBeneficiario.SelectedValue;
                        bsognDistritoFISESo.Filter = "CodigoDepartamentoSo=" + cboDepartamentoSolicitante.SelectedValue + " AND " + "CodigoProvinciaSo=" + cboProvinciaBeneficiario.SelectedValue + " AND " + "CodigoDistritoSo=" + cboDistritoBeneficiario.SelectedValue;
                        lblLatitudSolicitanteDatos.Text = lblLatitudSuministroBeneficiario.Text;
                        lblLongitudSolicitanteDatos.Text = lblLongitudSuministroBeneficiario.Text;
                    }
                    else
                    {
                        SIELSEUtil.SIELSEUtil.MostrarError("Por favor revise toda la información del Beneficiario");
                    }
                
                }
                    txtCodigoPadronRucSolicitante.Text = txtPadronRUCBeneficiario.Text;
            }
            else
            {
                LimpiarDatos();
                sielsePadronRUCSolicitante.Visible = true;
                txtDNISolicitante.Visible = false;
                sielsePadronRUCSolicitante.ResetText();

            }
        }

        private void cboDepartamentoSolicitante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboDepartamentoSolicitante.SelectedValue != null)
            {
                bsognProvinciaFISESo.Filter = "CodigoDepartamentoSo=" + cboDepartamentoSolicitante.SelectedValue;
            }
        }

        private void cboProvinciaSolicitante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboDepartamentoSolicitante.SelectedValue != null && cboProvinciaSolicitante.SelectedValue != null)
            {
                bsognDistritoFISESo.Filter = "CodigoDepartamentoSo=" + cboDepartamentoSolicitante.SelectedValue + " AND " + "CodigoProvinciaSo=" + cboProvinciaSolicitante.SelectedValue;

            }
        }

        private void sielsePadronRUCSolicitante_UserSelectedValueChanged(object sender, UserSelectedValuePropertyArgs e)
        {
            byte TipoDocumentoIdentidad;
            string NumeroDocumento;
            if (Convert.ToByte(sielsePadronRUCSolicitante.Registro["CodigoTipoEntidad"]) == 2)
            {
                TipoDocumentoIdentidad = Convert.ToByte(sielsePadronRUCSolicitante.Registro["CodigoTipoEntidad"]);
                txtDireccionSolicitante.Text = sielsePadronRUCSolicitante.Registro["RazonSocial"].ToString();
                NumeroDocumento = sielsePadronRUCSolicitante.Registro["NumeroRUC"].ToString();              
                txtApellidoPaternoSolicitante.Enabled = false;
                txtApellidoPaternoSolicitante.Clear();
                txtApellidoMaternoSolicitante.Enabled = false;
                txtApellidoMaternoSolicitante.Clear();
            }
            else
            {
                TipoDocumentoIdentidad = Convert.ToByte(sielsePadronRUCSolicitante.Registro["CodigoTipoEntidad"]);
                NumeroDocumento = sielsePadronRUCSolicitante.Registro["NumeroDNI"].ToString();
                txtApellidoPaternoA.Enabled = true;
                txtApellidoMaternoA.Enabled = true;
                txtApellidoPaternoSolicitante.Text = sielsePadronRUCSolicitante.Registro["RazonSocial"].ToString();
                txtApellidoMaternoSolicitante.Text = sielsePadronRUCSolicitante.Registro["RazonSocial"].ToString();
                txtNombresSolicitante.Text = sielsePadronRUCSolicitante.Registro["RazonSocial"].ToString();                
                txtCelularSolicitante.Text = sielsePadronRUCSolicitante.Registro["Telefono"].ToString();
                txtDireccionSolicitante.Text = sielsePadronRUCSolicitante.Registro["Direccion"].ToString();

                DataSet dstAux;
                dstAux = objComercial.fnObtenerDatosProcedimientoDataSetN("dbo.spFISE_Buscar_Suministro", TipoDocumentoIdentidad, NumeroDocumento);
                if (dstAux.Tables[0].Rows.Count > 0)
                {
                    lblLatitudSolicitanteDatos.Text = dstAux.Tables[0].Rows[0]["Latitud"].ToString();
                    lblLongitudSolicitanteDatos.Text = dstAux.Tables[0].Rows[0]["Longitud"].ToString();
                    txtCorreoSolicitante.Text = dstAux.Tables[0].Rows[0]["CorreoElectronico"].ToString();
                    txtCelularSolicitante.Text = dstAux.Tables[0].Rows[0]["Telefono"].ToString();
                    cboDepartamentoSolicitante.SelectedValue = dstAux.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString();
                    bsognProvinciaFISESo.Filter = "CodigoDepartamentoSo=" + dstAux.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString() + " AND " + "CodigoProvinciaSo=" + dstAux.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
                    bsognDistritoFISESo.Filter = "CodigoDepartamentoSo=" + cboDepartamentoSolicitante.SelectedValue + " AND " + "CodigoProvinciaSo=" + cboProvinciaSolicitante.SelectedValue + " AND " + "CodigoDistritoSo=" + dstAux.Tables[0].Rows[0]["CodigoDistritoPredio"].ToString();
                }
            }
            txtCodigoPadronRucSolicitante.Text = sielsePadronRUCSolicitante.Registro["CodigoPadronRUC"].ToString();
      
        }
        protected void LimpiarDatos()
        {
            txtApellidoPaternoSolicitante.Clear();
            txtApellidoMaternoSolicitante.Clear();
            txtNombresSolicitante.Clear();
            txtCorreoSolicitante.Clear();
            txtCelularSolicitante.Clear();
            txtDireccionSolicitante.Clear();
            lblLatitudSolicitanteDatos.Text = "";
            lblLongitudSolicitanteDatos.Text = "";
            txtCodigoPadronRucSolicitante.Text = "";
        }
    
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void sielsePadronRUCSolicitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender,e);
        }

        private void txtDNISolicitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void sielsePadronRUCBeneficiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCelularOtros_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void txtCelularSolicitante_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros_KeyPress(sender, e);
        }

        private void chkSinSuministroU_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
            {
                txtCodigoSuministroBeneficiario.Enabled = false;
                txtNombreTitularU.Enabled = false;
                txtCodigoSuministroBeneficiario.Clear();
                txtNombreTitularU.Clear();
                btnBuscarCodigoSuministroU.Enabled = false;
                cboRelacionSuministroVivienda.Enabled = false;
            }
            else
            {
                txtCodigoSuministroBeneficiario.Enabled = true;
                txtNombreTitularU.Enabled = true;
                btnBuscarCodigoSuministroU.Enabled = true;
                cboRelacionSuministroVivienda.Enabled = true;
            }
           
        }

        private void btnBuscarCodigoSuministroU_Click(object sender, EventArgs e)
        {

            if (txtCodigoSuministroBeneficiario.Text == "")
            {
                SIELSEUtil.SIELSEUtil.MostrarAdvertencia("Por favor ingrese un suministro para realizar la busqueda");
            }
            else
            {
                
                if (txtCodigoSuministroBeneficiario.Text != "")
                {
                    objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.spFISE_Buscar_Suministro", 3, txtCodigoSuministroBeneficiario.Text);
                }
                else
                {
                    objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.spFISE_Buscar_Suministro", Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]) == 1 ? 1 : 2, sielsePadronRUCBeneficiario.Registro["NumeroDNI"].ToString());
                }

                if (objDatosRetorno.Datos.Tables[0].Rows.Count > 0)
                {
                    txtCodigoSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoSuministro"].ToString();
                    lblLatitudSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Latitud"].ToString();
                    lblLongitudSuministroBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Longitud"].ToString();
                    txtCorreoOtros.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["CorreoElectronico"].ToString();
                    txtCelularOtros.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["Telefono"].ToString();
                    txtNombreTitularU.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["NombreSuministro"].ToString();
                    cboDepartamentoBeneficiario.SelectedValue = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString();
                    //cboProvinciaBeneficiario.SelectedValue = dstAux.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
                    bsognProvincia.Filter = "CodigoDepartamento=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoPredio"].ToString() + " AND " + "CodigoProvincia=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoProvinciaPredio"].ToString();
                    bsognDistrito.Filter = "CodigoDepartamento=" + cboDepartamentoBeneficiario.SelectedValue + " AND " + "CodigoProvincia=" + cboProvinciaBeneficiario.SelectedValue + " AND " + "CodigoDistrito=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDistritoPredio"].ToString();
                    txtDireccionBeneficiario.Text = objDatosRetorno.Datos.Tables[0].Rows[0]["DireccionPredio"].ToString();

                }
                else
                {
                    SIELSEUtil.SIELSEUtil.MostrarAdvertencia("El suministro no existe por favor vuelva a ingresar un suministro correcto");
                }
            }
//            chkSinSuministroU.Checked = false;
        }

        private void btnBuscarMapaU_Click(object sender, EventArgs e)
        {
            string Ubicacion;

            objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.taUsuarioCiudadPertenece",
                    SIELSESeguridad.Sesion.StrLoginUsuario);
            if (objDatosRetorno.IdRetorno == 0)
            {
                Ubicacion = objDatosRetorno.Datos.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                Ubicacion = SIELSESeguridad.Sesion.StrNombreSucursal + ", Perú";
            }

            //bool blnPoseeCoordenada = !(string.IsNullOrEmpty(lblLatitudSuministroBeneficiario.Text) || string.IsNullOrEmpty(lblLongitudSuministroBeneficiario.Text));
            bool blnPoseeCoordenada = false;
            if (Ubicacion != "")
                blnPoseeCoordenada = true;

            //if (
            //    !blnPoseeCoordenada &&
            //    SIELSEUtil.SIELSEUtil.MostrarPreguntaSiNo("Suministro vecino no posee coordenadas de ubicación, ¿Desea establecer ubicación manualmente en el mapa?") == DialogResult.No
            //  )
            //    return;

            if (blnPoseeCoordenada)
            {
                try
                {
                    var coordenadasCentroAtencion = CentroAtencionService.ObtenerCoordenadas(
                        this.objComercial,
                        SIELSESeguridad.Sesion.CodigoSucursal,
                        1
                      );

                    if (coordenadasCentroAtencion != null)
                    {
                        blnPoseeCoordenada = true;
                        lblLatitudSuministroBeneficiario.Text = coordenadasCentroAtencion.Latitud.ToString();
                        lblLongitudSuministroBeneficiario.Text = coordenadasCentroAtencion.Longitud.ToString();
                    }
                }
                catch (Exception ex)
                {
                    SIELSEUtil.SIELSEUtil.MostrarError(ex.Message);
                    return;
                }
            }


            f_Mapa fMapa = new f_Mapa(blnPoseeCoordenada, lblLatitudSuministroBeneficiario.Text, lblLongitudSuministroBeneficiario.Text, Ubicacion);


            if (fMapa.ShowDialog() == DialogResult.OK)
            {
                lblLatitudSuministroBeneficiario.Text = f_Mapa.Latitud;
                lblLongitudSuministroBeneficiario.Text = f_Mapa.Longitud;

                //DataRow droRegistro = ((DataRowView)bsotaSolicitudFiseBeneficiario.Current).Row;
                ////droRegistro["LatitudBeneficiario"] = f_Mapa.Latitud;
                ////droRegistro["LongitudBeneficiario"] = f_Mapa.Longitud;

                objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.spObtieneDatosUbigeo",
                      Convert.ToDouble(f_Mapa.Latitud), Convert.ToDouble(f_Mapa.Longitud));

                if (objDatosRetorno.IdRetorno == 0)
                {
                    if (objDatosRetorno.Datos.Tables[0].Rows.Count > 0)
                    {

                        //droRegistro = ((DataRowView)bsotaSolicitudFiseBeneficiario.Current).Row;
                        cboDepartamentoBeneficiario.SelectedValue = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamento"].ToString();
                        bsognProvincia.Filter = "CodigoDepartamento=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamento"].ToString() + " AND " + "CodigoProvincia=" + objDatosRetorno.Datos.Tables[1].Rows[0]["CodigoProvincia"].ToString();
                        bsognDistrito.Filter = "CodigoDepartamento=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamento"].ToString() + " AND " + "CodigoProvincia=" + objDatosRetorno.Datos.Tables[1].Rows[0]["CodigoProvincia"].ToString()+ " AND " + "CodigoDistrito=" + objDatosRetorno.Datos.Tables[2].Rows[0]["CodigoDistrito"].ToString();


                        //prCopiarDireccionPredio();
                    }
                }

                blnRegistroModificado = true;
            }
        }

        private void btnBuscarMapSolicitante_Click(object sender, EventArgs e)
        {
            string Ubicacion;

            objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.taUsuarioCiudadPertenece",
                    SIELSESeguridad.Sesion.StrLoginUsuario);
            if (objDatosRetorno.IdRetorno == 0)
            {
                Ubicacion = objDatosRetorno.Datos.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                Ubicacion = SIELSESeguridad.Sesion.StrNombreSucursal + ", Perú";
            }

            //bool blnPoseeCoordenada = !(string.IsNullOrEmpty(lblLatitudSuministroBeneficiario.Text) || string.IsNullOrEmpty(lblLongitudSuministroBeneficiario.Text));
            bool blnPoseeCoordenada = false;
            if (Ubicacion != "")
                blnPoseeCoordenada = true;

            //if (
            //    !blnPoseeCoordenada &&
            //    SIELSEUtil.SIELSEUtil.MostrarPreguntaSiNo("Suministro vecino no posee coordenadas de ubicación, ¿Desea establecer ubicación manualmente en el mapa?") == DialogResult.No
            //  )
            //    return;

            if (blnPoseeCoordenada)
            {
                try
                {
                    var coordenadasCentroAtencion = CentroAtencionService.ObtenerCoordenadas(
                        this.objComercial,
                        SIELSESeguridad.Sesion.CodigoSucursal,
                        1
                      );

                    if (coordenadasCentroAtencion != null)
                    {
                        blnPoseeCoordenada = true;
                        lblLatitudSolicitanteDatos.Text = coordenadasCentroAtencion.Latitud.ToString();
                        lblLongitudSolicitanteDatos.Text = coordenadasCentroAtencion.Longitud.ToString();
                    }
                }
                catch (Exception ex)
                {
                    SIELSEUtil.SIELSEUtil.MostrarError(ex.Message);
                    return;
                }
            }


            f_Mapa fMapa = new f_Mapa(blnPoseeCoordenada, lblLatitudSolicitanteDatos.Text, lblLongitudSolicitanteDatos.Text, Ubicacion);


            if (fMapa.ShowDialog() == DialogResult.OK)
            {
                lblLatitudSolicitanteDatos.Text = f_Mapa.Latitud;
                lblLongitudSolicitanteDatos.Text = f_Mapa.Longitud;

                //DataRow droRegistro = ((DataRowView)bsotaSolicitudFiseBeneficiario.Current).Row;
                ////droRegistro["LatitudBeneficiario"] = f_Mapa.Latitud;
                ////droRegistro["LongitudBeneficiario"] = f_Mapa.Longitud;

                objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.spObtieneDatosUbigeo",
                      Convert.ToDouble(f_Mapa.Latitud), Convert.ToDouble(f_Mapa.Longitud));

                if (objDatosRetorno.IdRetorno == 0)
                {
                    if (objDatosRetorno.Datos.Tables[0].Rows.Count > 0)
                    {
                        cboDepartamentoSolicitante.SelectedValue = objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamento"].ToString();
                        bsognProvinciaFISESo.Filter = "CodigoDepartamentoSo=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoSo"].ToString() + " AND " + "CodigoProvinciaSo=" + objDatosRetorno.Datos.Tables[1].Rows[0]["CodigoProvinciaSo"].ToString();
                        bsognDistritoFISESo.Filter = "CodigoDepartamentoSo=" + objDatosRetorno.Datos.Tables[0].Rows[0]["CodigoDepartamentoSo"].ToString() + " AND " + "CodigoProvinciaSo=" + objDatosRetorno.Datos.Tables[1].Rows[0]["CodigoProvinciaSo"].ToString() + " AND " + "CodigoDistritoSo=" + objDatosRetorno.Datos.Tables[2].Rows[0]["CodigoDistritoSo"].ToString();
                        //prCopiarDireccionPredio();
                    }
                }

                blnRegistroModificado = true;
            }
        }

    

        protected override bool fnGuardarCambios()
        {
            //string x = "Pruebas Guardar";
            //SIELSEUtil.SIELSEUtil.MostrarError(x);
           
            base.fnGuardarCambios();

            if (!base.fnGuardarCambios())
                return false;

            var dtabla = (bsotaSolicitudFISEBeneficiario.DataSource as dstAtencionesFISE).taSolicitudFISEBeneficiario;

            DataRow droGuardar = dtabla.Rows[0];

            droGuardar.AcceptChanges();

                objDatosRetorno = objComercial.fnObtenerDatosProcedimiento("dbo.taSolicitudFISEBeneficiarioSW",
                    droGuardar["CodigoSolicitudFISEBeneficiario"] == DBNull.Value ? 0 : Convert.ToInt64(droGuardar["CodigoSolicitudFISEBeneficiario"]),
                    SIELSESeguridad.Sesion.CodigoSucursal,
                    droGuardar["CodigoZonaAdministrativa"] == DBNull.Value ? 1 : 1,                   
                    1,
                    1,
                    droGuardar["CodigoTipoSolicitudFISE"],
                    droGuardar["CodigoClaseSolicitudFISE"],
                    droGuardar["CodigoMotivoSolicitudFISE"],
                    droGuardar["DescripcionSolicitudFISE"],
                    1,//droGuardar["NotificacionSolicitudFISE"] == DBNull.Value ? 0: Convert.ToByte(droGuardar["NotificacionSolicitudFISE"]),
                    droGuardar["CodigoPadronRUCBeneficiario"],
                    droGuardar["CodigoTipoDocumentoIdentidadBeneficiario"] == sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"],
                    Convert.ToByte(sielsePadronRUCBeneficiario.Registro["CodigoTipoEntidad"]) == 1 ? sielsePadronRUCBeneficiario.Registro["NumeroDNI"].ToString() : sielsePadronRUCBeneficiario.Registro["NumeroRUC"].ToString(),
                    //droGuardar["NumeroDocumentoIdentidadBeneficiario"],
                    droGuardar["ApellidoPaternoBeneficiario"],
                    droGuardar["ApellidoMaternoBeneficiario"],
                    droGuardar["NombresBeneficiario"],
                    droGuardar["RazonSocialBeneficiario"],
                    droGuardar["CodigoSuministroHidrocarburo"],
                    droGuardar["NombresTitularSuministroHidrocarburo"],                   
                    1,//droGuardar["SinSuministro"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["SinSuministro"]),
                    droGuardar["RelacionSuministroVivienda"],
                    droGuardar["CodigoDepartamentoBeneficiario"],
                    droGuardar["CodigoProvinciaBeneficiario"],
                    droGuardar["CodigoDistritoBeneficiario"],
                    droGuardar["LatitudBeneficiario"],
                    droGuardar["LongitudBeneficiario"],
                    droGuardar["DireccionBeneficiario"],
                    droGuardar["CorreoElectronicoBeneficiario"],
                    droGuardar["TelefonoBeneficiario"],
                    1,//droGuardar["ValeDigital"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["ValeDigital"]),
                    1,//droGuardar["AutorizaSMS"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["AutorizaSMS"]),
                    1,//droGuardar["CopiaDNI"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["CopiaDNI"]),
                    1,//droGuardar["DeclaracionJurada"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["DeclaracionJurada"]),
                    1,//droGuardar["AutorizaDatosPersonas"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["AutorizaDatosPersonas"]),
                    1,//droGuardar["CargaAnexo01"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["CargaAnexo01"]),
                    1,//droGuardar["DJUnicoBeneficiario"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["DJUnicoBeneficiario"]),
                    1,//droGuardar["MismaPersonaBeneficiario"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["MismaPersonaBeneficiario"]),                    
                    droGuardar["CodigoPadronRUCSolicitante"],
                    //droGuardar["NumeroDocumentoIdentidadSolicitante"],
                    1,//Convert.ToByte(sielsePadronRUCSolicitante.Registro["CodigoTipoEntidad"]) == 1 ? sielsePadronRUCSolicitante.Registro["NumeroDNI"].ToString() : sielsePadronRUCSolicitante.Registro["NumeroRUC"].ToString(),
                    droGuardar["ApellidoPaternoSolicitante"],
                    droGuardar["ApellidoMaternoSolicitante"],
                    droGuardar["NombresSolicitante"],
                    droGuardar["CorreoElectronicoSolicitante"],
                    droGuardar["TelefonoSolicitante"],
                    1,//droGuardar["CartaPoderSolicitante"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["CartaPoderSolicitante"]),                    
                    droGuardar["CodigoDepartamentoSolicitante"],
                    droGuardar["CodigoProvinciaSolicitante"],
                    droGuardar["CodigoDistritoSolicitante"],
                    droGuardar["LatitudSolicitante"],
                    droGuardar["LongitudSolicitante"],
                    droGuardar["DireccionSolicitante"],
                    1,//droGuardar["FactibleEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["FactibleEvaluacion"]),
                    1,//droGuardar["ReniecCotejoEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["ReniecCotejoEvaluacion"]),
                    1,//droGuardar["SunatCotejoEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["SunatCotejoEvaluacion"]),
                    1,//droGuardar["SisfohCotejoEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["SisfohCotejoEvaluacion"]),                    
                    droGuardar["OtrasValidacionesCotejoEvaluacion"],
                    1,//droGuardar["UnicoBeneficiarioEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["UnicoBeneficiarioEvaluacion"]),                    
                    droGuardar["DetalleUnicoBeneficiarioEvaluacion"],
                    1,// droGuardar["ConsumoPromedioEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["ConsumoPromedioEvaluacion"]),                    
                    droGuardar["DetalleConsumoPromedioEvaluacion"],
                    1,//droGuardar["UnicaViviendaEvaluacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["UnicaViviendaEvaluacion"]),                    
                    droGuardar["DetalleUnicaViviendaEvaluacion"],
                    1,//droGuardar["FactibleVerificacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["FactibleVerificacion"]),                    
                    droGuardar["CodigoVerificacionFISE"],
                    1,//droGuardar["FactibleAprobacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["FactibleAprobacion"]),
                    1,//droGuardar["InspeccionAprobacion"] == DBNull.Value ? 0 : Convert.ToByte(droGuardar["InspeccionAprobacion"]),                    
                    SIELSESeguridad.Sesion.StrLoginUsuario,
                    DateTime.Now
                    );

            if (objDatosRetorno.IdRetorno == 0)
            {
                SIELSEUtil.SIELSEUtil.CopiarTabla(objDatosRetorno.Datos.Tables[0], dstGuardar.Tables["taSolicitudFISEBeneficiario"]);

                lblCodigoSolicitudFISEBeneficiario.Text = dstAtencionesFISE.taSolicitudFISEBeneficiario.Rows[0]["CodigoSolicitudFISEBeneficiario"].ToString();

            }
            else
            {
                strMensajeOperacion = objDatosRetorno.Mensaje;
                return false;
            }

        
           
            return base.fnGuardarCambios();
        }

        protected override bool fnValidarCamposRequeridos()
        {
            eprControlError.Clear();
            strMensajeOperacion = string.Empty;

            if (base.fnValidarCamposRequeridos())
            {
                if (cboTipoSolicitud.SelectedValue is null || cboClaseSolicitudFISE is null || cboMotivoSolicitudOsinergmin is null)
                {
                    strMensajeOperacion += "Seleccione el tipo de Solicitud FISE.\n";
                    eprControlError.SetError(cboTipoSolicitud, "Ingrese un tipo de Solicitud FISE");

                    strMensajeOperacion += "Seleccione una clase de Solicitud FISE.\n";
                    eprControlError.SetError(cboClaseSolicitudFISE, "Ingrese una clase de Solicitud FISE");

                    strMensajeOperacion += "Seleccione un Motivo de Solicitud FISE.\n";
                    eprControlError.SetError(cboMotivoSolicitudOsinergmin, "Ingrese un motivo de Solicitud FISE");
                }
                else
                {
                    eprControlError.SetError(cboTipoSolicitud, "");
                    eprControlError.SetError(cboClaseSolicitudFISE, "");
                    eprControlError.SetError(cboMotivoSolicitudOsinergmin, "");
                }

                if (cboDepartamentoBeneficiario.SelectedValue is null || cboProvinciaBeneficiario.SelectedValue is null || cboDistritoBeneficiario is null)
                {
                    strMensajeOperacion += "Seleccione los datos de Ubigeo del Beneficiario.\n";

                    eprControlError.SetError(cboDepartamentoBeneficiario, "Ingrese Departamento");
                    eprControlError.SetError(cboProvinciaBeneficiario, "Ingrese Provincia");
                    eprControlError.SetError(cboDistritoBeneficiario, "Ingrese Distrito");
                }
                else
                {
                    eprControlError.SetError(cboDepartamentoBeneficiario, "");
                    eprControlError.SetError(cboProvinciaBeneficiario, "");
                    eprControlError.SetError(cboDistritoBeneficiario, "");
                }
                if (cboDepartamentoSolicitante.SelectedValue is null || cboProvinciaSolicitante.SelectedValue is null || cboDistritoSolicitante is null)
                {
                    strMensajeOperacion += "Seleccione los datos de Ubigeo del Solicitante.\n";

                    eprControlError.SetError(cboDepartamentoSolicitante, "Ingrese Departamento");
                    eprControlError.SetError(cboProvinciaSolicitante, "Ingrese Provincia");
                    eprControlError.SetError(cboDistritoSolicitante, "Ingrese Distrito");
                }
                else
                {
                    eprControlError.SetError(cboDepartamentoSolicitante, "");
                    eprControlError.SetError(cboProvinciaSolicitante, "");
                    eprControlError.SetError(cboDistritoSolicitante, "");
                }

                if (txtDescripcionSolicitudFISE.Text == "")
                {
                    strMensajeOperacion += "Debe agregar una descripción de la Solicitud FISE.\n";
                    eprControlError.SetError(txtDescripcionSolicitudFISE, "Ingrese una descripción de la Solicitud");
                }

                if (txtPadronRUCBeneficiario.Text == "")
                {
                    strMensajeOperacion += "Debe de seleccionar un Beneficario.\n";
                    eprControlError.SetError(sielsePadronRUCBeneficiario, "Ingrese un Documento de Identidad");
                    eprControlError.SetError(txtApellidoPaternoA, "Campo Obligatorio");
                    eprControlError.SetError(txtApellidoMaternoA, "Campo Obligatorio");

                }
                else  
                {
                    eprControlError.SetError(sielsePadronRUCBeneficiario, "");
                    eprControlError.SetError(txtApellidoPaternoA, "");
                    eprControlError.SetError(txtApellidoMaternoA, "");
                }
                if (txtCodigoPadronRucSolicitante.Text == "")
                {
                    strMensajeOperacion += "Debe de seleccionar un Solicitante.\n";
                    eprControlError.SetError(sielsePadronRUCSolicitante, "Ingrese un Documento de Identidad");
                    eprControlError.SetError(txtApellidoPaternoSolicitante, "Campo Obligatorio");
                    eprControlError.SetError(txtApellidoMaternoSolicitante, "Campo Obligatorio");
                    eprControlError.SetError(txtNombresSolicitante, "Campo Obligatorio");

                }
                else 
                {
                    eprControlError.SetError(sielsePadronRUCSolicitante, "");
                    eprControlError.SetError(txtApellidoPaternoSolicitante, "");
                    eprControlError.SetError(txtApellidoMaternoSolicitante, "");
                    eprControlError.SetError(txtNombresSolicitante, "");
                }
                if (txtNombreRazonSocialA.Text == "")
                {
                    eprControlError.SetError(txtNombreRazonSocialA, "Ingrese la Razon Social del Beneficiario");
                }
                if (txtDireccionBeneficiario.Text == "")
                {
                    eprControlError.SetError(txtDireccionBeneficiario, "Ingrese la Dirección del Beneficiario");
                }
                if (txtDireccionSolicitante.Text == "")
                {
                    eprControlError.SetError(txtDireccionSolicitante, "Ingrese la Dirección del Solicitante");
                }

            }
            //var retorno = strMensajeOperacion == string.Empty && base.fnValidarCamposRequeridos();

            return strMensajeOperacion == string.Empty && base.fnValidarCamposRequeridos();
            //return retorno;
        }

        private void tlsRegistrado_Click(object sender, EventArgs e)
        {
            SIELSEUtil.SIELSEUtil.MostrarPreguntaSiNoCancelar("Se guardará la información ingresada.\n\n¿Desea continuar ? ");
        }

        private void txtCodigoSuministroBeneficiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCodigoSuministroBeneficiario.Text.Length >= 50 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
