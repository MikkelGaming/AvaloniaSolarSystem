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
    DispatcherTimer timer;

    public MainWindow()
    {
        InitializeComponent();

        SizeChanged += OnSizeChanged;

        center = new();

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(10);


        Planet sun = new(140, 140, 0, new Vector2D(center.X, center.Y), Brushes.Yellow, 0);
        mainCanvas.Children.Add(sun.Shape);
        planets.Add(sun);
        timer.Tick += sun.Move;


        CreatePlanetWithMoons(
            new Planet(30, 30, 0.04, new Vector2D(center.X, center.Y), Brushes.Green, 200),
            new Moon(5, 5, 0.06, null, Brushes.LightGray, 50),
            0,
            1,
            true,
            10
        );


        CreatePlanetWithMoons(
            new Planet(20, 20, 0.02, new Vector2D(center.X, center.Y), Brushes.Green, 300),
            new Moon(2, 2, 0.01, null, Brushes.LightGray, 25),
            60,
            1200,
            false,
            75
        );

        CreatePlanetWithMoons(
            new Planet(20, 20, -0.02, new Vector2D(center.X, center.Y), Brushes.Green, 300),
            new Moon(2, 2, 0.01, null, Brushes.LightGray, 25),
            60,
            1200,
            true,
            75
        );

        CreatePlanetWithMoons(
            new Planet(60, 60, -0.01, new Vector2D(center.X, center.Y), Brushes.Green, 460),
            new Moon(8, 8, 0.01, null, Brushes.LightGray, 60),
            60,
            8,
            true,
            40
        );

        CreatePlanetWithMoons(
            new Planet(60, 60, 0.01, new Vector2D(center.X, center.Y), Brushes.Green, 460),
            new Moon(8, 8, 0.01, null, Brushes.LightGray, 60),
            60,
            8,
            false,
            40
        );

        timer.Start();
    }

    public void OnSizeChanged(object? a, SizeChangedEventArgs e)
    {
        center = new Vector2D(e.NewSize.Width / 2, e.NewSize.Height / 2);
        planets.ForEach((e) => e.SetPosition(new Vector2D(center.X - (e.Shape.Width / 2), center.Y - (e.Shape.Height / 2))));
    }

    public void CreatePlanetWithMoons(Planet planet, Moon moonBluePrint, double randomSpeedOfMoons, int numberOfMoons, bool moonDirection, int randomnessOfDistance = 0)
    {
        planets.Add(planet);

        Random rand = new();
        int direction = 1;

        if (moonDirection)
        {
            direction = -1;
        }

        for (int i = 0; i < numberOfMoons; i++)
        {
            var newMoon = new Moon(moonBluePrint);
            newMoon.Speed = moonBluePrint.Speed + (rand.NextDouble() / randomSpeedOfMoons) * direction;
            newMoon.planet = planet;
            if (randomnessOfDistance > 0)
            {
                newMoon.distance += rand.Next(randomnessOfDistance);
            }
            mainCanvas.Children.Add(newMoon.Shape);
            timer.Tick += newMoon.Move;
        }
        mainCanvas.Children.Add(planet.Shape);
        timer.Tick += planet.Move;
    }
}