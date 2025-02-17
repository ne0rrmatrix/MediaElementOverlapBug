using System.Diagnostics;
using CommunityToolkit.Maui.Views;

namespace MediaElementOverlapBug;
public partial class App : Application
{
    readonly AppShell appShell;

    public App(AppShell appShell)
    {
        InitializeComponent();

        this.appShell = appShell;
    }

    protected override Window CreateWindow(IActivationState? activationState) => new(appShell);
}
