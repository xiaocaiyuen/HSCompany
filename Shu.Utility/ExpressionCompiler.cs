/*
Author      : 肖亮
Date        : 2011-7-13
Description : 提供分页表达式的编译功能
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;

namespace Shu.Utility
{
    /// <summary>
    /// 基本表达式
    /// </summary>
    public abstract class ExpressionBase
    {
        /// <summary>
        /// 对表达式进行求值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public abstract ExpressionBase Eval(NameValueCollection val, Encoding encoding);

        /// <summary>
        /// 当前表达式是否能与指定表达式合并
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool CanCombine(ExpressionBase expression)
        {
            return expression == null;
        }

        /// <summary>
        ///当前表达式与指定表达式合并
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual ExpressionBase Combine(ExpressionBase expression)
        {
            if (expression == null)
                return this;

            throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// AND 分组表达式 他的特点是如果子表达式中有任意一个求值返回结果为null则对他求值时也返回null
    /// </summary>
    public class AndGroupExpression : ContainerExpression
    {
        public AndGroupExpression()
        {
        }

        public AndGroupExpression(List<ExpressionBase> children)
            : base(children)
        {
        }

        public override ExpressionBase Eval(NameValueCollection val, Encoding encoding)
        {
            var children = this.Children;
            var l = children.Count;

            if (l == 0)
                return null;

            var pre = children[0].Eval(val, encoding);
            if (l == 1 || pre == null)
                return pre;

            var exps = new List<ExpressionBase>();
            for (int i = 1; i < l; i++)
            {
                var exp = children[i].Eval(val, encoding);
                if (exp == null)
                {
                    return null;
                }

                if (pre.CanCombine(exp))
                {
                    pre = pre.Combine(exp);
                }
                else
                {
                    exps.Add(pre);
                    pre = exp;
                }
            }

            if (exps.Count == 0)
                return pre;

            exps.Add(pre);
            return new AndGroupExpression(exps);

        }
    }

    /// <summary>
    /// OR 分组表达式 他的特点是如果子表达式中有任意一个求值返回结果不为null则对他求值时不会为null
    /// </summary>
    public class OrGroupExpression : ContainerExpression
    {
        public OrGroupExpression()
        {
        }

        public OrGroupExpression(List<ExpressionBase> children)
            : base(children)
        {
        }
       
    }


    /// <summary>
    /// 文本常量表达式 
    /// </summary>
    public class TextConstantExpression : ExpressionBase
    {
        public TextConstantExpression(string text)
        {
            this.Text = text;
        }

        public string Text
        {
            get;
            private set;
        }

        public override ExpressionBase Eval(NameValueCollection val, Encoding encoding)
        {
            return this;
        }

        public override bool CanCombine(ExpressionBase expression)
        {
            return expression == null || expression is TextConstantExpression;
        }

        public override ExpressionBase Combine(ExpressionBase expression)
        {
            if (expression == null)
                return this;

            var cons = expression as TextConstantExpression;

            if (cons != null)
                return new TextConstantExpression(this.Text + cons.Text);

            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return this.Text;
        }

    }

    /// <summary>
    /// 表达式容器 可以包含子表达式的表达式
    /// </summary>
    public class ContainerExpression : ExpressionBase
    {
        List<ExpressionBase> _children;
        public ContainerExpression()
        {

        }

        public ContainerExpression(List<ExpressionBase> children)
        {
            _children = children;
        }

        public List<ExpressionBase> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<ExpressionBase>();
                }
                return this._children;
            }
        }

        public override ExpressionBase Eval(NameValueCollection val, Encoding encoding)
        {
            var children = this.Children;
            var l = children.Count;

            if (l == 0)
                return null;

            var pre = children[0].Eval(val, encoding);
            if (l == 1)
                return pre;

            var exps = new List<ExpressionBase>();
            for (int i = 1; i < l; i++)
            {
                var exp = children[i].Eval(val, encoding);
                if (pre == null)
                {
                    pre = exp;
                    continue;
                }

                if (pre.CanCombine(exp))
                {
                    pre = pre.Combine(exp);
                }
                else
                {
                    exps.Add(pre);
                    pre = exp;
                }
            }

            if (exps.Count != 0)
            {
                if (pre != null)
                    exps.Add(pre);

                if (exps.Count == 1)
                {
                    return exps[0];
                }

                return new ContainerExpression(exps);
            }
            else
            {
                return pre;
            }
        }
    }

    /// <summary>
    /// 特殊表达式
    /// </summary>
    public class SpecialVariableExpression : ExpressionBase
    {
        public override ExpressionBase Eval(NameValueCollection val, Encoding encoding)
        {
            return new TextConstantExpression(val["0"]);
        }
    }

    /// <summary>
    /// 变量表达式
    /// </summary>
    public class VariableExpression : ExpressionBase
    {

        public VariableExpression(string varName)
        {
            this.VariableName = varName;
        }

        /// <summary>
        /// 是否进行url编码
        /// </summary>
        public bool UrlEncode
        {
            get;
            set;
        }

        /// <summary>
        /// 变量名
        /// </summary>
        public string VariableName
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }

        public override ExpressionBase Eval(NameValueCollection val, Encoding encoding)
        {
            if (this.VariableName == "0")
            {
                return new SpecialVariableExpression();
            }

            var value = val[this.VariableName] ?? this.DefaultValue;

            if (string.IsNullOrEmpty(value))
                return null;

            if (UrlEncode)
            {
                value = WebUtil.UrlEncode(value, encoding);
            }

            return new TextConstantExpression(value);

        }
    }

    /// <summary>
    /// 表达式编译器
    /// </summary>
    public class ExpressionCompiler
    {
        /// <summary>
        /// 编译缓存
        /// </summary>
        readonly static Dictionary<string, ExpressionBase> _compileCache = new Dictionary<string, ExpressionBase>();

        /// <summary>
        /// 转义字符
        /// </summary>
        const char ESCAPE_CHAR = ';';

        /// <summary>
        /// AND 分组开始标记
        /// </summary>
        const char AND_GROUP_SYSNTAX_BEGIN = '<';

        /// <summary>
        /// AND 分组结束标记
        /// </summary>
        const char AND_GROUP_SYSNTAX_END = '>';

        /// <summary>
        /// OR 分组开始标记
        /// </summary>
        const char OR_GROUP_SYSNTAX_BEGIN = '[';

        /// <summary>
        /// OR 分组结束标记
        /// </summary>
        const char OR_GROUP_SYSNTAX_END = ']';

        /// <summary>
        /// 变量开始标记
        /// </summary>
        const char VAR_SYSNTAX_BEGIN = '{';

        /// <summary>
        /// 变量结束标记
        /// </summary>
        const char VAR_SYSNTAX_END = '}';

        /// <summary>
        /// 变量url编码标记
        /// </summary>
        const char VAR_SYSNTAX_URLENCODE = '&';

        /// <summary>
        /// 变量默认值分隔符
        /// </summary>
        const char VAR_DEFAULTVALUE_SEPARATOR = ':';



        private static void throwSysntaxException(string message, int index)
        {
            throw new InvalidOperationException(string.Format("{0} 索引为: {1} 处;", message, index.ToString()));
        }

        private static void throwNotEndException(ExpressionBase sy, int index)
        {
            string name = null;
            if (sy is VariableExpression)
                name = "变量语法元素";

            else if (sy is AndGroupExpression)
                name = "分组语法元素";

            name = name ?? Convert.ToString(sy);
            throw new InvalidOperationException(string.Format("索引为: {0} 之前 至少有一个[{1}]没有对应的开始/结束标记;", index.ToString(), name));

        }

        /// <summary>
        /// 读取字符常量
        /// </summary>
        /// <param name="pp_Char"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        unsafe static string readConstantText(char** pp_Char, int* index)
        {
            var p_Char = *pp_Char;
            var cIndex = *index;
            var textBuilder = new StringBuilder();
            char c;
            while ((c = *p_Char) != '\0')
            {
                switch (c)
                {
                   
                    case VAR_SYSNTAX_BEGIN:
                    case VAR_SYSNTAX_END:
                    case AND_GROUP_SYSNTAX_BEGIN:
                    case AND_GROUP_SYSNTAX_END:
                    case OR_GROUP_SYSNTAX_BEGIN:
                    case OR_GROUP_SYSNTAX_END:
                    case VAR_DEFAULTVALUE_SEPARATOR:
                        goto labEnd;
                    case ESCAPE_CHAR:
                        {
                            c = *(++p_Char);
                            if (c == '\0')
                                throwSysntaxException("没有可转义的字符", cIndex);

                            textBuilder.Append(c);
                            cIndex += 2;
                            p_Char++;
                        }
                        continue;
                }
                textBuilder.Append(c);
                p_Char++;
                cIndex++;
            }
        labEnd:
            *pp_Char = p_Char;
            *index = cIndex;
            return textBuilder.ToString();
        }

        private static readonly TextConstantExpression Empty = new TextConstantExpression(String.Empty);
        /// <summary>
        /// 编译表达式 
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="useCache">是否使用缓存</param>
        /// <returns></returns>
        public static ExpressionBase Compile(string expression, bool useCache)
        {
            if (!useCache)
                return Compile(expression);

            ExpressionBase exp;
            if (_compileCache.TryGetValue(expression, out exp))
            {
                return exp;
            }
            lock (_compileCache)
            {
                if (!_compileCache.TryGetValue(expression, out exp))
                {
                    exp = Compile(expression);
                    _compileCache[expression] = exp;
                }
            }

            return exp;
        }

        /// <summary>
        /// 编译表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        unsafe public static ExpressionBase Compile(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (expression.Length == 0)
                return Empty;

            var root = new ContainerExpression();
            var depth = new Stack<ContainerExpression>();
            depth.Push(root);

            fixed (char* p_Code = expression)
            {
                char c;
                char* p_Char = p_Code;
                int i = 0;
                do
                {
                    string text = readConstantText(&p_Char, &i);
                    var parent = depth.Peek();

                    if (text.Length != 0)
                        parent.Children.Add(new TextConstantExpression(text));

                    c = *p_Char;
                    p_Char++;
                    i++;

                    switch (c)
                    {
                        /*  如果当前是变量表达式开始 "{"   */
                        case VAR_SYSNTAX_BEGIN:
                            #region 变量表达式的推导
                            {
                                VariableExpression varExp;

                                /*
                                 *  如果表达式以"&"开头 则求值时对变量value 进行url编码例如:{&var}
                                 */
                                var encode = false;
                                if (*p_Char == VAR_SYSNTAX_URLENCODE)
                                {
                                    encode = true;
                                    p_Char++;
                                    i++;
                                }

                                /*  读取变量名称  */
                                string varName = readConstantText(&p_Char, &i);
                                if (varName.Length != 0)
                                {
                                    varExp = new VariableExpression(varName) { UrlEncode = encode };
                                }
                                else
                                {
                                    varExp = null;
                                    throwSysntaxException("变量名称不能为空", i);
                                }

                                c = *p_Char++;  //变量名之后的标记
                                i++;

                                switch (c)
                                {
                                    /*  结束标记 "}" */
                                    case VAR_SYSNTAX_END:
                                        ;
                                        break;
                                    /*  默认值标记 ":" */
                                    case VAR_DEFAULTVALUE_SEPARATOR:
                                        {
                                            varExp.DefaultValue = readConstantText(&p_Char, &i);

                                            c = *p_Char++;
                                            i++;

                                            /* 最后必须有一个变量表达式结束标记 */
                                            if (c != VAR_SYSNTAX_END)
                                            {
                                                throwNotEndException(parent, i);
                                            }
                                        }
                                        break;
                                    default:
                                        throwNotEndException(parent, i);
                                        break;
                                }

                                parent.Children.Add(varExp);
                            }
                            #endregion
                            break;
                        /*  如果当前是 AND 分组表达式开始 "<"  */
                        case AND_GROUP_SYSNTAX_BEGIN:
                            {
                                var andElement = new AndGroupExpression();
                                parent.Children.Add(andElement);
                                depth.Push(andElement);
                            }
                            break;
                        /*  如果当前是分组表达式结束 ">"  */
                        case AND_GROUP_SYSNTAX_END:
                            {
                                parent = depth.Pop();
                                if (!(parent is AndGroupExpression))
                                    throwNotEndException(parent, i);
                            }
                            break;
                        case OR_GROUP_SYSNTAX_BEGIN:
                            {
                                var orElement = new OrGroupExpression();
                                parent.Children.Add(orElement);
                                depth.Push(orElement);
                            }
                            break;
                        case OR_GROUP_SYSNTAX_END:
                            {
                                parent = depth.Pop();
                                if (!(parent is OrGroupExpression))
                                    throwNotEndException(parent, i);
                            }
                            break;
                    }

                } while (c != '\0');
            }

            if (depth.Count != 1 || (depth.Count == 1 && depth.Pop() != root))
                throw new InvalidOperationException("有表达式元素未结束");

            return root;
        }
    }
}
