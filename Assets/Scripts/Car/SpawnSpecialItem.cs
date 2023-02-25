using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecialItem : MonoBehaviour
{

    private bool hasSpecial = false;
    public Color outlineColor = Color.black;
    public float duration = 1;

    public float opportunity = 0.5f;

    private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        RandomSpawn();
        outline = GetComponent<Outline>();
        outline.OutlineColor = outlineColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpecial) {
            outline.enabled = true;
            StartCoroutine(OutlineTiming());
        } else {
            outline.enabled = false;
            StopAllCoroutines();
        }
    }

    IEnumerator OutlineTiming() {
        yield return new WaitForSeconds(duration);
        hasSpecial = false;
    }

    void RandomSpawn() {
        float random = Random.Range(0, 1.0f);
        Debug.Log("Random spawn special value: " + random);
        hasSpecial = random < opportunity;
    }
}
