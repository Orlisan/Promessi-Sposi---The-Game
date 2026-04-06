using UnityEngine;

public class GrassDeath : MonoBehaviour
{
    public GameObject player;
    public float reach;
    public GameObject grassItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseDown()
    {
        float difx = Mathf.Abs(transform.position.x - player.transform.position.x);
        float difz = Mathf.Abs(transform.position.z - player.transform.position.z);
        if(difx <= reach && difz <= reach) {
        int random = Random.Range(0, 11);
        if(random == 1) {
            itemInstancer.Istanzia(grassItem);
        }
        Destroy(gameObject);

        }
    }
}
