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
            //switch to other window
            try
            {
                Interaction.AppActivate(ProgText);
            }
            catch (Exception message)
            {

                MessageBox.Show("Fehler EasyLog nicht gefunden", "Waage", MessageBoxButtons.OK);
                MessageBox.Show(message.Message, "Waage", MessageBoxButtons.OK);
            }
            //send keys and enter
            SendKeys.Send(text);
            SendKeys.Send("{ENTER}");
            //switch back
            Interaction.AppActivate("Lieferschein");


        }
        public static void sendtoexternalandprint(string text)
        {
            //switch to other window
            try
            {
                Interaction.AppActivate(ProgText);
            }
            catch (Exception message)
            {
                MessageBox.Show("Fehler EasyLog nicht gefunden", "Waage", MessageBoxButtons.OK);
                MessageBox.Show(message.Message, "Waage", MessageBoxButtons.OK);
            }
            //send keys and tab
            SendKeys.Send(text);
            SendKeys.Send("{INS}");
            SendKeys.Send("{INS}");
            SendKeys.Send("{INS}");
            //switch back
            Interaction.AppActivate("Lieferschein");
        }
    }

}
