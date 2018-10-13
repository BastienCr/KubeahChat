﻿/*
 * Kubeah ! Open Source Project
 * 
 * Kubeah Chat
 * Just like Open Source
 * 
 * for more informations about Kubeah Chat
 * Please visit https://github.com/CrBast/KubeahChat
 * 
 * APPLICATION LICENSE
 * GNU General Public License v3.0
 * https://github.com/CrBast/KubeahChat/blob/master/LICENSE
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace KChat.Methods
{
    /// <summary>
    /// XML file manipulation
    /// </summary>
    class XMLManipulation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttributesName"></param>
        /// <returns>String : the result of the query. The result can be null</returns>
        public static string GetValue(string AttributesName)
        {
            if (File.Exists("./App.config") == false)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                CreateAppConfig(dict);
            }
            string resultToReturn = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./App.config");

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
            {
                if (xmlNode.Attributes["name"].Value == AttributesName)
                {
                     resultToReturn = xmlNode.Attributes["value"].Value.ToString();
                }
            }
            return resultToReturn;
        }

        /// <summary>
        /// Deletes the old XML configuration file. This allows you to update it by keeping the old settings
        /// </summary>
        /// <param name="dict">Dictionary containing the old parameters</param>
        private static void CreateAppConfig(Dictionary<string, string> dict)
        {
            XmlWriter xmlWriter = XmlWriter.Create("./App.config");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteAttributeString("name", "EnableLastIpConnexion");

            if (dict.ContainsKey("EnableLastIpConnexion"))
                xmlWriter.WriteAttributeString("value", dict["EnableLastIpConnexion"]);
            else
                xmlWriter.WriteAttributeString("value", "ON");

            xmlWriter.WriteAttributeString("choice", "ON - OFF");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteAttributeString("name", "SaveDiscussion");

            if(dict.ContainsKey("SaveDiscussion"))
                xmlWriter.WriteAttributeString("value", dict["SaveDiscussion"]);
            else
                xmlWriter.WriteAttributeString("value", "");

            xmlWriter.WriteAttributeString("choice", "ON - OFF");
            xmlWriter.WriteAttributeString("info", "Not used yet");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteAttributeString("name", "LastIpConnexion");

            if(dict.ContainsKey("LastIpConnexion"))
                xmlWriter.WriteAttributeString("value", dict["LastIpConnexion"].ToString());
            else
                xmlWriter.WriteAttributeString("value", "");

            xmlWriter.WriteAttributeString("choice", "Example : 192.168.0.2");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteAttributeString("name", "FocusActivate");

            if(dict.ContainsKey("+"))
                xmlWriter.WriteAttributeString("value", dict["FocusActivate"].ToString());
            else
                xmlWriter.WriteAttributeString("value", "ON");

            xmlWriter.WriteAttributeString("choice", "ON - OFF");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("param");
            xmlWriter.WriteAttributeString("name", "NotificationsEnable");

            if(dict.ContainsKey("NotificationsEnable"))
                xmlWriter.WriteAttributeString("value", dict["NotificationsEnable"]);
            else
                xmlWriter.WriteAttributeString("value", "ON");

            xmlWriter.WriteAttributeString("choice", "ON - OFF");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./App.config");
            xmlDoc.Save("./App.config");
        }

        /// <summary>
        /// Modify an existing item in the configuration xml file
        /// </summary>
        /// <param name="name">Name of the value to be changed</param>
        /// <param name="newValue">New value</param>
        public static void ModifyElementXML(string name, string newValue)
        {
            if (File.Exists("./App.config") == false)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                CreateAppConfig(dict);
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./App.config");

            bool verificationParameter = false;
            /* true : The parameter has been entered
             * false : The parameter has not been entered because it does not exist
             * */

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
            {
                if (xmlNode.Attributes["name"].Value == name)
                {
                    xmlNode.Attributes["value"].Value = newValue;
                    verificationParameter = true;
                }
            }
            xmlDoc.Save("./App.config");
            if (verificationParameter == false)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { name, newValue }
                };
                CreateAppConfig(dict);
            }
            
        }

        /// <summary>
        /// Create notification file for the Kubeah_SimpleNotification application
        /// </summary>
        /// <param name="content">Content of the notification</param>
        /// <example>CreateNotifFile("Hello")</example>
        public static void CreateNotifFile(string content)
        {
            try
            {
                // Creating a "notification" file for the Kubeah_SimpleNotification application 
                string title = DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss");
                XmlWriter xmlWriter = XmlWriter.Create($"./App/notifications/{title}.xml");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("param");
                xmlWriter.WriteStartElement("param");
                xmlWriter.WriteAttributeString("name", "content");
                xmlWriter.WriteAttributeString("value", $"{RemoveInvalidCharacters(content)}");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load($"./App/notifications/{title}.xml");
                xmlDoc.Save($"./App/notifications/{title}.xml");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

        }

        // https://sandeep-tada.blogspot.com/2014/02/c-systemargumentexception-hexadecimal_11.html

        private static string RemoveInvalidCharacters(string str)
        {
            if (str == null) return null;

            var formattedStr = new StringBuilder();

            foreach (var ch in str)
            {
                if ((ch < 0x00FD && ch > 0x001F) || ch == '\t' || ch == '\n' || ch == '\r')
                {
                    formattedStr.Append(ch);
                } 
            }
            return formattedStr.ToString();
        }
    }
}
