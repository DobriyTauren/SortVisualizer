public class SortService
{
    public event EventHandler StyleChanged;

    private int _delay = 600;

    private int _arrayAccessCount = 0;
    private int _swapCount = 0;
    private int _compareCount = 0;

    public int Delay { get => _delay; set => _delay = value; }
    public int SwapCount { get => _swapCount; }
    public int ArrayAccessCount { get => _arrayAccessCount; }
    public int CompareCount { get => _compareCount; }

    public async Task BubbleSort(List<ArrayElement> arrayElements)
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

                #endregion

                var value1 = arrayElements[j].Text;
                var value2 = arrayElements[j + 1].Text;

                _arrayAccessCount += 2;
                _compareCount++;

                await Task.Delay(Convert.ToInt32(_delay * 0.3));

                if (value1 > value2)
                {
                    await Swap(j, j + 1, arrayElements);

                    _swapCount++;
                }

                await Task.Delay(Convert.ToInt32(_delay * 0.7));

                #region color blue

                arrayElements[j].Color = "blue";

                #endregion
            }


            arrayElements[arrayElements.Count - i - 1].Color = "green";
        }
    }

    private async Task Swap(int index1, int index2, List<ArrayElement> arrayElements)
    {
        var temp = arrayElements[index1];
        _arrayAccessCount++;

        arrayElements[index1] = arrayElements[index2];
        _arrayAccessCount += 2;

        arrayElements[index2] = temp;
        _arrayAccessCount++;

        OnStyleChanged();
    }

    public void OnStyleChanged()
    {
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }
}


