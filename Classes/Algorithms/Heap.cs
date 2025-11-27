using SortVisualizer.Client.Classes.SortElements;

public class Heap : SortAlgorithm
{
    public Heap()
    {
        SortService = new SortService();
    }

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        ArrayAccessCount++;
        int n = arrayElements.Count;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            ArrayAccessCount++;
            await Heapify(arrayElements, n, i);
        }

        // Один за другим извлекаем элементы из кучи
        for (int i = n - 1; i > 0; i--)
        {
            await SortService.WaitColor(Delay, arrayElements[i]); 

            await SwapSWAG(0, i, arrayElements);

            ArrayAccessCount++;
            await Heapify(arrayElements, i, 0); // Обращение к массиву
        }
    }

    async Task Heapify<T>(List<T> arrayElements, int n, int i) where T : ISvgElement
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n && arrayElements[l].GetValue() > arrayElements[largest].GetValue())
            largest = l; 

        if (r < n && arrayElements[r].GetValue() > arrayElements[largest].GetValue())
            largest = r; 

        if (largest != i)
        {
            await SortService.WaitColor(Delay, arrayElements[i]); 

            await SwapSWAG(i, largest, arrayElements); 

            ArrayAccessCount++;
            await Heapify(arrayElements, n, largest); 
        }
    }
}
