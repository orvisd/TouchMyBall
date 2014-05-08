using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class WallGroup
    {
        public string Name;
        public List<GameObject> Group;
    }

    public Ball Ball;
    public List<WallGroup> WallGroups;

    Random random;
    WallGroup currentGroup;
    List<int> selectionIndices;
    bool readyToToggle;

    void Start()
    {
        random = new Random();

        selectionIndices = new List<int>();
        toggleWallGroup();

        readyToToggle = false;
    }

    void Update()
    {
        if(!readyToToggle &&
           (Ball.transform.position.x < -1 || Ball.transform.position.x > 1 ||
            Ball.transform.position.y < -1 || Ball.transform.position.y > 1))
        {
            readyToToggle = true;
        }
        else if(readyToToggle &&
                (Ball.transform.position.x > -1 && Ball.transform.position.x < 1 &&
                 Ball.transform.position.y > -1 && Ball.transform.position.y < 1))
        {
            Ball.Reset();
            toggleWallGroup();
            readyToToggle = false;
        }
    }

    void toggleWallGroup()
    {
        if(currentGroup != null)
        {
            currentGroup.Group.ForEach(wall => wall.SetActive(true));
        }

        if(selectionIndices.Count == 0)
        {
            for(int group = 0; group < WallGroups.Count; ++group)
            {
                if(WallGroups[group].Name != (currentGroup != null ? currentGroup.Name : ""))
                {
                    selectionIndices.Add(group);
                }
            }
        }

        int index = random.Next(selectionIndices.Count);

        currentGroup = WallGroups[selectionIndices[index]];
        currentGroup.Group.ForEach(wall => wall.SetActive(false));
        
        selectionIndices.RemoveAt(index);
    }
}
