using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float m_verticalMoveBorder = 4f; //上下方向への移動限界
    [SerializeField]
    private float m_speed = 6f; // 移動速度
    void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if(photonView.IsMine){
            var input = new Vector3(0,Input.GetAxis("Vertical"));
            transform.Translate(m_speed * Time.deltaTime * input.normalized);
            if(transform.position.y >= m_verticalMoveBorder){
                transform.position = new Vector3(transform.position.x,m_verticalMoveBorder);
            }
            else if (transform.position.y <= -m_verticalMoveBorder){
                transform.position = new Vector3(transform.position.x,-m_verticalMoveBorder);
            }
        }
    }
    
    private void LateUpdate() {
        PhotonNetwork.LocalPlayer.SendHashTable();
    }
}
