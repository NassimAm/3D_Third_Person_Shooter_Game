using UnityEngine;

public class UserManager : MonoBehaviour
{
    private int score;
    private int highScore;
    private int level;
    private int health;
    private bool isFinished;

    public string playerName = "Nassim";

    public virtual void GetDefaultData()
    {
        playerName = "Nassim";
        score = 0;
        level = 1;
        health = 3;
        highScore = 0;
        isFinished = false;
    }

    public string GetName()
    {
        return playerName;
    }
    public void SetName(string aName)
    {
        playerName = aName;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int num)
    {
        level = num;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetScore()
    {
        return score;
    }

    public virtual void AddScore(int anAmount)
    {
        score += anAmount;
    }

    public virtual void LostScore(int anAmount)
    {
        score -= anAmount;
    }

    public void setScore(int num)
    {
        score = num;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddHealth(int anAmount)
    {
        health += anAmount;
    }

    public virtual void LostHealth(int anAmount)
    {
        health -= anAmount;
    }

    public void SetHealth(int num)
    {
        health = num;
    }

    public bool GetIsFinished()
    {
        return isFinished;
    }

    public void SetIsFinished(bool aVal)
    {
        isFinished = aVal;
    }
}
