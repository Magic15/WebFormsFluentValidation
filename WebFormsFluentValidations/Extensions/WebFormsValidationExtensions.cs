using FluentValidation.Results;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsFluentValidations.Extensions
{
    public static class WebFormsValidationExtensions
    {
        public static void AddValidationErrors(
     this Page page,
     ValidationResult result,
     IDictionary<string, string> propertyToControlId = null,
     IDictionary<string, string> propertyToPlaceholderId = null,
     string validationGroup = null,
     string contentPlaceHolderId = "MainContent")
        {
            // Where to look for placeholders when using a master page
            Control lookupRoot = page;
            if (!string.IsNullOrEmpty(contentPlaceHolderId) && page.Master != null)
            {
                var cph = page.Master.FindControl(contentPlaceHolderId);
                if (cph != null) lookupRoot = cph;
            }

            foreach (var error in result.Errors)
            {
                var cv = new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = error.ErrorMessage,
                    Text = error.ErrorMessage,                // inline text
                    ToolTip = error.ErrorMessage,
                    Display = ValidatorDisplay.Dynamic,       // will switch to None if no placeholder
                    ValidateEmptyText = true,
                    ValidationGroup = validationGroup,
                    CssClass = "field-error"
                };

                if (propertyToControlId != null &&
                    propertyToControlId.TryGetValue(error.PropertyName, out var controlId))
                {
                    cv.ControlToValidate = controlId;
                }

                cv.ServerValidate += (s, e) => e.IsValid = false;

                // Choose a single parent in the control tree
                Control parentToAdd = null;

                if (propertyToPlaceholderId != null &&
                    propertyToPlaceholderId.TryGetValue(error.PropertyName, out var phId))
                {
                    parentToAdd = lookupRoot.FindControl(phId);
                }

                if (parentToAdd == null)
                {
                    // No inline placeholder: add to a neutral container so the Summary can see it,
                    // but don't render inline.
                    cv.Display = ValidatorDisplay.None;
                    parentToAdd = lookupRoot ?? page; // fallback
                }

                parentToAdd.Controls.Add(cv);

                // IMPORTANT: do NOT call page.Validators.Add(cv); adding to the tree is enough.
            }
        }
    }
}