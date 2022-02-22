using ExitGames.Client.Photon;
using Photon.Realtime;

public static class GameRoomProperty 
{
    private const string ScoreKey = "s";
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int GetScore(this Room room,int id){
        return (room.CustomProperties[ScoreKey + id.ToString()] is int score) ? score : 0;
    }

    public static void SetScore(this Room room,int id,int score){
        propsToSet[ScoreKey + id.ToString()] = score;
    }

    public static void AddScore(this Room room,int id,int value){
        propsToSet[ScoreKey + id.ToString()] = room.GetScore(id) + value;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }

    public static void SendRoomProperties(this Room room){
        if(propsToSet.Count > 0){
            room.SetCustomProperties(propsToSet);
            propsToSet.Clear();
        }
    }
}
