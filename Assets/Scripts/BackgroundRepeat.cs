using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BackgroundRepeat : MonoBehaviour
{
    public Transform player;
    public GameObject background;
    public float offset;
    GameObject currentBg;
    List<GameObject> bgClons = new List<GameObject>();

    private void Start()
    {
        currentBg = background;
        for (int i = 0; i < 9; i++)
        {
            bgClons.Add(Instantiate(background) as GameObject);
            bgClons[i].transform.position = new Vector3(1000, 1000);
            bgClons[i].SetActive(false);
        }
        bgClons.Insert(0, currentBg);

        lastPos = player.transform.position;
        lastIndex = bgClons.Count - 1;
    }

    Vector3 lastPos;
    int lastIndex;
    bool rightCheckable = true;
    bool rightUpCheckable = true;
    bool rightDownCheckable = true;
    bool leftCheckable = true;
    bool leftUpCheckable = true;
    bool leftDownCheckable = true;
    bool upCheckable = true;
    bool downCheckable = true;
    private void Update()
    {


        float halfObjectWidth = currentBg.GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfObjectHeight = currentBg.GetComponent<SpriteRenderer>().bounds.extents.y ;

        foreach (var bg in bgClons)
        {
            bool flag1 = (bg.transform.position.x - (halfObjectWidth)) < player.position.x;
            bool flag2 = (bg.transform.position.x + (halfObjectWidth)) > player.position.x;
            bool flag3 = (bg.transform.position.y - (halfObjectHeight)) < player.position.y;
            bool flag4 = (bg.transform.position.y + (halfObjectHeight)) > player.position.y;
            if (flag1 && flag2 && flag3 && flag4)
            {
                if (currentBg != bg)
                {
                    //lastPos = player.transform.position;
                    rightCheckable = true;
                    rightUpCheckable = true;
                    rightDownCheckable = true;
                    leftCheckable = true;
                    leftUpCheckable = true;
                    leftDownCheckable = true;
                    upCheckable = true;
                    downCheckable = true;
                }
                currentBg = bg;
              //  Debug.Log("curBG : " + currentBg.transform.position);
                //Debug.Log("player Pos : " + player.transform.position);
                currentBg.SetActive(true);
            }
        }
       // Debug.Log("pos : " + currentBg.transform.position);


     
        if (player.position.x > currentBg.transform.position.x + offset && rightCheckable)
        {
            rightCheckable = false;
           // Debug.Log("1, player : " + player.position + ", bg : " + currentBg.transform.position);
            
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x + halfObjectWidth * 2, currentBg.transform.position.y, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
            //bgClons.Insert(0, bgClons[bgClons.Count - 1]);
            //bgClons.RemoveAt(bgClons.Count - 1);
        }
        // right up
        if (player.position.x > currentBg.transform.position.x + offset && player.position.y > currentBg.transform.position.y + offset && rightUpCheckable)
        {
            rightUpCheckable = false;
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x + halfObjectWidth * 2, currentBg.transform.position.y + halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
        // right down
        if (player.position.x > currentBg.transform.position.x + offset && player.position.y < currentBg.transform.position.y - offset && rightDownCheckable)
        {
            rightDownCheckable = false;
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x + halfObjectWidth * 2, currentBg.transform.position.y - halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
        if (player.position.x < currentBg.transform.position.x - offset && leftCheckable)
        {
            leftCheckable = false;
           // Debug.Log("2, player : " + player.position + ", bg : " + currentBg.transform.position);

            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x - halfObjectWidth * 2, currentBg.transform.position.y, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
        // left up
        if (player.position.x < currentBg.transform.position.x - offset && player.position.y > currentBg.transform.position.y + offset && leftUpCheckable)
        {
            leftUpCheckable = false;
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x - halfObjectWidth * 2, currentBg.transform.position.y + halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
        // leftdown
        if (player.position.x < currentBg.transform.position.x - offset && player.position.y < currentBg.transform.position.y - offset && leftDownCheckable)
        {
            leftDownCheckable = false;
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x - halfObjectWidth * 2, currentBg.transform.position.y - halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }

        if (player.position.y > currentBg.transform.position.y + offset && upCheckable)
        {
            upCheckable = false;
            //  Debug.Log("3");
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x, currentBg.transform.position.y + halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
        if (player.position.y < currentBg.transform.position.y - offset && downCheckable)
        {
            downCheckable = false;
         //   Debug.Log("4");
            bgClons[lastIndex].transform.position = new Vector3(currentBg.transform.position.x, currentBg.transform.position.y - halfObjectHeight * 2, currentBg.transform.position.z);
            bgClons[lastIndex].SetActive(true);
            if (lastIndex == bgClons.Count - 1)
                lastIndex = 0;
            else
                lastIndex++;
        }
    }
}

