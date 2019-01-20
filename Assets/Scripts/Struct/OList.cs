using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class OList<T> {
	
	private List<T> array;

	public OList(){
        array = new List<T>();
	}

	public OList(List<T> list){
        array = new List<T>();
        for (int i = 0; i < list.Count; i++) {
			Add(list [i]);
		}
	}

    //Dunno if copy or same reference!
	public List<T> ToList(){
		return array.ToList ();
	}

	public override string ToString ()
	{
		string output = "";
		for (int i = 0; i < array.Count; i++) {
			output += array [i].ToString () + ", ";
		}
		return output;
	}

    public OList<T> Copy()
    {
        OList<T> copy = new OList<T>();
        for(int i=0; i<Count; i++)
        {
            copy.Add(array[i]);
        }
        return copy;
    }

	/// <summary>
	/// Add the specified newItem on the end of list.
	/// </summary>
	/// <param name="newItem">New item.</param>
	public void Add(T newItem){
        array.Add(newItem);
	}

	public void RemoveAt(int _position){
        array.RemoveAt(_position);
	}

    public void Clear()
    {
        array = new List<T>();
    }

	public void Remove(T obj){
        array.Remove(obj);
	}

	public void Insert(T newItem, int index){
		if (index < Count) {
			array [index] = newItem;
		}
	}

	public int IndexOf(T value){
        return array.IndexOf(value);
	}

	public int Count{
		get{
			return array.Count;
		}
	}

    #region Get Item

    public T Get(int i){
		return array [i];
	}

    public T Get(string _objectName)
    {
        for(int i=0; i<Count; i++)
        {
            if(array[i].ToString() == _objectName)
            {
                return array[i];
            }
        }
        return default(T);
    }

	public T this[int index]{
		get{
			return Get (index);
		}
		set{
			Insert (value, index);
		}
	}

    #endregion

    #region Contains

    public bool Contains(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    public bool Contains(string _objectName)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].ToString() == _objectName)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

}
