using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M0109100164666
{
    public partial class f_VerificacionFISE : Form
    {
        AccesoDatos.clsAccesoDatos acc;
        public f_VerificacionFISE(AccesoDatos.clsAccesoDatos acc_, DataTable _DatosAccionVerificacion, DataTable _DatosMotivoVerificacion, DataTable _DatosMaterialPiso, DataTable _DatosRelacionFamiliar)
        {
            InitializeComponent();
            this.acc = acc_;
            SIELSEUtil.SIELSEUtil.CopiarTabla(_DatosAccionVerificacion, dstAtencionesFISE.gnAccionVerificacionFISE);
            SIELSEUtil.SIELSEUtil.CopiarTabla(_DatosMotivoVerificacion, dstAtencionesFISE.gnMotivoVerificacionFISE);
            SIELSEUtil.SIELSEUtil.CopiarTabla(_DatosMaterialPiso, dstAtencionesFISE.gnMaterialPisoVerificacionFISE);
            SIELSEUtil.SIELSEUtil.CopiarTabla(_DatosRelacionFamiliar, dstAtencionesFISE.gnRelacionFamiliarVerificacionFISE);
        }

        private void btnAgregarFamiliares_Click(object sender, EventArgs e)
        {
            f_AgregarFamiliares oFormAgregarFamiliares = new f_AgregarFamiliares(acc, dstAtencionesFISE.gnRelacionFamiliarVerificacionFISE);
            oFormAgregarFamiliares.ShowDialog();
        }
      
     
    
    }
}
