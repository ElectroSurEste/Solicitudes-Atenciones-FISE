using System;
using System.Windows.Forms;

namespace M0101100123000
{
  public partial class f_Mapa : Form
  {
    public static string Latitud;
    public static string Longitud;

    public f_Mapa(bool blnPoseeDatos, string Lati, string longi, string UbicacionMapa)
    {
      InitializeComponent();
      gMapSIELSE1.blnGraficaUnPunto = true;

      //********Establecer el origen de la busqueda*********
      gMapSIELSE1.tstxtBuscarUbicacion.Text = UbicacionMapa;
      //****************************************************

      if (blnPoseeDatos)
      {
        gMapSIELSE1.prDibujarPunto(Convert.ToDouble(Lati), Convert.ToDouble(longi));
      }
      else
      {
        gMapSIELSE1.prBuscaLugarPorNombre();
      }
    }

    public f_Mapa()
    {
      InitializeComponent();

      gMapSIELSE1.tsbAgregarPunto.Enabled = false;
      gMapSIELSE1.tsbBorrarMarca.Enabled = false;
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      if (gMapSIELSE1.CantidadMarcadoresDibujados == 1)
      {
        if (gMapSIELSE1.UltimaLatitudDibujada.ToString() != "0")
        {
          Latitud = gMapSIELSE1.UltimaLatitudDibujada.ToString();
          Longitud = gMapSIELSE1.UltimaLongitudDibujada.ToString();
        }
      }
      else
      {
        SIELSEUtil.SIELSEUtil.MostrarAdvertencia("Usted no tiene dibujado ningun punto");
      }
    }
  }
}
