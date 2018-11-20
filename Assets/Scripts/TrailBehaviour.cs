using UnityEngine;

public class TrailBehaviour : MonoBehaviour {

    public float shrinkCoef = 0.45f;
    public float sizeToDecrease;
	[HideInInspector] public uint id;

    private void Start()
    {
        sizeToDecrease = transform.localScale.x * shrinkCoef;
    }


    private void Update() {
        float p = sizeToDecrease * Time.deltaTime;
        transform.localScale = transform.localScale - new Vector3(p, p, p);
        if (transform.localScale.x < 0.01)
        {
            Destroy(gameObject);
        }
	}





}
