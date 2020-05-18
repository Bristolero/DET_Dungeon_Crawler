using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject tile;
    public GameObject wall;
    public GameObject corridorTile;
    private GameObject[,] positionFloor;
   

  public class Tree {
    public Tree left, right;
    public Rect rect;
    public Rect room = new Rect(-1,-1, 0, 0); // i.e null
    public int id;
    public List<Rect> connectors = new List<Rect>();
    
    private static int idCounter = 0;

    //Binärer Baum als Datenstruktur, welcher eine id, sowie eine Fläche besitzt
    public Tree(Rect rectang) {
      rect = rectang;
      id = idCounter;
      idCounter++;
    }

    public void CreateRoom() {
      if (left != null) {
        left.CreateRoom ();
      }
      if (right != null) {
        right.CreateRoom ();
      }

     
      //DELETE IF NOT WORKING
      if(right!=null && left !=null)
      {
        CreateConnectors(left, right);
	  }

      if (IsLeaf()) {
        int roomWidth = (int)Random.Range (rect.width / 2, rect.width - 2);
        int roomHeight = (int)Random.Range (rect.height / 2, rect.height - 2);
        int roomX = (int)Random.Range (1, rect.width - roomWidth - 1);
        int roomY = (int)Random.Range (1, rect.height - roomHeight - 1);

        //Erstellt Raum innerhalb der Fläche eines Knotens
        room = new Rect (rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
        Debug.Log ("Raum erstellt " + room + " in Fläche " + id + " " + rect);
      }
    }

    //Falls Knoten = Blatt
    public bool IsLeaf() {
      return left == null && right == null;
    }

    public Rect GetRoom() {
      if (IsLeaf()) {
        return room;
      }
      if (left != null) {
        Rect leftRoom = left.GetRoom ();
        if (leftRoom.x != -1) {
          return leftRoom;
        }
      }
      if (right != null) {
        Rect rightRoom = right.GetRoom ();
        if (rightRoom.x != -1) {
          return rightRoom;
        }
      }
       return new Rect (-1, -1, 0, 0);
      }
      

    public bool Split(int minRoomSize, int maxRoomSize) {
      if (!IsLeaf()) {
         return false;
      }

      //Je nachdem wie das Verhältnis von Länge zu Breite ist, wird die Fläche entweder horizontal
      //oder vertikal geteilt (Dies geschieht wenn z.B x*länge>breite ist)
      //falls das Verhältnis nahe quadratisch ist, wird zufällig geteilt
      bool splitHorizontal;
      if (rect.width / rect.height >= 1.25) {
        splitHorizontal = false;
      } else if (rect.height / rect.width >= 1.25) {
        splitHorizontal = true;
      } else {
        splitHorizontal = Random.Range (0.0f, 1.0f) > 0.5;
      }

      if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize) {
        Debug.Log ("Fläche " + id + " ist ein Blatt");
        return false;
      }

      if (splitHorizontal) {
        //Aufteilung unter Beachtung der minimalen Raumgröße
        int split = Random.Range (minRoomSize, (int)(rect.width - minRoomSize));

        left = new Tree (new Rect (rect.x, rect.y, rect.width, split));
        right = new Tree (
          new Rect (rect.x, rect.y + split, rect.width, rect.height - split));
      }
      else {
        int split = Random.Range (minRoomSize, (int)(rect.height - minRoomSize));

        left = new Tree (new Rect (rect.x, rect.y, split, rect.height));
        right = new Tree (
          new Rect (rect.x + split, rect.y, rect.width - split, rect.height));
      }

      return true;
    }



    public void CreateConnectors(Tree left, Tree right) {
      Rect leftRoom = left.GetRoom ();
      Rect rightRoom = right.GetRoom (); 

      Debug.Log("Creating corridor(s) between " + left.id + "(" + leftRoom + ") and " + right.id + " (" + rightRoom + ")");

      // Jeder Gang startet in einem zufälligen Punkt eines Raumes
      Vector2 leftPoint = new Vector2 ((int)Random.Range (leftRoom.x + 3, leftRoom.xMax - 3), (int)Random.Range (leftRoom.y + 3, leftRoom.yMax - 3));
      Vector2 rightPoint = new Vector2 ((int)Random.Range (rightRoom.x + 3, rightRoom.xMax - 3), (int)Random.Range (rightRoom.y + 3, rightRoom.yMax - 3));
      
      // Punkt "leftPoint" soll sich im Koordinatensystem links von "rightPoint" befinden
      if (leftPoint.x > rightPoint.x) {
        Vector2 temp = leftPoint;
        leftPoint = rightPoint;
        rightPoint = temp;
      }

      int w = (int)(leftPoint.x - rightPoint.x);
      int h = (int)(leftPoint.y - rightPoint.y);

      Debug.Log ("leftPoint: " + leftPoint + ", rightPoint: " + rightPoint + ", w: " + w + ", h: " + h);

      // falls die Punkte horizontal nicht auf einer Linie liegen
      if (w != 0) {
        // Zufällig auswählen, ob erst vertikal und dann horizontal den Gang erstellen oder anders herum 
        if (Random.Range (0, 1) > 2) {
          // Füge einen weiteren Gang nach rechts ein
          connectors.Add (new Rect (leftPoint.x, leftPoint.y, Mathf.Abs (w) + 1, 1));

          // Befindet sich der linke Punkt unter dem Rechten, dann geht der Weg nach oben, ansonsten nach unten
          if (h < 0) {
            connectors.Add (new Rect (rightPoint.x, leftPoint.y, 1, Mathf.Abs (h)));
          } else {
            connectors.Add (new Rect (rightPoint.x, leftPoint.y, 1, -Mathf.Abs (h)));
          }
        } else {
          // Gehe nach oben oder unten
          if (h < 0) {
            connectors.Add (new Rect (leftPoint.x, leftPoint.y, 1, Mathf.Abs (h)));
          } else {
            connectors.Add (new Rect (leftPoint.x, rightPoint.y, 1, Mathf.Abs (h)));
          }

          // dann rechts
          connectors.Add (new Rect (leftPoint.x, rightPoint.y, Mathf.Abs (w) + 1, 1));
        }
      } else {       
        //Befinden sich die Punkte horizontal auf einer Linie, dann gehe hoch oder runter
        if (h < 0) {
          connectors.Add (new Rect ((int)leftPoint.x, (int)leftPoint.y, 1, Mathf.Abs (h)));
        } else {
          connectors.Add (new Rect ((int)rightPoint.x, (int)rightPoint.y, 1, Mathf.Abs (h)));
        }
      }

      Debug.Log ("Corridors: ");
      foreach (Rect corridor in connectors) {
        Debug.Log ("corridor: " + corridor);
      }
    }

  }

  public void CreateTree(Tree tree) {
    Debug.Log ("Teile Fläche " + tree.id + ": " + tree.rect);
    if (tree.IsLeaf()) {
      // Falls die Fläche zu groß ist (Maximale Raumgröße überschritten)
      if (tree.rect.width > maxRoomSize
        || tree.rect.height > maxRoomSize
        || Random.Range(0.0f,1.0f) > 0.25) {

        if (tree.Split (minRoomSize, maxRoomSize)) {
          Debug.Log ("Fläche geteilt " + tree.id + " in "
            + tree.left.id + ": " + tree.left.rect + ", "
            + tree.right.id + ": " + tree.right.rect);

          CreateTree(tree.left);
          CreateTree(tree.right);
        }
      }
    }
  }

  //Zeichnet aus gegebenen GameObjects den Floor
  public void drawRoom(Tree tree)
  {
    if(tree==null)
    {
      return;
	}
    if(tree.IsLeaf()) {
     for (int i = (int) tree.room.x; i < tree.room.xMax; i++ ) {
      for (int j = (int) tree.room.y; j < tree.room.yMax; j++ ) {
       GameObject newTile = Instantiate (tile, new Vector3 (i,j,0f), Quaternion.identity) as GameObject;
       newTile.transform.SetParent(transform);
       positionFloor[i,j] = newTile;
	  }
	 }
	}
    else {
     drawRoom(tree.left);
     drawRoom(tree.right);
	}
  }
   //BAUSTELLE: JEWEILS ANDERE SPRITES VERWENDEN 

  public void drawWall(Tree tree)
  {
    if(tree==null)
    {
      return;
	}
    //Wände für jeden Raum
    if(tree.IsLeaf()) {
     for (int i = (int) tree.room.x - 1; i < tree.room.xMax + 1; i++ ) {
       int j = (int) tree.room.y - 1;
       Vector3 toAdd = new Vector3(i,j,0f);
       if(isObjectHere(toAdd)) {
         GameObject newWall = Instantiate (wall, toAdd, Quaternion.identity) as GameObject;
         newWall.transform.SetParent(transform);
         positionFloor[i,j] = newWall;      
         }
	 }
     for (int i = (int) tree.room.x - 1; i < tree.room.xMax + 1; i++ ) {
       int j = (int) tree.room.yMax;
       Vector3 toAdd = new Vector3(i,j,0f);
       if(isObjectHere(toAdd)) {
         GameObject newWall = Instantiate (wall, toAdd, Quaternion.identity) as GameObject;
         newWall.transform.SetParent(transform);
         positionFloor[i,j] = newWall;      
         }      
	 }
     for (int j = (int) tree.room.y - 1; j < tree.room.yMax + 1; j++ ) {
       int i = (int) tree.room.xMax;
       Vector3 toAdd = new Vector3(i,j,0f);
       if(isObjectHere(toAdd)) {
         GameObject newWall = Instantiate (wall, toAdd, Quaternion.identity) as GameObject;
         newWall.transform.SetParent(transform);
         positionFloor[i,j] = newWall;      
         }      
	 }
     for (int j = (int) tree.room.y - 1; j < tree.room.yMax + 1; j++ ) {
       int i = (int) tree.room.x - 1;
       Vector3 toAdd = new Vector3(i,j,0f);
       if(isObjectHere(toAdd)) {
         GameObject newWall = Instantiate (wall, toAdd, Quaternion.identity) as GameObject;
         newWall.transform.SetParent(transform);
         positionFloor[i,j] = newWall;      
         }     
	 }
      
	}
    else {
     drawWall(tree.left);
     drawWall(tree.right);
	}
  }

  void DrawConnectors(Tree tree) {
    if (tree == null) {
      return;
    }

    DrawConnectors (tree.left);
    DrawConnectors (tree.right);

    foreach (Rect connector in tree.connectors) {
      for (int i = (int)connector.x; i < connector.xMax; i++) {
        for (int j = (int)connector.y; j < connector.yMax; j++) {
          if (positionFloor[i,j] == null) {
            GameObject instance = Instantiate (corridorTile, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (transform);
            positionFloor [i, j] = instance;
          }
        }
      }
    }
    }

    void DrawConnectorWall(Tree tree) {
    if(tree==null) {       
      return;
    }
    DrawConnectorWall(tree.left);
    DrawConnectorWall(tree.right);
    
    foreach (Rect connector in tree.connectors) {     
      //Horizontale Wand links
      for (int i = (int)connector.x - 1 ; i < connector.xMax + 1; i++) {
          int j = (int) connector.y - 1;
          if (positionFloor[i,j] == null) {
            GameObject instance = Instantiate (wall, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (transform);
            positionFloor [i, j] = instance;
          }
      }
      //Horizontale Wand rechts
      for (int i = (int)connector.x - 1 ; i < connector.xMax + 1; i++) {
          int j = (int) connector.yMax;
          if (positionFloor[i,j] == null) {
            GameObject instance = Instantiate (wall, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (transform);
            positionFloor [i, j] = instance;
          }
      }
      //Vertikale Wand rechts
      for (int j = (int)connector.y - 1; j < connector.yMax + 1; j++) {
          int i =  (int) connector.xMax;
          if (positionFloor[i,j] == null) {
            GameObject instance = Instantiate (wall, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (transform);
            positionFloor [i, j] = instance;
          }
      }
      //Vertikale Wand links
      for (int j = (int)connector.y - 1; j < connector.yMax + 1; j++) {
          int i =  (int) connector.x - 1;
          if (positionFloor[i,j] == null) {
            GameObject instance = Instantiate (wall, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (transform);
            positionFloor [i, j] = instance;
          }
      }
      
    }

  }


   public bool isObjectHere(Vector3 targetPos)
    {
        GameObject[] allThings = GameObject.FindGameObjectsWithTag("Floor");
        foreach(GameObject current in allThings)
     {
           if(current.transform.position == targetPos)
           return false;
     }
     return true;
}

  //init
  void Start () {
    Debug.Log( boardColumns );
    Tree root = new Tree (new Rect (0, 0, boardRows, boardColumns));
    CreateTree (root);
    root.CreateRoom();
    positionFloor = new GameObject[boardRows, boardColumns];     
    drawRoom(root);
    DrawConnectors(root);
    drawWall(root);
    DrawConnectorWall(root);
    
    }
        
  }


  