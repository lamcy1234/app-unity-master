using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class UEWorkScheduler : MonoBehaviour
{
    Dictionary<string, List<IEnumerator>> queues = new Dictionary<string, List<IEnumerator>> ();

    public void ScheduleWorkItem (string name, IEnumerator task)
    {
        var exists = queues.ContainsKey (name);
        if (!exists) {
            queues [name] = new List<IEnumerator> ();
            StartCoroutine(ProcessQueue(name));
        }
        queues [name].Add (task);
    }
	
	public void ScheduleWorkItemWithoutStarting(string name, IEnumerator task) {
        var exists = queues.ContainsKey (name);
        if (!exists) {
            queues [name] = new List<IEnumerator> ();
            //StartCoroutine(ProcessQueue(name));
        }
        queues [name].Add (task);
    }
	
	public void StartProcessingQueue(string name, Action onComplete) {
		StartCoroutine(ProcessQueue(name), onComplete);
	}

    IEnumerator ProcessQueue (string name)
    {
        var tasks = queues [name];
        while (true) {
            yield return null;
            if (tasks.Count > 0) {
                var task = tasks.Pop (0);
                while (task.MoveNext())
                    yield return task.Current;
            }
            if(tasks.Count == 0) {
                queues.Remove(name);
                yield break;
            }
        }
    }
	
	IEnumerator ProcessQueue (string name, Action onComplete)
    {
        var tasks = queues [name];
        while (true) {
            yield return null;
            if (tasks.Count > 0) {
                var task = tasks.Pop (0);
                while (task.MoveNext())
                    yield return task.Current;
            }
            if(tasks.Count == 0) {
                queues.Remove(name);
                yield break;
            }
        }
		
		onComplete();
    }


}

