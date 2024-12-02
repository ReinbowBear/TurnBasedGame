using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Content : MonoBehaviour
{
    public static ContentSO data;
    public ContentSO myContentSO; //https://dtf.ru/u/306597-mihail-nikitin/273585-polza-addressables-v-unity3d-i-varianty-ispolzovaniya збилдить адресейбл надо по своему

    void Awake()
    {
        data = myContentSO;
    }


    public static async Task<GameObject> GetAsset(AssetReference address, Transform parent = null, bool worldSpace = false)
    {
        var handle = Addressables.InstantiateAsync(address, parent, worldSpace);
        GameObject newObject = await handle.Task;

        return newObject;
    }

    public static void DestroyAsset(GameObject asset)
    {
        //asset.SetActive(false);
        Addressables.ReleaseInstance(asset);
    }
}
