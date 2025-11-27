
public class Shell : SortAlgorithm
{
    public Shell() 
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        ArrayAccessCount++;
        int n = arrayElements.Count;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i += 1)
            {
                var temp = arrayElements[i];
                int j;

                await SortService.WaitColor(Delay, arrayElements[i]);

                ArrayAccessCount += 3;
                for (j = i; j >= gap && arrayElements[j - gap].GetValue() > temp.GetValue(); j -= gap)
                {
                    await SwapSWAG(j, j - gap, arrayElements);
                }

                await SortService.WaitColor(Delay, arrayElements[j]);

                arrayElements[j] = temp;
            }
        }
    }
}