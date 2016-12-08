using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Model
{
    [Serializable]
    public class ModelFile
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string File_ID { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string File_Name { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string File_Path { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string File_Extension { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public decimal File_Size { get; set; }

        /// <summary>
        /// 文件类型（非必须有）
        /// </summary>
        public string File_Type { get; set; }

        ///// <summary>
        ///// 文件真名字
        ///// </summary>
        //public string File_RealName { get; set; }

        /// <summary>
        /// 文件添加时间
        /// </summary>
        public DateTime File_AddTime { get; set; }

        /// <summary>
        /// 文件ywuID
        /// </summary>
        public string File_OperationID { get; set; }


    }
}
