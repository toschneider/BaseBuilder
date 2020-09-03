using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Colonist : Pawn
{
	public GameObject Scheduler;
	public GameObject Head;
	public GameObject Body;
	public GameObject Hair;
	public GameObject WeaponSlot;
	MyScheduler<List<int>, Job> schedule;
	public MyScheduler<List<int>, Job> Schedule
	{
		get
		{
			return schedule;
		}
		set
		{
			schedule = value;
		}
	}
	void Start()
	{
		Scheduler = GameObject.Find("Scheduler");
		Schedule = new MyScheduler<List<int>, Job>();
		for (int i = 0; i < Schedule.length; i++)
		{
			foreach(var sc in Enum.GetValues(typeof(Job)).Cast<Job>().ToList())
			{
				Schedule[sc][0] = 3;
			}
		}
	}

	public void SetSchedule(Job job, int priority)
	{
		Schedule[job][0] = priority;
	}


}
