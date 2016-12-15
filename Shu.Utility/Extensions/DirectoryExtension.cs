/*
Author      : 肖亮
Date        : 2011-4-12
Description : 对 System.IO.Directory的扩展
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对Directory方法的扩展
    /// </summary>
    public static class DirectoryExtension
    {
        #region 创建
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">物理路径地址</param>
        public static void CreateDirectory(this string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">物理路径地址</param>
        public static void CreateFile(this string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">物理路径地址</param>
        public static bool DeleteFile(this string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="isAll">是否删除所有文件夹(默认删除当前文件夹  true:删除所有文件)</param>
        public static bool DeleteDirectory(this string path, bool isAll = false)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, isAll);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定目录下所有的文件和文件夹
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="DirectoryMethod">方法(默认1)：1.删除目录后，创建空目录 2.找出下层文件和文件夹路径迭代删除</param>
        public static bool DeleteDirectoryAll(this string path, DirectoryMethod Method = DirectoryMethod.Delete)
        {
            if (Directory.Exists(path))
            {
                switch (Method)
                {
                    case DirectoryMethod.Delete://方法一
                        Directory.Delete(path);
                        Directory.CreateDirectory(path);
                        break;
                    case DirectoryMethod.Select://方法二
                        foreach (string content in Directory.GetFileSystemEntries(path))
                        {
                            if (Directory.Exists(content))
                            {
                                Directory.Delete(content, true);
                            }
                            else if (File.Exists(content))
                            {
                                File.Delete(content);
                            }
                        }
                        break;
                    default:
                        Directory.Delete(path);
                        Directory.CreateDirectory(path);
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum DirectoryMethod
        {
            /// <summary>
            /// 先删除目录后，创建空目录
            /// </summary>
            Delete = 0,
            /// <summary>
            /// 先找出下层文件和文件夹路径迭代删除
            /// </summary>
            Select = 1
        }
        #endregion

        #region 读取
        /// <summary>
        /// 读取文件（返回string）
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="encoding">格式</param>
        public static string ReadAllText(this string path, Encoding encoding)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path, encoding);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取文件（返回string[]）
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="encoding">格式</param>
        public static string[] ReadAllLines(this string path, Encoding encoding)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path, encoding);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取文件（返回IEnumerable<string>）
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="encoding">格式</param>
        public static IEnumerable<string> ReadLines(this string path, Encoding encoding)
        {
            if (File.Exists(path))
            {
                return File.ReadLines(path, encoding);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取文件（返回byte[]）
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="encoding">格式</param>
        public static byte[] ReadAllBytes(this string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取文件（返回string）
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="encoding">格式</param>
        public static string ReadStreamLines(this string path, Method Method = Method.OneMethod)
        {
            string fileContent = string.Empty;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    switch (Method)
                    {
                        case Method.OneMethod://方法一：从流的当前位置到末尾读取流
                            fileContent = sr.ReadToEnd();
                            break;
                        case Method.TwoMethod://方法二：一行行读取直至为NULL
                            string strLine = string.Empty;
                            while (strLine != null)
                            {
                                strLine = sr.ReadLine();
                                fileContent += strLine + "\r\n";
                            }
                            break;
                    }
                }
            }
            return fileContent;
        }

        public enum Method
        {
            /// <summary>
            /// 方法一：从流的当前位置到末尾读取流
            /// </summary>
            OneMethod,
            /// <summary>
            /// 方法二：一行行读取直至为NULL
            /// </summary>
            TwoMethod
        }
        #endregion

        #region 写入
        /// <summary>
        /// 写文件内容
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="Content">需要写入的内容</param>
        /// <param name="encoding">格式</param>
        /// <returns></returns>
        public static bool WriteAllText(this string path, string Content, Encoding encoding)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, Content, encoding);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写文件内容
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="Content">需要写入的内容</param>
        /// <param name="encoding">格式</param>
        public static bool WriteAllLines(this string path, string[] Content, Encoding encoding)
        {
            if (File.Exists(path))
            {
                File.WriteAllLines(path, Content, encoding);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写文件内容
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="Content">需要写入的内容</param>
        public static bool WriteAllBytes(this string path, Byte[] Content)
        {
            if (File.Exists(path))
            {
                File.WriteAllBytes(path, Content);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 写文件内容(文件流StreamWriter)
        /// </summary>
        /// <param name="path">物理路径地址</param>
        /// <param name="Content">需要写入的内容</param>
        /// <param name="encoding">格式</param>
        /// <returns></returns>
        public static bool WriterStream(this string path, string Content)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(Content);
                    sw.Flush();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 移动文件
        public enum MoveMethod
        {
            /// <summary>
            /// 同一个盘符
            /// </summary>
            EqualDrive,
            /// <summary>
            /// 跨盘符
            /// </summary>
            CrossDrive
        }

        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(this string sourcePath, string destPath, MoveMethod MoveMethod = MoveMethod.EqualDrive)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //覆盖模式
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    switch (MoveMethod)
                    {
                        case MoveMethod.EqualDrive:
                            //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                            Directory.Move(c, destDir);
                            break;
                        case MoveMethod.CrossDrive:
                            //采用递归的方法实现
                            MoveFolder(c, destDir);
                            break;
                    }



                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 复制文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }
        #endregion
    }
}
