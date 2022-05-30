using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    private static PhotonManager _instance;
    public static PhotonManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PhotonManager>();
            }
            return _instance;
        }
    }
    #endregion

    public static int gameMode = 0;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    [PunRPC]
    public void ChangeScene(string scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }
}
