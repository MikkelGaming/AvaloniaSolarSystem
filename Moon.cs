
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Media;

namespace AvaloniaSolarSystem;

public class Moon : Planet
{
    public Planet planet;

    public Moon(int height, int width, double speed, Planet parent, IImmutableSolidColorBrush colour, int multiplier = 0) : base(height, width, speed, parent?.Position ?? new Vector2D(), colour, multiplier)
    {
        planet = parent;
    }

    public Moon(Moon moon) : base((int)moon.CachedWidth, (int)moon.CachedHeight, moon.Speed, moon.Position, (IImmutableSolidColorBrush)moon.Shape.Fill, moon.distance)
    {
        planet = moon.planet;
    }

    public override void Move(Object? sender, EventArgs e)
    {
        angle += Speed;

        newPosition = new(
            Math.Sin(angle) * distance + planet.newPosition.X + (planet.Shape.Width / 2),
            Math.Cos(angle) * distance + planet.newPosition.Y + (planet.Shape.Height / 2)
        );

        if (Shape.Parent != null)
        {
            Canvas.SetTop(Shape, newPosition.Y);
            Canvas.SetLeft(Shape, newPosition.X);
        }
        //Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }

    public void CalcMoveMoon()
    {
        angle += Speed;

        double xOffset = 0;
        double yOffset = 0;

        if (planet != null)
        {
            // Use cached width/height for thread safety
            xOffset = planet.newPosition.X + (planet.CachedWidth / 2) - (CachedWidth / 2);
            yOffset = planet.newPosition.Y + (planet.CachedHeight / 2) - (CachedHeight / 2);
        }

        newPosition = new(
            Math.Sin(angle) * distance + xOffset,
            Math.Cos(angle) * distance + yOffset
        );
    }
}