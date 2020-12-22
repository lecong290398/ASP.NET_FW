using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SettingDm
    {
        public class Settings_GetAllSettingsOutput
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public bool? IsManual { get; set; }
            public string Id2 { get; set; }
            public int RowStatus { get; set; }
            public string ImageSlug { get; set; }
        }

        public class Settings_Edit
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }

        }
    }
}
