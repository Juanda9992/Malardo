using System.Collections.Generic;
using Shop.Voucher;
using UnityEngine;

[CreateAssetMenu(fileName = "Voucher Container Data",menuName = "Scriptables/Database/Voucher Container")]
public class VoucherDatabase : ScriptableObject
{
    public List<VoucherPairData> voucherPairDatas;
}
[System.Serializable]
public class VoucherPairData
{
    public bool isBought;
    public VoucherBoughtData[] voucherBoughtData;
}
[System.Serializable]
public class VoucherBoughtData
{
    public VoucherData voucher;
    public bool hasBought;
}
