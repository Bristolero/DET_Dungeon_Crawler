using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject tile;
    private GameObject[,] positionFloor;

  public class Tree {
    public Tree left, right;
    public Rect rect;
    public Rect room = new Rect(-1,-1, 0, 0); // i.e null
    public int id;

    private static int idCounter = 0;

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

        // room position will be absolute in the board, not relative to the sub-dungeon
        room = new Rect (rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
        Debug.Log ("Raum erstellt " + room + " in Fläche " + id + " " + rect);
      }
    }

    public bool IsLeaf() {
      return left == null && right == null;
    }

    public bool Split(int minRoomSize, int maxRoomSize) {
      if (!IsLeaf()) {
         return false;
      }

      // choose a vertical or horizontal split depending on the proportions
      // i.e. if too wide split vertically, or too long horizontally,
      // or if nearly square choose vertical or horizontal at random
      bool splitHorizontal;
      if (rect.width / rect.height >= 1.25) {
        splitHorizontal = false;
      } else if (rect.height / rect.width >= 1.25) {
        splitHorizontal = true;
      } else {
        splitHorizontal = Random.Range (0.0f, 1.0f) > 0.5;
      }

      if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize) {
        Debug.Log ("Sub-dungeon " + id + " will be a leaf");
        return false;
      }

      if (splitHorizontal) {
        // split so that the resulting sub-dungeons widths are not too small
        // (since we are splitting horizontally)
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
      // if the sub-dungeon is too large
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

  void Start () {
    Tree root = new Tree (new Rect (0, 0, boardRows, boardColumns));
    CreateTree (root);
    root.CreateRoom();
    positionFloor = new GameObject[boardRows, boardColumns];
    drawRoom(root);
  }

  }