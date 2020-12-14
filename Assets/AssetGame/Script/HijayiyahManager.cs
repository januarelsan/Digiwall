using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijayiyahManager : MonoBehaviour
{
    [SerializeField] private Transform itemHandlerHolder;
    // Start is called before the first frame update
    void Start()
    {
        // itemHandlerHolder.GetChild(ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex).GetComponent<ItemHandler>().PlayGame();
        StartCoroutine(Spawn());
        
    }

    IEnumerator Spawn()
    {        
        yield return new WaitForSeconds(3);
        itemHandlerHolder.GetChild(ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex).GetComponent<ItemHandler>().Spawn();
        
    }

}
