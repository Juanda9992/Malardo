using UnityEngine;
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    public CardColorDatabase cardColorDatabase;
    public PackDatabase shopPacksDatabase;
    public CardSpriteDatabase cardSpriteDatabase;
    public PlanetCardsDatabase planetCardsDatabase;
    public TarotCardDatabase tarotCardDatabase;
    public JokerListContainer jokerContainer;
    public PricesDatabase pricesDatabase;
    [SerializeField] private VoucherDatabase voucherDatabase;

    public VoucherShopContainer matchVoucherDatabase;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        matchVoucherDatabase = new VoucherShopContainer();

        foreach (var data in voucherDatabase.voucherShopContainer.voucherPairDatas)
        {
            matchVoucherDatabase.voucherPairDatas.Add(new VoucherPairData(data));
        }
    }

}
