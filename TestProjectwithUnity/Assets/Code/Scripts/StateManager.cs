using UnityEngine;
using Assets.Code.States;
using Assets.Code.Interfaces;

public class StateManager : MonoBehaviour
{
    private IStateBase activeState;
    private static StateManager stateReference;

    [HideInInspector]
    public GameData gamedata;

    void Awake()
    {
        if(stateReference == null)
        {
            stateReference = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        activeState = new BeginState(this);
        gamedata = this.GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
            activeState.StateUpdate();
    }

    void OnGUI()
    {
        if (activeState != null)
            activeState.ShowIt();
    }

    public void SwitchState(IStateBase newState)
    {
        activeState = newState;
    }
}
