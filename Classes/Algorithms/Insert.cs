public class Insert : SortAlgorithm
{
    public Insert()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        ArrayAccessCount++;
        int n = arrayElements.Count;
        for (int i = 1; i < n; ++i)
        {
            ArrayAccessCount += 2;
            T key = arrayElements[i];
            int j = i - 1;

            while (j >= 0 && arrayElements[j].GetValue() > key.GetValue())
            {
                ArrayAccessCount++;

                await SortService.WaitColor(Delay, arrayElements[j + 1]);
                await SwapSWAG(j + 1, j, arrayElements);

                j--;
            }

            MoveCount++;
            arrayElements[j + 1] = key;
        }
    }

}