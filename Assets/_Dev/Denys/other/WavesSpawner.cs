using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private WaveStruct[] wavesArray;

    private byte waveIndex;
    private byte Index;
    private byte delay;

    private int leftToSpawn;

    void Start()
    {
        leftToSpawn = wavesArray[0].enemyArray.Length;
    }

    public void LaunchWave() //задержка перед спауном должна быть минимум 1 ход, иначе всё поломается
    {
        delay++;
        if (leftToSpawn > 0)
        {
            if (delay == wavesArray[waveIndex].delayArray[Index])
            {
                Instantiate(wavesArray[waveIndex].enemyArray[Index], wavesArray[waveIndex].spawnerArray[Index].transform.position, Quaternion.identity);
                Index++;
                leftToSpawn--;
                delay = 0;
            }
        }
        else
        {
            if (waveIndex < wavesArray.Length -1)
            {
                waveIndex++;
                leftToSpawn = wavesArray[waveIndex].enemyArray.Length;
                Index = 0;
                delay = 0;
            }
        }
    }
}


[System.Serializable]
public class WaveStruct //хорошая идея сделать из них скриптбл обджекты, с заранее заготовлеными волнами
{
    public GameObject[] enemyArray;
    public GameObject[] spawnerArray;
    public byte[] delayArray;
}
