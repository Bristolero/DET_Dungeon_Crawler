using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject tile;
    public GameObject wall;
    private GameObject[,] positionFloor;
   

  public class Tree {
    public Tree left, right;
    public Rect rect;
    public Rect room = new Rect(-1,-1, 0, 0); // i.e null
    public int id;

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
       GameObject newWall = Instantiate (wall, new Vector3 (i,j,0f), Quaternion.identity) as GameObject;
       newWall.transform.SetParent(transform);
       positionFloor[i,j] = newWall;      
	  }
     for (int i = (int) tree.room.x - 1; i < tree.room.xMax + 1; i++ ) {
      int j = (int) tree.room.yMax;
       GameObject newWall = Instantiate (wall, new Vector3 (i,j,0f), Quaternion.identity) as GameObject;
       newWall.transform.SetParent(transform);
       positionFloor[i,j] = newWall;      
	 }
     for (int j = (int) tree.room.y - 1; j < tree.room.yMax + 1; j++ ) {
      int i = (int) tree.room.xMax;
       GameObject newWall = Instantiate (wall, new Vector3 (i,j,0f), Quaternion.identity) as GameObject;
       newWall.transform.SetParent(transform);
       positionFloor[i,j] = newWall;      
	 }
     for (int j = (int) tree.room.y - 1; j < tree.room.yMax + 1; j++ ) {
      int i = (int) tree.room.x - 1;
       GameObject newWall = Instantiate (wall, new Vector3 (i,j,0f), Quaternion.identity) as GameObject;
       newWall.transform.SetParent(transform);
       positionFloor[i,j] = newWall;      
	 }
      
	}
    else {
     drawWall(tree.left);
     drawWall(tree.right);
	}
  }

  //init
  void Start () {
    Tree root = new Tree (new Rect (0, 0, boardRows, boardColumns));
    CreateTree (root);
    root.CreateRoom();
    positionFloor = new GameObject[boardRows, boardColumns]; 
    drawRoom(root);
    drawWall(root);
    
    
  }

  }