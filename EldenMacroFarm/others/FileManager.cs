namespace EldenMacroFarm.others;

public static class FileManager
{
    public static void CopyImgToDebug()
    {
        string projectDir;
        DirectoryInfo? dir = Directory.GetParent(Application.StartupPath);
        if (dir != null && dir.Parent != null && dir.Parent.Parent != null)
        {
             projectDir = dir.Parent.Parent.FullName;
             string sourcePath = Path.Combine(projectDir, "..", "..","img");
             string destinationPath = Path.Combine(Application.StartupPath, "img");
            
             // Verifica se a pasta de origem existe
             if (Directory.Exists(sourcePath))
             {
                 // Cria a pasta de destino se não existir
                 if (!Directory.Exists(destinationPath))
                 {
                     Directory.CreateDirectory(destinationPath);
                 }

                 // Copia todos os arquivos e subpastas da pasta img para a pasta Debug\img
                 foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                 {
                     Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                 }

                 foreach (string newPath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
                 {
                     File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true); // Substitui se já existir
                 }
                    
                 CustomMessage.MessageYesNo("\u2705 Operação Concluída Com Sucesso",
                     "Pasta 'img' copiada com sucesso para o diretório Debug. \n\n" +
                     "Fechar Aplicação para Atualizar?",
                     MessageBoxIcon.Warning);
             }
             else
             {
                 CustomMessage.MessageYesNo("\u274c Erro Na Operação",
                     "A pasta 'img' não foi encontrada no diretório de origem.\n\n" +
                     "Fechar Aplicação para Atualizar?",
                     MessageBoxIcon.Error);
             }
        }
        
    }
}