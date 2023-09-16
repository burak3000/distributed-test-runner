using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;

namespace distributedtestrunnerdesktopapp;

public partial class LoginWindow : Window
{
    private ILogger<LoginWindow> _logger;

    public LoginWindow(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<LoginWindow>();
        InitializeComponent();
    }
}
