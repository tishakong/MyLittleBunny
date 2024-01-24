using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private bool isEnter = false;
    private int originalLayer; // 오브젝트의 원래 레이어 저장
    private Rigidbody2D rb;
    private Vector2 originalPosition; // 오브젝트의 원래 위치 저장
    private Vector2 stonetop; // 돌탑 위치 저장
    public Sprite backgroundimage;
    private InputField inputField;
    private GameObject canvasObject;
    public GameObject sparkleMotion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;
        originalPosition = transform.position;
        stonetop = new Vector2(-15.87f, -18.69f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
                // 드래그 시작 시 레이어 변경
                gameObject.layer = 31;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;

                // 마우스 뗀 지점을 월드 좌표로 변환
                Vector2 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 다시 원래 레이어로 변경
                gameObject.layer = originalLayer;

                // 만약 특정 영역 안에 없다면 원래 위치로 되돌리기
                if (!IsInResetArea(releasePosition))
                {
                    transform.position = originalPosition;
                }
                else
                {
                    // InputField를 생성하고 띄우기
                    ShowInputField();

                }
            }
        }

        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mousePosition);
        }

        if (isEnter)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }
    }

    bool IsInResetArea(Vector2 position)
    {
        // 특정 영역에 들어가면 true 반환
        // 예시: 특정 영역은 원점을 중심으로 반지름이 1인 원이라 가정
        return Vector2.Distance(position, stonetop) <= 0.6f;
    }

    void ShowInputField()
    {
        // 새로운 Canvas 생성
        canvasObject = new GameObject("InputCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();

        // Image를 생성
        UnityEngine.UI.Image background = CreateImage(canvas.transform, new Vector2(0f, 0f), backgroundimage);

        // Text를 생성
        Text textComponent = CreateText("Enter Your Wish", canvas.transform, new Vector2(0f, 0f), 40, Color.black);

        // InputField 생성 및 설정
        GameObject inputFieldObject = new GameObject("InputField");
        inputFieldObject.transform.SetParent(canvas.transform);
        RectTransform inputFieldRect = inputFieldObject.AddComponent<RectTransform>();

        // 크기 조절
        inputFieldRect.sizeDelta = new Vector2(1000f, 200f);  // 원하는 크기로 조절

        inputFieldRect.anchoredPosition = new Vector2(0f, 0f);

        inputField = inputFieldObject.AddComponent<InputField>();
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        inputField.textComponent = textComponent;


        inputField.characterLimit = 20;

        Text placeholderText = CreateText("Enter Your Wish", inputFieldObject.transform, new Vector2(10f, 0f), 40, Color.gray);
        inputField.placeholder = placeholderText;


        // InputField에 대한 이벤트 리스너 추가 (옵션)
        inputField.onEndEdit.AddListener(HandleInputEnd);

        // InputField에 포커스 설정
        inputField.ActivateInputField();
        inputField.Select();
        isEnter= true;
    }





    Text CreateText(string text, Transform parent, Vector2 anchoredPosition, int fontSize, Color color)
    {
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(parent);

        Text textComponent = textObject.AddComponent<Text>();  // Text 컴포넌트를 추가

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(1000f, 200f);  // 원하는 크기로 조절
        textRect.anchoredPosition = anchoredPosition;

        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); // 폰트 수정
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
        imageRect.sizeDelta = new Vector2(1000f, 200f);  // 원하는 크기로 조절
        imageRect.anchoredPosition = anchoredPosition;

        return background;
    }



    void HandleInputEnd(string value)
    {
        Debug.Log("Input End: " + value);
        Destroy(canvasObject);
        isEnter = false;
        StartCoroutine(SparkleMotion());

    }

    IEnumerator SparkleMotion()
    {
        Vector3 SparkleMotionVector = new Vector3(-15.9f, -15.5f, 10);

        // 새로운 오브젝트 인스턴스화
        GameObject newObject = Instantiate(sparkleMotion, SparkleMotionVector, transform.rotation);

        // 일정 시간 대기
        yield return new WaitForSeconds(2.0f); // 예시로 1초 대기

        // 새로운 오브젝트 삭제
        Destroy(newObject);
        Destroy(this);
    }
}
