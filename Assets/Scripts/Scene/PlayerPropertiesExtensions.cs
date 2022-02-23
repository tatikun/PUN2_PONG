using ExitGames.Client.Photon;
using Photon.Realtime;

public static class PlayerPropertiesExtensions
{
    private const string LostPointKey = "p";

    private static readonly Hashtable propsToSet = new Hashtable();

    // プレイヤーのスコアを取得する
    public static int GetLostPoint(this Player player) {
        return (player.CustomProperties[LostPointKey] is int score) ? score : 0;
    }

    // プレイヤーのスコアを加算する
    public static void AddLostPoint(this Player player, int value) {
        propsToSet[LostPointKey] = player.GetLostPoint() + value;
    }
    
    public static void SendHashTable(this Player player)
    {
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    
}