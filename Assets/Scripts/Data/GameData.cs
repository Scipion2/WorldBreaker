using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    
	public List<string> Players=new List<string>();
	public List<int> PlayerScores=new List<int>();
	public List<int> PlayerStages=new List<int>();

}
