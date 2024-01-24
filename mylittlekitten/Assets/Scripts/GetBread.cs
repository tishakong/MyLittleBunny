using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetBread : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public bool getable;
    public GameObject hitObject;
    public int breadcount;
    public float breadheight;
    private bool buyable;
    public bool exitable=true;
    public GameObject buysuccess;
    public GameObject buyfail;
    public GameObject cantExit;
    AudioManager audioManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "BakeryShop")
        {
            Destroy(this);
        }

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        breadcount = 0;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            getable = false;
            buyable = false;
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
                int hitObjectLayer = hitObject.layer;
                string hitObjectLayerName = LayerMask.LayerToName(hitObjectLayer);
                if (hitObjectLayerName == "Bread")
                {
                    getable = true;
                    print("�� �ݱ� ����");
                } else if (hitObjectLayerName == "InteractiveNPC")
                {
                    buyable = true;
                    print("���ɱ� ����");
                }
            }
            else
            {
                hitObject = null;
            }
        }

        if (getable && Input.GetKeyDown(KeyCode.Z))
        {
            GetBreadMotion();
        }

        if (buyable && Input.GetKeyDown(KeyCode.Z))
        {
            BuyMotion();
        }

        if (breadcount!=0 && !exitable)
        {
            StartCoroutine(Exit());
        }
    }
    void GetBreadMotion()
    {
        // hitObject�� null�� �ƴϰ� SpriteRenderer ������Ʈ�� �ִٸ�
        if (hitObject != null && hitObject.TryGetComponent(out SpriteRenderer hitObjectRenderer))
        {
            breadcount++;
            breadheight = breadheight+hitObjectRenderer.sprite.bounds.size.y-0.1f;
            // �÷��̾��� ��ġ�� BreadMotion�� ����
            Vector3 breadMotionPosition = transform.position + new Vector3(0, 0.3f+breadheight, 0);
            GameObject breadMotionObject = new GameObject("BreadMotion", typeof(SpriteRenderer));
            breadMotionObject.transform.position = breadMotionPosition;

            // BreadMotion�� �÷��̾��� �ڽ����� ����
            breadMotionObject.transform.parent = transform;

            // BreadMotion�� SpriteRenderer�� hitObject�� Sprite ����
            SpriteRenderer breadMotionRenderer = breadMotionObject.GetComponent<SpriteRenderer>();
            breadMotionRenderer.sprite = hitObjectRenderer.sprite;
            breadMotionRenderer.sortingOrder = 13+breadcount;

            // hitObject ����
            Destroy(hitObject);

            audioManager.PlaySound("GetBread");
        }
    }

    void BuyMotion()
    {
        //�귡��ī��Ʈ�� �� �������� ���� �ϴ� buyfail, buysuccess�ൿ �޶����� �ϱ�
        breadcount = 0; breadheight=0;
        foreach (Transform child in transform)
        {
            if (child.name == "BreadMotion")
            {
                Destroy(child.gameObject);
            }
        }
        StartCoroutine(BuySuccess());
        audioManager.PlaySound("BuySuccess");
        //�� ���̱�
    }

    IEnumerator BuySuccess()
    {
        Vector3 spawnVector = new Vector3(-5.8f, 2.6f,0);
        GameObject newObject = Instantiate(buysuccess, spawnVector, hitObject.transform.rotation);
        yield return new WaitForSeconds(2.0f);
        Destroy(newObject);
    }

    IEnumerator BuyFail()
    {
        Vector3 wateringVector = new Vector3(-5.8f, 2.6f, 0);
        GameObject newObject = Instantiate(buyfail, wateringVector, hitObject.transform.rotation);

        // ���� �ð� ���
        yield return new WaitForSeconds(2.0f);
        Destroy(newObject);
    }

    IEnumerator Exit()
    {
        if (GameObject.Find("ExitObject") == null)
        {
            Vector3 spawnVector = new Vector3(-5.8f, 2.6f, 0);
            GameObject ExitObject = Instantiate(cantExit, spawnVector, transform.rotation);
            yield return new WaitForSeconds(1.0f);
            Destroy(ExitObject);
            exitable = true;
        }
    }
}
