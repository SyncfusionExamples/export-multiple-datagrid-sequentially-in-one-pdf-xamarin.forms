using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridXamarin
{
    public class ViewModel : NotificationObject
    {
        OrderInfoRepository order;
        public ViewModel()
        {
            order = new OrderInfoRepository();
            SetRowstoGenerate();
        }

       
        #region ItemsSource

        private ObservableCollection<OrderInfo> ordersInfo;

        public ObservableCollection<OrderInfo> OrdersInfo
        {
            get { return ordersInfo; }
            set
            {
                this.ordersInfo = value;
                RaisePropertyChanged("OrdersInfo");
            }
        }
        private ObservableCollection<OrderInfo> ordersInfo1;

        public ObservableCollection<OrderInfo> OrdersInfo1
        {
            get { return ordersInfo1; }
            set
            {
                this.ordersInfo1 = value;
                RaisePropertyChanged("OrdersInfo1");
            }
        }
        #endregion

        #region ItemSource Generator

        public void SetRowstoGenerate()
        {
            ordersInfo = order.GetOrderDetails(10);
            ordersInfo1 = order.GetOrderDetails(50);
        }

        #endregion
    }

    public class NotificationObject : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

