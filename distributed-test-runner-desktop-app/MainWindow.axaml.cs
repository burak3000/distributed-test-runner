using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DistributedTestRunnerDesktop.Core;
using distributedtestrunnerdesktopapp;
using Microsoft.Extensions.Logging;

namespace distributed_test_runner_desktop_app;

public partial class MainWindow : Window
{
    private IAbstractFactory<LoginWindow> _loginViewFactory;
    private ILogger<MainWindow> _logger;

    public MainWindow(ILoggerFactory loggerFactory, IAbstractFactory<LoginWindow> loginWindowFactory)
    {
        _loginViewFactory = loginWindowFactory;
        _logger = loggerFactory.CreateLogger<MainWindow>();
        InitializeComponent();
    }

    public void ButtonClicked(object source, RoutedEventArgs args)
    {
        _logger.LogInformation("Login view is requested.");
        _loginViewFactory.Create().Show();
    }
}