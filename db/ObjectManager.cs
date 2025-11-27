[Serializable]
public class ObjectManager
{
    public List<HistoryModel> History { get; set; } = new();
    public int CurrentMaxId { get; set; }
}