using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState", order = 1)]
public class GameState : ScriptableObject
{
    public int LevelsPassed = 0;
    public int CurrentLevelIndex = 0;
    public int Money;
    public int CoolerLevel;
    public int StorageLevel;
    public int EngineLevel;

}
