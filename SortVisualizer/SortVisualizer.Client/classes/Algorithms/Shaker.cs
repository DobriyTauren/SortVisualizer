
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
                arrayElements[i].Color = "red";
                SortService.OnStyleChanged();
                await Task.Delay(Delay);
                arrayElements[i].Color = "blue";

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

                arrayElements[i].Color = "red";
                SortService.OnStyleChanged();
                await Task.Delay(Delay);
                arrayElements[i].Color = "blue";
            }

            start++;
        }
        while (swapped);

        SortService.ArrayCheck(arrayElements, this);
    }
}