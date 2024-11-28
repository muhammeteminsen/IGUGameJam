using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
//using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    public Button PlayButton;
    public Button Credits;
    [SerializeField] private Transform maincamera;
   
    [Range (5,20)]
    [SerializeField] private float Cameraspeed=5f;

    private bool isRotating = false;
    private Quaternion targetRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        Credits.onClick.AddListener(ShowCredits);
        maincamera= GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            maincamera.rotation = Quaternion.Lerp(maincamera.rotation, targetRotation, Cameraspeed * Time.deltaTime);
            if (Quaternion.Angle(maincamera.rotation, targetRotation) < 0.1f)
            {
                maincamera.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
   
    void ShowCredits()
    {
        targetRotation = Quaternion.Euler(0, maincamera.eulerAngles.y + 35, 0);
        isRotating = true;
    }

    void StartGame()
    {
        SceneManager.LoadScene(1); // 1 yerine yüklemek istediðiniz sahnenin indeks numarasýný yazýn
    }
}
