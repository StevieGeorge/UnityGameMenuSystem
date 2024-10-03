using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    RoundManager holds the Round and updates its game logic. 
    How often that happens is determined: 
    A real-time game updating every frame:  stepTime = 0f
    Tetris, updating every X seconds:       stepTime = X
    Chess, after the player has moved:      timeBasedRoundSteps = false
    The Round class is defined after RoundManager in this script because Unity was getting hung up on it not being a MonoBehavior

    The round manager thus controls whether or not the game is paused
    Paused in this sense means that time will not affect the game and neither will player input
        Thus "game over' pauses the game.
*/

public class RoundManager : MonoBehaviour
{
    
    public GameManager game;
    public Round round = new Round();
 //   public StageManager stageManager;

    public float roundTime = 0f;

    public bool timeBasedRoundSteps = true;
    public float stepTime  = 1f;
    float stepping = 0f;
    public bool paused = false;

    public Stage stage { 
        get { return round.stage; } 
        set { round.stage = value; } 
    }


    void Update()
    {
        
        if (!paused)
        {
            roundUpdate();
        }
    }

    public void newRound()
    {
        newStage();
        paused = false;
    }
    void newStage()
    {
        round.stage = new Stage();
    }

    public void pause()
    {
        paused = true;
    }
    public void unpause()
    {
        paused = true;
    }
    void roundUpdate()
    {
        float frameTime = Time.deltaTime; // copying deltaTime now because it changes with every script instruction
        round.roundTime += frameTime;
        if (timeBasedRoundSteps)
        {
            stepping += frameTime;
            if (stepping > stepTime)
            {
                stepping = 0f;
                round.Update();
            }
        }
    }

    public void roundOver()
    {
        paused = true;
        game.roundOver();
    }
}


/* 
    Round is an actively running game round, containing all variables of the game state
    Everything needed for saving and loading should live in the Round.
    Its void Update advances the game logic one frame.

    The Round contains the Campaign, which is global progress within an extended presentation of the game. 
    It may seem reversed for Round to hold Campaign, however:
    the player directly interacts with the Round, and only indirectly the Campaign
    When a Round is loaded and presented, the campaign information is part of it,
    however presenting campaign progress does not necessarily need to contain a Round.

    The Round contains the Stage, which is the changing positioning of in-game elements
    
*/
public class Round
{
    //placeholder score
    public int score = 0;
    public Stage stage;
    public Campaign campaign;
    public float roundTime = 0f;
    public void Update()
    {

    }
}

/*
    The Campaign is global progress within an extended presentation of the game. 
    It can be progress within a story,
    It can be statistics such as wins and losses,
    Or user data
*/
public class Campaign
{
    //placeholder values to start with, such as this high Score value 
    public int highScore = 0;
    public int stagesCompleted = 0;

    // a very crude list 
    public List<Stage> stages = new List<Stage>();

    //nextStage brings up the stage at the current state of the campaign; it does not advance the progess
    public Stage nextStage()
    {
        //If stage progess is beyond our list of stages
        if (stagesCompleted >= stages.Count)
        {
            return new Stage();
        }
        else
        {
            return stages[stagesCompleted];
        }
    }
}
