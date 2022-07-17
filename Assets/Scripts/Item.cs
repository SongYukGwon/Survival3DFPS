using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템의 이름

    [TextArea]
    public string itemDesc; //아이템의 설명
    public ItemType itemType;
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefab; //아이템의 프리팹

    public string weaponType; //무기 유형.

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
