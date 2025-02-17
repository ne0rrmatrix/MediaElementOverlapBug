using CommunityToolkit.Maui.Views;

namespace MediaElementOverlapBug; 
public partial class MainPage : ContentPage {
    //============================================================
    //TOGGLE WHICH OF TWO HALF SCREEN VIDEOS TO ADD
    //============================================================
    //== SHOULD SHOW ONE VIDEO ON EACH HALF OF SCREEN
    //== WORKS FINE IN WINDOWS
    //== BUT IN ANDROID, WILL ONLY SHOW THE RIGHT SIDE ON FULL SCREEN (the highest overlaying video will cover any underlying videos irrespective of clipping rules)
    //== UNSURE ABOUT IOS

    bool SHOW_LEFT_HALF_VIDEO = true;
    bool SHOW_RIGHT_HALF_VIDEO = true;


    List<MediaElement> mediaElements = [];
    List<Border> borders = [];
    List<AbsoluteLayout> absoluteLayouts = [];

    public MainPage() {
        InitializeComponent();
        AbsoluteLayout abs = [];
        mainPage.Content = abs;


        for (int i = 0; i < 2; i++)
        {
            Border border = new();
            border.StrokeThickness = 0;
            borders.Add(border);

            AbsoluteLayout absolute = new();
            absoluteLayouts.Add(absolute);
            border.Content = absolute;

            MediaElement mediaElement = new();
            if (i == 0)
            {
                mediaElement.Source = MediaSource.FromResource("bunny.mp4");
            }
            else
            {
                mediaElement.Source = MediaSource.FromResource("woods.mp4");
            }
            mediaElement.MetadataTitle = "Texture View";
            mediaElement.MetadataArtist = "Community Toolkit";
            mediaElement.MetadataArtworkUrl = "https://lh3.googleusercontent.com/pw/AP1GczNRrebWCJvfdIau1EbsyyYiwAfwHS0JXjbioXvHqEwYIIdCzuLodQCZmA57GADIo5iB3yMMx3t_vsefbfoHwSg0jfUjIXaI83xpiih6d-oT7qD_slR0VgNtfAwJhDBU09kS5V2T5ZML-WWZn8IrjD4J-g=w1792-h1024-s-no-gm";
            mediaElement.ShouldShowPlaybackControls = true;
            mediaElement.ShouldAutoPlay = true;
            mediaElement.Aspect = Aspect.AspectFill;
            mediaElement.ShouldLoopPlayback = true;
            mediaElement.Play();

            mediaElements.Add(mediaElement);
            absolute.Add(mediaElement);
        }


        //=============================
        //ADD VIDEOS TO HIERARCHY
        //=============================
        if (SHOW_LEFT_HALF_VIDEO)
        {
            abs.Add(borders[0]);
        }
        if (SHOW_RIGHT_HALF_VIDEO)
        {
            abs.Add(borders[1]);
        }


        mainPage.SizeChanged += delegate {
            if (mainPage.Width > 0)
            {
                for (int i = 0; i < borders.Count; i++)
                {
                    borders[i].HeightRequest = mainPage.Height;
                    borders[i].WidthRequest = mainPage.Width * 0.5;

                    mediaElements[i].HeightRequest = mainPage.Height;
                    mediaElements[i].WidthRequest = mainPage.Width;

                    if (i == 1)
                    {
                        borders[i].TranslationX = 0.5 * mainPage.Width;
                        mediaElements[i].TranslationX = -0.5 * mainPage.Width;
                    }
                }
            }
        };
    }
}
