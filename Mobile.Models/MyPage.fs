namespace Mobile.Models

open System
open Xamarin.Forms

type App() =
    static member GetMainPage =
        let button = Button(Text = "Click Me!",
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand)
        ContentPage(Title = "My content page", Content = button)
