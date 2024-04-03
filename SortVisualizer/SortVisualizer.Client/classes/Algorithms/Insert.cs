public class Insert : SortAlgorithm
{
    public Insert()
    {
        SortService = new SortService();

        Delay = 30;
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        ClearValues();

        int n = arrayElements.Count;
        for (int i = 1; i < n; ++i)
        {
            ArrayAccessCount++;
            ArrayElement key = arrayElements[i];
            int j = i - 1;

            CompareCount++;

            while (j >= 0 && arrayElements[j].Value > key.Value)
            {
                CompareCount++;

                await Swap(j + 1, j, arrayElements);

                arrayElements[j].Color = "red";
                SortService.OnStyleChanged();
                await Task.Delay(Delay);

                arrayElements[j].Color = "blue";

                j--;
            }

            arrayElements[j + 1] = key;
        }

        SortService.ArrayCheck(arrayElements, this);
    }

}