using System;
using System.Collections.Generic;
using System.Numerics;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaSolarSystem;

public partial class MainWindow : Window
{
    Vector2D center;
    List<Planet> planets = [];

    public MainWindow()
    {
        InitializeComponent();

        SizeChanged += OnSizeChanged;

        center = new();

        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(10);


        Planet sun = new(140, 140, 0, new Vector2D(center.X, center.Y), Brushes.Yellow, 0);
        mainCanvas.Children.Add(sun.Shape);
        planets.Add(sun);

        Planet earth = new(
            30,
            30,
            0.04,
            new Vector2D(center.X, center.Y),
            Brushes.Blue,
            200
        );
        mainCanvas.Children.Add(earth.Shape);
        planets.Add(earth);

        Moon earthMoon = new(
            5,
            5,
            0.06,
            earth,
            Brushes.LightGray,
            40
        );
        mainCanvas.Children.Add(earthMoon.Shape);

        Planet applePlanet = new(
            20,
            20,
            0.02,
            new Vector2D(center.X, center.Y),
            Brushes.Red,
            300
        );
        mainCanvas.Children.Add(applePlanet.Shape);
        planets.Add(applePlanet);

        Random rand = new Random();
        for (int i = 0; i < 1200; i++)
        {
            Moon randomMoon = new(
                2,
                2,
                0.01 + (rand.NextDouble() / 60),
                applePlanet,
                Brushes.LightGray,
                rand.Next(25, 100)
            );
            mainCanvas.Children.Add(randomMoon.Shape);
            timer.Tick += randomMoon.Move;
        }

        Planet bigOne = new(
            60,
            60,
            0.01,
            new Vector2D(center.X, center.Y),
            Brushes.Green,
            460
        );
        planets.Add(bigOne);

        for (int i = 0; i < 8; i++)
        {
            Moon randomMoon = new(
                8,
                8,
                0.01 + (rand.NextDouble() / 60),
                bigOne,
                Brushes.LightGray,
                rand.Next(60, 100)
            );
            mainCanvas.Children.Add(randomMoon.Shape);
            timer.Tick += randomMoon.Move;
        }
        mainCanvas.Children.Add(bigOne.Shape);

        timer.Tick += earth.Move;
        timer.Tick += sun.Move;
        timer.Tick += applePlanet.Move;
        timer.Tick += earthMoon.Move;
        timer.Tick += bigOne.Move;

        timer.Start();
    }

    public void OnSizeChanged(object? a, SizeChangedEventArgs e)
    {
        center = new Vector2D(e.NewSize.Width / 2, e.NewSize.Height / 2);
        planets.ForEach((e) => e.SetPosition(new Vector2D(center.X - (e.Shape.Width / 2), center.Y - (e.Shape.Height / 2))));
    }
}