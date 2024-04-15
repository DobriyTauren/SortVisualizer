using SortVisualizer.Client.Classes.SortElements;

public class Generator
{
    public const double MARGIN_WIDTH = 0.15;
    private const double SORT_PANEL_WIDTH = 100;

    private const int HEIGHT_MODIFICATOR = 3;
    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;
    private const int MAX_HEIGHT = 310;

    private int _lineWidth = 4;
    private int _itemsCount = 200;

    public int ContainerWidth { get; private set; } = 600;

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
                case > 310:
                    _itemsCount = 305;
                    break;
                default:
                    _itemsCount = value;
                    break;
            }
        }
    }

    public int LINE_WIDTH { get => _lineWidth; set => _lineWidth = value; }

    public List<SvgLine> GenerateLines()
    {
        var lines = new List<SvgLine>();
        var random = new Random();
        var generatedValues = new HashSet<int>(); // Для отслеживания уже сгенерированных значений
        var offset = 3;

        switch (_itemsCount)
        {
            case <= 10:
                _lineWidth = 30;
                break;
            case <= 24:
                _lineWidth = 15;
                break;
            case <= 35:
                _lineWidth = 10;
                break;
            case <= 100:
                _lineWidth = 5;
                break;
            default:
                _lineWidth = 4;
                break;
        }

        for (int i = 0; i < _itemsCount; i++)
        {
            int value;
            do
            {
                var height = random.Next(MIN_VALUE, MAX_HEIGHT);
                value = MAX_HEIGHT - height;
            } while (!generatedValues.Add(value)); // Генерируем значение, пока оно не станет уникальным

            Point startPoint = new Point(i * LINE_WIDTH + offset, MAX_HEIGHT);

            var line = new SvgLine
            {
                StartPoint = startPoint,
                FixedStartPoint = startPoint,
                EndPoint = new Point(i * LINE_WIDTH + offset, MAX_HEIGHT - value),
                Value = value,
                Color = "blue",
            };

            lines.Add(line);
        }

        ContainerWidth = LINE_WIDTH * lines.Count + offset;

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

            Point center = new Point(random.Next(10, 360), random.Next(10, 390));

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

}