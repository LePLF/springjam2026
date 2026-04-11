using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PointsList : MonoBehaviour
{
    public List<Vector2Wrapper> path;

    [Serializable]
    public class Vector2Wrapper
    {
        public List<Transform> pathPoints;
    }

    public Vector2 GetPathPointInList(int listChoice)
    {
        if (listChoice < 0 || listChoice >= path.Count)
        {
            Debug.LogWarning("List index out of range");
            return Vector2.zero;
        }

        List<Transform> chosenList = path[listChoice].pathPoints;

        if (chosenList == null || chosenList.Count == 0)
        {
            Debug.LogWarning("Chosen path list is empty");
            return Vector2.zero;
        }

        int randomIndex = UnityEngine.Random.Range(0, chosenList.Count);
        return chosenList[randomIndex].position;
    }
    public int GetPathCount()
    {
        return path.Count;
    }



}
