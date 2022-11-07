using System;
using System.Windows.Forms;

namespace FinsMes
{
    public partial class WizardControl : TabControl
    {
        // デザイナ画面以外でタブを消す
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }

        // Ctrl+Tab で遷移できなくする
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Tab) return;
            base.OnKeyDown(e);
        }
    }
}
