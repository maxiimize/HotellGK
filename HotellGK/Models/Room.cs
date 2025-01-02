public class Room
{
    public int RoomId { get; set; }
    public string RoomType { get; set; } 
    public bool HasExtraBeds { get; set; }
    public int MaxExtraBeds { get; set; } 
    public string RoomSize { get; set; } 
    public bool IsAvailable { get; set; }
}
