using Microsoft.Extensions.Logging;

namespace MauiNativeAotQuirks;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Parse command line args: -t <seconds>, -l <logPath>
        var args = Environment.GetCommandLineArgs();
        for (var i = 1; i < args.Length; i++) {
            if (args[i] == "-t" && i + 1 < args.Length && int.TryParse(args[i + 1], out var seconds)) {
                _ = Task.Run(async () => {
                    await Task.Delay(seconds * 1000);
                    Log.Append("=== Auto-shutdown ===");
                    Environment.Exit(0);
                });
                i++;
            }
            else if (args[i] == "-l" && i + 1 < args.Length) {
                Log.Path = Path.GetFullPath(args[i + 1]);
                i++;
            }
        }

        Log.Append("=== App starting ===");

        AppDomain.CurrentDomain.FirstChanceException += (_, e) => {
            Log.Append($"FIRST-CHANCE: {e.Exception}");
        };

        AppDomain.CurrentDomain.UnhandledException += (_, e) => {
            Log.Append($"UNHANDLED: {e.ExceptionObject}");
        };

        try {
            var builder = MauiApp.CreateBuilder();
            Log.Append("Builder created");

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            Log.Append("App configured");

            builder.Services.AddMauiBlazorWebView();
            Log.Append("BlazorWebView added");

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            Log.Append("App built successfully");
            return app;
        }
        catch (Exception ex) {
            Log.Append($"FATAL: {ex}");
            throw;
        }
    }
}
