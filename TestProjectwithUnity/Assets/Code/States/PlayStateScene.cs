using UnityEngine;
using Assets.Code.Interfaces;

namespace Assets.Code.States
{
    public class PlayStateScene : IStateBase
    {
        private StateManager manager;
        //Constructor
        public PlayStateScene(StateManager managerRef)
        {
            manager = managerRef;
            Debug.Log("PlayState has been constructed");
        }
        public void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                manager.SwitchState(new WonStateScene(manager));
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                manager.SwitchState(new LostStateScene(manager));
            }
        }
        public void ShowIt()
        {

        }
    }
}
