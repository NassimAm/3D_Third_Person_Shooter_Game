using UnityEngine;

public class GameController : MonoBehaviour
{
    bool paused;

    public virtual void StartGame()
    {

    }

    public virtual void SpawnPlayer()
    {

    }

    public virtual void Respawn()
    {

    }

    public bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }
}
