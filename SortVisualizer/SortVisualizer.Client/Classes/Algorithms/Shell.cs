
public class Shell : SortAlgorithm
{
    public Shell() 
    {
        SortService = new SortService();
    }

    public override async Task Sort(List<ArrayElement> arrayElements)
    {
        int n = arrayElements.Count;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {            
            for (int i = gap; i < n; i += 1)
            {
                var temp = arrayElements[i];
                int j;

                await SortService.WaitColor(Delay, arrayElements[i]);
                
                for (j = i; j >= gap && arrayElements[j - gap].Value > temp.Value; j -= gap)
                {
                    arrayElements[j] = arrayElements[j - gap];
                }

                await SortService.WaitColor(Delay, arrayElements[j]);
                arrayElements[j] = temp;
            }
        }
    }
}