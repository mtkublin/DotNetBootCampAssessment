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
                errMessage = "Zły format identyfikatora klienta";
            }
            else if (r.clientId == null)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak identyfikatora klienta";
            }
            else if (r.name == null)
            {
                isRequestFormatCorrect = false;
                errMessage = "Brak nazwy produktu";
            }
            else if (r.name.Length > 255)
            {
                isRequestFormatCorrect = false;
                errMessage = "Zły format nazwy produktu";
            }
        }
    }
}
