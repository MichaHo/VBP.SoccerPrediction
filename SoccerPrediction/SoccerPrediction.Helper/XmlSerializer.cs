using System;
using System.IO;

namespace SoccerPrediction.Helper
{
    /// <summary>
    /// Der XMLSerializer serialisiert eine beliebige Klasse welche auch Primitive Datentypen oder solche welche als Serialisiert gekennzeichnet sind beinhalten
    /// Klasse ist generisch aufgebaut und gibt den selben Type von Klasse zurück welche ihm übergeben wird.
    /// </summary>
    /// 
    public class XmlSerializer
    {

        public void Serialize<T>(string path, T instance)
        {
            try
            {
                string dirName = Path.GetDirectoryName(path);
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);

                File.Delete(path);

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    SaveToStream(fs, instance);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Serialisiert eine Klasse über einen Stream
        /// </summary>
        /// <typeparam name="T">Den Klassentyp angeben</typeparam>
        /// <param name="stream">Der Stream welcher die Daten enthält (Filestream, MemoryStream,...)</param>
        /// <param name="oClass">Die Instanz der Klasse welche serialisiert werden soll</param>
        public void SaveToStream<T>(Stream stream, T oClass)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                x.Serialize(stream, oClass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Deserialisiert eine XML in eine Klasseninstanz - Die Instanz muss also nicht erstellt sein.
        /// </summary>
        /// <typeparam name="T">Der Typ der Klasse welche erwartet wird.</typeparam>
        /// <param name="path">Der Pfad zur XML-Datei inkl. Dateiendung</param>
        /// <param name="defaultInstance">Die Instanz der Klasse falls die Datei noch nicht Existiert oder nicht gefunden werden kann</param>
        /// <returns>Gibt die Deserialissierte Klasse zurück</returns>
        public T DeSerialize<T>(string path, T defaultInstance)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return defaultInstance;
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    return LoadFromStream<T>(fs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Läd ein Klasse generisch über einen Stream, Methode ist gut geeignet für UnitTests
        /// </summary>
        /// <typeparam name="T">Der Typ der Klasse welche erwartet wird</typeparam>
        /// <param name="stream">Der Stream welcher die Daten enthält (Filestream, MemoryStream,...)</param>
        /// <returns>Gibt die Deserialisierte Klasse zurück</returns>
        public T LoadFromStream<T>(Stream stream)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)x.Deserialize(stream);
        }

    }
}
