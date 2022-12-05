using System;
using System.ComponentModel;

namespace DataGridXamarin
{
    public class OrderInfo : INotifyPropertyChanged
    {
        public OrderInfo()
        {
        }

        #region private variables

        private string _orderID;
        private string _customerName;
        private string _customerID;
        private string _gender;


        #endregion

        #region Public Properties

        public string OrderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                this._orderID = value;
                RaisePropertyChanged("OrderID");
            }
        }

        public string CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                this._customerID = value;
                RaisePropertyChanged("CustomerID");
            }
        }

        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                this._customerName = value;
                RaisePropertyChanged("CustomerName");
            }
        }


        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                this._gender = value;
                RaisePropertyChanged("Gender");
            }
        }


        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String Name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        #endregion
    }
}
