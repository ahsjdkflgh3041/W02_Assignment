using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparency : MonoBehaviour
{

    #region PublicVariables
    public float alphaValue = 0.3f;
    public float headPos = 0.5f;
    public float feetPos = -0.5f;
    #endregion

    #region PrivateVariables
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCamera;

    private List<Renderer> objectsBetween = new List<Renderer>();
    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods
    private void Update()
    {
        Vector3 headPosition = player.position + player.up * headPos;
        Vector3 feetPosition = player.position + player.up * feetPos;

        CheckObjectsBetween(headPosition, playerCamera.position);
        CheckObjectsBetween(feetPosition, playerCamera.position);

        //List�� �Էµ� ������Ʈ ����ȭ
        foreach (Renderer rend in objectsBetween)
        {
            Color tempColor = rend.material.color;
            tempColor.a = alphaValue;
            rend.material.color = tempColor;
        }
        
        //����ȭ �� ������Ʈ�� ���� �ʱ�ȭ
        if (objectsBetween.Count == 0)
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                Color tempColor = rend.material.color;
                tempColor.a = 1.0f; // Reset alpha to fully opaque
                rend.material.color = tempColor;
            }
        }

        objectsBetween.Clear();
    }

    //������Ʈ�� RayHit �ƴ��� Ȯ�� �� List�� �Է�
    private void CheckObjectsBetween(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 rayDirection = endPosition - startPosition;
        Ray ray = new Ray(startPosition, rayDirection);

        RaycastHit[] hits = Physics.RaycastAll(ray, rayDirection.magnitude);
        foreach (RaycastHit hit in hits)
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer != null)
            {
                objectsBetween.Add(hitRenderer);
            }
        }
    }
    #endregion
}