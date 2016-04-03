using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {
    MapGenerationScript mapGenerator;
    public char[,] map = new char[15, 2];
    public char step;
    int direction;
    public Vector3 nextLocation;
    public GameObject Ground;
    public GameObject GroundT;
    Queue<Object> mapQueue;
    Object piece;

    // Use this for initialization
    void Start () {
        nextLocation = new Vector3 (0, 0, 0);
        mapGenerator = new MapGenerationScript();
        mapQueue = new Queue<Object>();
        map = mapGenerator.initialize();


        piece = Instantiate(Ground, nextLocation, Quaternion.identity);
        mapQueue.Enqueue(piece);
        nextLocation.z += 20;
        Debug.Log(map[0, 0]+ ", " + map[0, 1]);
        direction = (int)map[0, 1] - '0';
        map[1, 1] = '0';
        for (int i = 1; i < 15 ;i++){
            Debug.Log(map[i, 0] + ", " + map[i, 1]);
            if (map[i, 0].Equals('S')){
                if (map[i, 1].Equals('0'))
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0,0,0));
                    nextLocation.z += 20;
                }
                else if (map[i, 1].Equals('1'))
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x += 20;
                }
                else if (map[i, 1].Equals('2'))
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (map[i, 1].Equals('3'))
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }                
            }
            else if(map[i, 0].Equals('R')){
                if (map[i, 1].Equals('0'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.x += 20;
                }
                else if (map[i, 1].Equals('1'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.z -= 20;
                }
                else if (map[i, 1].Equals('2'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.x -= 20;
                }
                else if (map[i, 1].Equals('3'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.z += 20;
                }
                direction = (int)map[i, 1] - '0';
            }
            else if (map[i, 0].Equals('L')){
                if (map[i, 1].Equals('0'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }
                else if (map[i, 1].Equals('1'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.z += 20;
                }
                else if (map[i, 1].Equals('2'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.x += 20;
                }
                else if (map[i, 1].Equals('3'))
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                direction = (int)map[i, 1] - '0';
            }
            mapQueue.Enqueue(piece);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            step = mapGenerator.mapStep();
            direction = mapGenerator.getDirection();
            Debug.Log(step + "," + direction);
            
            if (step.Equals('S')){
                if (direction == 0)
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = Instantiate(Ground, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }

            }
            else if (step.Equals('R')){
                if (direction == 0)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.x -= 20;
                }
                
            }
            else if (step.Equals('L')){
                if (direction == 0)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 180, 0));
                    nextLocation.z += 20;
                }
                else if (direction == 1)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 270, 0));
                    nextLocation.x += 20;
                }
                else if (direction == 2)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 0, 0));
                    nextLocation.z -= 20;
                }
                else if (direction == 3)
                {
                    piece = Instantiate(GroundT, nextLocation, Quaternion.Euler(0, 90, 0));
                    nextLocation.x -= 20;
                }               

            }
            mapQueue.Enqueue(piece);
            Destroy(mapQueue.Dequeue());  
        }
	}
}
