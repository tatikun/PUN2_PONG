using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private int actorNumber = 1;
    [SerializeField]
    private TextMeshPro TMPro = default;
    private float elapsedTime;
    private void Start()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // ルームに参加していない場合には更新処理を行わない
        if(!PhotonNetwork.InRoom){return;}

        elapsedTime += Time.deltaTime;
        if(elapsedTime > 0.1f){
            elapsedTime = 0f;
            UpadateLabel();
        }
    }

    private void UpadateLabel()
    {
        var players = PhotonNetwork.PlayerList;
        foreach(var player in players){
            if(player.ActorNumber == actorNumber)
            {
                TMPro.text = player.GetLostPoint().ToString();
            }
        }
    }
}
