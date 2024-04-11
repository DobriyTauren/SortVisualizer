﻿using SortVisualizer.Client.Classes.SortElements;

public class Generator
{
    public const double MARGIN_WIDTH = 0.15;
    private const double SORT_PANEL_WIDTH = 100;

    public const int LINE_WIDTH = 5;
    private const int HEIGHT_MODIFICATOR = 3;
    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;
    private const int MAX_HEIGHT = 310;

    private int _itemsCount = 150;

    public int ContainerWidth { get; private set; } = 600;

    public int ItemsCount { get => _itemsCount; set => _itemsCount = value; }

    public List<SvgLine> GenerateLines()
    {
        var lines = new List<SvgLine>();
        var random = new Random();
        var generatedValues = new HashSet<int>(); // Для отслеживания уже сгенерированных значений

        for (int i = 0; i < _itemsCount; i++)
        {
            int value;
            do
            {
                var height = random.Next(MIN_VALUE, MAX_HEIGHT);
                value = MAX_HEIGHT - height;
            } while (!generatedValues.Add(value)); // Генерируем значение, пока оно не станет уникальным

            var line = new SvgLine
            {
                StartPoint = new Point(i * LINE_WIDTH + 3, MAX_HEIGHT),
                EndPoint = new Point(i * LINE_WIDTH + 3, MAX_HEIGHT - value),
                Value = value,
                Color = "blue",
            };

            lines.Add(line);
        }

        ContainerWidth = LINE_WIDTH * lines.Count + 3;

        return lines;
    }

    public List<SvgCircle> GenerateCircles()
    {
        var points = new List<SvgCircle>();
        var random = new Random();
        var generatedValues = new HashSet<int>(); // Для отслеживания уже сгенерированных значений

        for (int i = 0; i < _itemsCount; i++)
        {
            int value;
            do
            {
                value = random.Next(0, 361);
            } while (!generatedValues.Add(value)); // Генерируем значение, пока оно не станет уникальным

            var point = new SvgCircle
            {
                Value = value,
                Center = new Point(random.Next(10, 360), random.Next(10, 390)),
            };

            point.Color = $"hsl({point.Value}, 100%, 50%)";

            points.Add(point);
        }

        return points;
    }

}