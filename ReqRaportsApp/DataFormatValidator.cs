namespace ReqRaportsApp
{
    public class DataFormatValidator
    {
        public bool isRequestFormatCorrect { get; set; }
        public string errMessage { get; set; }

        public DataFormatValidator(bool irfc, string em, request r)
        {
            isRequestFormatCorrect = irfc;
            errMessage = em;

            if (r.clientId.Length > 6 || r.clientId.Contains(" "))
            {
                isRequestFormatCorrect = false;
                errMessage = "Zły format identyfikatora klienta w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
            else if (r.clientId == null || r.clientId == string.Empty)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak identyfikatora klienta w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
            else if (r.name == null || r.name == string.Empty)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak nazwy produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
            else if (r.name.Length > 255)
            {
                isRequestFormatCorrect = false;
                errMessage = "Za długa nazwa produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
            else if (r.price == 0)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak ceny produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
            else if (r.quantity == 0)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak ilości produktu w zamówieniu \"" + r.requestId.ToString() + "\" klienta \"" + r.clientId + "\"";
                return;
            }
        }
    }
}
