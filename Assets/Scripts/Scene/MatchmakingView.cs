using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField m_roomidInputField = default;
    [SerializeField]
    private Button m_joinRoomButton = default;
    private CanvasGroup m_canvasGroup;
    
    void Start()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        // マスターサーバーに接続するまでは入力できないようにする
        m_canvasGroup.interactable = false;

        // ルームIDを入力する前派、ルーム参加ボタンを押せないようにする
        m_joinRoomButton.interactable = false;

        m_roomidInputField.onValueChanged.AddListener(OnRoomidInputFieldValueChanged);
        m_joinRoomButton.onClick.AddListener(OnJoinRoomButtonClick);
    }

    public override void OnConnectedToMaster()
    {
        // マスターサーバーに接続したら、入力できるようにする
        m_canvasGroup.interactable = true;
    }

    private void OnRoomidInputFieldValueChanged(string value)
    {
        // ルームIDを4桁入力したときのみ、ルーム参加ボタンを推せるようにする
        m_joinRoomButton.interactable = (value.Length == 4);
    }

    private void OnJoinRoomButtonClick(){
        // ルーム参加処理中は、入力できないようにする
        m_canvasGroup.interactable = false;

        // ルームを非公開に設定する (新規でルームを作成する場合)
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;

        // ルームIDと同じ名前のルームに参加する (存在しなければ作成してから参加する)
        PhotonNetwork.JoinOrCreateRoom(m_roomidInputField.text, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // ルームへの参加が成功したらUIを非表示にする
        gameObject.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode,string message){
        // ルームへの参加が失敗したら、再びルームIDを入力できるようにする
        m_roomidInputField.text = string.Empty;
        m_canvasGroup.interactable = true;
    }
}
