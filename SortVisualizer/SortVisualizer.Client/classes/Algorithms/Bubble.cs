﻿public class Bubble : SortAlgorithm
{
    public Bubble()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        for (int i = 0; i < arrayElements.Count; i++)
        {
            for (int j = 0; j < arrayElements.Count - i - 1; j++)
            {
                await SortService.WaitColor(Delay, arrayElements[j]);

                ArrayAccessCount += 2;
                CompareCount++;

                if (arrayElements[j].Value > arrayElements[j + 1].Value)
                {
                    if (arrayElements is List<Bar>) // peredelat'?
                    {
                        await Swap(j, j + 1, arrayElements as List<Bar>);
                    }
                    else
                    {
                        await Swap(j, j + 1, arrayElements as List<Point>);
                    }
                }
            }
        }
    }
}