using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace Domain
{
    public class MultiLangString : BaseEntity
    {
        private static string _defaultCulture = "et";
        
        [MaxLength(10240)]
        [Required]
        public string Value { get; set; }

        public ICollection<Translation> Translations { get; set; }

        public MultiLangString()
        {
            
        }

        //use current UI culture
        public MultiLangString(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
        {
        }

        public MultiLangString(string value, string culture) : this(value, culture, value)
        {
        }

        public MultiLangString(string value, string culture, string defaultValue)
        {
            Value = defaultValue;
            SetTranslation(value, culture);
        }
        
        public void SetTranslation(string value)
        {
            SetTranslation(value, Thread.CurrentThread.CurrentUICulture.Name);
        }

        public void SetTranslation(string value, string culture)
        {
            culture = culture.Substring(0, 2).ToLower();
            
            if (Translations == null)
            {
                Translations = new List<Translation>();
            }

            var found = Translations.FirstOrDefault(translation => translation.Culture.ToLower().Equals(culture));
            if (found == null)
            {
                Translations.Add(new Translation(culture, value));
            }
            else
            {
                found.Value = value;
            }

            if (culture == _defaultCulture.Substring(0, 2).ToLower())
            {
                Value = value;
            }

        }

        public string Translate(string culture = "")
        {
            if (string.IsNullOrWhiteSpace(culture) && culture.Length == 1) throw new Exception("Illegal lang");
            if (string.IsNullOrWhiteSpace(culture))
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }
            culture = culture.Substring(0, 2).ToLower();
            
            var translation = Translations.FirstOrDefault(t => t.Culture.StartsWith(culture));

            return translation == null ? Value : translation.Value;
        }
    }
    
}