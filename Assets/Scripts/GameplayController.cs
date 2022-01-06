using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public BoxSpawner boxSpawner;

    [HideInInspector]
    public BoxScript currentBox;

    public CameraFollow cameraScript;

    private int moveCount;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        boxSpawner.SpawnBox();
    }
    void Update()
    {
        DetectInput();
    }
    void DetectInput()
    {
        //detect touch or click
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        //Invoke function after 2 seconds
        //Invoke("NewBox", 1.0f);
        StartCoroutine(NewBox());
    }
    IEnumerator NewBox()
    {
        yield return new WaitForSeconds(.4f);
        boxSpawner.SpawnBox();
        //currentBox.mDistancejoint.connectedAnchor = new Vector2(0,6);
    }
    public void moveCamera()
    {
        moveCount++;
        //move camera after stacking 5 boxes
        if(moveCount == 5)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 2f;
        }
    }
    public void Restart()
    {
        // Reload the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name );
    }
}
