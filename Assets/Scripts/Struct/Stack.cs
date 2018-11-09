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

	public T Last(){
		return stack [stack.Count-1];
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

	public override string ToString ()
	{
		string output = "";
		for (int i = 0; i < stack.Count; i++) {
			output += stack [i].ToString () + ", ";
		}
		return output;
	}

}
