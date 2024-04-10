public class Bubble : SortAlgorithm
{
    public Bubble()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<Bar> arrayElements)
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
                    await Swap(j, j + 1, arrayElements);
                }
            }
        }
    }

    public override async Task Sort(List<Point> arrayElements)
    {
        for (int i = 0; i < arrayElements.Count; i++)
        {
            for (int j = 0; j < arrayElements.Count - i - 1; j++)
            {
                await SortService.Wait(Delay);

                ArrayAccessCount += 2;
                CompareCount++;

                if (arrayElements[j].Value > arrayElements[j + 1].Value)
                {
                    await Swap(j, j + 1, arrayElements);
                }
            }
        }
    }
}