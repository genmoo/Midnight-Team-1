using UnityEngine;

public class PlaneTextureLoader : MonoBehaviour
{
    public Renderer planeRenderer; // Plane의 Renderer
    public string resourceFolder = "Textures"; // Resources 하위 폴더명

    private Texture2D[] textures;
    private int currentIndex = 0;

    private void Start()
    {
        // Resources/Textures 폴더 안에 있는 모든 텍스처 로드
        textures = Resources.LoadAll<Texture2D>(resourceFolder);

        if (textures.Length == 0)
        {
            Debug.LogError("텍스처를 찾을 수 없습니다. 경로를 확인하세요.");
            return;
        }

        Debug.Log($"총 {textures.Length}개의 텍스처 로드 완료.");
        
        // 초기 텍스처 설정
        ApplyTexture(textures[0]);
    }

    public void ApplyNextTexture()
    {
        if (textures == null || textures.Length == 0) return;

        currentIndex = (currentIndex + 1) % textures.Length;
        ApplyTexture(textures[currentIndex]);
    }

    private void ApplyTexture(Texture2D tex)
    {
        if (planeRenderer != null)
        {
            planeRenderer.material.SetTexture("_Canva", tex); // 쉐이더에 따라 이름 다를 수 있음
            Debug.Log($"텍스처 적용됨: {tex.name}");
        }
    }
}