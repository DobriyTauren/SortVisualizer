using SortVisualizer.Client.Classes.SortElements;

public class Generator
{
    public const double MARGIN_WIDTH = 0.15;

    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;

    private SortService _sortService = new SortService();

    private double _lineWidth = 4;
    private double _lineWidthPercentage = 4;
    private int _itemsCount = 75;

    public double ContainerWidth { get; set; } = 600;
    public double ContainerHeight { get; set; }

    public int ItemsCount 
    { 
        get => _itemsCount; 
        set
        {
            switch (value)
            {
                case < 1:
                    _itemsCount = 1;
                    break;
                case > 355:
                    _itemsCount = 355;
                    break;
                default:
                    _itemsCount = value;
                    break;
            }
        }
    }

    public double LineWidthPercentage { get => _lineWidthPercentage; set => _lineWidthPercentage = value; }

    public List<SvgLine> GenerateLines()
    {
        var lines = new List<SvgLine>();
        var random = new Random();
        var generatedValues = new HashSet<int>(); // Для отслеживания уже сгенерированных значений

        _lineWidth = (ContainerWidth - 8) / ItemsCount;
        _lineWidthPercentage = (_lineWidth / ContainerWidth) * 100;

        double offset = _lineWidth / 2;

        for (int i = 0; i < _itemsCount; i++)
        {
            int value;
            do
            {
                var height = random.Next(MIN_VALUE, (int)ContainerHeight);
                value = (int)(ContainerHeight - height);
            } while (!generatedValues.Add(value)); // Генерируем значение, пока оно не станет уникальным

            double x = i * _lineWidth + offset;
            x = x / ContainerWidth * 100;

            Point startPoint = new Point(x, MAX_VALUE);

            var line = new SvgLine
            {
                StartPoint = startPoint,
                FixedStartPoint = startPoint,
                EndPoint = new Point(x, (double)(ContainerHeight - value) / ContainerHeight * 100),
                Value = value,
                Color = _sortService.BaseColor,
            };

            lines.Add(line);
        }

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

            double centerXPercentage = random.Next(30, (int)ContainerWidth - 30) / ContainerWidth * 100;
            double centerYPercentage = random.Next(30, (int)ContainerHeight - 30) / ContainerHeight * 100;

            Point center = new Point(centerXPercentage, centerYPercentage);

            var point = new SvgCircle
            {
                Value = value,
                Center = center,
                FixedCenter = center,
            };

            point.Color = $"hsl({point.Value}, 100%, 50%)";

            points.Add(point);
        }

        return points;
    }

    public static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}