﻿public class HistoryModel
{
    public int Id { get; set; }

    public required string UserId { get; set; }
    public int? AlgorithmId { get; set; }
    public required VisualizationType VisualizationType { get; set; }
    public required int ArrayAccessCount { get; set; }
    public required int MoveCount { get; set; }
    public required string TimeWasted { get; set; } 
    public required int Delay {  get; set; }
    public required int ElementsCount { get; set; }
}