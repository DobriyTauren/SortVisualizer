public class Insert : SortAlgorithm
{
    public Insert()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<Bar> arrayElements)
    {
        ClearValues();

        int n = arrayElements.Count;
        for (int i = 1; i < n; ++i)
        {
            ArrayAccessCount++;
            Bar key = arrayElements[i];
            int j = i - 1;

            CompareCount++;

            while (j >= 0 && arrayElements[j].Value > key.Value)
            {
                CompareCount++;

                await SortService.WaitColor(Delay, arrayElements[j + 1]);

                await Swap(j + 1, j, arrayElements);

                j--;
            }

            arrayElements[j + 1] = key;
        }
    }

}