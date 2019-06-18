/*
Тема роботи: "Алгоритмізація прогнозування часу прибуття пасажирського транспорту".
Розробив: стужент групи СА-12 Іванчишин Данило Ярославович.
Дата створення: 12 березня 2019 року
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;


namespace Map_v1._0_project_team7_sa
{
    public partial class Form1 : Form
    {
        //оголошення списку для зберігання координат точок
        public List<PointLatLng> _points;

        public Form1()
        {
            //створення нової карти
            map = new GMapControl();
            InitializeComponent();
            //ініціалізація нового списку
            _points = new List<PointLatLng>();            
        }
        /*
        txtLat, txtLong - текст-бокси
        point - точка з параметрами lat i lng
        */
        private void btnLoadIntoMap_Click(object sender, EventArgs e)
        {
            //створення нової точки з параметрами lng і lat (довгота і широта)
            var point = new PointLatLng(Convert.ToDouble(txtLat.Text), Convert.ToDouble(txtLong.Text));
            //фокусування карти на заданій точці
            LoadMap(point);
            //відображення точки на карті
            AddMarker(point);
            map.Refresh();
            map.Update();
        }

        private void LoadMap(PointLatLng point)
        {
            //переміщення курсору у вказану точку
            map.Position = point;
        }

        private void AddMarker(PointLatLng pointToAdd, GMarkerGoogleType markerType = GMarkerGoogleType.arrow)
        {
            var markers = new GMapOverlay("markers");
            var marker = new GMarkerGoogle(pointToAdd, markerType);
            markers.Markers.Add(marker);
            //додавання нового маркера
            map.Overlays.Add(markers);
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            //додавання точки в список
            _points.Add(new PointLatLng(Convert.ToDouble(txtLat.Text),
                Convert.ToDouble(txtLong.Text)));           
        }

        private void btnGetRoute_Click(object sender, EventArgs e)
        {
            double l = 0;
            MapRoute route;
            GMapRoute r;
            GMapOverlay routes;
            for (int i = 0; i < _points.Count-1; i++)
            {
                //первірка умови наявності наступної точки в списку
                if (_points[i + 1] != null)
                {
                    //побудова маршруту
                    route = GoogleMapProvider.Instance
                    .GetRoute(_points[i], _points[i + 1], false, false, 14);

                    //відображення маршруту на карті
                    r = new GMapRoute(route.Points, "My Route")
                    {
                        Stroke = new Pen(Color.Blue, 4)
                    };

                    routes = new GMapOverlay("routes");
                    routes.Routes.Add(r);
                    map.Overlays.Add(routes);
                    //розрахунок довжини шляху
                    l += route.Distance;
                }
            }
            label4.Text = l + @" Km";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = "AIzaSyB_ZBxpV6axQxnIyDSf23lyg54w_vprPOk";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.DragButton = MouseButtons.Left;            
            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.SetPositionByKeywords("Chennai, India");
            map.Zoom = 40;
            map.MaxZoom = 50;
            map.MinZoom = 0;
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            //очищення списку
            _points.Clear();
            map.Update();
        }

        private List<String> GetAddress(PointLatLng point)
        {
            List<Placemark> placemarks = null;
            var statusCode = GMapProviders.GoogleMap.GetPlacemarks(point, out placemarks);
            if (statusCode == GeoCoderStatusCode.OK && placemarks != null)
            {
                //отримання адреси кожної точки списку
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

                //завантаження позиції
                LoadMap(point);
                //додавання маркера                 
                AddMarker(point);

                //отримання адреси
                var addresses = GetAddress(point);
                map.Refresh();
                //відображення адреси
                if (addresses != null)
                    label5.Text = "Адреса: \n-----------------------\n" + addresses[0];
                else
                    label5.Text = "Неможливо завантажити адресу.";
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (map.Overlays.Count > 0)
            {
                //очищення карти від візуальних компонентів
                int n = map.Overlays.Count;
                map.Overlays.RemoveAt(n-1);
                map.Refresh();
            }
        }
    }
}    

