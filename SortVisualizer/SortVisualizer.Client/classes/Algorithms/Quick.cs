﻿using SortVisualizer.Client.Classes.SortElements;

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

    private async Task QuickSort<T>(List<T> arrayElements, int low, int high) where T : ISvgElement
    {
        if (low < high)
        {
            ArrayAccessCount++;
            int pi = await Partition(arrayElements, low, high); 

            await QuickSort(arrayElements, low, pi - 1); 
            await QuickSort(arrayElements, pi + 1, high);
        }
    }

    private async Task<int> Partition<T>(List<T> arrayElements, int low, int high) where T : ISvgElement
    {
        var pivot = arrayElements[high]; 
        ArrayAccessCount++;

        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            await SortService.WaitColor(Delay, arrayElements[j]);

            CompareCount++;

            if (arrayElements[j].GetValue() < pivot.GetValue()) 
            {
                i++;

                await SwapSWAG(i, j, arrayElements); 
            }
        }

        await SwapSWAG(i + 1, high, arrayElements);

        return i + 1;
    }
}
