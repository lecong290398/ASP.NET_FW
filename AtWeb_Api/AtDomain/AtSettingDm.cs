using System;
using System.Collections.Generic;
using System.Text;

namespace AtDomain
{
    public class AtSettingDm
    {

        public class AtSettingDmListOutput
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public bool? IsManual { get; set; }
            public int RowStatus { get; set; }
            public byte[] RowVersion { get; set; }
            public string ImageSlug { get; set; }
            public string IdParent { get; set; }
            public string NameParentGroup { get; set; }
        }

        public class AtSettingDmCreateInput
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public string IdParent { get; set; }
        }


        public class AtSettingDmEditOutput
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public string IdParent { get; set; }
            public byte[] RowVersion { get; set; }
        }

        public class SettingParent
        {
            public string Id { get; set; }
            public string  Value { get; set; }
        }

        public class AtSettingDmEditInput
        {
            public string Id { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
            public string IdParent { get; set; }
            public byte[] RowVersion { get; set; }
        }


        public class AtSettingDmInputDelete
        {
            public string Id { get; set; }
            public byte[] RowVersion { get; set; }
        }


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
