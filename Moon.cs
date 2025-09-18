
using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaSolarSystem;

public class Moon : Planet
{
    public Planet planet;
    private Vector2D position = new();
    public Moon(int height, int width, double speed, Planet parent, IImmutableSolidColorBrush colour, int multiplier = 0) : base(height, width, speed, parent.Position, colour, multiplier)
    {
        planet = parent;
    }

    public override void Move(Object? sender, EventArgs e)
    {
        angle += Speed;

        Vector2D newPosition = new();

        newPosition = new(
            Math.Sin(angle) * distance + planet.newPosition.X + (planet.Shape.Width / 2),
            Math.Cos(angle) * distance + planet.newPosition.Y + (planet.Shape.Height / 2)
        );

        if (Shape.Parent != null)
        {
            Canvas.SetTop(Shape, newPosition.Y);
            Canvas.SetLeft(Shape, newPosition.X);
        }
        Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }
}