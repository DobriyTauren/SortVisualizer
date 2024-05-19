using SortVisualizer.Client.Classes.SortElements;

public class Generator
{
    private const int MIN_VALUE = 1;
    private const float MAX_VALUE = 100;

    private SortService _sortService = new SortService();

    private double _lineWidth = 4;
    private double _lineWidthPercentage = 4;

    public double ContainerWidth { get; set; } = 600;
    public double ContainerHeight { get; set; }

    public double LineWidthPercentage { get => _lineWidthPercentage; set => _lineWidthPercentage = value; }

    public List<SvgLine> GenerateLines(int elementsCount)
    {
        var lines = new List<SvgLine>();
        var values = GenerateValues(elementsCount, MAX_VALUE);

        Shuffle(values);

        _lineWidth = (ContainerWidth - 8) / elementsCount;

        _lineWidthPercentage = (_lineWidth / ContainerWidth) * 100;

        if (ContainerWidth < 400) // mobile scalse cringe
        {
            _lineWidthPercentage *= 0.7;
        }

        double offset = _lineWidth / 2;

        for (int i = 0; i < elementsCount; i++)
        {
            double x = i * _lineWidth + offset;
            x = x / ContainerWidth * 100;

            Point startPoint = new Point(x, MAX_VALUE);

            var line = new SvgLine
            {
                StartPoint = startPoint,
                FixedStartPoint = startPoint,
                EndPoint = new Point(x, MAX_VALUE - values[i]),
                Value = values[i],
                Color = _sortService.BaseColor,
            };

            lines.Add(line);
        }

        return lines;
    }

    public List<SvgCircle> GenerateCircles(int elementsCount)
    {
        var points = new List<SvgCircle>();
        var random = new Random();
        var generatedValues = new HashSet<int>(); // Для отслеживания уже сгенерированных значений


        for (int i = 0; i < elementsCount; i++)
        {
            int value;
            do
            {
                value = random.Next(0, 361);
            } while (!generatedValues.Add(value)); // Генер значение, пока оно не станет уникальным

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

    public void Shuffle(List<float> elems)
    {
        Random rng = new Random();
        int n = elems.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = elems[k];
            elems[k] = elems[n];
            elems[n] = value;
        }
    }

    public static List<float> GenerateValues(int count, float maxValue)
    {
        List<float> values = new List<float>();

        float step = maxValue / (count - 1); // Расчет шага между значениями

        float currentValue = MIN_VALUE; // Начальное значение

        for (int i = 0; i < count; i++)
        {
            values.Add(currentValue); // Добавляем значение в список
            currentValue += step; // Увеличиваем текущее значение на шаг
        }

        return values;
    }
}