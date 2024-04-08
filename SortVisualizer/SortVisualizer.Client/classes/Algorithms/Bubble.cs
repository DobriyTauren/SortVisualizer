public class Bubble : SortAlgorithm
{
    public Bubble()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        ClearValues();

        for (int i = 0; i < arrayElements.Count; i++)
        {
            for (int j = 0; j < arrayElements.Count - i - 1; j++)
            {
                #region color red

                arrayElements[j].Color = "red";
                SortService.OnStyleChanged();
                await Task.Delay(Delay);
                arrayElements[j].Color = "blue";

                #endregion

                var value1 = arrayElements[j].Value;
                var value2 = arrayElements[j + 1].Value;

                ArrayAccessCount += 2;
                CompareCount++;


                if (value1 > value2)
                {
                    Swap(j, j + 1, arrayElements);
                }
            }
        }

        SortService.ArrayCheck(arrayElements, this);
    }
}