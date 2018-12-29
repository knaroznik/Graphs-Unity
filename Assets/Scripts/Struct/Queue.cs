using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue<T> {

    private OList<T> queue;
    private int queueStart;

    public Queue()
    {
        queue = new OList<T>();
        queueStart = 0;
    }

    /// <summary>
    /// Adds new item on the end of the queue.
    /// </summary>
    public void Add(T newItem)
    {
        queue.Add(newItem);
    }

    public bool Contains(T item)
    {
        for(int i=0; i<queue.Count; i++)
        {
            if (queue[i].Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes first item in the queue
    /// </summary>
    public void Remove()
    {
        queueStart++;
    }

    public T GetFirstItem()
    {
        return queue[queueStart];
    }

    public OList<T> GetQueue()
    {
        return queue;
    }

    public bool isEmpty()
    {
        if(queueStart >= queue.Count-1)
        {
            return true;
        }
        return false;
    }
}
