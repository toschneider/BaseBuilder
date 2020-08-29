using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
	public string Name { get; set; }
	public StatPassion Passion { get; set; }
	public int Level { get; set; }
	public int CurrentLevelProgress { get; set; }
	public int MaxLevelProgress { get; set; }

	public Stat(string name, StatPassion passion, int level)
	{
		this.Name = name;
		this.Passion = passion;
		this.Level = level;
		this.CurrentLevelProgress = 0;
		this.MaxLevelProgress = (this.Level + 1) * 1000;
	}

	public void Action()
	{

	}

	void Progress()
	{
		if(Level >= 20)
		{
			return;
		}
		//Todo how to get exp value
		int expValue = 0;

		switch (Passion)
		{
			case StatPassion.None:
				expValue = (int)(expValue * 0.35);
				break;
			case StatPassion.Interested:
				break;
			case StatPassion.Burning:
				expValue = (int)(expValue * 1.5);
				break;
			default:
				break;
		}
		CurrentLevelProgress += expValue;
		if(CurrentLevelProgress >= MaxLevelProgress)
		{
			Level += 1;
			int tmpLevelProgressDiff = CurrentLevelProgress - MaxLevelProgress;
			if(Level >= 20)
			{
				CurrentLevelProgress = 0;
				MaxLevelProgress = 0;
				return;
			}
			MaxLevelProgress = (this.Level + 1) * 1000;
			CurrentLevelProgress = tmpLevelProgressDiff;

		}
	}



	public enum StatPassion
	{
		None,
		Interested,
		Burning
	}
}
