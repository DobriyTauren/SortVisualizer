using SortVisualizer.Client.Classes.SortElements;
using System.Drawing;

public class SortService
{
    public event EventHandler StyleChanged;

    public string BaseColor { get; private set; } = "#297373";
    public string ActionColor { get; private set; } = "red";

    public async Task ArrayCheck(List<SvgLine> arrayElements, SortAlgorithm algorithm)
    {
        arrayElements[0].Color = "green";

        for (int i = 0; i < arrayElements.Count - 1; i++)
        {
            if (arrayElements[i].Value <= arrayElements[i + 1].Value)
            {
                arrayElements[i + 1].Color = "green";
            }

            await Task.Delay(algorithm.Delay / 2);
            OnStyleChanged();
        }
    }

    public async Task WaitColor<T>(int delay, T elem) where T : ISvgElement
    {
        if (elem is SvgLine svgLine)
        {
            svgLine.Color = ActionColor;

            OnStyleChanged();
            await Task.Delay(delay);

            svgLine.Color = BaseColor;
        }
        else
        {
            await Wait(delay);
        }
    }

    public async Task Wait(int delay)
    {
        OnStyleChanged();
        await Task.Delay(delay);
    }

    public void OnStyleChanged()
    {
        StyleChanged?.Invoke(null, EventArgs.Empty);
    }
}


