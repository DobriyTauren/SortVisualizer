public abstract class SortAlgorithm
{
    public int Delay { get; set; } = 1;
    public int SwapCount { get; protected set; }
    public int CompareCount { get; protected set; }
    public int ArrayAccessCount { get; protected set; }
    public SortService SortService { get; protected set; }


    public abstract Task Sort (List<Bar> arrayElements);
    public virtual Task Sort (List<Point> arrayElements)
    {
        return null;
    }

    protected virtual async Task Swap (int index1, int index2, List<Bar> arrayElements)
    {
        SwapCount++;

        var temp = arrayElements[index1];
        ArrayAccessCount++;

        arrayElements[index1] = arrayElements[index2];
        ArrayAccessCount += 2;

        arrayElements[index2] = temp;
        ArrayAccessCount++;
    }        
    
    protected virtual async Task Swap (int index1, int index2, List<Point> arrayElements) // peredelat'
    {
        (double, double) pos1 = (arrayElements[index1].X, arrayElements[index1].Y);
        (double, double) pos2 = (arrayElements[index2].X, arrayElements[index2].Y);

        SwapCount++;

        var temp = arrayElements[index1];
        ArrayAccessCount++;

        arrayElements[index1] = arrayElements[index2];
        ArrayAccessCount += 2;

        arrayElements[index2] = temp;
        ArrayAccessCount++;

        arrayElements[index1].X = pos1.Item1;
        arrayElements[index1].Y = pos1.Item2;
        
        arrayElements[index2].X = pos2.Item1;
        arrayElements[index2].Y = pos2.Item2;
    }

    protected virtual void ClearValues() 
    {
        ArrayAccessCount = 0;
        SwapCount = 0;
        CompareCount = 0;
    }  
}