/*
Author      : 肖亮
Date        : 2012-10-29
Description : 对字节数组的扩展
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Utility.Extensions
{
    /// <summary>
    /// 对字节数组的扩展
    /// </summary>
    public static class ByteArrayExtension
    {
        /// <summary>
        /// 比较两个字节块是否完全相等
        /// </summary>
        /// <param name="src"></param>
        /// <param name="srcOffset"></param>
        /// <param name="dst"></param>
        /// <param name="dstOffset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        unsafe public static bool CompareEqualBlock(this byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            if (src == null)
                throw new ArgumentNullException("src");

            if (dst == null)
                throw new ArgumentNullException("dst");

            if (srcOffset < 0)
                throw new ArgumentOutOfRangeException("srcOffset");

            if (dstOffset < 0)
                throw new ArgumentOutOfRangeException("dstOffset");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            if (src.Length < srcOffset + count)
                throw new ArgumentException("count");

            if (dst.Length < dstOffset + count)
                throw new ArgumentException("count");

            fixed (byte* p_src = src)
            {
                var np_src = p_src + srcOffset;
                fixed (byte* p_dst = dst)
                {
                    var np_dst = p_dst + dstOffset;
                    while (count >= 8)
                    {
                        if (*((long*)np_src) != *((long*)np_dst))
                        {
                            return false;
                        }

                        np_src += 8;
                        np_dst += 8;
                        count -= 8;
                    }

                    if ((count & 4) != 0)
                    {
                        if (*((int*)np_src) != *((int*)np_dst))
                        {
                            return false;
                        }

                        np_src += 4;
                        np_dst += 4;
                        count -= 4;
                    }

                    if ((count & 2) != 0)
                    {
                        if (*((short*)np_src) != *((short*)np_dst))
                        {
                            return false;
                        }

                        np_src += 2;
                        np_dst += 2;
                        count -= 2;
                    }

                    if ((count & 1) != 0)
                    {
                        return np_src[0] == np_dst[0];
                    }

                }
            }

            return true;
        }
    }
}
