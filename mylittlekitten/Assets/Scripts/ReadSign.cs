using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour
{
    //�ȱ� ���� ����
    private Vector3 vector;

    //������Ʈ ���� ����
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool readable;
    public GameObject hitObject;
    public GameObject canvasObject;
    AudioManager audioManager;

    //�̹��� ���� ����
    public Sprite backgroundimage;
    public string InfoText;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        canvasObject = null; // �ʱ⿡�� Canvas�� �����Ƿ� null�� �����մϴ�.
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
                print("read ����");
            }
            else
            {
                hitObject = null;
            }
        }

        if (readable && Input.GetKeyDown(KeyCode.Z))
        {
            print("read��");
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
        // ���ο� Canvas ����
        canvasObject = new GameObject("InfoCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();

        // Image�� ����
        UnityEngine.UI.Image background = CreateImage(canvas.transform, new Vector2(0f, 0f), backgroundimage);

        // Text�� ����

        if(hitObject != null)
        {
            if (hitObject.name.Contains("Store"))
            {
                InfoText = "�ù� ����\n��ٰ� ����� ���� ���� ��";
            } 
            else if (hitObject.name.Contains("StoneTop"))
            {
                InfoText = "�ҿ��� ����ִ� ��ž\n��ó�� ���� �׾ƺ�����!";
            }
            else if (hitObject.name.Contains("Bakery"))
            {
                InfoText = "�Ǵ� ����Ŀ��";
            }
            else if (hitObject.name.Contains("White"))
            {
                InfoText = "���̳�";
            }
            else if (hitObject.name.Contains("Garden"))
            {
                InfoText = "�Ĺ���";
            }
            else if (hitObject.name.Contains("CarrotField"))
            {
                InfoText = "���� ���� ��� ��";
            }
            else if (hitObject.name.Contains("Black"))
            {
                InfoText = "���̳�";
            }
            else if (hitObject.name.Contains("Christmas"))
            {
                InfoText = "��Ÿ��";
            }
            else if (hitObject.name.Contains("Fishing"))
            {
                InfoText = "����� ��� ȣ��";
            }
        }

        print("���� InfoText" + InfoText);

        Text textComponent = CreateText(InfoText, canvas.transform, new Vector2(0f, 0f), 40, Color.black);
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
    }





    Text CreateText(string text, Transform parent, Vector2 anchoredPosition, int fontSize, Color color)
    {
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(parent);

        Text textComponent = textObject.AddComponent<Text>();  // Text ������Ʈ�� �߰�

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(750f, 200f);  // ���ϴ� ũ��� ����
        textRect.anchoredPosition = anchoredPosition;

        textComponent.text = text;
        textComponent.font = Resources.Load<Font>("Fonts/KOTRAHOPE");
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
        imageRect.sizeDelta = new Vector2(750f, 200f);  // ���ϴ� ũ��� ����
        imageRect.anchoredPosition = anchoredPosition;

        return background;
    }
}
