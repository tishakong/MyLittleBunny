using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SellItem : MonoBehaviour
{
    private Vector3 vector;
    private CapsuleCollider2D capsuleCollider;
    public LayerMask layerMask;
    public float raycastLength;
    public GameObject hitObject;
    private bool sellable;
    public GameObject buysuccess;
    AudioManager audioManager;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Store")
        {
            Destroy(this);
        }
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            sellable = false;
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
                sellable = true;
            }
            else
            {
                hitObject = null;
            }
        }

        if (sellable && Input.GetKeyDown(KeyCode.Z))
        {
            SellMotion();
        }
    }
    void SellMotion()
    {

        //carrot fish 에 따라 coin 팔기
        if(DataManager.Instance.myFish==0 && DataManager.Instance.myCarrot==0)
        {
            //당근과 물고기가 없는 상태임 못 판다 표시해줘야함
        }
        else
        {
            DataManager.Instance.myCoin += DataManager.Instance.myFish*2;
            DataManager.Instance.myCoin += DataManager.Instance.myCarrot*5;
            DataManager.Instance.myCarrot = 0;
            DataManager.Instance.myFish = 0;

            StartCoroutine(SellSuccess());
            audioManager.PlaySound("BuySuccess");
            print("현재 myCoin :"+DataManager.Instance.myCoin);
        }
    }

    IEnumerator SellSuccess()
    {
        Vector3 spawnVector = new Vector3(-4.67f, 2f, 0);
        GameObject newObject = Instantiate(buysuccess, spawnVector, transform.rotation);
        yield return new WaitForSeconds(2.0f);
        Destroy(newObject);
    }
}
