public abstract class SortAlgorithm
{
    public int Delay { get; set; } = 1;
    public int SwapCount { get; protected set; }
    public int CompareCount { get; protected set; }
    public int ArrayAccessCount { get; protected set; }
    public SortService SortService { get; protected set; }


    public abstract Task Sort (List<ArrayElement> arrayElements);
    
    protected virtual async Task Swap (int index1, int index2, List<ArrayElement> arrayElements)
    {
        SwapCount++;

        var temp = arrayElements[index1];
        ArrayAccessCount++;

        arrayElements[index1] = arrayElements[index2];
        ArrayAccessCount += 2;

        arrayElements[index2] = temp;
        ArrayAccessCount++;
    }

    protected virtual void ClearValues() 
    {
        ArrayAccessCount = 0;
        SwapCount = 0;
        CompareCount = 0;
    }  
}