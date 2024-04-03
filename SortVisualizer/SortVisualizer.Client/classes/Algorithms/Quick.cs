﻿public class Quick : SortAlgorithm
{
    public Quick()
    {
        SortService = new SortService();

        Delay = 25;
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        ClearValues();

        await QuickSort(arrayElements, 0, arrayElements.Count - 1);

        SortService.ArrayCheck(arrayElements, this);
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
        ArrayAccessCount++;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            arrayElements[j].Color = "red";
            SortService.OnStyleChanged();
            await Task.Delay(Delay);

            CompareCount++;
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

}