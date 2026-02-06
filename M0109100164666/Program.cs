using System;
using System.Collections.Generic;


using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M0109100164666
{
    static class Program
    {
        static string strCodigoModulo = "0109100164666";
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new f_ma_AtencionFISE(new Guid(args[0]), args[1], args[2], strCodigoModulo));

            switch (args.Length)
            {
                case 3:
                    Application.Run(new f_ma_AtencionFISE(new Guid(args[0]), args[1], args[2], strCodigoModulo));
                    break;
                default:
                    SIELSEUtil.SIELSEUtil.MostrarError("Número de argumentos incorrecto");
                    break;
            }

        }
    }
}
