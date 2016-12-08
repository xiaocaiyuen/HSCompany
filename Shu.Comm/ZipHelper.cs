using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Shu.Model;

namespace Shu.Comm
{
    public class ZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationZipFilePath"></param>
        public static void CreateZip(string sourceFilePath, string destinationZipFilePath)
        {
            if (sourceFilePath[sourceFilePath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                sourceFilePath += System.IO.Path.DirectorySeparatorChar;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(destinationZipFilePath));
            zipStream.SetLevel(6);  // 压缩级别 0-9
            CreateZipFiles(sourceFilePath, zipStream);
            zipStream.Finish();
            zipStream.Close();
        }

        /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:\WorkSpace\a.zip）,全路径包括文件名和.zip扩展名
        ///</param>
        /// <param name="staticFile"></param>
        public static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream)
        {
            Crc32 crc = new Crc32();
            string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
            foreach (string file in filesArray)
            {
                if (Directory.Exists(file))                     //如果当前是文件夹，递归
                {
                    CreateZipFiles(file, zipStream);
                }
                else                                            //如果是文件，开始压缩
                {
                    FileStream fileStream = File.OpenRead(file);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(sourceFilePath.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempFile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipStream.PutNextEntry(entry);
                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileNames">要打包的文件列表</param>   
        /// <param name="GzipFileName">目标文件名</param>   
        /// <param name="CompressionLevel">压缩品质级别（0~9）</param>   
        /// <param name="SleepTimer">休眠时间（单位毫秒）</param>        
        public static void CreateZipFiles(List<Sys_ModelFile> fileModels,string path, string GzipFileName, int CompressionLevel, int SleepTimer)   
        {
            CMMLog.Debug(string.Format("ZIP打包下载ZipHelper，参数。path：{0}", path));
            ZipOutputStream s = new ZipOutputStream(File.Create(GzipFileName));   
            try  
            {   
                s.SetLevel(CompressionLevel);   //0 - store only to 9 - means best compression   
                foreach (Sys_ModelFile model in fileModels)
                {
                    string pathStr = model.File_Path;
                    pathStr = path + pathStr;
                    FileInfo file = new FileInfo(pathStr); 
                    FileStream fs = null;   
                    try  
                    {
                        fs = file.Open(FileMode.Open, FileAccess.ReadWrite);   
                    }   
                    catch(Exception ex)  
                    {
                        CMMLog.Debug(string.Format("ZipHelper，file.Open出错。错误信息：{0}", ex.Message));
                        continue; 
                    }                      
                    //  方法二，将文件分批读入缓冲区   
                    byte[] data = new byte[2048];   
                    int size = 2048;
                    ZipEntry entry = new ZipEntry(model.File_Name);
                    entry.DateTime = (file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime);   
                    s.PutNextEntry(entry);   
                    while (true)   
                    {   
                        size = fs.Read(data, 0, size);   
                        if (size <= 0) break;                          
                        s.Write(data, 0, size);   
                    }   
                    fs.Close();   
                    Thread.Sleep(SleepTimer);   
                }
            }
            catch (Exception e)
            {
                CMMLog.Debug(string.Format("ZipHelper出错。错误信息：{0}", e.Message+e.StackTrace));
            }
            finally  
            {   
                s.Finish();   
                s.Close();   
            }   

        }
    }
}
