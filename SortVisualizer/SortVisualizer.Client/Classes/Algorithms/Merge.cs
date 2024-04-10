public class Merge : SortAlgorithm
{
    public Merge()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<Bar> arrayElements)
    {
        ClearValues();

        await MergeSort(arrayElements, 0, arrayElements.Count - 1);
    }

    private async Task MergeSort(List<Bar> arrayElements, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            await MergeSort(arrayElements, left, middle);
            await MergeSort(arrayElements, middle + 1, right);

            await MergeElements(arrayElements, left, middle, right);
        }
    }

    private async Task MergeElements(List<Bar> arrayElements, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        List<Bar> leftArray = new();
        List<Bar> rightArray = new();

        for (int i = 0; i < n1; i++)
        {
            leftArray.Add(new());
        }

        for (int i = 0; i < n2; i++)
        {
            rightArray.Add(new());
        }

        for (int i = 0; i < n1; ++i)
            leftArray[i] = arrayElements[left + i];
        for (int j = 0; j < n2; ++j)
            rightArray[j] = arrayElements[middle + 1 + j];

        int x = 0, y = 0;
        int k = left;

        while (x < n1 && y < n2)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            if (leftArray[x].Value <= rightArray[y].Value)
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