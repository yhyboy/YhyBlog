using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common
{
  public  static class ConfigHelper
    {
        /// <summary>
        /// 通过Name获取值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetXmlValueByName(string path, string name)
        {
            XElement xe = XElement.Load(path);
            XElement ele = xe.Element(name);
            if (ele != null)
            {
                return ele.Value;
            }
            return null;
        }



      /// <summary>
      /// 设置Value
      /// </summary>
      /// <param name="path"></param>
      /// <param name="name"></param>
      /// <param name="value"></param>
        public static void SetXmlValueByName(string path, string name,string value)
        {
            XElement xe = XElement.Load(path);
            XElement ele = xe.Element(name);
            ele.SetValue(value);
            xe.Save(path);       
        }
      /// <summary>
      /// 获得节点树
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
        public static XElement GetXmlElement(string path, string name)
        {
            XElement xe = XElement.Load(path);
            XElement ele = xe.Element(name);
            return ele;
        }

    }
}
