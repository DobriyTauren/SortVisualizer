
public class Shaker : SortAlgorithm
{
    public Shaker()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        ClearValues();

        bool swapped;
        int start = 0;
        int end = arrayElements.Count - 1;

        do
        {
            swapped = false;

            for (int i = start; i < end; i++)
            {
                await SortService.WaitColor(Delay, arrayElements[i]);

                CompareCount++;
                if (arrayElements[i].Value > arrayElements[i + 1].Value)
                {
                    await Swap(i, i + 1, arrayElements);
                    swapped = true;
                }
            }

            if (!swapped)
                break;

            swapped = false;
            end--;

            for (int i = end - 1; i >= start; i--)
            {
                CompareCount++;
                if (arrayElements[i].Value > arrayElements[i + 1].Value)
                {
                    await Swap(i, i + 1, arrayElements);
                    swapped = true;
                }

                await SortService.WaitColor(Delay, arrayElements[i]);
            }

            start++;
        }
        while (swapped);
    }
}