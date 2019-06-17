using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace Map_v1._0_project_team7_sa
{
    public partial class Form1 : Form
    {
        public List<PointLatLng> _points;

        public Form1()
        {
            map = new GMapControl();
            InitializeComponent();
            _points = new List<PointLatLng>();

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.mapquestapi.com/directions/v2/route?key=KEY&from=Denver%2C+CO&to=Boulder%2C+CO&outFormat=json&ambiguities=ignore&routeType=fastest&doReverseGeocode=false&enhancedNarrative=false&avoidTimedConditions=false");
            //string authInfo = "hrystynavladyka18:ms63hYgsGEq62dnX";
            //authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            //request.Headers.Add("Authorization", "Basic " + authInfo);
            //request.Credentials = new NetworkCredential("hrystynavladyka18", "ms63hYgsGEq62dnX");
            //request.Method = WebRequestMethods.Http.Get;
            //request.AllowAutoRedirect = true;
            //request.Proxy = null;
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader streamreader = new StreamReader(stream);
            //string s = streamreader.ReadToEnd();
            //button5.Text = s;


            //OkHttpClient client = new OkHttpClient();

            //Request request = new Request.Builder()
            //        .url("https://graphhopper.com/api/1/route?point=51.131,12.414&point=48.224,3.867&vehicle=car&locale=de&calc_points=false&key=api_key")
            //        .get()
            //        .build();

            //Response response = client.newCall(request).execute();
        }

        private void btnLoadIntoMap_Click(object sender, EventArgs e)
        {
            var point = new PointLatLng(Convert.ToDouble(txtLat.Text), Convert.ToDouble(txtLong.Text));
            LoadMap(point);
            AddMarker(point);
            map.Refresh();
            map.Update();
        }

        private void LoadMap(PointLatLng point)
        {
            map.Position = point;
        }

        private void AddMarker(PointLatLng pointToAdd, GMarkerGoogleType markerType = GMarkerGoogleType.arrow)
        {
            var markers = new GMapOverlay("markers");
            var marker = new GMarkerGoogle(pointToAdd, markerType);
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            _points.Add(new PointLatLng(Convert.ToDouble(txtLat.Text),
                Convert.ToDouble(txtLong.Text)));           
        }

        private void btnGetRoute_Click(object sender, EventArgs e)
        {
            //double l = 0;
            //MapRoute route;
            //GMapRoute r;
            //GMapOverlay routes;
            //for (int i = 0; i < _points.Count; i++)
            //{
            //    if (_points[i + 1] != null)
            //    {
            //        route = GoogleMapProvider.Instance
            //        .GetRoute(_points[i], _points[i + 1], false, false, 14);

            //        r = new GMapRoute(route.Points, "My Route")
            //        {
            //            Stroke = new Pen(Color.Blue, 4)
            //        };

            //        routes = new GMapOverlay("routes");
            //        routes.Routes.Add(r);
            //        map.Overlays.Add(routes);
            //        l += route.Distance;
            //    }
            //}

            var route = GoogleMapProvider.Instance
               .GetRoute(_points[0], _points[1], false, false, 14);


            var r = new GMapRoute(route.Points, "My Route")
            {
                Stroke = new Pen(Color.Blue, 4)
            };


            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            map.Overlays.Add(routes);
            map.Refresh();
            map.Update();
            map.Zoom--;
            map.Zoom++;
            label4.Text = route.Distance + @" Km";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = "AIzaSyB_ZBxpV6axQxnIyDSf23lyg54w_vprPOk";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.DragButton = MouseButtons.Left;
            //map.Update();
            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.SetPositionByKeywords("Chennai, India");
            map.Zoom = 30;
            map.MaxZoom = 40;
            map.MinZoom = 0;
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            _points.Clear();
            map.Update();
        }

        private List<String> GetAddress(PointLatLng point)
        {
            List<Placemark> placemarks = null;
            var statusCode = GMapProviders.GoogleMap.GetPlacemarks(point, out placemarks);
            if (statusCode == GeoCoderStatusCode.OK && placemarks != null)
            {
                List<String> addresses = new List<string>();
                foreach (var placemark in placemarks)
                {
                    addresses.Add(placemark.Address);
                }
                return addresses;
            }
            return null;
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var point = map.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lng = point.Lng;

                txtLat.Text = lat + "";
                txtLong.Text = lng + "";

                // Load Location
                LoadMap(point);

                // Adding Marker
                AddMarker(point);

                // Get Address
                var addresses = GetAddress(point);
                map.Refresh();
                // Display Address
                if (addresses != null)
                    label5.Text = "Address: \n-----------------------\n" + addresses[0];
                else
                    label5.Text = "Unable To Load Address";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (map.Overlays.Count > 0)
            {
                int n = map.Overlays.Count;
                map.Overlays.RemoveAt(n-1);
                map.Refresh();
            }
        }
               
    }
}    

