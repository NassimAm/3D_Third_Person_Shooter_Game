using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Code.States
{
    public class LostStateScene : IStateBase
    {
        private StateManager manager;
        //Constructor
        public LostStateScene(StateManager managerRef)
        {
            manager = managerRef;
            Debug.Log("LostState1 has been constructed");
        }
        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
                manager.SwitchState(new BeginState(manager));
            }
        }
        public void ShowIt()
        {

        }
    }
}

