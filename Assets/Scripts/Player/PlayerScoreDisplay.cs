using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private int m_actorNumber = 1; // どのプレイヤーの失点を表示するか
    [SerializeField]
    private TextMeshPro m_TMPro = default;
    private float m_elapsedTime;
    private void Start()
    {
        m_elapsedTime = 0f;
    }

    private void Update()
    {
        // ルームに参加していない場合には更新処理を行わない
        if(!PhotonNetwork.InRoom){return;}

        m_elapsedTime += Time.deltaTime;
        if(m_elapsedTime > 0.1f){
            m_elapsedTime = 0f;
            UpadateLabel();
        }
    }

    private void UpadateLabel()
    {
        var players = PhotonNetwork.PlayerList;
        foreach(var player in players){
            if(player.ActorNumber == m_actorNumber)
            {
                m_TMPro.text = player.GetLostPoint().ToString();
            }
        }
    }
}
