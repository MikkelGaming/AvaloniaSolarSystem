using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaSolarSystem;

public partial class MainWindowMulti : Window
{
    Vector2D center;
    List<Planet> planets = [];

    Timer t = null;
    Timer t1 = null;
    Timer t2 = null;
    Timer t3 = null;


    public MainWindowMulti()
    {
        InitializeComponent();

        SizeChanged += OnSizeChanged;

        center = new();


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

        t = new Timer(20);
        t.Elapsed += (s, e) =>
        {
            Console.WriteLine("Earth?");
            MovePlanet(earth);
        };
        t.AutoReset = true;
        t.Enabled = true;


        Moon earthMoon = new(
            5,
            5,
            0.06,
            earth,
            Brushes.LightGray,
            40
        );
        mainCanvas.Children.Add(earthMoon.Shape);

        t1 = new Timer(20);
        t1.Elapsed += (s, e) =>
        {
            MovePlanet(earthMoon);
        };
        t1.AutoReset = true;
        t1.Enabled = true;

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

        t2 = new Timer(20);
        t2.Elapsed += (s, e) =>
        {
            MovePlanet(applePlanet);
        };
        t2.AutoReset = true;
        t2.Enabled = true;

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

        t3 = new Timer(20);
        t3.Elapsed += (s, e) =>
        {
            MovePlanet(bigOne);
        };
        t3.AutoReset = true;
        t3.Enabled = true;

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
        }
        mainCanvas.Children.Add(bigOne.Shape);
    }

    public void OnSizeChanged(object? a, SizeChangedEventArgs e)
    {
        center = new Vector2D(e.NewSize.Width / 2, e.NewSize.Height / 2);
        planets.ForEach((e) => e.SetPosition(new Vector2D(center.X - (e.Shape.Width / 2), center.Y - (e.Shape.Height / 2))));
    }

    /// <summary>
    /// Animates the planet movement by using timers (Threads)
    /// </summary>
    /// <param name="state"></param>
    public void MovePlanet(object state)
    {
        Planet planet = (Planet)state;
        Task.Run(() => planet.Move());
    }
}