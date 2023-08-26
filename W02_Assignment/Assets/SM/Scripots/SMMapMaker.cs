using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMMapMaker : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private int chessBoardX;
    [SerializeField] private int chessBoardZ;
    [SerializeField] private float boardSize = 1f;

    [SerializeField] private GameObject whiteFloor;
    [SerializeField] private GameObject blackFloor;

    [SerializeField] private List<GameObject> floors;

    [SerializeField] private float term = 5f;
    [SerializeField] private Vector3 startVector = new Vector3(-12.5f, 0f, -12.5f);
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        for (int i = 0; i < chessBoardX; i++)
        {
            for (int j = 0; j < chessBoardZ; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    var floor = Instantiate(whiteFloor, new Vector3(startVector.x + term * i, startVector.y, startVector.z + term * j), Quaternion.Euler(Vector3.zero));
                    floor.transform.SetParent(gameObject.transform);
                    floors.Add(floor);
                }
                else
                {
                    var floor = Instantiate(blackFloor, new Vector3(startVector.x + term * i, startVector.y, startVector.z + term * j), Quaternion.Euler(Vector3.zero));
                    floor.transform.SetParent(gameObject.transform);
                    floors.Add(floor);
                }
            }
        }
    }
    #endregion
}
