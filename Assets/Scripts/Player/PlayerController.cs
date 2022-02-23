using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float verticalMoveBorder = 0f;
    void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if(photonView.IsMine){
            var input = new Vector3(0,Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);
            if(transform.position.y >= verticalMoveBorder){
                transform.position = new Vector3(transform.position.x,verticalMoveBorder);
            }
            else if (transform.position.y <= -verticalMoveBorder){
                transform.position = new Vector3(transform.position.x,-verticalMoveBorder);
            }
        }
    }
    
    private void LateUpdate() {
        PhotonNetwork.LocalPlayer.SendHashTable();
    }
}
