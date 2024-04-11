public class Quick : SortAlgorithm
{
    public Quick()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        await QuickSort(arrayElements, 0, arrayElements.Count - 1);
    }

    private async Task QuickSort<T>(List<T> arrayElements, int low, int high) where T : SortElement
    {
        if (low < high)
        {
            int pi = await Partition(arrayElements, low, high);

            await QuickSort(arrayElements, low, pi - 1);
            await QuickSort(arrayElements, pi + 1, high);
        }
    }

    private async Task<int> Partition<T>(List<T> arrayElements, int low, int high) where T : SortElement
    {
        var pivot = arrayElements[high];
        ArrayAccessCount++;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            await SortService.WaitColor(Delay, arrayElements[j]);

            CompareCount++;

            if (arrayElements[j].Value < pivot.Value)
            {
                i++;

                if (arrayElements is List<Bar>) // peredelat'?
                {
                    await Swap(i, j, arrayElements as List<Bar>);
                }
                else
                {
                    await Swap(i, j, arrayElements as List<Point>);
                }
            }
        }

        if (arrayElements is List<Bar>) // peredelat'?
        {
            await Swap(i + 1, high, arrayElements as List<Bar>);
        }
        else
        {
            await Swap(i + 1, high, arrayElements as List<Point>);
        }

        return i + 1;
    }

}