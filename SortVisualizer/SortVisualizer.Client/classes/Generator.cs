public class Generator
{
    public const double MARGIN_WIDTH = 0.2;
    private const double SORT_PANEL_WIDTH = 100;
    private const int HEIGHT_MODIFICATOR = 3;

    private int _itemsCount = 100;

    public int ItemsCount { get => _itemsCount; set => _itemsCount = value; }

    public List<ArrayElement> GenerateRandomArray()
    {
        var arrayElements = new List<ArrayElement>();
        var random = new Random();

        double totalMargin = _itemsCount * MARGIN_WIDTH;
        double currentWidth = SORT_PANEL_WIDTH - totalMargin;
        double itemWidth = currentWidth / _itemsCount;

        for (int i = 0; i < _itemsCount; i++)
        {
            var value = random.Next(1, 100);

            var arrayElement = new ArrayElement
            {
                Value = value,
                Height = $"{value * HEIGHT_MODIFICATOR}px",
                Color = "blue",
                Width = $"{itemWidth}%",
            };

            arrayElements.Add(arrayElement);
        }

        return arrayElements;
    }
}