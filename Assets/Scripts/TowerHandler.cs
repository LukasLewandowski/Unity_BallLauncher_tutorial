/** TODO: how to respown the tower after it has been destroyed? */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Rigidbody2D currentTower;
    [SerializeField] private float respawnDelay;

    private float towerXPosition;
    // private bool towerDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTower();
        towerXPosition = currentTower.GetComponent<Rigidbody2D>().position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // if (towerXPosition < 0 || towerXPosition > 0) {
            // TODO: how to detect that tower has been destroyed?
        //     Invoke(nameof(SpawnTower), respawnDelay);
        // }
    }

    // public GameObject deadPanel;

    // void OnDestroy()
    // {
    //     deadPanel.SetActive(true);
    // }

    private void SpawnTower() {
        GameObject towerInstance = Instantiate(towerPrefab, currentTower.position, Quaternion.identity);
        currentTower = towerInstance.GetComponent<Rigidbody2D>();
    }
}
