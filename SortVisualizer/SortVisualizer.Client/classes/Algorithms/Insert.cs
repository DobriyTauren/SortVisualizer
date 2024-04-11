public class Insert : SortAlgorithm
{
    public Insert()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        int n = arrayElements.Count;
        for (int i = 1; i < n; ++i)
        {
            ArrayAccessCount++;
            T key = arrayElements[i];
            int j = i - 1;

            CompareCount++;

            while (j >= 0 && arrayElements[j].Value > key.Value)
            {
                CompareCount++;

                await SortService.WaitColor(Delay, arrayElements[j + 1]);

                if (arrayElements is List<Bar>) // peredelat'?
                {
                    await Swap(j + 1, j, arrayElements as List<Bar>);
                }
                else
                {
                    await Swap(j + 1, j, arrayElements as List<Point>);
                }

                j--;
            }

            arrayElements[j + 1] = key;
        }
    }

}