namespace OOP_2__console_text_editor_.Services;

public class WindowSizeService
{
    private int width  = Console.WindowWidth;
    private int height = Console.WindowHeight;

    private int headerHeight = 5;
    private int sectionHeight = 0;
    private bool _watching;
    private readonly CancellationTokenSource _cts = new();

    public int Width  => width;
    public int Height => height;

    public int HeaderHeight
    {
        get => headerHeight;
    }

    public int SectionHeight
    {
        get
        {
            return height - headerHeight;
        }
    }

    public event Action? SizeChanged;

    public void StartWatching(int intervalMs = 10)
    {
        if (_watching) return;
        _watching = true;

        Task.Run(async () =>
        {
            while (!_cts.IsCancellationRequested)
            {
                await Task.Delay(intervalMs, _cts.Token);
                
                int w = Console.WindowWidth;
                int h = Console.WindowHeight;
                
                if (w != width || h != height)
                {
                    width = w;
                    height = h;
                    SizeChanged?.Invoke();
                }
            }
        }, _cts.Token);
    }

    public void StopWatching()
    {
        if (!_watching) return;
        _cts.Cancel();
        _cts.Dispose();
        _watching = false;
    }
    
}
