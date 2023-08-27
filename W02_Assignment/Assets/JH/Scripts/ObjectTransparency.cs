using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparency : MonoBehaviour
{
    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }
    #region PublicVariables
    
    #endregion

    #region PrivateVariables
    private Transform player;
    private Transform playerCamera;
    private float alphaValue = 0.3f;
    private float headPos = 0.5f;
    private float feetPos = -0.5f;
    private LayerMask playerLayerMask;
    private List<Renderer> objectsBetween = new List<Renderer>();
    #endregion

    #region PublicMethods
    public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }

    }
	#endregion

	#region PrivateMethods
	private void OnEnable()
	{
		player = GameManager.instance.GetPlayer().transform;
		playerCamera = GameManager.instance.GetMainCamera().transform;
	}
	private void FixedUpdate()
    {
        Vector3 headPosition = player.position + player.up * headPos;
        Vector3 feetPosition = player.position + player.up * feetPos;

        CheckObjectsBetween(headPosition, playerCamera.position);
        CheckObjectsBetween(feetPosition, playerCamera.position);

        //List에 입력된 오브젝트 투명화
        foreach (Renderer rend in objectsBetween)
        {
            Color tempColor = rend.material.color;
            ChangeRenderMode(rend.material, BlendMode.Fade);
            
            tempColor.a = alphaValue;
            rend.material.color = tempColor;
        }
        
        //투명화 된 오브젝트들 투명값 초기화
        if (objectsBetween.Count == 0)
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                Color tempColor = rend.material.color;
                tempColor.a = 1.0f;
                rend.material.color = tempColor;

                ChangeRenderMode(rend.material, BlendMode.Opaque);
            }
        }

        objectsBetween.Clear();
    }

    //오브젝트가 RayHit 됐는지 확인 후 List에 입력
    private void CheckObjectsBetween(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 rayDirection = endPosition - startPosition;
        Ray ray = new Ray(startPosition, rayDirection);
        int playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        float distance = Vector3.Distance(startPosition, endPosition); // 두 지점 사이의 거리 계산
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, ~playerLayerMask);
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