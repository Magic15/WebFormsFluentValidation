using FluentValidation;
using System;
using System.Collections.Generic;
using System.Web.UI;
using WebFormsFluentValidations.Extensions;
using WebFormsFluentValidations.Models;

namespace WebFormsFluentValidations
{
    public partial class _Default : Page
    {
        private readonly IValidator<RegisterVm> _validator = new RegisterVmValidator();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private const string RegisterGroup = "Register";       

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Build the VM
            var vm = new RegisterVm
            {
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                RepeatPassword = txtRepeatPassword.Text
            };

            // Validate with FluentValidation
            var result = _validator.Validate(vm);

            if (!result.IsValid)
            {
                // Map property -> input ID (for ControlToValidate)
                var controlMap = new Dictionary<string, string>
                {
                    { nameof(RegisterVm.Email), txtEmail.ID },
                    { nameof(RegisterVm.Password), txtPassword.ID },
                    { nameof(RegisterVm.RepeatPassword), txtRepeatPassword.ID }
                };

                // Map property -> placeholder ID (for inline rendering)
                var phMap = new Dictionary<string, string>
                {
                    { nameof(RegisterVm.Email), phEmailError.ID },
                    { nameof(RegisterVm.Password), phPasswordError.ID },
                    { nameof(RegisterVm.RepeatPassword), phRepeatPasswordError.ID }
                };

                            // Inject validators
               this.AddValidationErrors(
                  result,
                  propertyToControlId: controlMap,
                  propertyToPlaceholderId: phMap,
                  validationGroup: "Register",
                  contentPlaceHolderId: "MainContent"   // <-- important
              );

                // Re-run WebForms validation so ValidationSummary sees them now
                //  Page.Validate(RegisterGroup);

                return; // Page.IsValid will be false; summary + inline now show
            }

            // Success path...
            // save/redirect/etc.
        }
    }
}
