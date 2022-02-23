using Photon.Pun;
using TMPro;

public class PlayerNameDisplay : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // どちらが自身かを表示
        if(photonView.IsMine)
        {
            nameLabel.text = "You\n▼";
        }
        else
        {
            nameLabel.text = "";
        }
    }
}
