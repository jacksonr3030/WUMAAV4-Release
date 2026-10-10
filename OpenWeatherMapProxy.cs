using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace UWP_WeatherAppTest1
{
    public class OpenWeatherMapProxy
    {
        //在返回值前加上一个Task就能确保返回值一定是一个RootObject类型。
        public async static Task<RootObject> GetWeather(String City)
        {
            //生成一个Http访问对象
            var http = new HttpClient();
            //通过设置URL为访问对象赋值，并得到该页面对象response
            var response = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?" +
                "q=Melbourne&units=metric&APPID=97066f70dbb3d235a5ce9bf33da71f20");
            //把网页的内容转换成string字符串 
            var result = await response.Content.ReadAsStringAsync();
            //把拿到的json字符串转换为RootObject对象
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            //把接收到的数据流放到内存中
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //得到最终的数据
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }
    //为每个类添加额外的属性，告诉serializer对象把以下内容看作“类”和“对象”。
    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }
    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public int pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
    }

    //public class Wind
    //{
    //    public int speed { get; set; }
    //    public int deg { get; set; }
    //}

    //public class Clouds
    //{
    //    public int all { get; set; }
    //}
    [DataContract]
    public class Sys
    {
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public int visibility { get; set; }
        //public Wind wind { get; set; }
        //public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }
}
