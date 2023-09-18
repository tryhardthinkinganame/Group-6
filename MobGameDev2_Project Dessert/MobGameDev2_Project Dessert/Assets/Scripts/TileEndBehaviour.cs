using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("How much time to wait before destroying " + "the tile after reaching the end")]
    public float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerBehaviour>())
        {
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            Destroy(transform.parent.gameObject, destroyTime);
        }

        if (col.gameObject.GetComponent<MenuPlayer>())
        {
            GameObject.FindObjectOfType<MenuTiles>().SpawnNextTile();

            Destroy(transform.parent.gameObject, destroyTime);
        }
    }
}

