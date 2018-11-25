using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OList<T> {
	
	private T[] array;

	public OList(){
		array = new T[0];
	}

	public OList(List<T> list){
		array = new T[list.Count];
		for (int i = 0; i < list.Count; i++) {
			array [i] = list [i];
		}
	}

	public List<T> ToList(){
		return array.ToList ();
	}

	public override string ToString ()
	{
		string output = "";
		for (int i = 0; i < array.Length; i++) {
			output += array [i].ToString () + ", ";
		}
		return output;
	}

	/// <summary>
	/// Add the specified newItem on the end of list.
	/// </summary>
	/// <param name="newItem">New item.</param>
	public void Add(T newItem){
		T[] tempArray = new T[array.Length + 1];
		for (int i = 0; i < array.Length; i++) {
			tempArray [i] = array [i];
		}
		tempArray [tempArray.Length - 1] = newItem;
		array = tempArray;
	}

	public void RemoveAt(int _position){
		T[] tempArray = new T[array.Length-1];
		int i = 0;
		int j = 0;
		while (i < array.Length) {
			if (i != _position) {
				tempArray [j] = array [i];
				j++;
			}
			i++;
		}
		array = tempArray;
	}

    public void Clear()
    {
        array = new T[0];
    }

	public void Remove(T obj){
		T[] tempArray = new T[array.Length-1];
		int i = 0;
		int j = 0;
		while (i < array.Length) {
			if (!array[i].Equals(obj)) {
				tempArray [j] = array [i];
				j++;
			}
			i++;
		}
		array = tempArray;
	}

	public void Insert(T newItem, int index){
		if (index < Count) {
			array [index] = newItem;
		}
	}

	public int IndexOf(T value){
		for (int i = 0; i < array.Length; i++) {
			if (array [i].Equals (value)) {
				return i;
			}
		}
		return -1;
	}

	public int Count{
		get{
			return array.Length;
		}
	}

	public T Get(int i){
		return array [i];
	}

	public T this[int index]{
		get{
			return Get (index);
		}
		set{
			Insert (value, index);
		}
	}

}
