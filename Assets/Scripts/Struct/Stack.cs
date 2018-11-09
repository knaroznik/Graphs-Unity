using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack<T> {

	private OList<T> stack;

	public Stack(){
		stack = new OList<T>();
	}

	public T Pop(){
		T output = stack [stack.Count-1];
		stack.RemoveAt (stack.Count - 1);
		return output;
	}

	public void Push(T _newItem){
		stack.Add (_newItem);
	}

	public bool IsEmpty(){
		if (stack.Count == 0) {
			return true;
		}
		return false;
	}

}
