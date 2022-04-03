using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{

    public ParticleSystem Area;
    public GameObject TutorialInfo;

    // Start is called before the first frame update
    void Start()
    {
        TutorialInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.main.Truck.HasGoods()) {
            Destroy(gameObject);
        }
    }

    public void Show(bool show) {
        if (show) {
            TutorialInfo.SetActive(true);
            Area.Stop();
        } else {
            //TutorialInfo.SetActive(false);
        }
    }
}
