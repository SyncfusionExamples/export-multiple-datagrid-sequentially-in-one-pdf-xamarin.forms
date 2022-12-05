using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataGridXamarin
{
    public class OrderInfoRepository : NotificationObject
    {
        private ObservableCollection<OrderInfo> orderDetails;
        public OrderInfoRepository()
        {
            
        }

        #region private variables

        private Random random = new Random();

        #endregion

        #region GetOrderDetails

        public ObservableCollection<OrderInfo> GetOrderDetails(int count)
        {
            orderDetails = new ObservableCollection<OrderInfo>();
            for (int i = 1; i <= count; i++)
            {

                var ord = new OrderInfo()
                {
                    OrderID = i.ToString(),
                    CustomerName = CustomerNames[random.Next(15)],
                    CustomerID = random.Next(1700, 1800).ToString(),
                    Gender = Genders[random.Next(5)],
                };
                orderDetails.Add(ord);
            }
            return orderDetails;
        }

        #endregion

        public class NotificationObject : INotifyPropertyChanged
        {
            public void RaisePropertyChanged(string propName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        #region MainGrid DataSource

        string[] Genders = new string[] {
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Female",
            "Female",
            "Female",
            "Male",
            "Male",
            "Male",
            "Male"
        };



        string[] CustomerNames = new string[] {
            "Alfki",
            "Frans",
            "Merep",
            "Folko",
            "Simob",
            "Warth",
            "Vaffe",
            "Furib",
            "Seves",
            "Linod",
            "Riscu",
            "Picco",
            "Blonp",
            "Welli",
            "Folig"
        };

        #endregion
    }
}
