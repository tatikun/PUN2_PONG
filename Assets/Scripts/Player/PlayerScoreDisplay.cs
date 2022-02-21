using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private int id = 1;
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
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 0.1f){
            elapsedTime = 0f;
            UpadateLabel();
        }
    }

    private void UpadateLabel()
    {
        TMPro.text = PhotonNetwork.CurrentRoom.GetScore(id).ToString();
    }
}
