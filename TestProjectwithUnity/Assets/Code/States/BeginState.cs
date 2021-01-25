using UnityEngine;
using Assets.Code.Interfaces;
using UnityEngine.SceneManagement;

namespace Assets.Code.States
{
    public class BeginState : IStateBase
    {
        private StateManager manager;
        
        //Constructor
        public BeginState(StateManager managerRef)
        {
            manager = managerRef;
            Debug.Log("Begin State has been constructed");
            
            if(SceneManager.GetActiveScene() != SceneManager.GetSceneAt(0))
            {
                SceneManager.LoadScene(0);
            }
        }
        public void StateUpdate()
        {
            
        }
        public void ShowIt()
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), manager.gamedata.BeginStateScr,ScaleMode.StretchToFill);
            GUI.Box(new Rect(50,50, 250, 60), "Press any key to continue");
            if(Input.anyKey)
            {
                Switch();
            }
        }

        private void Switch()
        {
            manager.SwitchState(new SetupState(manager));
        }
    }
}
