public class Generator
{
    public const double MARGIN_WIDTH = 0.15;
    private const double SORT_PANEL_WIDTH = 100;

    private const int HEIGHT_MODIFICATOR = 3;
    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;

    private int _itemsCount = 150;

    public int ItemsCount { get => _itemsCount; set => _itemsCount = value; }

    public List<Bar> GenerateBars()
    {
        var arrayElements = new List<Bar>();
        var random = new Random();

        double totalMargin = _itemsCount * MARGIN_WIDTH;
        double currentWidth = SORT_PANEL_WIDTH - totalMargin;
        double itemWidth = currentWidth / _itemsCount;

        int fontSize = 12;

        if (_itemsCount > 70)
        {
            fontSize = 0;
        }

        for (int i = 0; i < _itemsCount; i++)
        {
            var value = random.Next(MIN_VALUE, MAX_VALUE);

            var arrayElement = new Bar
            {
                Value = value,
                Height = $"{value * HEIGHT_MODIFICATOR}px",
                Color = "blue",
                Width = $"{itemWidth}%",  
                FontSize = $"{fontSize}px",
            };

            arrayElements.Add(arrayElement);
        }

        return arrayElements;
    }

    public List<Point> GeneratePoints()
    {
        var points = new List<Point>();
        var random = new Random();

        for (int i = 0; i < _itemsCount; i++)
        {
            var point = new Point
            {
                Value = random.Next(0, 361),
                X = random.Next(10, 590),
                Y = random.Next(10, 390),
            };



            // Сопоставляем значение с цветом в палитре
            // Например, можно использовать градиент цветов от одного к другому
            point.Color =  $"hsl({point.Value}, 100%, 50%)"; // Используем HSL для представления цвета

            points.Add(point);
        }

        return points;
    }
}