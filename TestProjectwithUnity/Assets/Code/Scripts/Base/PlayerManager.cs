using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool didInit;

    public UserManager DataManager;

    public virtual void Awake()
    {
        didInit = false;
        Init();
    }

    public virtual void Init()
    {
        DataManager = gameObject.GetComponent<UserManager>();
        if (DataManager == null)
            DataManager = gameObject.AddComponent<UserManager>();

        didInit = true;
    }

    public virtual void GameFinished()
    {
        DataManager.SetIsFinished(true);
    }

    public virtual void GameStart()
    {
        DataManager.SetIsFinished(false);
    }
}
