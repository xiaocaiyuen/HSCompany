using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Model
{
    /// <summary>
    /// 分页Model
    /// </summary>
    public partial class PaginModel
    {
        private string tableName;  //表名
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private string fieldList;//显示列名，如果是全部字段则为*
        /// <summary>
        /// 显示列名，如果是全部字段则为*
        /// </summary>
        public string FieldList
        {
            get { return fieldList; }
            set { fieldList = value; }
        }
        private string primaryKey; //单一主键或唯一值键
        /// <summary>
        /// 单一主键或唯一值键
        /// </summary>
        public string PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        private string whereStr; //查询条件 不含'where'字符，如id>10 and len(userid)>9
        /// <summary>
        /// 查询条件 不含'where'字符，如id>10 and len(userid)>9
        /// </summary>
        public string WhereStr
        {
            get { return whereStr; }
            set { whereStr = value; }
        }
        private string order; //排序 不含'order by'字符，如id asc;userid desc，必须指定asc或desc    
        /// <summary>
        /// 排序 不含'order by'字符，如id asc;userid desc，必须指定asc或desc  
        /// </summary>
        public string Order
        {
            get { return order; }
            set { order = value; }
        }
        private int sortType; //注意当@SortType=3时生效，记住一定要在最后加上主键;排序规则 1:正序asc 2:倒序desc 3:多列排序方法
        /// <summary>
        /// 注意当@SortType=3时生效，记住一定要在最后加上主键;排序规则 1:正序asc 2:倒序desc 3:多列排序方法
        /// </summary>
        public int SortType
        {
            get { return sortType; }
            set { sortType = value; }
        }
        private int recorderCount = 0; //记录总数 0:会返回总记录
        /// <summary>
        /// 记录总数 0:会返回总记录
        /// </summary>
        public int RecorderCount
        {
            get { return recorderCount; }
            set { recorderCount = value; }
        }
        private int pageSize; //每页输出的记录数

        /// <summary>
        /// 每页输出的记录数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private int pageIndex; //当前页数
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
    }
}
