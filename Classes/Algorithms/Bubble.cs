public class Bubble : SortAlgorithm
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

                if (arrayElements[j].GetValue() > arrayElements[j + 1].GetValue())
                {
                    await SwapSWAG(j, j + 1, arrayElements);
                }
            }
        }
    }
}