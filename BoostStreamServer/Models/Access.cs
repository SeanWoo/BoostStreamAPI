namespace BoostStreamServer.Models
{
    public enum Access : int
    {
        YTViewers = 1,
        YTLiker = 2,
        YTDisliker = 4,
        YTChat = 8,
        YTSubscribers = 16,
    }
}
