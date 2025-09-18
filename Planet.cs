using System;
using System.Numerics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Metadata;


namespace AvaloniaSolarSystem;

public class Planet
{
    public Ellipse Shape { get; set; }
    public double Speed { get; set; }
    public Vector2D Position { get; private set; }
    public Vector2D newPosition = new();
    protected double angle;
    protected int distance = 0;
    public Planet(int height, int width, double speed, Vector2D position, IImmutableSolidColorBrush colour, int distance = 0)
    {
        Shape = new Ellipse()
        {
            Height = height,
            Width = width,
            Fill = colour,
        };

        Speed = speed;
        this.Position = position;
        this.distance = distance;
    }

    public virtual void Move(Object? sender, EventArgs e)
    {
        angle += Speed;


        newPosition = new(
            Math.Sin(angle) * distance + Position.X,
            Math.Cos(angle) * distance + Position.Y
        );

        if (Shape.Parent != null)
        {
            Canvas.SetTop(Shape, newPosition.Y);
            Canvas.SetLeft(Shape, newPosition.X);
        }
        Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }

    public virtual void CalcMove()
    {
        angle += this.Speed;

        Position = new(Math.Sin(angle) * distance + Position.X, Math.Sin(angle) * distance + Position.Y);

        Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }

    internal virtual void Move()
    {
        if (Shape.Parent != null)
        {
            Canvas.SetTop(Shape, Position.X);
            Canvas.SetLeft(Shape, Position.Y);
        }
    }

    public void SetPosition(Vector2D position)
    {
        this.Position = position;
    }
}
