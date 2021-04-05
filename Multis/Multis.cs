using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Multis
{
    public static class Multis
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";


        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
        public static void InsertToXmlFile(string fileName, string userid, string messagesend, string timersend, string receiverid)
        {
            try
            {


                if (File.Exists(fileName))
                {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(fileName);
                        if (doc.SelectSingleNode("root") == null)
                        {
                        XElement xml_pre = XElement.Load(fileName);
                        xml_pre.Add(new XElement("message"));
                        xml_pre.Save(fileName);
                        }
                        XElement xml = XElement.Load(fileName);
                        xml.Add(new XElement("doc",
                        new XElement("message",
                        new XAttribute("userid", userid),
                        new XAttribute("messagesend", messagesend),
                        new XAttribute("timersend", timersend),
                        new XAttribute("receiverid", receiverid))));
                        xml.Save(fileName);
                }
                else
                {
                    XDocument doc =
                    new XDocument(new XElement("root",
                    new XElement("doc",
                    new XElement("message",
                    new XAttribute("userid", userid),
                    new XAttribute("messagesend", messagesend),
                    new XAttribute("timersend", timersend),
                    new XAttribute("receiverid", receiverid)
                    )
                )));
                    doc.Save(fileName, SaveOptions.None);

                }



            }
            catch (Exception e)
            {

            }

        }
    }
}
