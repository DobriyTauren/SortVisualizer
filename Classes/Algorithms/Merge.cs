using SortVisualizer.Client.Classes.SortElements;

public class Merge : SortAlgorithm
{
    public Merge()
    {
        SortService = new SortService();
    }

    private List<ISvgElement> _savedElements;    

    public override async Task Sort<T>(List<T> arrayElements)
    {
        ClearValues();

        _savedElements = arrayElements.Cast<ISvgElement>().ToList();

        ArrayAccessCount += 2;
        await MergeSort(arrayElements, 0, arrayElements.Count - 1);
    }

    private async Task MergeSort<T>(List<T> arrayElements, int left, int right) where T : ISvgElement
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            ArrayAccessCount += 3;
            await MergeSort(arrayElements, left, middle);
            await MergeSort(arrayElements, middle + 1, right);

            await MergeElements(arrayElements, left, middle, right);
        }
    }

    private async Task MergeElements<T>(List<T> arrayElements, int left, int middle, int right) where T : ISvgElement
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;
        
        ArrayAccessCount += 2;
        List<T> leftArray = new List<T>(arrayElements.GetRange(left, n1));
        List<T> rightArray = new List<T>(arrayElements.GetRange(middle + 1, n2));


        for (int i = 0; i < n1; ++i)
        {
            ArrayAccessCount++;
            leftArray[i] = arrayElements[left + i];
        }    

        for (int j = 0; j < n2; ++j)
        {
            ArrayAccessCount++;
            rightArray[j] = arrayElements[middle + 1 + j];
        }

        int x = 0, y = 0;
        int k = left;

        while (x < n1 && y < n2)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            ArrayAccessCount++;
            MoveCount++;

            if (leftArray[x].GetValue() <= rightArray[y].GetValue())
            {
                
                leftArray[x].Move(_savedElements[k].GetFixedPosition());
                arrayElements[k] = leftArray[x];
                x++;
            }
            else
            {
                rightArray[y].Move(_savedElements[k].GetFixedPosition());
                arrayElements[k] = rightArray[y];
                y++;
            }

            //Console.WriteLine($"{k} : {_savedElements[k].GetFixedPosition().X}");

            k++;
        }

        while (x < n1)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            MoveCount++;
            leftArray[x].Move(_savedElements[k].GetFixedPosition());
            
            ArrayAccessCount++;
            arrayElements[k] = leftArray[x];
            x++;
            k++;
        }

        while (y < n2)
        {
            await SortService.WaitColor(Delay, arrayElements[k]);

            MoveCount++;
            rightArray[y].Move(_savedElements[k].GetFixedPosition());
            
            ArrayAccessCount++;
            arrayElements[k] = rightArray[y];
            y++;
            k++;
        }
    }
}