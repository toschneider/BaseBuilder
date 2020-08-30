using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	public GameObject Target;
	public bool Free;
	public Job TaskJob;



	int Work;

	public Task(GameObject target, bool free, Job taskJob, int work)
	{
		Target = target;
		Free = free;
		TaskJob = taskJob;
		Work = work;
	}

	public bool workOnTask(int value)
	{
		Work -= value;
		if(Work <= 0)
		{
			return true;
		}
		return false;
	}
}
public enum Job
{
	Build,
	Mine
}
