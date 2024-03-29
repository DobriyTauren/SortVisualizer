public class ArrayElement
{
    public int Text { get; set; }
    
    public string Height { get; set; }

    public string Color { get; set; }

    public string Width { get; set; }

    //public void ChangeStyle(string attribute, string value)
    //{
    //    int attributeIndex = _style.IndexOf(attribute);

    //    if (attributeIndex != -1)
    //    {
    //        _style = _style.Remove(attributeIndex, _style.IndexOf(";", attributeIndex) - attributeIndex);
    //    }
    //    else
    //    {
    //        Console.WriteLine($"attribute not found!\nattribute: {attribute}");
    //    }

    //    _style += $"{attribute}: {value};";
    //}

    //public void SetStyle(string value)
    //{
    //    _style = value;
    //}
}