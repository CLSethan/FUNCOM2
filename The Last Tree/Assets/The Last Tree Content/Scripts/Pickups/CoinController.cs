using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public static CoinController instance;

    public CoinPickup coinToDrop;

    public int currentCoins;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
    }

    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup coinDrop = Instantiate(coinToDrop, position + new Vector3(.2f,.1f,0), Quaternion.identity);

        coinDrop.coinAmount = value;
        coinDrop.gameObject.SetActive(true);
    }
}
