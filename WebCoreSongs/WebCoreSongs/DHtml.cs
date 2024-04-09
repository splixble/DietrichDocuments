using Microsoft.AspNetCore.Html;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;
using WebCoreSongs.Models;

namespace WebCoreSongs
{
    public static class DHtml
    {
        const string CssClassName = "dietrich-control";
 
        public static IHtmlContent TypeInLookup(string baseName, string editedColumnName, IComparable initialKeyValue, 
            IList lookupTable, string lookupKeyColumnName, string lookupDisplayColumnName) 
        {
            string activeFieldName = baseName + "_TypeInLookup";
            string hiddenFieldName = baseName + "_HiddenField";
            string dataListName = baseName + "_DataList";
            string initialDisplayValue = null; // or blank? Null seems to work

            // Get class properties for the table rows in lookupTable:
            if (lookupTable.Count == 0)
                throw new Exception("Lookup table cannot be empty; must be able to get column properties");
            PropertyInfo? keyColumnProperty = GetTableRowProperty(lookupTable[0], lookupKeyColumnName);
            PropertyInfo? displayColumnProperty = GetTableRowProperty(lookupTable[0], lookupDisplayColumnName);

            // Find initial display value for control:
            // (This could be streamlined and performance improved by making a custom object for the table list)
            if (initialKeyValue != null)
            {
                foreach (object rowObj in lookupTable)
                {
                    IComparable currentKeyValue = keyColumnProperty.GetValue(rowObj) as IComparable;
                    if (currentKeyValue.Equals(initialKeyValue))
                    {
                        initialDisplayValue = displayColumnProperty.GetValue(rowObj).ToString();
                        break;
                    }
                }
            }

            HtmlContentBuilder htmlCode = new HtmlContentBuilder();
            AddHTMLLine(htmlCode, "<div>");

            // Define hidden control:
            // was this, but it won't parse the asp-for: AddHTMLLine(htmlCode, "<input asp-for=\"" + editedColumnName + "\" id=\""+ hiddenFieldName + "\" class=\"" + CssClassName + "\" hidden />");
            AddHTMLLine(htmlCode, "<input id='" + hiddenFieldName + "' name='" + editedColumnName + "' value='" + initialKeyValue.ToString() + "' hidden />");
            // need type=number?

            // Define active control:
            AddHTMLLine(htmlCode, "<input id='" + activeFieldName  + "' class='" + CssClassName + "' list='" + dataListName + "' name='" + editedColumnName + "'");
            // DIAG do I need both ID and Name on prev line?

            // note: this line needs escaped-out double quotes, since it uses single quotes for params
            AddHTMLLine(htmlCode, "onchange=\"OnTypeInLookupChanged('" + activeFieldName + "', '" + hiddenFieldName +"', '"+ dataListName + "')\"");
            
            if (initialDisplayValue != null)
                AddHTMLLine(htmlCode, "value = '" + initialDisplayValue + "'");
            AddHTMLLine(htmlCode, "/>");

            // Define datalist:
            AddHTMLLine(htmlCode, "<datalist id='"+ dataListName + "'>"); // DIAG can I add Environment.NewLine?
            foreach (object rowObj in lookupTable)
            {
                AddHTMLLine(htmlCode, "<option value = '" + displayColumnProperty.GetValue(rowObj).ToString() + "' dbkey = '" +
                    keyColumnProperty.GetValue(rowObj) + "' ></option>");
            }
            AddHTMLLine(htmlCode, "</datalist>");
            AddHTMLLine(htmlCode, "</div>");

            /* Here's unused code to set initial value... but, don't complicate with c# code when we could use common javascript! eh? right?
            PropertyInfo? lookupDisplayProperty = lookupTable.GetType().GetProperty(lookupDisplayColumnName);
            if (lookupDisplayProperty == null)
                throw new Exception("Column '" + lookupDisplayProperty + "' not found in lookup table");

            // look for row in lookupTable with matching display name: ...wait...DIAG 
            */

            return htmlCode;


        }

        static void AddHTMLLine(HtmlContentBuilder htmlCode, string htmlLine)
        // convenenience func that makes the HTML much more readable
        {
            htmlCode.AppendHtml("            " + htmlLine + "\n");
        }

        static PropertyInfo? GetTableRowProperty(object rowObj, string columnName)
        {
            Type rowType = rowObj.GetType();
            PropertyInfo? columnProperty = rowType.GetProperty(columnName);
            if (columnProperty == null)
                throw (new Exception("Column property '" + columnName + "' does not exist in lookup table"));

            return columnProperty;
        }

    }
}
