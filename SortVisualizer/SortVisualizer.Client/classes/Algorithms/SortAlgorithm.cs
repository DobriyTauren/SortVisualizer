﻿using SortVisualizer.Client.Classes.SortElements;

public abstract class SortAlgorithm
{
    public int Delay { get; set; } = 1;
    public int SwapCount { get; protected set; }
    public int CompareCount { get; protected set; }
    public int ArrayAccessCount { get; protected set; }
    public SortService SortService { get; protected set; }


    public abstract Task Sort<T> (List<T> arrayElements) where T : ISvgElement;

    protected virtual async Task SwapSWAG<T>(int index1, int index2, List<T> arrayElements) where T : ISvgElement
    {
        var startPosIndex1 = arrayElements[index1].GetStartPosition();
        var startPosIndex2 = arrayElements[index2].GetStartPosition();

        SwapCount++;

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
        SwapCount = 0;
        CompareCount = 0;
    }  
}