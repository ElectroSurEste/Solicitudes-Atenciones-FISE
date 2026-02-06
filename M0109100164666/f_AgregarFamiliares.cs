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
    public partial class f_AgregarFamiliares : Form
    {
        AccesoDatos.clsAccesoDatos acc;
        public f_AgregarFamiliares(AccesoDatos.clsAccesoDatos acc_, DataTable _DatosRelacionFamiliares)
        {
            InitializeComponent();
            this.acc = acc_;
            SIELSEUtil.SIELSEUtil.CopiarTabla(_DatosRelacionFamiliares, dstAtencionesFISE.gnRelacionFamiliarVerificacionFISE);

        }
    }
}
