using System;

namespace SaleService.Domain
{
    public class Sale
    {
        private int _id;
        private int _artworkId;
        private int _employeeId;
        private DateTime _saleDate;
        private double _price;

        public Sale()
        {
            this._id = 0;
            this._artworkId = 0;
            this._employeeId = 0;
            this._saleDate = DateTime.MinValue;
            this._price = 0.0;
        }

        public Sale(int id, int artworkId, int employeeId, DateTime saleDate, double price)
        {
            this._id = id;
            this._artworkId = artworkId;
            this._employeeId = employeeId;
            this._saleDate = saleDate;
            this._price = price;
        }

      
        public Sale(Sale sale)
        {
            this._id = sale._id;
            this._artworkId = sale._artworkId;
            this._employeeId = sale._employeeId;
            this._saleDate = sale._saleDate;
            this._price = sale._price;
        }

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public int ArtworkId
        {
            get { return this._artworkId; }
            set { this._artworkId = value; }
        }

        public int EmployeeId
        {
            get { return this._employeeId; }
            set { this._employeeId = value; }
        }

        public DateTime SaleDate
        {
            get { return this._saleDate; }
            set { this._saleDate = value; }
        }

        public double Price
        {
            get { return this._price; }
            set { this._price = value; }
        }
    }
}
