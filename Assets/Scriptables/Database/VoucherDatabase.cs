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

    public void SetVoucherBought(VoucherData data)
    {
        foreach (var pair in voucherPairDatas)
        {
            foreach (var voucher in pair.voucherBoughtData)
            {
                if (VoucherData.ReferenceEquals(voucher.voucher, data))
                {
                    Debug.Log(data.voucherName);
                    Debug.Log(voucher.voucher.voucherName);
                    voucher.hasBought = true;
                    Debug.Log(voucher.hasBought);
                }
            }
        }
    }

    public VoucherData GetRandomVoucher()
    {
        List<VoucherPairData> voucherPairData = voucherPairDatas.FindAll(x => x.isComplete == false);

        VoucherPairData selectedVoucher = voucherPairData[Random.Range(0, voucherPairData.Count)];

        for (int i = 0; i < selectedVoucher.voucherBoughtData.Length; i++)
        {
            if (!selectedVoucher.voucherBoughtData[i].hasBought)
            {
                return selectedVoucher.voucherBoughtData[i].voucher;
            }
        }

        return null;
    }
}
[System.Serializable]
public class VoucherPairData
{
    public bool isComplete {get{ return VoucherSetBought(); }}
    public VoucherBoughtData[] voucherBoughtData;

    public VoucherPairData(VoucherPairData data)
    {
        voucherBoughtData = new VoucherBoughtData[data.voucherBoughtData.Length];

        for (int i = 0; i < data.voucherBoughtData.Length; i++)
        {
            voucherBoughtData[i] = new VoucherBoughtData(data.voucherBoughtData[i].voucher);
        }
    }

    public bool VoucherSetBought()
    {
        for (int i = 0; i < voucherBoughtData.Length; i++)
        {
            if (!voucherBoughtData[i].hasBought)
            {
                return false;
            }
        }
        return true;
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
