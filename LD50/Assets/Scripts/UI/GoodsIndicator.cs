using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsIndicator : MonoBehaviour
{
    public Vector3 targetPosition;
    
    float moveSpeed = 2000.0f;
    public Goods goods;

    public Image ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current: " + transform.position);
        Debug.Log("Target: " + targetPosition);
        var dir = targetPosition - transform.position;
        if (dir.magnitude < moveSpeed * Time.deltaTime) {
            transform.position = targetPosition;
        } else {
            transform.position = transform.position + dir.normalized * moveSpeed * Time.deltaTime;
        }

        ProgressBar.fillAmount = goods.Goodness;
    }

    public void Perish() {
        Destroy(gameObject);
    }

    public void Deliver() {
        Destroy(gameObject);
    }
}
