
public class Shell : SortAlgorithm
{
    public Shell() 
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        int n = arrayElements.Count;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i += 1)
            {
                var temp = arrayElements[i];
                int j;

                ArrayAccessCount++;

                await SortService.WaitColor(Delay, arrayElements[i]);

                for (j = i; j >= gap && arrayElements[j - gap].GetValue() > temp.GetValue(); j -= gap)
                {
                    CompareCount++;
                    await SwapSWAG(j, j - gap, arrayElements);
                }

                await SortService.WaitColor(Delay, arrayElements[j]);

                arrayElements[j] = temp;
            }
        }
    }
}