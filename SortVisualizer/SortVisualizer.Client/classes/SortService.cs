public static class SortService
{
    public static event EventHandler StyleChanged;

    private static int _delay = 10;
    private static int _arrayAccessCount = 0;
    private static int _swapCount = 0;
    private static int _compareCount = 0;
    
    public static int Delay { get => _delay; set => _delay = value; }
    public static int SwapCount { get => _swapCount; }
    public static int ArrayAccessCount { get => _arrayAccessCount; }
    public static int CompareCount { get => _compareCount; }

    public static List<ArrayElement> GenerateArray()
    {
        var arrayElements = new List<ArrayElement>();

        var random = new Random();
        for (int i = 0; i < 40; i++)
        {
            var value = random.Next(1, 100);
            var arrayElement = new ArrayElement
            {
                Text = value,
            };

            arrayElement.Height = $"{value * 3}px";
            arrayElement.Color = "blue";

            arrayElements.Add(arrayElement);
        }

        return arrayElements;
    }

    public static async Task BubbleSort(List<ArrayElement> arrayElements)
    {
        _arrayAccessCount = 0;
        _swapCount = 0;
        _compareCount = 0;

        for (int i = 0; i < arrayElements.Count; i++)
        {
            for (int j = 0; j < arrayElements.Count - i - 1; j++)
            {
                #region color red
                arrayElements[j].Color = "red";
                arrayElements[j + 1].Color = "red";

                OnStyleChanged();

                await Task.Delay(_delay);
                #endregion
                
                var value1 = arrayElements[j].Text;
                _arrayAccessCount++;

                var value2 = arrayElements[j + 1].Text;
                _arrayAccessCount++;

                _compareCount++;
                if (value1 > value2)
                {
                    await Swap(j, j + 1, arrayElements);
                    
                    _swapCount++;                    
                }

                #region color blue
                arrayElements[j].Color = "blue";
                arrayElements[j + 1].Color = "blue";

                OnStyleChanged();

                await Task.Delay(_delay);
                #endregion
            }
            arrayElements[arrayElements.Count - i - 1].Color = "green";
        }
    }

    private static async Task Swap(int index1, int index2, List<ArrayElement> arrayElements)
    {
        var temp = arrayElements[index1];
        _arrayAccessCount++;

        arrayElements[index1] = arrayElements[index2];
        _arrayAccessCount++;
        _arrayAccessCount++;

        arrayElements[index2] = temp;
        _arrayAccessCount++;

        OnStyleChanged();
    }

    public static void OnStyleChanged()
    {
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }
}


