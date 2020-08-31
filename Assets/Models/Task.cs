using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	public GameObject Target;
	public bool Free;
	public Job TaskJob;
	Action action;



	int Work;

	public Task(GameObject target, bool free, Job taskJob, int work, Action action)
	{
		Target = target;
		Free = free;
		TaskJob = taskJob;
		Work = work;
		this.action = action;
	}

	public bool workOnTask(int value)
	{
		Work -= value;
		//Todo Action
		//action(value);
		if(Work <= 0)
		{

			return true;
		}
		return false;
	}
}
public enum Job
{
	Firefight,
	Patient,
	Doctor,
	Bedrest,
	HaulPlus,
	General,
	Warden,
	Handle,
	Entertain,
	Cook,
	Hunt,
	Construct,
	Grow,
	Mine,
	PlantCut,
	Smith,
	Tailor,
	Art,
	Craft,
	Haul,
	Clean,
	Research,
	Management,
	Teach
}
