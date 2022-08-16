using DynaCore.Application.Features.Vendors.Commands.AddEdit;
using DynaCore.Application.Features.Vendors.Commands.Delete;
using DynaCore.Application.Features.Vendors.Queries.Export;
using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Application.Features.Vendors.Queries.GetById;
using DynaCore.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DynaCore.Server.Controllers.v1.Catalog
{
    public class VendorsController : BaseApiController<VendorsController>
    {
        /// <summary>
        /// Get All Vendors
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Vendors.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _mediator.Send(new GetAllVendorsQuery());
            return Ok(vendors);
        }

        /// <summary>
        /// Get a Vendor By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Vendors.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vendor = await _mediator.Send(new GetVendorByIdQuery() { Id = id });
            return Ok(vendor);
        }

        /// <summary>
        /// Create/Update a Vendor
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Vendors.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditVendorCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Vendor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Vendors.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteVendorCommand { Id = id }));
        }

        /// <summary>
        /// Search Vendors and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Vendors.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportVendorsQuery(searchString)));
        }
    }
}