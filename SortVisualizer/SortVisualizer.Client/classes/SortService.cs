public class SortService
{
    public event EventHandler StyleChanged;

    private int _delay = 1;

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
                OnStyleChanged();
                await Task.Delay(_delay);

                #endregion

                var value1 = arrayElements[j].Value;
                var value2 = arrayElements[j + 1].Value;

                _arrayAccessCount += 2;
                _compareCount++;

                
                if (value1 > value2)
                {
                    Swap(j, j + 1, arrayElements);

                    arrayElements[j + 1].Color = "blue";
                }
                else
                {
                    arrayElements[j].Color = "blue";
                }
            }

            #region color green

            //arrayElements[arrayElements.Count - i - 1].Color = "green";

            #endregion
        }

        ArrayCheck(arrayElements);
    }

    public async Task InsertSort(List<ArrayElement> arrayElements)
    {
        _arrayAccessCount = 0;
        _swapCount = 0;
        _compareCount = 0;

        int n = arrayElements.Count;
        for (int i = 1; i < n; ++i)
        {
            _arrayAccessCount++;
            ArrayElement key = arrayElements[i];
            int j = i - 1;

            _compareCount++;

            while (j >= 0 && arrayElements[j].Value > key.Value)
            {
                _compareCount++;

                Swap(j + 1, j, arrayElements);

                arrayElements[j].Color = "red";
                OnStyleChanged();
                await Task.Delay(_delay);

                arrayElements[j].Color = "blue";

                j--;
            }

            arrayElements[j + 1] = key;
        }

        ArrayCheck(arrayElements);
    }

    public async Task QuickSort(List<ArrayElement> arrayElements)
    {
        _arrayAccessCount = 0;
        _swapCount = 0;
        _compareCount = 0;

        await QuickSort(arrayElements, 0, arrayElements.Count - 1);

        ArrayCheck(arrayElements);
    }

    public async Task ShakerSort(List<ArrayElement> arrayElements)
    {
        _arrayAccessCount = 0;
        _swapCount = 0;
        _compareCount = 0;

        bool swapped;
        int start = 0;
        int end = arrayElements.Count - 1;

        do
        {
            swapped = false;

            for (int i = start; i < end; i++)
            {
                arrayElements[i].Color = "red";
                OnStyleChanged();
                await Task.Delay(_delay);
                arrayElements[i].Color = "blue";

                if (arrayElements[i].Value > arrayElements[i + 1].Value)
                {
                    _compareCount += 2;
                    Swap(i, i + 1, arrayElements);
                    swapped = true;
                }
            }

            if (!swapped)
                break;

            swapped = false;
            end--;

            for (int i = end - 1; i >= start; i--)
            {
                arrayElements[i].Color = "red";
                OnStyleChanged();
                await Task.Delay(_delay);
                arrayElements[i].Color = "blue";

                if (arrayElements[i].Value > arrayElements[i + 1].Value)
                {
                    _compareCount += 2;
                    Swap(i, i + 1, arrayElements);
                    swapped = true;
                }
            }

            start++;
        }
        while (swapped);

        ArrayCheck(arrayElements);
    }

    private async Task QuickSort(List<ArrayElement> arrayElements, int low, int high)
    {
        if (low < high)
        {
            int pi = await Partition(arrayElements, low, high);

            await QuickSort(arrayElements, low, pi - 1);
            await QuickSort(arrayElements, pi + 1, high);
        }
    }

    private async Task<int> Partition(List<ArrayElement> arrayElements, int low, int high)
    {
        ArrayElement pivot = arrayElements[high];
        _arrayAccessCount++;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            arrayElements[j].Color = "red";
            OnStyleChanged();
            await Task.Delay(_delay);

            _compareCount++;
            if (arrayElements[j].Value < pivot.Value)
            {
                i++;
                Swap(i, j, arrayElements);

                arrayElements[i].Color = "blue";
            }
            else
            {
                arrayElements[j].Color = "blue";
            }
        }

        Swap(i + 1, high, arrayElements);

        return i + 1;
    }

    private async Task ArrayCheck(List<ArrayElement> arrayElements)
    {
        arrayElements[0].Color = "green";

        for (int i = 0; i < arrayElements.Count - 1; i++)
        {
            if (arrayElements[i].Value <= arrayElements[i + 1].Value)
            {
                arrayElements[i + 1].Color = "green";
            }

            await Task.Delay(_delay);
            OnStyleChanged();
        }
    }

    private void Swap(int index1, int index2, List<ArrayElement> arrayElements)
    {
        _swapCount++;

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


