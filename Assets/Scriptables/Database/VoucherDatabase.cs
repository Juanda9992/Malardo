using System.Collections.Generic;
using System.Linq;
using Shop.Voucher;
using UnityEngine;

[CreateAssetMenu(fileName = "Voucher Container Data", menuName = "Scriptables/Database/Voucher Container")]
public class VoucherDatabase : ScriptableObject
{
    public VoucherShopContainer voucherShopContainer;
}

[System.Serializable]
public class VoucherShopContainer
{
    public List<VoucherPairData> voucherPairDatas;
    public VoucherShopContainer()
    {
        voucherPairDatas = new List<VoucherPairData>();
    }
}
[System.Serializable]
public class VoucherPairData
{
    public bool isBought;
    public VoucherBoughtData[] voucherBoughtData;

    public VoucherPairData(VoucherPairData data)
    {
        voucherBoughtData = new VoucherBoughtData[data.voucherBoughtData.Length];

        for (int i = 0; i < data.voucherBoughtData.Length; i++)
        {
            voucherBoughtData[i] = new VoucherBoughtData(data.voucherBoughtData[i].voucher);
        }
    }
}
[System.Serializable]
public class VoucherBoughtData
{
    public VoucherBoughtData(VoucherData data)
    {
        voucher = data;
    }
    public VoucherData voucher;
    public bool hasBought;
}
