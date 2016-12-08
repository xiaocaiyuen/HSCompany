/*
 * Author:  肖亮
 * Date:    2015-9-29
 * Description: 域名常量及相关操作
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Shu.Utility;
using Shu.Utility.Extensions;
using System.Web;

namespace Shu.Comm
{
    /// <summary>
    /// 无图尺寸
    /// </summary>
    public enum NoPictureType
    {
        /// <summary>
        /// 100*100
        /// </summary>
        T100_100 = 1,
        /// <summary>
        /// 100*75
        /// </summary>
        T100_75 = 2,
        /// <summary>
        /// 60*60
        /// </summary>
        T60_60 = 3,
        /// <summary>
        /// 120*100
        /// </summary>
        T120_100 = 4,

    }

    /// <summary>
    /// 域名常量及相关操作
    /// </summary>
    public static class Domain
    {

        /// <summary>
        /// 映射大图片路径
        /// </summary>
        /// <param name="imagePath">图片存放位置路径</param>
        /// <returns></returns>
        public static string MapImagePath(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
                return Image + '/' + imagePath.TrimStart('/');
            else
                return string.Empty;
        }
        /// <summary>
        /// 映射小图片路径,无图默认100*100
        /// </summary>
        /// <param name="imagePath">图片存放位置路径</param>
        /// <returns></returns>
        public static string MapSmallImagePath(string imagePath)
        {
            return MapSmallImagePath(imagePath, NoPictureType.T100_100);
        }
        /// <summary>
        /// 映射小图片路径
        /// </summary>
        /// <param name="imagePath">图片存放位置路径</param>
        /// <param name="type">小图类别</param>
        /// <returns></returns>
        public static string MapSmallImagePath(string imagePath, NoPictureType type)
        {
            string mopicsrc = "";
            switch (type)
            {
                case NoPictureType.T100_100:
                    mopicsrc = "";
                    break;
                case NoPictureType.T100_75:
                    mopicsrc = "";
                    break;
                case NoPictureType.T60_60:
                    mopicsrc = "";
                    break;
                case NoPictureType.T120_100:
                    mopicsrc = "";
                    break;
                default:
                    mopicsrc = "";
                    break;
            }
            if (!string.IsNullOrEmpty(imagePath))
                return SmallImage + '/' + imagePath.TrimStart('/');
            else
                return mopicsrc;
        }

        /// <summary>
        /// 大图片域名
        /// </summary>
        public static string Image
        {
            get
            {
                return WebConfigurationManager.AppSettings["ImageDomain"] ?? WebUtil.GetDomain();
            }
        }
        /// <summary>
        /// 小图域地址
        /// </summary>
        public static string SmallImage
        {
            get
            {
                return WebConfigurationManager.AppSettings["ImageDomainSmall"] ?? WebUtil.GetDomain();
            }
        }
        



        
    }
}
