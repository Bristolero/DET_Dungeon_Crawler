using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree 
{
    
    public abstract class Node {
        
	}
    
    public enum NodeStatus {
        RUNNING,
        SUCCESS,
        FAILURE
	}
    
    //Composite hat eine Liste von Children, die alle ausgeführt werden solange dies passiert ist der Status RUNNING
    public class Composite : Node {

        public List<Node> children = new List<Node>();
        public string name;

        public Composite(string n, List<Node> childNodes)
        {
            name = n;
            children.AddRange(childNodes);
		}


	}

    //Keine Children, returning SUCCESS oder FAILURE, Aktionen finden hier statt
    public class Leaf : Node {
    
	}

    //Decorator hat nur einen child, 
    public class Decorator : Node {
        
        public Node child;

        public Decorator(Node node)
        {
            child = node;
		}
	}

}
