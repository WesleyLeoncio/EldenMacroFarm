using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Events;

namespace EldenMacroFarm;

public partial class Main : Form
{
    private CancellationTokenSource? _cts;
    private int _executionCount;
    
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        DefinirCorBarraTitulo(Color.DarkSlateBlue);
    }
    
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    // Importa funções do Windows para ativar a janela do jogo
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    public Main()
    {
        InitializeComponent();
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        if (_cts != null) return; // Evita múltiplas execuções

        int loops = (int)numericUpDownLoops.Value;
        int loadDelay = (int)numericLoadDelay.Value;
        
      
        _cts = new CancellationTokenSource();
        btnStart.Enabled = false;
        btnStop.Enabled = true;

        await RunMacro(loops, loadDelay, _cts.Token);

        btnStart.Enabled = true;
        btnStop.Enabled = false;
        _cts.Dispose();
        _cts = null;
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        _cts?.Cancel();
    }

    private async Task RunMacro(int loops, int loaDelay, CancellationToken token)
    {
        for (int i = 0; i < loops; i++)
        {
            if (AtualizarBotao(token)) return;

            // Ativa a janela do Elden Ring
            IntPtr hWnd = FindWindow(null, "ELDEN RING™");
            if (hWnd != IntPtr.Zero)
                SetForegroundWindow(hWnd);
            
            await GerarDelay(1000);
            
            await AcionarTecla(KeyCode.W, 3000, 50, token);
            
            await AcionarTecla(KeyCode.A, 500, 50, token);
            
            await AcionarTecla(KeyCode.W, 2000, 50, token);
            
            await AcionarTecla(KeyCode.A, 500, 100, token);
            
            await AcionarTecla(KeyCode.W, 950, 50, token);
            
            await AcionarTecla(KeyCode.F, 0, 5700, token);
            
            await AcionarTecla(KeyCode.M, 0, 50, token);
            
            await AcionarTecla(KeyCode.M, 0, 50, token);
            
            await AcionarTecla(KeyCode.F, 0, 50, token);
           
            await AcionarTecla(KeyCode.E, 100, 100, token);
            
            await AcionarTecla(KeyCode.E, 100, 100, token);

            await LiberaTeclas();
            UpdateCounter();
            await GerarDelay(loaDelay);
        }
        AtualizarBotao(token);
    }
    
    private void UpdateCounter()
    {
        _executionCount++;
        lblNumeroR.Text = $"Número de Execuções: {_executionCount}";
    }

    private bool AtualizarBotao(CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            return true;
        }
        return false;
    }

    private async Task AcionarTecla(KeyCode keyCode, int wat, int delay, CancellationToken token)
    {
        if (AtualizarBotao(token)) return;
        
        await Simulate.Events().Hold(keyCode).Wait(wat).Release().Invoke();
        await GerarDelay(delay);
        await LiberaTeclasManualmente(keyCode);
        await GerarDelay(50);
    }

    private static async Task LiberaTeclasManualmente(KeyCode keyCode)
    {
        await Simulate.Events().Release(keyCode).Invoke(); // 🔹 Libera novamente
        await GerarDelay(50);
    }

    private static async Task LiberaTeclas()
    {
        await Simulate.Events()
            .Release(KeyCode.W)
            .Release(KeyCode.A)
            .Release(KeyCode.F)
            .Release(KeyCode.G)
            .Release(KeyCode.M)
            .Release(KeyCode.S)
            .Release(KeyCode.E)
            .Invoke();
    }

    private static async Task GerarDelay(int tempo)
    {
        if (tempo <= 0) await Task.Delay(100);

        await Task.Delay(tempo);
    }
    
    private void DefinirCorBarraTitulo(Color cor)
    {
        int attr = 35; // DWMWA_CAPTION_COLOR
        int color = cor.R | (cor.G << 8) | (cor.B << 16);
        DwmSetWindowAttribute(this.Handle, attr, ref color, sizeof(int));
    }
}