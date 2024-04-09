public class Heap : SortAlgorithm
{
    public Heap()
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        int n = arrayElements.Count;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            await Heapify(arrayElements, n, i);
        }

        for (int i = n - 1; i > 0; i--)
        {
            await SortService.WaitColor(Delay, arrayElements[i]);
            await Swap(0, i, arrayElements);

            await Heapify(arrayElements, i, 0);
        }
    }

    async Task Heapify(List<ArrayElement> arrayElements, int n, int i)
    {
        int largest = i; 
        int l = 2 * i + 1;  
        int r = 2 * i + 2;  

        if (l < n && arrayElements[l].Value > arrayElements[largest].Value)
            largest = l;

        if (r < n && arrayElements[r].Value > arrayElements[largest].Value)
            largest = r;

        if (largest != i)
        {
            await SortService.WaitColor(Delay, arrayElements[i]);

            await Swap(i, largest, arrayElements);

            await Heapify(arrayElements, n, largest);
        }
    }

}