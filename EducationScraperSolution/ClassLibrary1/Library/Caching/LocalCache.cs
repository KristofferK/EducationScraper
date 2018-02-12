using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EducationScrapers.Library.Caching
{
    public abstract class LocalCache
    {
        private string folder = AppContext.BaseDirectory + @"caching\";
        protected string identifier;
        protected string filepath;
        protected string prefix;

        public LocalCache(string prefix, string identifier)
        {
            this.prefix = prefix;
            this.identifier = identifier;
            filepath = GetFilePath(identifier);
            if (!string.IsNullOrWhiteSpace(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public string[] GetFileList()
        {
            return Directory.GetFiles(folder, prefix + "*.json").Select(
                e => new FileInfo(e).Name.Substring(prefix.Length).Split('.')[0]
            ).ToArray();
        }


        protected JArray FileToJArray(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    var fileContent = File.ReadAllText(path);
                    return JArray.Parse(fileContent);
                }
            }
            catch { }
            return null;
        }


        protected JObject FileToJObject(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    var fileContent = File.ReadAllText(path);
                    return JObject.Parse(fileContent);
                }
            }
            catch { }
            return null;
        }

        protected string GetFilePath(string identifier)
        {
            return $"{folder}{prefix}{identifier}.json";
        }

        public bool Flush()
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                return true;
            }
            return false;
        }
    }
}
