using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour
{
    //걷기 위한 변수
    private Vector3 vector;

    //오브젝트 감지 변수
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool readable;
    public GameObject hitObject;
    public GameObject canvasObject;
    AudioManager audioManager;

    //이미지 생성 변수
    public Sprite backgroundimage;
    public string InfoText;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        canvasObject = null; // 초기에는 Canvas가 없으므로 null로 설정합니다.
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            readable = false;
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            RaycastHit2D Interactivehit;

            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;
            Vector2 start = capsuleCollider.bounds.center;
            Vector2 end = start + inputDirection * raycastLength;

            capsuleCollider.enabled = false;
            Interactivehit = Physics2D.Linecast(start, end, layerMask);
            Debug.DrawRay(start, inputDirection * raycastLength, Color.green);
            capsuleCollider.enabled = true;

            if (Interactivehit.transform != null)
            {
                hitObject = Interactivehit.collider.gameObject;
                print(hitObject.name);
                readable = true;
                print("read 가능");
            }
            else
            {
                hitObject = null;
            }
        }

        if (readable && Input.GetKeyDown(KeyCode.Z))
        {
            print("read함");
            audioManager.PlaySound("UIButtonClick");
            ShowInfo();
        }

        if (!readable && canvasObject != null)
        {
            Destroy(canvasObject);
            canvasObject = null;
        }
    }

    void ShowInfo()
    {
        // 새로운 Canvas 생성
        canvasObject = new GameObject("InfoCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();

        // Image를 생성
        UnityEngine.UI.Image background = CreateImage(canvas.transform, new Vector2(0f, 0f), backgroundimage);

        // Text를 생성

        if(hitObject != null)
        {
            if (hitObject.name.Contains("Store"))
            {
                InfoText = "시바 상점\n당근과 물고기 무한 구매 중";
            } 
            else if (hitObject.name.Contains("StoneTop"))
            {
                InfoText = "소원을 들어주는 돌탑\n근처의 돌을 쌓아보세요!";
            }
            else if (hitObject.name.Contains("Bakery"))
            {
                InfoText = "판다 베이커리";
            }
            else if (hitObject.name.Contains("White"))
            {
                InfoText = "양이네";
            }
            else if (hitObject.name.Contains("Garden"))
            {
                InfoText = "식물원";
            }
            else if (hitObject.name.Contains("CarrotField"))
            {
                InfoText = "마을 공용 당근 밭";
            }
            else if (hitObject.name.Contains("Black"))
            {
                InfoText = "망이네";
            }
            else if (hitObject.name.Contains("Christmas"))
            {
                InfoText = "산타네";
            }
            else if (hitObject.name.Contains("Fishing"))
            {
                InfoText = "물고기 출몰 호수";
            }
        }

        print("현재 InfoText" + InfoText);

        Text textComponent = CreateText(InfoText, canvas.transform, new Vector2(0f, 0f), 40, Color.black);
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
    }





    Text CreateText(string text, Transform parent, Vector2 anchoredPosition, int fontSize, Color color)
    {
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(parent);

        Text textComponent = textObject.AddComponent<Text>();  // Text 컴포넌트를 추가

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(750f, 200f);  // 원하는 크기로 조절
        textRect.anchoredPosition = anchoredPosition;

        textComponent.text = text;
        textComponent.font = Resources.Load<Font>("Fonts/KOTRAHOPE");
        textComponent.fontSize = fontSize;  // 원하는 글씨 크기로 조절
        textComponent.color = color;

        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.alignment = TextAnchor.MiddleCenter;  // 가운데 정렬 설정

        return textComponent;
    }



    UnityEngine.UI.Image CreateImage(Transform parent, Vector2 anchoredPosition, Sprite backgroundSprite)
    {
        GameObject imageObject = new GameObject("ImageObject");
        imageObject.transform.SetParent(parent);

        // Image 컴포넌트 추가
        UnityEngine.UI.Image background = imageObject.AddComponent<UnityEngine.UI.Image>();
        background.sprite = backgroundSprite;  // 특정 스프라이트 설정

        // 이미지의 크기와 위치 설정
        RectTransform imageRect = imageObject.GetComponent<RectTransform>();
        imageRect.sizeDelta = new Vector2(750f, 200f);  // 원하는 크기로 조절
        imageRect.anchoredPosition = anchoredPosition;

        return background;
    }
}
