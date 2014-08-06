using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBERREP.CommonLayer
{
    [Serializable]
    public class GenericProperties : Dictionary<string, Object>, System.Xml.Serialization.IXmlSerializable
    {
        [System.Xml.Serialization.XmlIgnore]
        internal new Object this[string key]
        {
            get
            {
                return this.ContainsKey(key) ? base[key] : null;
            }
            set
            {
                if (this.ContainsKey(key)) base[key] = value; else base.Add(key, value);
            }
        }
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        public void ReadXml(System.Xml.XmlReader reader)
        {
            System.Xml.Serialization.XmlSerializer xs = null;
            while (reader.Read())
            {
                if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
                {
                    reader.ReadEndElement();
                    continue;
                }
                if (!string.IsNullOrEmpty(reader.GetAttribute("Type")))
                {
                    try
                    {
                        string key = reader.Name;
                        string type = reader.GetAttribute("Type");
                        xs = new System.Xml.Serialization.XmlSerializer(Type.GetType(type));
                        // go inside wrapper as we need to deserialize that object
                        if (reader.Read())
                        {
                            object o = xs.Deserialize(reader.ReadSubtree());
                            this.Add(key, o);
                            reader.ReadEndElement(); // read end element of wrapper
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("De-Serialization error " + ex.Message, ex);
                    }
                }
                else
                {
                    this.Add(reader.Name, reader.ReadString());
                }
            }
            if (reader.NodeType == System.Xml.XmlNodeType.EndElement) reader.ReadEndElement();
        }
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (string key in this.Keys)
            {
                if (this[key] != null)
                {
                    if (this[key].GetType().IsValueType || this[key].GetType() == typeof(string))
                    {
                        writer.WriteElementString(key, this[key].ToString());
                    }
                    else
                    {
                        System.Xml.Serialization.XmlSerializer xs = null;
                        try
                        {
                            System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                            ns.Add("", "");
                            xs = new System.Xml.Serialization.XmlSerializer(this[key].GetType());
                            writer.WriteStartElement(key);
                            writer.WriteAttributeString("Type", this[key].GetType().AssemblyQualifiedName);
                            xs.Serialize(writer, this[key], ns);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Serialization error " + ex.Message, ex);
                        }
                        finally
                        {
                            writer.WriteEndElement();
                            xs = null;
                        }
                    }
                }
            }
        }

        //internal new Object this[UBERREP.API.APIPropertyTypes type]
        //{
        //    get { return this[type.ToString()]; }
        //}
    }
}
