using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Validators
{

    /// <summary>
    /// Generates client-side validation for ASP.NET Web Forms 
    /// based on DataAnnotations
    /// </summary>
    public class DataAnnotationValidator : BaseValidator
    {
        //Template has the raw string; spanstring holds the completed validation span
        private static string template = "<span id=\"{0}\"  data-val-evaluationfunction=\"{1}\" data-val=\"true\" data-val-errormessage=\"{2}\" data-val-controltovalidate=\"{3}\" {4} {5}>{6}</span>";
        private string spanString = string.Empty;
        //to add uniqueness to the ids of the spans in case there are multiple data annotations
        private int counter = 0;

        /// <summary>
        /// The fully-qualified name of the class that contains the property to be validated
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        ///  The fully-qualified name of the property which has the DataAnnotation for which validation should be generated
        /// </summary>
        public string TypeProperty { get; set; }
        /// <summary>
        /// The name of the physical assembly that contains the class to be validated
        /// </summary>
        public string TypeAssembly { get; set; }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //Find out what we are dealing with
            Control c = this.Parent.FindControl(this.ControlToValidate);
            Type type = Type.GetType(this.TypeName + "," + TypeAssembly, true);
            PropertyInfo property = type.GetProperty(this.TypeProperty);

            //set up appropriate defaults to be overwritten below
            string message = string.Empty;
            string display = "style=\"display: none;\" data-val-display=\"None\"";

            //loop through each validation attribute, read it and create the appropriate span output
            foreach (ValidationAttribute vat in property
                .GetCustomAttributes(typeof(ValidationAttribute), true)
                .OfType<ValidationAttribute>())
            {
                //ensure unique names
                counter++;
                string idToSet = this.ClientID + counter.ToString();

                //style how it appears on the page
                if (this.Display != ValidatorDisplay.None)
                {
                    message = (this.Text != string.Empty) ? this.Text : vat.ErrorMessage;
                    display = (this.Display == ValidatorDisplay.Dynamic)
                              ? "style=\"display: none;\" data-val-display=\"Dynamic\""
                              : "style=\"visibility: hidden;\"";
                }
                //If the developer didn't specify an error message, grab the default message
                //from the underlying type
                if (vat.ErrorMessage != null && vat.ErrorMessage.Contains("{0}"))
                {
                    vat.ErrorMessage = string.Format(vat.ErrorMessage, this.TypeProperty);
                }
                
                switch (vat.GetType().Name)
                {
                    case "RequiredAttribute":
                        spanString += string.Format(DataAnnotationValidator.template,
                                                    idToSet,
                                                    "RequiredFieldValidatorEvaluateIsValid",
                                                    vat.ErrorMessage,
                                                    c.ClientID,
                                                    "data-val-initialvalue=\"\"", 
                                                    display,
                                                    message);

                        break;
                    case "StringLengthAttribute":
                        StringLengthAttribute a = (StringLengthAttribute)vat;
                        if (string.IsNullOrEmpty(vat.ErrorMessage))
                        {
                            vat.ErrorMessage = "must not exceed "
                                     + a.MaximumLength + " characters";
                            if (a.MinimumLength > 0)
                            {
                                vat.ErrorMessage += " or have fewer than "
                                    + a.MinimumLength + " characters";
                            }
                        }
                        string reg = "^.{" + string.Format("{0},{1}",
                            a.MinimumLength, a.MaximumLength) + "}$";
                        BuildRegularExpression(idToSet,
                                               vat.ErrorMessage,
                                               c.ClientID,
                                               reg,
                                               display,
                                               message);
                        break;
                    case "RangeAttribute":
                        RangeAttribute ra = (RangeAttribute)vat;
                        if (string.IsNullOrEmpty(vat.ErrorMessage))
                        {
                            vat.ErrorMessage = "Cannot be less than " + ra.Minimum + " or more than " + ra.Maximum;
                        }
                        spanString += string.Format(DataAnnotationValidator.template,
                                                    idToSet,
                                                    "RangeValidatorEvaluateIsValid",
                                                    vat.ErrorMessage,
                                                    c.ClientID,
                                                    "data-val-minimumvalue=\"" + ra.Minimum + "\" data-val-maximumvalue=\"" + ra.Maximum + "\"",
                                                    display,
                                                    message);

                        break;
                    case "EmailAddressAttribute":
                        BuildRegularExpression(idToSet,
                                               vat.ErrorMessage,
                                               c.ClientID,
                                               @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                                               display,
                                               message);
                        break;
                    case "PhoneAttribute":
                        BuildRegularExpression(idToSet,
                                                vat.ErrorMessage,
                                                c.ClientID,
                                                @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                                                display,
                                                message);
                        break;
                    case "UrlAttribute":
                        BuildRegularExpression(idToSet,
                                                vat.ErrorMessage,
                                                c.ClientID,
                                                @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?",
                                                display,
                                                message);
                        break;
                    case "CreditCardAttribute":
                        BuildRegularExpression(idToSet,
                                               vat.ErrorMessage,
                                               c.ClientID,
                                               @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$",
                                               display,
                                               message);
                        break;
                    case "MinLengthAttribute":
                        MinLengthAttribute mi = (MinLengthAttribute)vat;
                        if (string.IsNullOrEmpty(vat.ErrorMessage))
                        {
                            vat.ErrorMessage = "Cannot not be fewer than " + mi.Length + " characters";
                        }
                        string r = "^.{" + string.Format("{0},", mi.Length) + "}$";
                        BuildRegularExpression(idToSet, 
                                               vat.ErrorMessage, 
                                               c.ClientID, 
                                               r, 
                                               display,
                                               message);
                        break;
                    case "MaxLengthAttribute":
                        MaxLengthAttribute m = (MaxLengthAttribute)vat;
                        if (string.IsNullOrEmpty(vat.ErrorMessage))
                        {
                            vat.ErrorMessage = "Cannot not exceed " + m.Length + " characters";
                        }
                        string re = "^.{" + string.Format("0,{0}", m.Length) + "}$";
                        BuildRegularExpression(idToSet, 
                                               vat.ErrorMessage, 
                                               c.ClientID, 
                                               re, 
                                               display, 
                                               message);
                        break;
                    case "RegularExpressionAttribute":
                        RegularExpressionAttribute rea = (RegularExpressionAttribute)vat;
                        BuildRegularExpression(idToSet,
                                               vat.ErrorMessage,
                                               c.ClientID,
                                               rea.Pattern,
                                               display,
                                               message);
                        break;
                }


            }

        }


        private void BuildRegularExpression(string id,
                                            string errorMessage,
                                            string clientId,
                                            string expression,
                                            string display,
                                            string message)
        {
            spanString += string.Format(DataAnnotationValidator.template,
                          id,
                          "RegularExpressionValidatorEvaluateIsValid",
                          errorMessage,
                          clientId,
                          "data-val-validationexpression=\"" + expression + "\"",
                          display,
                          message);

        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(spanString);
        }




        protected override bool EvaluateIsValid()
        {

            return true;
        }
    }
}
