using System;
using System.Collections.Generic;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaSolarSystem;

public partial class MainWindowMulti : Window
{
    Vector2D center;
    readonly List<Planet> planets = [];
    readonly List<Moon> moons = [];

    public MainWindowMulti()
    {
        InitializeComponent();

        center = new();

        SizeChanged += OnSizeChanged;
        SetupUniverse();

        // Use a single timer for all movement
        var timer = new Timer(1);
        timer.Elapsed += (s, e) => MoveAllPlanetsAndMoons();
        timer.Start();
    }

    private void SetupUniverse()
    {
        Random rand = new();


        Planet sun = new(140, 140, 0, new Vector2D(center.X, center.Y), Brushes.Yellow, 0);
        mainCanvas.Children.Add(sun.Shape);
        planets.Add(sun);


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

    }

    public void OnSizeChanged(object? a, SizeChangedEventArgs e)
    {
        center = new Vector2D(e.NewSize.Width / 2, e.NewSize.Height / 2);
        planets.ForEach((e) => e.SetPosition(new Vector2D(center.X - (e.Shape.Width / 2), center.Y - (e.Shape.Height / 2))));
    }


    private volatile bool _isUpdating = false;
    /// <summary>
    /// Animates all planet and moon movement using a single timer.
    /// </summary>
    private async void MoveAllPlanetsAndMoons()
    {
        if (_isUpdating) return;
        _isUpdating = true;

        // Calculate new positions (background thread)
        foreach (var planet in planets)
            planet.CalcMove();
        foreach (var moon in moons)
            moon.CalcMoveMoon();

        // Await UI update at Render priority
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            foreach (var planet in planets)
            {
                Canvas.SetTop(planet.Shape, planet.newPosition.Y);
                Canvas.SetLeft(planet.Shape, planet.newPosition.X);
            }
            foreach (var moon in moons)
            {
                Canvas.SetTop(moon.Shape, moon.newPosition.Y);
                Canvas.SetLeft(moon.Shape, moon.newPosition.X);
            }
        }, DispatcherPriority.Render);

        _isUpdating = false;
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
            moons.Add(newMoon);
        }
        mainCanvas.Children.Add(planet.Shape);
    }
}