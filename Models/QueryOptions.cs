﻿using System.Linq.Expressions;

namespace TequliasResturant.Models
{

    // . Repository عند استعلام البيانات من قاعدة البيانات في (Includes) كلاس هدفه تسهيل فلترة وترتيب وتحميل العلاقات 

    public class QueryOptions<T> where T : class
    {
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, bool>> Where { get; set; } = null;

        private string[] includes = Array.Empty<string>();
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',',StringSplitOptions.RemoveEmptyEntries);
        }
        public string[] GetIncludes() => includes;
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;


    }
}