using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace UBERREP.CommonLayer
{
    public class Functions
    {
        public static bool IsTestSystem
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["IsTestSystem"] == "true";
            }
        }

        public static string Serialize(object rqObject)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = null;
            string retValue = string.Empty;
            try
            {
                List<Type> additionalTypes = new List<Type>();
                xs = new XmlSerializer(rqObject.GetType(), additionalTypes.ToArray());

                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                xs.Serialize(mStream, rqObject, xsn);
                mStream.Position = 0;

                retValue = Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Serialization error. " + ex.Message, ex);
            }
            finally
            {
                mStream.Dispose();
                mStream = null;
                xs = null;
            }
            return retValue;
        }
        public static void Serialize(object o, System.IO.Stream stream)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(o.GetType());
            xs.Serialize(stream, o, ns);
        }


        public static object DeSerialize(System.IO.StringReader reader, Type retType)
        {
            try
            {
                //deserialize
                System.Xml.Serialization.XmlSerializer xs = new XmlSerializer(retType);
                return xs.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("Decoding error. " + ex.Message, ex);
            }
        }

        public static object DeSerialize(System.Xml.XmlReader reader, Type retType)
        {
            try
            {
                //deserialize
                System.Xml.Serialization.XmlSerializer xs = new XmlSerializer(retType);
                return xs.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("Decoding error. " + ex.Message, ex);
            }
        }

        public static object DeSerialize(System.IO.Stream stream,Type retType)
        {
            System.Xml.Serialization.XmlSerializer xs = new XmlSerializer(retType);
            return xs.Deserialize(stream);
        }
        public static object CloneObject(object objectToClone)
        {
            System.Xml.Serialization.XmlSerializer sz = new System.Xml.Serialization.XmlSerializer(objectToClone.GetType());
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();
            sz.Serialize(mStream, objectToClone);
            mStream.Position = 0;
            return sz.Deserialize(mStream);
        }
        public static void WriteToDisk(string data,string Prefix,string Path=null)
        {
            //string UniqueID = DateTime.Now.ToString("hhmmssffff");            
            string logpath = string.IsNullOrEmpty(Path) ? AppDomain.CurrentDomain.BaseDirectory + "Logs\\" : Path;
            string logFolder = logpath + DateTime.Now.ToString("yyyy_MM MMM_dd ddd_").Replace("_", "\\");
            if (!System.IO.Directory.Exists(logFolder)) System.IO.Directory.CreateDirectory(logFolder);
            //string filepath = logFolder + UniqueID + "_" + Prefix + ".xml";
            string filepath = logFolder + Prefix + ".xml";
         
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath))
            {
                file.WriteLine(data);
                file.Close();
            }
        }


        /// <summary>
        /// Encrypts CC# for Db like BIN + XXXXXXXXXX + last 4 digits
        /// </summary>
        /// <param name="CCNumber">Plain Card Number - Decrypted</param>
        /// <returns></returns>
        public static string EncryptCreditCardNumberForDB(string CCNumber)
        {
            string retValue = string.Empty;
            if (!string.IsNullOrEmpty(CCNumber))
            {
                string Decrypted = CCNumber;
                string BIN = Decrypted.Substring(0, 6);

                int midDigits = 6,last4DigitStartsFrom=12;
                if (CCNumber.Length == 15)//for american express 15 chars long
                {
                    midDigits = 5;
                    last4DigitStartsFrom = 11;
                }
                else if (CCNumber.Length == 13)//for Visa's variation like 4222222222222
                {
                    midDigits = 3;
                    last4DigitStartsFrom = 9;
                }

                string EncryptedMidPart = Encrypt(Decrypted.Substring(6, midDigits),EncryptionDataType.CCDataKey);
                //string last4digits = Decrypted.Substring(11);

                string last4digits = Decrypted.Substring(last4DigitStartsFrom);

                retValue = BIN + EncryptedMidPart + last4digits;
            }
            return retValue;
        }

        /// <summary>
        /// Decrypts CC# to plain text from BIN + XXXXXX + last 4 digits
        /// </summary>
        /// <param name="DBEncryptedCCNumber"></param>
        /// <returns></returns>
        public static string DecrypteCreditCardNumberFromDB(string DBEncryptedCCNumber)
        {
            string retValue = string.Empty;
            if (!string.IsNullOrEmpty(DBEncryptedCCNumber))
            {

                string OriginalString = DBEncryptedCCNumber;
                string ReplaceFirst6Digits = DBEncryptedCCNumber.Replace(DBEncryptedCCNumber.Substring(0, 6), "");
                string ReplaceLast4Digits = ReplaceFirst6Digits.Replace(DBEncryptedCCNumber.Substring(DBEncryptedCCNumber.Length - 4), "");                
                string Decrypted = Decrypt(ReplaceLast4Digits,EncryptionDataType.CCDataKey);

                retValue = DBEncryptedCCNumber.Substring(0, 6) + Decrypted + DBEncryptedCCNumber.Substring(DBEncryptedCCNumber.Length - 4);
            }
            return retValue;
        }

        public static string Decrypt(string strText,EncryptionDataType Type)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            byte[] buffer = new byte[strText.Length + 1];
            rgbKey = Encoding.UTF8.GetBytes(getTransactionKey(Type).Substring(0, 8));
            buffer = Convert.FromBase64String(strText);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                }
                provider = null;
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        public static string Encrypt(string strText,EncryptionDataType Type)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            rgbKey = Encoding.UTF8.GetBytes(getTransactionKey(Type).Substring(0, 8));
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(strText);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                }
                provider = null;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        private static string getTransactionKey(EncryptionDataType Type)
        {
            switch (Type)
            {
                case EncryptionDataType.CCDataKey:
                    return "OdyCruise123";
                case EncryptionDataType.CVVDataKey:
                    return "FFCVVData147";
                case EncryptionDataType.DataConnectionKey:
                    return "FFConn769";
                case EncryptionDataType.PassportDataKey:
                    return "OdyCruise123"; //return "FFPassport348";
                case EncryptionDataType.PasswordDataKey:
                    return "OdyFFPW527";
                case EncryptionDataType.TransactionDataKey:
                    return "FFTransaction554";
                case EncryptionDataType.TransactionPaymentDataKey:
                    return "OdyFFPayment664";
                default :
                    return null;
            }            
        }



        public enum EncryptionDataType
        {
            /// <summary>
            /// Passport
            /// </summary>
            PassportDataKey,
            /// <summary>
            /// ClientId, Password, ClientAPIProperites, User Login Password
            /// </summary>
            PasswordDataKey,
            /// <summary>
            /// CC Number
            /// </summary>
            CCDataKey,
            /// <summary>
            /// CVV Data
            /// </summary>
            CVVDataKey,
            /// <summary>
            /// TransactionKey,RefernceNumber[BookingNumber],ConfirmationNumber
            /// </summary>
            TransactionDataKey,
            /// <summary>
            /// ExternalAuthCode, PaymentID,ExternalTransactionNumber, AuthCode
            /// </summary>
            TransactionPaymentDataKey,
            /// <summary>
            /// DataConnectionKey - used SQLHelper.cs
            /// </summary>
            DataConnectionKey

        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
