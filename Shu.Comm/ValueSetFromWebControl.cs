using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Shu.Comm
{

    public class WebControlValue
    {

        /// <summary>
        /// Web控件取值
        /// </summary>
        public static object GetValue(Control ctrl)
        {
            object value = null;
            if (ctrl is TextBox)
            {
                value = (ctrl as TextBox).Text.Trim();
                return value;
            }
            else if (ctrl is RadioButtonList)
            {
                value = (ctrl as RadioButtonList).SelectedValue;
                return value;
            }

            else if (ctrl is DropDownList)
            {
                value = (ctrl as DropDownList).SelectedValue;
                return value;
            }
            else if (ctrl is CheckBox)
            {
                value = (ctrl as CheckBox).Checked.ToString();
                return value;
            }
            else if (ctrl is CheckBoxList)
            {
                value = (ctrl as CheckBoxList).SelectedValue;
                return value;
            }
            else if (ctrl is RadioButton)
            {
                value = (ctrl as RadioButton).Checked.ToString();
                return value;
            }
            else if (ctrl is Image)
            {
                value = (ctrl as Image).ImageUrl;
                return value;
            }
            else if (ctrl is Label)
            {
                value = (ctrl as Label).Text;
                return value;
            }
            else if (ctrl is Literal)
            {
                value = (ctrl as Literal).Text;
                return value;
            }
            else if (ctrl is HiddenField)
            {
                value = (ctrl as HiddenField).Value;
                return value;
            }
            else if (ctrl is HtmlImage)
            {
                value = (ctrl as HtmlImage).Src;
                return value;
            }
            else if (ctrl is HtmlInputCheckBox)
            {
                value = (ctrl as HtmlInputCheckBox).Checked;
                return value;
            }
            else if (ctrl is HtmlInputHidden)
            {
                value = (ctrl as HtmlInputHidden).Value;
                return value;
            }
            else if (ctrl is HtmlInputImage)
            {
                value = (ctrl as HtmlInputImage).Src;
                return value;
            }
            else if (ctrl is HtmlInputPassword)
            {
                value = (ctrl as HtmlInputPassword).Value;
                return value;
            }
            else if (ctrl is HtmlInputRadioButton)
            {
                value = (ctrl as HtmlInputRadioButton).Checked;
                return value;
            }
            else if (ctrl is HtmlInputText)
            {
                value = (ctrl as HtmlInputText).Value.Trim();
                return value;
            }
            else if (ctrl is HtmlLink)
            {
                value = (ctrl as HtmlLink).Href;
                return value;
            }
            else if (ctrl is HtmlSelect)
            {
                value = (ctrl as HtmlSelect).Value;
                return value;
            }
            else if (ctrl is HtmlTextArea)
            {
                value = (ctrl as HtmlTextArea).Value.Trim();
                return value;
            }
            else if (ctrl is HtmlGenericControl)
            {
                value = (ctrl as HtmlGenericControl).InnerHtml;
                return value;
            }
            return value;
        }

        /// <summary>
        /// Web控件赋值
        /// </summary>
        public static void SetValue(Control ctrl, object val)
        {
            string value = val.ToString();

            if (ctrl is TextBox)
                (ctrl as TextBox).Text = value;
            else if (ctrl is RadioButtonList)
            {
                if ((ctrl as RadioButtonList).Items.FindByValue(value) != null)
                    (ctrl as RadioButtonList).Items.FindByValue(value).Selected = true;
            }
            else if (ctrl is DropDownList)
            {
                if ((ctrl as DropDownList).Items.FindByValue(value) != null)
                    (ctrl as DropDownList).Items.FindByValue(value).Selected = true;
            }
            else if (ctrl is CheckBox)
            {
                (ctrl as CheckBox).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is RadioButton)
            {
                (ctrl as RadioButton).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is Image)
            {
                (ctrl as Image).ImageUrl = value;
            }
            else if (ctrl is Label)
            {
                value = value.Replace("\r\n", "</br>");
                (ctrl as Label).Text = value;
            }
            else if (ctrl is Literal)
            {
                (ctrl as Literal).Text = value;
            }
            else if (ctrl is HiddenField)
            {
                (ctrl as HiddenField).Value = value;
            }
            else if (ctrl is HtmlImage)
            {
                (ctrl as HtmlImage).Src = value;
            }
            else if (ctrl is HtmlInputCheckBox)
            {
                (ctrl as HtmlInputCheckBox).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is HtmlInputHidden)
            {
                (ctrl as HtmlInputHidden).Value = value;
            }
            else if (ctrl is HtmlInputImage)
            {
                (ctrl as HtmlInputImage).Src = value;
            }
            else if (ctrl is HtmlInputPassword)
            {
                (ctrl as HtmlInputPassword).Value = value;
            }
            else if (ctrl is HtmlInputRadioButton)
            {
                (ctrl as HtmlInputRadioButton).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is HtmlInputText)
            {
                (ctrl as HtmlInputText).Value = value;
            }
            else if (ctrl is HtmlLink)
            {
                (ctrl as HtmlLink).Href = value;
            }
            else if (ctrl is HtmlSelect)
            {
                if ((ctrl as HtmlSelect).Items.FindByValue(value) != null)
                    (ctrl as HtmlSelect).Items.FindByValue(value).Selected = true;
            }
            else if (ctrl is HtmlTextArea)
            {
                (ctrl as HtmlTextArea).Value = value;
            }
            else if (ctrl is HtmlGenericControl)
            {
                (ctrl as HtmlGenericControl).InnerHtml = value;
            }
        }
    }
}
