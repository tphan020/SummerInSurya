using UnityEngine;
using System.Collections;

[System.Serializable]
public class PathFollower : MonoBehaviour {
    Node[] PathNode;

    public GameObject Player;
    public float MoveSpeed;
    float Timer;

    int CurrentNode = 0;
    Vector2 startPosition;

    public bool isMove = false;
    static Vector3 CurrentPositionHolder;
    public int Scene = 1;
    void Start() {
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    public void SetPositionHolder()
    {
        CurrentPositionHolder = Player.transform.position;
    }

    void CheckNode() {
        Timer = 0;
        startPosition = Player.transform.position;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
    }

    void Update() {
        if (isMove) {
            /*
            if (Input.GetMouseButtonDown(0)) {
                isMove = !isMove;
            }*/

            Timer += Time.deltaTime * MoveSpeed;

            if (Player.transform.position.x != CurrentPositionHolder.x || Player.transform.position.y != CurrentPositionHolder.y) {
                Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -1);
                

            } else {
                if (CurrentNode < PathNode.Length - 1) {
                    CurrentNode++;
                    CheckNode();
                } else {
                    System.Array.Reverse(PathNode);
                    CurrentNode = 0;
                    isMove = false;
                    if (Scene == 1)
                    {
                        GameObject.Find("FirstScene").GetComponent<FirstScene>().ReverseCamera = true;
                    }
                    else if (Scene == 2)
                    {
                        GameObject.Find("SecondScene").GetComponent<SecondScene>().ReverseCamera = true;
                    }
                    else if (Scene == 3)
                    {
                        GameObject.Find("ThirdScene").GetComponent<ThirdScene>().ReverseCamera = true;
                    }
                }
            }

        }
    }

    public void canMove(float speed)
    {
        isMove = true;
        MoveSpeed = speed;
    }
}