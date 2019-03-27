namespace ReqRaportsApp
{
    public class RaportTypes
    {
        public const string ReqQuantType = "Ilość zamówień";
        public const string ReqQuantForClientType = "Ilość zamówień dla klienta o wskazanym identyfikatorze";
        public const string ReqValueSumType = "Łączna kwota zamówień";
        public const string ReqValueSumForClientType = "Łączna kwota zamówień dla klienta o wskazanym identyfikatorze";
        public const string AllReqsListType = "Lista wszystkich zamówień";
        public const string AllReqsListForClientType = "Lista zamówień dla klienta o wskazanym identyfikatorze";
        public const string AverageReqValueType = "Średnia wartość zamówienia";
        public const string AverageReqValueForClientType = "Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze";
        public const string ReqQuantByProdNameType = "Ilość zamówień pogrupowanych po nazwie";
        public const string ReqQuantByProdNameForClientType = "Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze";
        public const string ReqsInValueRangeType = "Zamówienia w podanym przedziale cenowym";

        public static string[] dropListItemsList =
        {
            ReqQuantType,
            ReqQuantForClientType,
            ReqValueSumType,
            ReqValueSumForClientType,
            AllReqsListType,
            AllReqsListForClientType,
            AverageReqValueType,
            AverageReqValueForClientType,
            ReqQuantByProdNameType,
            ReqQuantByProdNameForClientType,
            ReqsInValueRangeType
        };

        public static string[] clientIdRaportsList =
        {
                ReqQuantForClientType,
                ReqValueSumForClientType,
                AllReqsListForClientType,
                AverageReqValueForClientType,
                ReqQuantByProdNameForClientType,
        };
    }
}
