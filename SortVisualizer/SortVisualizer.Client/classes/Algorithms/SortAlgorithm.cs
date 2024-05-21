using SortVisualizer.Client.Classes.SortElements;

public abstract class SortAlgorithm
{
    private int _delay = 1;

    public int Delay
    {
        get => _delay;
        set
        {
            switch (value)
            {
                case < 1:
                    _delay = 1;
                    break;
                case > 9999:
                    _delay = 9999;
                    break;
                default:
                    _delay = value;
                    break;
            }
        }
    }

    public int MoveCount { get; protected set; }
    public int ArrayAccessCount { get; protected set; }
    public SortService SortService { get; protected set; }


    public abstract Task Sort<T> (List<T> arrayElements) where T : ISvgElement;

    protected virtual async Task SwapSWAG<T>(int index1, int index2, List<T> arrayElements) where T : ISvgElement
    {
        var startPosIndex1 = arrayElements[index1].GetStartPosition();
        var startPosIndex2 = arrayElements[index2].GetStartPosition();

        MoveCount++;

        var temp = arrayElements[index1];
        arrayElements[index1] = arrayElements[index2];
        arrayElements[index2] = temp;

        ArrayAccessCount += 3;

        arrayElements[index1].Move(startPosIndex1);
        arrayElements[index2].Move(startPosIndex2);
    }

    protected virtual void ClearValues() 
    {
        ArrayAccessCount = 0;
        MoveCount = 0;
    }  
}