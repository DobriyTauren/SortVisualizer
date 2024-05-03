
public class Shaker : SortAlgorithm
{
    public Shaker()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        bool swapped;
        int start = 0;
        int end = arrayElements.Count - 1;

        ArrayAccessCount++;

        do
        {
            swapped = false;

            for (int i = start; i < end; i++)
            {
                await SortService.WaitColor(Delay, arrayElements[i]);

                ArrayAccessCount += 2;
                if (arrayElements[i].GetValue() > arrayElements[i + 1].GetValue())
                {
                    await SwapSWAG(i, i + 1, arrayElements);

                    swapped = true;
                }
            }

            if (!swapped)
                break;

            swapped = false;
            end--;

            for (int i = end - 1; i >= start; i--)
            {
                ArrayAccessCount += 2;
                if (arrayElements[i].GetValue() > arrayElements[i + 1].GetValue())
                {
                    await SwapSWAG(i, i + 1, arrayElements);

                    swapped = true;
                }

                await SortService.WaitColor(Delay, arrayElements[i]);
            }

            start++;
        }
        while (swapped);
    }
}