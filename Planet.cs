using System;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Metadata;


namespace AvaloniaSolarSystem;

public class Planet : INotifyPropertyChanged
{
    public Ellipse Shape { get; set; }
    public double Speed { get; set; }
    public Vector2D Position { get; private set; }
    public Vector2D newPosition = new();
    protected double angle;
    public int distance = 0;
    public double CachedWidth { get; }
    public double CachedHeight { get; }

    public Planet(int height, int width, double speed, Vector2D position, IImmutableSolidColorBrush colour, int distance = 0)
    {
        Shape = new Ellipse()
        {
            Height = height,
            Width = width,
            Fill = colour,
        };

        CachedWidth = width;
        CachedHeight = height;

        Speed = speed;
        Position = position;
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
        //Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }

    public virtual void CalcMove()
    {
        angle += Speed;

        newPosition = new(
            Math.Sin(angle) * distance + Position.X,
            Math.Cos(angle) * distance + Position.Y
        );

        //Console.WriteLine("PlanetMovement thread id: " + Thread.CurrentThread.ManagedThreadId);
    }

    public void SetPosition(Vector2D position)
    {
        Position = position;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
