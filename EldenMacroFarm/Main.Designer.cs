using EldenMacroFarm.others;

namespace EldenMacroFarm
{
    partial class Main
    {
        // Constantes para os layouts e dimensões
        private const int LabelX = 12;
        private const int InputX = 100;
        private const int ButtonX = 190;
        private const int Row1Y = 12;
        private const int Row2Y = 45;
        private const int RowSpacing = 10;  // Espaço entre as linhas
        private const int ControlWidth = 80;
        private const int ControlHeight = 23;
        private const int ButtonWidth = 80;
        private const int ButtonHeight = 28;
        private const int TopOffset = 35;

        private System.ComponentModel.IContainer components = null;
        private NumericUpDown numericUpDownLoops;
        private NumericUpDown numericLoadDelay;
        private Button btnStart;
        private Button btnStop;
        private Label lblNumeroR;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            ConfigJanela();
            
            // Criando os componentes
            var lblLoops = CreateLabel("Repetições:", LabelX, Row1Y + TopOffset);
            numericUpDownLoops = CreateNumericUpDown(InputX + 10, Row1Y + TopOffset, 1, 9999, 1);

            var lblDelay = CreateLabel("Load (ms):", LabelX, Row2Y + RowSpacing + TopOffset);
            numericLoadDelay = CreateNumericUpDown(InputX + 10, Row2Y + RowSpacing + TopOffset, 1, 99999, 6000);
            
            lblNumeroR = CreateLabel("Número de Execuções: 0", (this.ClientSize.Width / 2) - 75, 10);

            // Botões
            btnStart = CreateButton("Iniciar", ButtonX + 10, Row1Y + TopOffset);
            btnStart.Click += new EventHandler(this.btnStart_Click);

            btnStop = CreateButton("Parar", ButtonX + 10, Row2Y + RowSpacing + TopOffset);
            btnStop.Click += new EventHandler(this.btnStop_Click);
            btnStop.Enabled = false;

            // Alterando as cores dos controles
            SetControlColors();

            // Adicionando os componentes ao formulário
            this.Controls.Add(lblLoops);
            this.Controls.Add(numericUpDownLoops);
            this.Controls.Add(lblDelay);
            this.Controls.Add(lblNumeroR);
            this.Controls.Add(numericLoadDelay);
            this.Controls.Add(btnStart);
            this.Controls.Add(btnStop);

            // Configurações do formulário
            this.ClientSize = new System.Drawing.Size(290, 150);  // Aumentando o tamanho do formulário para acomodar os controles
            this.Text = "Macro Elden Ring";

            // Centralizando o formulário na tela
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Impedindo o redimensionamento da janela
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; // Desabilita o botão de maximizar
            this.MinimizeBox = false; // Desabilita o botão de minimizar

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Método para criar Label com estilo
        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                AutoSize = true,
                Font = new Font("Comic Sans MS", 10, FontStyle.Italic | FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                BackColor = Color.FromArgb(128, 0, 0, 0)
            };
        }

        // Método para criar NumericUpDown com estilo
        private NumericUpDown CreateNumericUpDown(int x, int y, int min, int max, int value)
        {
            return new NumericUpDown
            {
                Location = new System.Drawing.Point(x, y),
                Minimum = min,
                Maximum = max,
                Value = value,
                Size = new System.Drawing.Size(ControlWidth, ControlHeight),
                Font = new System.Drawing.Font("Comic Sans MS", 10, System.Drawing.FontStyle.Bold),
                BackColor = Color.Indigo,
                ForeColor = Color.White
            };
        }

        // Método para criar Button com estilo
        private Button CreateButton(string text, int x, int y)
        {
            Button button = new Button
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(ButtonWidth, ButtonHeight),
                Font = new System.Drawing.Font("Comic Sans MS", 10, System.Drawing.FontStyle.Bold)
            };
            button.MouseEnter += (s, e) => button.Cursor = Cursors.Hand; 
            button.MouseLeave += (s, e) => button.Cursor = Cursors.Default;
            return button;
        }

        // Método para ajustar as cores do formulário
        private void SetControlColors()
        {
            btnStart.BackColor = System.Drawing.Color.Green; // Cor do fundo do botão Iniciar
            btnStart.ForeColor = System.Drawing.Color.White; // Cor do texto do botão Iniciar
            btnStop.BackColor = System.Drawing.Color.Red; // Cor do fundo do botão Parar
            btnStop.ForeColor = System.Drawing.Color.White; // Cor do texto do botão Parar
        }
        private void BackGroundImage()
        {
            
            string backGroundPath = Path.Combine(Application.StartupPath, "img", "background.png");
            if (VerificaArquivo(backGroundPath))
            {
                this.BackgroundImage = Image.FromFile(backGroundPath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            } 
        }

        private void IcomImage()
        {
            string iconPath = Path.Combine(Application.StartupPath, "img", "icone.ico");
            if (VerificaArquivo(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }
        }
        
        private bool VerificaArquivo(string arquivo)
        {
            if (!File.Exists(arquivo))
            {   
                CustomMessage.MessageYesNo("\u274c Erro Na Operação",
                    $"Arquivo não encontrado: {arquivo}\n\n" +
                    "Fechar Aplicação para Atualizar?",
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private void ConfigJanela()
        {
            BackGroundImage();
            IcomImage();
        }
        
    }
}
