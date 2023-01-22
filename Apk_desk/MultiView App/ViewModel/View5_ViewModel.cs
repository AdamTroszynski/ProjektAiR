using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace MultiViewApp.ViewModel
{
    using Model;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System;
    using System.Diagnostics;
    using System.Windows.Documents;

    public class View5_ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MeasurementViewModel> Measurements { get; set; }
        public ButtonCommand Refresh { get; set; }

        private ServerIoTmock ServerMock = new ServerIoTmock();

        public View5_ViewModel()
        {
            // Create new collection for measurements data
            Measurements = new ObservableCollection<MeasurementViewModel>();

            // Bind button with action
            Refresh = new ButtonCommand(RefreshHandler);
        }

        //private static readonly HttpClient client = new HttpClient();

        async void RefreshHandler()
        {
            // Read data from server in JSON array format
            // TODO: replace mock with network comunnication

            //String json = await TestAsyncAwaitMethods();
            JArray measurementsJsonArray;
            string json = await Server.GetData();
            try
            {
                measurementsJsonArray = JArray.Parse(json);                        
            }
            catch (Exception e)
            {
                Debug.WriteLine("JSON ERROR");
                Debug.WriteLine(e);

                return;
            }

            // Convert generic JSON array container to list of specific type
            var measurementsList = measurementsJsonArray.ToObject<List<MeasurementModel>>();

            // Add new elements to collection
            if (Measurements.Count < measurementsList.Count)
            {
                foreach (var m in measurementsList)
                    Measurements.Add(new MeasurementViewModel(m));
            }
            // Update existing elements in collection
            else
            {
                for (int i = 0; i < Measurements.Count; i++)
                    Measurements[i].UpdateWithModel(measurementsList[i]);
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * @brief Simple function to trigger event handler
         * @params propertyName Name of ViewModel property as string
         */
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
