using SortVisualizer.Client.Classes.SortElements;

public class Generator
{
    private const float MIN_VALUE = 1f;
    private const float MAX_VALUE = 100f;

    private SortService _sortService = new SortService();

    private float _lineWidth = 4;
    private float _lineWidthPercentage = 4;

    public float ContainerWidth { get; set; } = 600;
    public float ContainerHeight { get; set; }

    public float LineWidthPercentage { get => _lineWidthPercentage; set => _lineWidthPercentage = value; }

    public List<SvgLine> GenerateRects(int elementsCount)
    {
        var rects = new List<SvgLine>();
        var values = GenerateValues(elementsCount, MAX_VALUE);

        Shuffle(values);

        _lineWidth = (ContainerWidth - 8) / elementsCount;

        float offset = _lineWidth * 0.25f;

        _lineWidthPercentage = (_lineWidth - offset) / ContainerWidth * 100;

        Console.WriteLine($"ContainerWidth: {ContainerWidth}, _lineWidth: {_lineWidth}, _lineWidthPercentage: {_lineWidthPercentage}, elementsCount: {elementsCount}, offset: {offset}");

        for (int i = 0; i < elementsCount; i++)
        {
            float xPercentage = i * _lineWidth / ContainerWidth * 100; // Исправлено на (_lineWidth + offset)

            Point startPoint = new Point(xPercentage, MAX_VALUE - values[i]);

            var line = new SvgLine
            {
                Position = startPoint,
                FixedPosition = startPoint,
                Value = values[i],
                Color = _sortService.BaseColor,
            };

            rects.Add(line);
        }

        return rects;
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

            float centerXPercentage = random.Next(30, (int)ContainerWidth - 30) / ContainerWidth * 100;
            float centerYPercentage = random.Next(30, (int)ContainerHeight - 30) / ContainerHeight * 100;

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