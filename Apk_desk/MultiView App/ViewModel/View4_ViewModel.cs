#define CLIENT
#define GET
//#define DYNAMIC

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Newtonsoft.Json;

namespace MultiViewApp.ViewModel
{
    using Model;
    using Newtonsoft.Json.Linq;
    using System.Windows.Controls;
    using System.Windows;

    public class View4_ViewModel : INotifyPropertyChanged
    {
        // Moje własne
        private float m_currTime = 0.0f;
        private static int m_sampleTime = 500;

        #region Properties
        private string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }
        private int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public PlotModel DataPlotModel { get; set; }
        public ButtonCommand StartButton { get; set; }
        public ButtonCommand StopButton { get; set; }
        public ButtonCommand UpdateConfigButton { get; set; }
        public ButtonCommand DefaultConfigButton { get; set; }
        #endregion

        #region Fields
        private int timeStamp = 0;
        private ConfigParams config = new ConfigParams();
        private Timer RequestTimer;
        //private ServerIoT ServerIoT;
        #endregion

        public View4_ViewModel()
        {
            DataPlotModel = new PlotModel { Title = "Angle data" };

            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            //pierwsze dane
            DataPlotModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 360,
                Key = "Vertical",
                Unit = "*deg",
                Title = "Angle"
            });

            //pierwsze dane
            DataPlotModel.Series.Add(new LineSeries() { Title = "pitch data series", Color = OxyColor.Parse("#FFFF0000") });
            DataPlotModel.Series.Add(new LineSeries() { Title = "roll data series", Color = OxyColor.Parse("#FF00FF00") });
            DataPlotModel.Series.Add(new LineSeries() { Title = "yaw data series", Color = OxyColor.Parse("#FF0000FF") });

            StartButton = new ButtonCommand(StartTimer);
            StopButton = new ButtonCommand(StopTimer);
            UpdateConfigButton = new ButtonCommand(UpdateConfig);
            DefaultConfigButton = new ButtonCommand(DefaultConfig);

            ipAddress = Server.GetIp();
            config.SampleTime = m_sampleTime;
            sampleTime = config.SampleTime;
        }

        /**
          * @brief Time series plot update procedure.
          * @param t X axis data: Time stamp [ms].
          * @param d Y axis data: Real-time measurement [-].
          */
        private void UpdatePlot(double time, double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                LineSeries lineSeries = DataPlotModel.Series[i] as LineSeries;
                lineSeries.Points.Add(new DataPoint(time, data[i]));

                if (lineSeries.Points.Count > config.MaxSampleNumber)
                    lineSeries.Points.RemoveAt(0);

                if (time >= config.XAxisMax)
                {
                    DataPlotModel.Axes[0].Minimum = (time - config.XAxisMax);
                    DataPlotModel.Axes[0].Maximum = time + config.SampleTime / 1000.0; ;
                }
            }

            DataPlotModel.InvalidatePlot(true);
        }

        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */

        /**
          * @brief Synchronous procedure for request queries to the IoT server.
          * @param sender Source of the event: RequestTimer.
          * @param e An System.Timers.ElapsedEventArgs object that contains the event data.
          */
        private async void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            string json = await Server.GetData();
            JArray measurements;
            try
            {
                measurements = JArray.Parse(json);
            }
            catch (Exception ex)
            {                
                Debug.WriteLine("JSON ERROR");
                Debug.WriteLine(ex);

                return;
            }

            double[] data = { (double)measurements[0]["value"], (double)measurements[1]["value"], (double)measurements[2]["value"] };
            UpdatePlot(m_currTime, data);
            m_currTime += (float)config.SampleTime / 1000.0f;

            //UpdatePlotWithServerResponse();
        }

        #region ButtonCommands

        /**
         * @brief RequestTimer start procedure.
         */
        private void StartTimer()
        {
            if (RequestTimer == null)
            {
                RequestTimer = new Timer(config.SampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;

                DataPlotModel.ResetAllAxes();
            }
        }

        /**
         * @brief RequestTimer stop procedure.
         */
        private void StopTimer()
        {
            if (RequestTimer != null)
            {
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
        }

        /**
         * @brief Configuration parameters update
         */
        private void UpdateConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams(ipAddress, sampleTime);

            m_sampleTime = config.SampleTime;
            Server.SetUrls(ipAddress);

            if (restartTimer)
                StartTimer();
        }

        /**
          * @brief Configuration parameters defualt values
          */
        private void DefaultConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams();
            IpAddress = config.IpAddress;
            SampleTime = config.SampleTime.ToString();
            //ServerIoT = new ServerIoT("HTTPS", IpAddress);

            if (restartTimer)
                StartTimer();
        }

        #endregion

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

