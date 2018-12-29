using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area<T> {

    private OList<T> area;

    public Area()
    {
        area = new OList<T>();
    }

    public void Add(T newItem)
    {
        area.Add(newItem);
    }

    public Area(OList<T> _area) : this()
    {
        for(int i=0; i<_area.Count; i++)
        {
            Add(_area[i]);
        }
    }

    public void CreateArea(OList<T> allElements, Queue<T> excudeElements)
    {
        for(int i=0; i<allElements.Count; i++)
        {
            if (!excudeElements.Contains(allElements[i]))
            {
                Add(allElements[i]);
            }
        }
    }

    public override string ToString()
    {
        string output = "";
        for(int i=0; i<area.Count; i++)
        {
            output += area[i].ToString() + " ";
        }
        return output;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < area.Count; i++)
        {
            if (area[i].Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    public int Size
    {
        get
        {
            return area.Count;
        }
    }

    public T Get(int i)
    {
        return area[i];
    }

    public T this[int index]
    {
        get
        {
            return Get(index);
        }
        set
        {
            
        }
    }
}
