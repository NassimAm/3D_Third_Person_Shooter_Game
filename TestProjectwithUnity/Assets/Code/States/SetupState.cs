using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Code.States
{
    public class SetupState : IStateBase
    {
        private StateManager manager;
        private PlayerController controller;
        private GameObject player;
        //Constructor
        public SetupState(StateManager managerRef)
        {
            manager = managerRef;
            Debug.Log("SetupState has been constructed");
            player = GameObject.Find("Player");
            //controller = player.GetComponent<PlayerController>();
        }
        public void StateUpdate()
        {
            /*if (Input.GetKey(KeyCode.Space))
            {
                manager.SwitchState(new PlayStateScene(manager));
            }*/
        }
        public void ShowIt()
        {
            
        }
    }
}