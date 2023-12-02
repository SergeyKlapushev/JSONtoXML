using System;
using System.Text.Json;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        // Произвольный JSON
        string json = "{ \"name\": \"Sergey\", \"age\": 27, \"city\": \"Yaroslavl'\" }";


        JsonDocument jsonDoc = JsonDocument.Parse(json);

        XmlDocument xmlDoc = new XmlDocument();

        XmlElement rootElement = xmlDoc.CreateElement("root");
        xmlDoc.AppendChild(rootElement);

        ConvertJsonToXml(jsonDoc.RootElement, rootElement, xmlDoc);

        xmlDoc.Save("ThisFile.xml");

        Console.WriteLine("Конвертация завершена.");
    }

    static void ConvertJsonToXml(JsonElement jsonElement, XmlElement xmlElement, XmlDocument xmlDoc)
    {
        foreach (JsonProperty property in jsonElement.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.Object)
            {
               
                XmlElement newXmlElement = xmlDoc.CreateElement(property.Name);
                xmlElement.AppendChild(newXmlElement);

                ConvertJsonToXml(property.Value, newXmlElement, xmlDoc);
            }
            else if (property.Value.ValueKind == JsonValueKind.Array)
            {
               
                foreach (JsonElement arrayElement in property.Value.EnumerateArray())
                {
                    XmlElement newXmlElement = xmlDoc.CreateElement(property.Name);
                    xmlElement.AppendChild(newXmlElement);

                    ConvertJsonToXml(arrayElement, newXmlElement, xmlDoc);
                }
            }
            else
            {
                XmlElement newXmlElement = xmlDoc.CreateElement(property.Name);
                newXmlElement.InnerText = property.Value.ToString();
                xmlElement.AppendChild(newXmlElement);
            }
        }
    }
}