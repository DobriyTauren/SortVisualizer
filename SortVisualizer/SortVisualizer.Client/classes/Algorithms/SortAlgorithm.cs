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
                case < 0:
                    _delay = 0;
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

    protected virtual void Swap<T>(int index1, int index2, List<T> arrayElements) where T : ISvgElement
    {
        var point1 = arrayElements[index1].GetPosition();
        var point2 = arrayElements[index2].GetPosition();

        MoveCount++;

        var temp = arrayElements[index1];
        arrayElements[index1] = arrayElements[index2];
        arrayElements[index2] = temp;

        ArrayAccessCount += 3;

        arrayElements[index1].Move(point1);
        arrayElements[index2].Move(point2);
    }

    protected virtual void ClearValues() 
    {
        ArrayAccessCount = 0;
        MoveCount = 0;
    }  
}