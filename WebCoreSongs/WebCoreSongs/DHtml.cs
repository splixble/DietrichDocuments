using System.Collections;
using System.Reflection;

namespace WebCoreSongs
{
    public static class DHtml
    {
        public static string GenerateSumpin()
        {
            return "Yakatac";
        }

        public static string TypeInLookup(string baseName, string editedColumnName, IList lookupTable, string lookupDisplayColumnName) 
        {
            /* DIAG generates this code:----------WORK ON THIS!!
                <div>
                    <input asp-for="Venue" id="venueID" class="dietrich-control" hidden />
                    @{
                        string initialValue = "";
                        Venues venRow = perfModel._VenuesList.Find(venRow => venRow.Id == perfModel.Venue);
                        if (venRow != null)
                            initialValue = venRow.Name;
                    }
                    <input id="altVenue" list="venues" name="venue"
                           onchange="OnTypeInLookupChanged('altVenue', 'venueID', 'venues')"
                           value = "@initialValue"
                        />

                    <datalist id="venues">
                        @foreach (var venueRow in perfModel._VenuesList)
                        {
                            <option value="@venueRow.Name" dbkey="@venueRow.Id"></option>
                        }
                    </datalist>

                </div>
             * ...and more?
             * */


            /* DIAG unused code to set initial value... but, don't complicate with c# code when we could use common javascript! eh? right?
            PropertyInfo? lookupDisplayProperty = lookupTable.GetType().GetProperty(lookupDisplayColumnName);
            if (lookupDisplayProperty == null)
                throw new Exception("Column '" + lookupDisplayProperty + "' not found in lookup table");

            // look for row in lookupTable with matching display name: ...wait...DIAG 
            */

            return "fld: " + editedColumnName;


        }
    }
}
