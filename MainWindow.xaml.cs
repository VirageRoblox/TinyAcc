using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace RobloxMultiInstance;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => ShowStatus();
    }

    private void ShowStatus()
    {
        bool active = (Application.Current as App)?.Active == true;

        if (active)
        {
            StatusDot.Fill  = new SolidColorBrush(Color.FromRgb(0x34, 0xD3, 0x99));   // green
            StatusText.Text = "Multi-instance is ACTIVE";
            StatusPill.Background = new SolidColorBrush(Color.FromRgb(0x14, 0x2A, 0x22));
            FixPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            StatusDot.Fill  = new SolidColorBrush(Color.FromRgb(0xF4, 0x3F, 0x5E));   // red
            StatusText.Text = "Not active — Roblox is running";
            StatusPill.Background = new SolidColorBrush(Color.FromRgb(0x2A, 0x16, 0x1B));
            FixPanel.Visibility = Visibility.Visible;
        }
    }

    private void Retry_Click(object sender, RoutedEventArgs e)
    {
        (Application.Current as App)?.TryAcquire();
        ShowStatus();
    }

    private void ForceClose_Click(object sender, RoutedEventArgs e)
    {
        // Kill every Roblox client process so the singleton frees up.
        foreach (var name in new[] { "RobloxPlayerBeta", "RobloxPlayerLauncher", "Windows10Universal" })
        {
            foreach (var p in Process.GetProcessesByName(name))
            {
                try { p.Kill(); } catch { }
            }
        }
        // Give the OS a moment to release the named event, then re-claim.
        Dispatcher.InvokeAsync(async () =>
        {
            await System.Threading.Tasks.Task.Delay(700);
            (Application.Current as App)?.TryAcquire();
            ShowStatus();
        });
    }
}
