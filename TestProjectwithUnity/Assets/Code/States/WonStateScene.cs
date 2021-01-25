using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Code.States
{
    public class WonStateScene : IStateBase
    {
        private StateManager manager;
        //Constructor
        public WonStateScene(StateManager managerRef)
        {
            manager = managerRef;
            Debug.Log("WonState has been constructed");
        }
        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(2);
            }
        }
        public void ShowIt()
        {

        }
    }
}
