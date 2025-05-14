using UnityEngine;
using System.Collections;

public class UIStateControllerVR : MonoBehaviour
{
    [Header("버튼 오브젝트")]
    public GameObject recordButton;
    public GameObject submitButton;
    public GameObject completeButton;
    public GameObject saveButton;

    [Header("설명 스프라이트 UI")]
    public GameObject statusText_Recording;
    public GameObject statusText_Generating;  

    [Header("설명 표시 시간")]
    public float statusDuration = 2f;

    private void Start()
    {
        SetInitialUIState();
    }

    void SetInitialUIState()
    {
        recordButton.SetActive(true);
        submitButton.SetActive(false);
        completeButton.SetActive(false);
        saveButton.SetActive(false);
        
        statusText_Recording.SetActive(false);
        statusText_Generating.SetActive(false);
    }
    
    public void OnRecordButton()
    {
        Debug.Log("녹음 버튼 눌림");
        ShowStatus(statusText_Recording);
        
        submitButton.SetActive(true);
    }
    
    public void OnSubmitButton()
    {
        Debug.Log("전달 버튼 눌림");
        ShowStatus(statusText_Generating);
        
        recordButton.SetActive(false);
        submitButton.SetActive(false);
        completeButton.SetActive(true);
        saveButton.SetActive(true);
    }
    
    void ShowStatus(GameObject statusUI)
    {
        statusUI.SetActive(true);
        StartCoroutine(HideAfterDelay(statusUI));
    }

    IEnumerator HideAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(statusDuration);
        obj.SetActive(false);
    }
}
