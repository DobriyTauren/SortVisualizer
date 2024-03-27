﻿public class SortService
{
    public event EventHandler StyleChanged;

    private int _delay = 10;

    public List<ArrayElement> GenerateArray()
    {
        var arrayElements = new List<ArrayElement>();

        var random = new Random();
        for (int i = 0; i < 40; i++)
        {
            var value = random.Next(1, 100);
            var arrayElement = new ArrayElement
            {
                Text = value,
            };

            arrayElement.Height = $"{value * 3}px";
            arrayElement.Color = "blue";

            arrayElements.Add(arrayElement);
        }

        return arrayElements;
    }

    public async Task BubbleSort(List<ArrayElement> arrayElements)
    {
        for (int i = 0; i < arrayElements.Count; i++)
        {
            for (int j = 0; j < arrayElements.Count - i - 1; j++)
            {
                arrayElements[j].Color = "red";
                arrayElements[j + 1].Color = "red";

                OnStyleChanged();

                await Task.Delay(_delay);
                var value1 = arrayElements[j].Text;
                var value2 = arrayElements[j + 1].Text;
                if (value1 > value2)
                {
                    await Swap(j, j + 1, arrayElements);
                }

                arrayElements[j].Color = "blue";
                arrayElements[j + 1].Color = "blue";

                OnStyleChanged();

                await Task.Delay(_delay);
            }
            arrayElements[arrayElements.Count - i - 1].Color = "green";
        }
    }

    private async Task Swap(int index1, int index2, List<ArrayElement> arrayElements)
    {
        var temp = arrayElements[index1];
        arrayElements[index1] = arrayElements[index2];
        arrayElements[index2] = temp;

        OnStyleChanged();
    }

    protected virtual void OnStyleChanged()
    {
        StyleChanged?.Invoke(this, EventArgs.Empty);
    }
}


