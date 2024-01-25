using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private bool isEnter = false;
    private int originalLayer; // ������Ʈ�� ���� ���̾� ����
    private Rigidbody2D rb;
    private Vector2 originalPosition; // ������Ʈ�� ���� ��ġ ����
    private Vector2 stonetop; // ��ž ��ġ ����
    public Sprite backgroundimage;
    private InputField inputField;
    private GameObject canvasObject;
    public GameObject sparkleMotion;
    AudioManager audioManager;
    CharacterMove characterMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        isEnter=false;
        characterMove = FindObjectOfType<CharacterMove>();
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
                // �巡�� ���� �� ���̾� ����
                gameObject.layer = 31;
            }

        }
        

        if (Input.GetMouseButtonUp(0))
        {

                    
            if (isDragging)
            {
                isDragging = false;

                // ���콺 �� ������ ���� ��ǥ�� ��ȯ
                Vector2 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // �ٽ� ���� ���̾�� ����
                gameObject.layer = originalLayer;

                // ���� Ư�� ���� �ȿ� ���ٸ� ���� ��ġ�� �ǵ�����
                if (!IsInResetArea(releasePosition))
                {
                    transform.position = originalPosition;
                }
                else
                {
                    characterMove.isAction = true;
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
        // Ư�� ������ ���� true ��ȯ
        // ����: Ư�� ������ ������ �߽����� �������� 1�� ���̶� ����
        return Vector2.Distance(position, stonetop) <= 0.6f;
    }

    void ShowInputField()
    {
        // ���ο� Canvas ����
        canvasObject = new GameObject("InputCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();

        // Image�� ����
        UnityEngine.UI.Image background = CreateImage(canvas.transform, new Vector2(0f, 0f), backgroundimage);

        // Text�� ����
        Text textComponent = CreateText("Enter Your Wish", canvas.transform, new Vector2(0f, 0f), 40, Color.black);

        // InputField ���� �� ����
        GameObject inputFieldObject = new GameObject("InputField");
        inputFieldObject.transform.SetParent(canvas.transform);
        RectTransform inputFieldRect = inputFieldObject.AddComponent<RectTransform>();

        // ũ�� ����
        inputFieldRect.sizeDelta = new Vector2(1000f, 200f);  // ���ϴ� ũ��� ����

        inputFieldRect.anchoredPosition = new Vector2(0f, 0f);

        inputField = inputFieldObject.AddComponent<InputField>();
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        inputField.textComponent = textComponent;


        inputField.characterLimit = 20;

        Text placeholderText = CreateText("Enter Your Wish", inputFieldObject.transform, new Vector2(10f, 0f), 40, Color.gray);
        inputField.placeholder = placeholderText;


        // InputField�� ���� �̺�Ʈ ������ �߰� (�ɼ�)
        inputField.onEndEdit.AddListener(HandleInputEnd);

        // InputField�� ��Ŀ�� ����
        inputField.ActivateInputField();
        inputField.Select();
        isEnter= true;
    }





    Text CreateText(string text, Transform parent, Vector2 anchoredPosition, int fontSize, Color color)
    {
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(parent);

        Text textComponent = textObject.AddComponent<Text>();  // Text ������Ʈ�� �߰�

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(1000f, 200f);  // ���ϴ� ũ��� ����
        textRect.anchoredPosition = anchoredPosition;

        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); // ��Ʈ ����
        textComponent.fontSize = fontSize;  // ���ϴ� �۾� ũ��� ����
        textComponent.color = color;

        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.alignment = TextAnchor.MiddleCenter;  // ��� ���� ����

        return textComponent;
    }



    UnityEngine.UI.Image CreateImage(Transform parent, Vector2 anchoredPosition, Sprite backgroundSprite)
    {
        GameObject imageObject = new GameObject("ImageObject");
        imageObject.transform.SetParent(parent);

        // Image ������Ʈ �߰�
        UnityEngine.UI.Image background = imageObject.AddComponent<UnityEngine.UI.Image>();
        background.sprite = backgroundSprite;  // Ư�� ��������Ʈ ����

        // �̹����� ũ��� ��ġ ����
        RectTransform imageRect = imageObject.GetComponent<RectTransform>();
        imageRect.sizeDelta = new Vector2(1000f, 200f);  // ���ϴ� ũ��� ����
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
        characterMove.isAction = false;
        audioManager.PlaySound("Wish");
        Vector3 SparkleMotionVector = new Vector3(-15.9f, -15.5f, 10);

        // ���ο� ������Ʈ �ν��Ͻ�ȭ
        GameObject newObject = Instantiate(sparkleMotion, SparkleMotionVector, transform.rotation);

        // ���� �ð� ���
        yield return new WaitForSeconds(2.0f); // ���÷� 1�� ���

        // ���ο� ������Ʈ ����
        Destroy(newObject);
        Destroy(this);
    }
}
