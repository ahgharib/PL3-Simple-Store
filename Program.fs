namespace SimpleStoreSimulator

open Avalonia
open Avalonia.Controls.ApplicationLifetimes

module Program =
    [<EntryPoint>]
    let main argv =
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .StartWithClassicDesktopLifetime(argv)
