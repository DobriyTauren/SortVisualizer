using SortVisualizer.Client.Classes.SortElements;

public class Merge : SortAlgorithm
{
    public Merge()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        await MergeSort(arrayElements, 0, arrayElements.Count - 1);
    }

    private async Task MergeSort<T>(List<T> arrayElements, int left, int right) where T : ISvgElement
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            await MergeSort(arrayElements, left, middle);
            await MergeSort(arrayElements, middle + 1, right);

            await MergeElements(arrayElements, left, middle, right);
        }
    }

    private async Task MergeElements<T>(List<T> arrayElements, int left, int middle, int right) where T : ISvgElement
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        List<T> leftArray = new List<T>(arrayElements.GetRange(left, n1));
        List<T> rightArray = new List<T>(arrayElements.GetRange(middle + 1, n2));


        for (int i = 0; i < n1; ++i)
            leftArray[i] = arrayElements[left + i];
        for (int j = 0; j < n2; ++j)
            rightArray[j] = arrayElements[middle + 1 + j];

        int x = 0, y = 0;
        int k = left;

        while (x < n1 && y < n2)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            if (leftArray[x].GetValue() <= rightArray[y].GetValue())
            {
                arrayElements[k] = leftArray[x];
                x++;
            }
            else
            {
                arrayElements[k] = rightArray[y];
                y++;
            }
            k++;
        }

        while (x < n1)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            arrayElements[k] = leftArray[x];
            x++;
            k++;
        }

        while (y < n2)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            arrayElements[k] = rightArray[y];
            y++;
            k++;
        }
    }
}