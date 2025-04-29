namespace SaleService.Domain
{
    public class SaleID
    {
        private int _saleId;

        public SaleID()
        {
            this._saleId = 0;
        }

        public SaleID(int saleId)
        {
            this._saleId = saleId;
        }

        public SaleID(SaleID saleId)
        {
            this._saleId = saleId._saleId;
        }

        public int Id
        {
            get { return this._saleId; }
            set { this._saleId = value; }
        }
    }
}