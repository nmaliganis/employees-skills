using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using employee.skill.common.dtos.Links;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Extensions;
using employee.skill.common.infrastructure.Helpers;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Employees;
using employee.skill.common.infrastructure.Paging;
using employee.skill.common.infrastructure.PropertyMappings;
using employee.skill.common.infrastructure.PropertyMappings.TypeHelpers;
using employees.Employees.contracts.Employees;
using employees.skills.api.Controllers.API.Base;
using employees.skills.api.Validators;
using employees.skills.contracts.Employees;
using employees.skills.contracts.V1;
using employees.skills.model.Employees;
using FluentNHibernate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace employees.skills.api.Controllers.API.V1;

/// <summary>
/// Employee
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[ResponseCache(Duration = 0, NoStore = true, VaryByHeader = "*")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    private readonly IUrlHelper _urlHelper;
    private readonly ITypeHelperService _typeHelperService;
    private readonly IPropertyMappingService _propertyMappingService;

    private readonly IInquiryAllEmployeesProcessor _inquiryAllEmployeesProcessor;
    private readonly IInquiryEmployeeProcessor _inquiryEmployeeProcessor;
    private readonly IUpdateEmployeeProcessor _updateEmployeeProcessor;
    private readonly ICreateEmployeeProcessor _createEmployeeProcessor;
    private readonly IDeleteEmployeeProcessor _deleteEmployeeProcessor;


    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="urlHelper"></param>
    /// <param name="typeHelperService"></param>
    /// <param name="propertyMappingService"></param>
    /// <param name="blockEmployee"></param>
    /// <param name="blockUser"></param>
    public EmployeesController(IUrlHelper urlHelper,
        ITypeHelperService typeHelperService,
        IPropertyMappingService propertyMappingService,
        IEmployeesControllerDependencyBlock blockEmployee)
    {
        this._urlHelper = urlHelper;
        this._typeHelperService = typeHelperService;
        this._propertyMappingService = propertyMappingService;

        this._inquiryAllEmployeesProcessor = blockEmployee.InquiryAllEmployeesProcessor;
        this._inquiryEmployeeProcessor = blockEmployee.InquiryEmployeeProcessor;
        this._createEmployeeProcessor = blockEmployee.CreateEmployeeProcessor;
        this._updateEmployeeProcessor = blockEmployee.UpdateEmployeeProcessor;
        this._deleteEmployeeProcessor = blockEmployee.DeleteEmployeeProcessor;
    }

    /// <summary>
    /// POST : Create a New Employee.
    /// </summary>
    /// <param name="employeeForCreationUiModel">EmployeeForCreationUiModel the Request Model for Creation</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new Employee is created, 400 (Bad Request), 500 (Server Error) </remarks>
    /// <response code="201">Created (if the Employee is created)</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost(Name = "PostEmployeeRoute")]
    [ValidateModel]
    public async Task<IActionResult> PostEmployeeRouteAsync(
        [FromBody] EmployeeCreationUiModel employeeForCreationUiModel)
    {
        var newCreatedEmployee = await this._createEmployeeProcessor.CreateEmployeeAsync(employeeForCreationUiModel);

        switch (newCreatedEmployee.Message)
        {
            case ("SUCCESS_CREATION"):
            {
                Log.Information(
                    $"--Method:PostEmployeeRouteAsync -- Message:EMPLOYEE_CREATION_SUCCESSFULLY -- Datetime:{DateTime.Now} -- EmployeeInfo:{employeeForCreationUiModel.Name}");
                return Created(nameof(PostEmployeeRouteAsync), newCreatedEmployee);
            }
            case ("ERROR_ERROR_ALREADY_EXISTS"):
            {
                Log.Error(
                    $"--Method:PostEmployeeRouteAsync -- Message:ERROR_EMPLOYEE_ALREADY_EXISTS -- Datetime:{DateTime.Now} -- EmployeeInfo:{employeeForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "EMPLOYEE_ALREADY_EXISTS" });
            }
            case ("ERROR_EMPLOYEE_NOT_MADE_PERSISTENT"):
            {
                Log.Error(
                    $"--Method:PostEmployeeRouteAsync -- Message:ERROR_EMPLOYEE_NOT_MADE_PERSISTENT -- Datetime:{DateTime.Now} -- EmployeeInfo:{employeeForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "ERROR_CREATION_NEW_EMPLOYEE" });
            }
            case ("UNKNOWN_ERROR"):
            {
                Log.Error(
                    $"--Method:PostEmployeeRouteAsync -- Message:ERROR_CREATION_NEW_EMPLOYEE -- Datetime:{DateTime.Now} -- EmployeeInfo:{employeeForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "ERROR_CREATION_NEW_EMPLOYEE" });
            }
        }

        return NotFound();
    }

    /// <summary>
    /// Get : Retrieve Stored providing Employee Id
    /// </summary>
    /// <param name="id">Employee Id the Request Index for Retrieval</param>
    /// <param name="fields">Fiends to be filtered with for the returned Employee</param>
    /// <remarks>Retrieve Employee Role providing Id and [Optional] fields</remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="404">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet("{id}", Name = "GetEmployee")]
    public async Task<IActionResult> GetEmployeeAsync(Guid id, [FromQuery] string fields)
    {
        if (!this._typeHelperService.TypeHasProperties<EmployeeUiModel>
                (fields))
        {
            return this.BadRequest("ERROR_RESOURCE_PARAMETER");
        }

        var employeeFromRepo = await this._inquiryEmployeeProcessor.GetEmployeeByIdAsync(id);

        if (employeeFromRepo == null)
        {
            return this.NotFound("EMPLOYEE_NOT_FOUND");
        }

        var employee = Mapper.Map<EmployeeUiModel>(employeeFromRepo);

        var links = this.CreateLinksForEmployee(id, fields);

        var linkedResourceToReturn = employee.ShapeData(fields)
            as IDictionary<string, object>;

        linkedResourceToReturn.Add("links", links);

        return this.Ok(linkedResourceToReturn);
    }


    /// <summary>
    /// PUT : Update Employee with New Employee Name
    /// </summary>
    /// <param name="id">Employee Id the Request Index for Retrieval</param>
    /// <param name="updatedEmployee">EmployeeForModification the Request Model with New Employee Name</param>
    /// <remarks>Change Employee providing EmployeeForModificationUiModel with Modified Employee Name</remarks>
    /// <response code="200">Resource updated correctly.</response>
    /// <response code="400">The model is not in valid state.</response>
    /// <response code="403">You have not access for this action.</response>
    /// <response code="404">Wrong attributes provided.</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpPut("{id}", Name = "UpdateEmployeeRoot")]
    [ValidateModel]
    public async Task<IActionResult> UpdateEmployeeAsync(Guid id,
        [FromBody] EmployeeModificationUiModel updatedEmployee)
    {
        if (id == Guid.Empty || string.IsNullOrEmpty(updatedEmployee.Firstname) ||
            string.IsNullOrEmpty(updatedEmployee.Lastname))
        {
            Log.Error(
                $"--Method:UpdateEmployeeAsync -- Message:ERROR_VALIDATION_ID_MODEL" +
                $" -- Datetime:{DateTime.Now}");
            return this.BadRequest("ERROR_VALIDATION_ID_MODEL");
        }

        await this._updateEmployeeProcessor.UpdateEmployeeAsync(id, updatedEmployee);

        return this.Ok(await this._inquiryEmployeeProcessor.GetEmployeeByIdAsync(id));
    }

    /// <summary>
    /// Delete - Delete an existing Employee 
    /// </summary>
    /// <param name="id">Employee Id for Deletion</param>
    /// <remarks>Delete Existing Employee </remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="400">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpDelete("{id}", Name = "DeleteEmployeeRoot")]
    public async Task<IActionResult> DeleteEmployeeRoot(Guid id)
    {
        var employeeToBeSoftDeleted = await this._deleteEmployeeProcessor.SoftDeleteEmployeeAsync(id);

        return this.Ok(employeeToBeSoftDeleted);
    }

    /// <summary>
    /// Get : Retrieve All/or Partial Paged Stored Employees 
    /// </summary>
    /// <remarks>Retrieve paged Employees providing Paging Query</remarks>
    /// <response code="200">Resource retrieved correctly.</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet(Name = "GetEmployees")]
    public async Task<IActionResult> GetEmployeesAsync(
        [FromQuery] EmployeesResourceParameters EmployeesResourceParameters)
    {
        if (!this._propertyMappingService.ValidMappingExistsFor<EmployeeUiModel, Employee>
                (EmployeesResourceParameters.OrderBy))
        {
            return this.BadRequest("ERROR_VALIDATION_MODEL");
        }

        if (!this._typeHelperService.TypeHasProperties<EmployeeUiModel>
                (EmployeesResourceParameters.Fields))
        {
            return this.BadRequest("ERROR_RESOURCE_PARAMETER");
        }

        var employeesQueryable = await this._inquiryAllEmployeesProcessor.GetEmployeesAsync(EmployeesResourceParameters);

        var employees = Mapper.Map<IEnumerable<EmployeeUiModel>>(employeesQueryable);

        var previousPageLink = employeesQueryable.HasPrevious
            ? this.CreateEmployeesResourceUri(EmployeesResourceParameters,
                ResourceUriType.PreviousPage)
            : null;

        var nextPageLink = employeesQueryable.HasNext
            ? this.CreateEmployeesResourceUri(EmployeesResourceParameters, ResourceUriType.NextPage)
            : null;

        var paginationMetadata = new
        {
            previousPageLink = previousPageLink,
            nextPageLink = nextPageLink,
            totalCount = employeesQueryable.TotalCount,
            pageSize = employeesQueryable.PageSize,
            currentPage = employeesQueryable.CurrentPage,
            totalPages = employeesQueryable.TotalPages
        };

        this.Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(paginationMetadata));

        return this.Ok(employees.ShapeData(EmployeesResourceParameters.Fields));
    }

    #region Link Builder

    private IEnumerable<LinkDto> CreateLinksForEmployee(Guid id, string fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(
                new LinkDto(this._urlHelper.Link("GetEmployee", new { id = id }),
                    "self",
                    "GET"));
        }
        else
        {
            links.Add(
                new LinkDto(this._urlHelper.Link("GetEmployee", new { id = id, fields = fields }),
                    "self",
                    "GET"));
        }

        return links;
    }


    private IEnumerable<LinkDto> CreateLinksForEmployees(
        EmployeesResourceParameters employeesResourceParameters,
        bool hasNext, bool hasPrevious)
    {
        var links = new List<LinkDto>
        {
            new LinkDto(this.CreateEmployeesResourceUri(employeesResourceParameters,
                    ResourceUriType.Current)
                , "self", "GET")
        };

        if (hasNext)
        {
            links.Add(
                new LinkDto(this.CreateEmployeesResourceUri(employeesResourceParameters,
                        ResourceUriType.NextPage),
                    "nextPage", "GET"));
        }

        if (hasPrevious)
        {
            links.Add(
                new LinkDto(this.CreateEmployeesResourceUri(employeesResourceParameters,
                        ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
        }

        return links;
    }

    private string CreateEmployeesResourceUri(EmployeesResourceParameters employeesResourceParameters,
        ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return this._urlHelper.Link("GetEmployees",
                    new
                    {
                        fields = employeesResourceParameters.Fields,
                        orderBy = employeesResourceParameters.OrderBy,
                        searchQuery = employeesResourceParameters.SearchQuery,
                        pageNumber = employeesResourceParameters.PageIndex - 1,
                        pageSize = employeesResourceParameters.PageSize
                    });
            case ResourceUriType.NextPage:
                return this._urlHelper.Link("GetEmployees",
                    new
                    {
                        fields = employeesResourceParameters.Fields,
                        orderBy = employeesResourceParameters.OrderBy,
                        searchQuery = employeesResourceParameters.SearchQuery,
                        pageNumber = employeesResourceParameters.PageIndex + 1,
                        pageSize = employeesResourceParameters.PageSize
                    });
            case ResourceUriType.Current:
            default:
                return this._urlHelper.Link("GetEmployees",
                    new
                    {
                        fields = employeesResourceParameters.Fields,
                        orderBy = employeesResourceParameters.OrderBy,
                        searchQuery = employeesResourceParameters.SearchQuery,
                        pageNumber = employeesResourceParameters.PageIndex,
                        pageSize = employeesResourceParameters.PageSize
                    });
        }
    }
    #endregion
}