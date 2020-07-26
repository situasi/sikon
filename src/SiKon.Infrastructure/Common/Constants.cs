namespace SiKon.Infrastructure.Common
{
    public static class Constants
    {
        public static class SiKonLogDB
        {
            public static class TableName
            {
                public const string AppLogs = "applogs";
            }
            
            public static class ColumnName
            {
                public const string Message = "message";
                public const string Template = "template";
                public const string Level = "level";
                public const string Created = "created";
                public const string Exception = "exception";
                public const string Serialized = "serialized";
                public const string Properties = "properties";
                public const string Machine = "machine";
            }

            public static class PropertyName
            {
                public const string MachineName = "MachineName";
            }
        }
    }
}