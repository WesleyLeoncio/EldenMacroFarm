namespace EldenMacroFarm.others;

public static class CustomMessage
{
    public static void MessageYesNo(string titulo, string mensagem, MessageBoxIcon icon)
    {
        DialogResult dialogResult = MessageBox.Show(mensagem, titulo, 
            MessageBoxButtons.YesNo, icon);
        
        if (dialogResult == DialogResult.Yes)
        {
            Environment.Exit(0);
        }
       
    }
}