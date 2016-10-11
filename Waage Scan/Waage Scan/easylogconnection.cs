using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace Waage_Scan
{
    public static class easylogconnection
    {
        private static string ProgText = "EasyLog 6.8 XL";




        public static void sendtoexternal(string text)
        {
            //lieferschein

            //todo switch to other window
            SendKeys.Send(text);
            SendKeys.Send("{ TAB}");

            //switch back
            Interaction.AppActivate("Gewicht eingeben");

        }
        public static void sendtoexternalandprint(string text)
        {
            //gewicht
            Interaction.AppActivate(ProgText);
            SendKeys.Send(text);
            SendKeys.Send("{INS}");
            Interaction.AppActivate("Gewicht eingeben");
        }
    }
   
}
