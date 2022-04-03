using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsUI : MonoBehaviour
{
    public GoodsIndicator IndicatorPrefab;
    public Transform IndicatorRoot;
    public Transform IndicatorSpawn;
    Vector3 indicatorOffset = new Vector3(0, -70, 0);

    List<GoodsIndicator> indicators = new List<GoodsIndicator>();

    public GameObject empty;

    public static GoodsUI main;

    void Awake() {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var removedIndicators = new List<GoodsIndicator>();
        foreach (var ind in indicators) {
            if (ind.goods.IsPerished) {
                ind.Perish();
                removedIndicators.Add(ind);
            }
            if (ind.goods.IsDelivered) {
                ind.Deliver();
                removedIndicators.Add(ind);
            }
        }
        foreach (var ind in removedIndicators) {
            indicators.Remove(ind);
        }
        SetTargetPositions();

        if (indicators.Count == 0) {
            empty.SetActive(true);
        }
        else {
            empty.SetActive(false);
        }
    }

    public void AddGoods(Goods goods) {
        GoodsIndicator indicator = Instantiate(IndicatorPrefab, transform);
        indicator.transform.position = IndicatorSpawn.position;
        indicator.goods = goods;
        indicators.Insert(0, indicator);
        SetTargetPositions();
    }

    public void SetTargetPositions() {
        for(var i = 0; i < indicators.Count; i++) {
            var indicator = indicators[i];
            indicator.targetPosition = IndicatorRoot.position + indicatorOffset * i;
        }
    }
}
