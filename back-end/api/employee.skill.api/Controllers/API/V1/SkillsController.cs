using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using employee.skill.common.dtos.Links;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.Extensions;
using employee.skill.common.infrastructure.Helpers;
using employee.skill.common.infrastructure.Helpers.ResourceParameters.Skills;
using employee.skill.common.infrastructure.Paging;
using employee.skill.common.infrastructure.PropertyMappings;
using employee.skill.common.infrastructure.PropertyMappings.TypeHelpers;
using employees.skills.api.Controllers.API.Base;
using employees.skills.api.Validators;
using employees.skills.contracts.Skills;
using employees.skills.contracts.V1;
using employees.skills.model.Skills;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace employees.skills.api.Controllers.API.V1;

/// <summary>
/// Skill
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[ResponseCache(Duration = 0, NoStore = true, VaryByHeader = "*")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class SkillsController : BaseController {
    private readonly IUrlHelper _urlHelper;
    private readonly ITypeHelperService _typeHelperService;
    private readonly IPropertyMappingService _propertyMappingService;

    private readonly IInquiryAllSkillsProcessor _inquiryAllSkillsProcessor;
    private readonly IInquirySkillProcessor _inquirySkillProcessor;
    private readonly ICreateSkillProcessor _createSkillProcessor;
    private readonly IUpdateSkillProcessor _updateSkillProcessor;
    private readonly IDeleteSkillProcessor _deleteSkillProcessor;


    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="urlHelper"></param>
    /// <param name="typeHelperService"></param>
    /// <param name="propertyMappingService"></param>
    /// <param name="blockSkill"></param>
    /// <param name="blockUser"></param>
    public SkillsController(IUrlHelper urlHelper,
        ITypeHelperService typeHelperService,
        IPropertyMappingService propertyMappingService,
        ISkillsControllerDependencyBlock blockSkill) {
        this._urlHelper = urlHelper;
        this._typeHelperService = typeHelperService;
        this._propertyMappingService = propertyMappingService;

        this._inquiryAllSkillsProcessor = blockSkill.InquiryAllSkillsProcessor;
        this._inquirySkillProcessor = blockSkill.InquirySkillProcessor;
        this._createSkillProcessor = blockSkill.CreateSkillProcessor;
        this._updateSkillProcessor = blockSkill.UpdateSkillProcessor;
        this._deleteSkillProcessor = blockSkill.DeleteSkillProcessor;
    }
    
    
    /// <summary>
    /// POST : Create a New Skill.
    /// </summary>
    /// <param name="SkillForCreationUiModel">SkillForCreationUiModel the Request Model for Creation</param>
    /// <remarks> return a ResponseEntity with status 201 (Created) if the new Skill is created, 400 (Bad Request), 500 (Server Error) </remarks>
    /// <response code="201">Created (if the Skill is created)</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost(Name = "PostSkillRoute")]
    [ValidateModel]
    public async Task<IActionResult> PostSkillRouteAsync(
        [FromBody] SkillCreationUiModel SkillForCreationUiModel)
    {
        var newCreatedSkill = await this._createSkillProcessor.CreateSkillAsync(SkillForCreationUiModel);

        switch (newCreatedSkill.Message)
        {
            case ("SUCCESS_CREATION"):
            {
                Log.Information(
                    $"--Method:PostSkillRouteAsync -- Message:SKILL_CREATION_SUCCESSFULLY -- Datetime:{DateTime.Now} -- SkillInfo:{SkillForCreationUiModel.Name}");
                return Created(nameof(PostSkillRouteAsync), newCreatedSkill);
            }
            case ("ERROR_ERROR_ALREADY_EXISTS"):
            {
                Log.Error(
                    $"--Method:PostSkillRouteAsync -- Message:ERROR_SKILL_ALREADY_EXISTS -- Datetime:{DateTime.Now} -- SkillInfo:{SkillForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "Skill_ALREADY_EXISTS" });
            }
            case ("ERROR_Skill_NOT_MADE_PERSISTENT"):
            {
                Log.Error(
                    $"--Method:PostSkillRouteAsync -- Message:ERROR_SKILL_NOT_MADE_PERSISTENT -- Datetime:{DateTime.Now} -- SkillInfo:{SkillForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "ERROR_CREATION_NEW_Skill" });
            }
            case ("UNKNOWN_ERROR"):
            {
                Log.Error(
                    $"--Method:PostSkillRouteAsync -- Message:ERROR_CREATION_NEW_SKILL -- Datetime:{DateTime.Now} -- SkillInfo:{SkillForCreationUiModel.Name}");
                return BadRequest(new { errorMessage = "ERROR_CREATION_NEW_Skill" });
            }
        }

        return NotFound();
    }

    /// <summary>
    /// Get : Retrieve Stored providing Skill Id
    /// </summary>
    /// <param name="id">Skill Id the Request Index for Retrieval</param>
    /// <param name="fields">Fiends to be filtered with for the returned Skill</param>
    /// <remarks>Retrieve Skill Role providing Id and [Optional] fields</remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="404">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet("{id}", Name = "GetSkill")]
    public async Task<IActionResult> GetSkillAsync(Guid id, [FromQuery] string fields) {
        if (!this._typeHelperService.TypeHasProperties<SkillUiModel>
                (fields)) {
            return this.BadRequest("ERROR_RESOURCE_PARAMETER");
        }

        var skillFromRepo = await this._inquirySkillProcessor.GetSkillByIdAsync(id);

        if (skillFromRepo == null) {
            return this.NotFound("SKILL_NOT_FOUND");
        }

        var Skill = Mapper.Map<SkillUiModel>(skillFromRepo);

        var links = this.CreateLinksForSkill(id, fields);

        var linkedResourceToReturn = Skill.ShapeData(fields)
            as IDictionary<string, object>;

        linkedResourceToReturn.Add("links", links);

        return this.Ok(linkedResourceToReturn);
    }
    

    /// <summary>
    /// PUT : Update Skill with New Skill Name
    /// </summary>
    /// <param name="id">Skill Id the Request Index for Retrieval</param>
    /// <param name="updatedSkill">SkillForModification the Request Model with New Skill Name</param>
    /// <remarks>Change Skill providing SkillForModificationUiModel with Modified Skill Name</remarks>
    /// <response code="200">Resource updated correctly.</response>
    /// <response code="400">The model is not in valid state.</response>
    /// <response code="403">You have not access for this action.</response>
    /// <response code="404">Wrong attributes provided.</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpPut("{id}", Name = "UpdateSkillRoot")]
    [ValidateModel]
    public async Task<IActionResult> UpdateSkillAsync(Guid id,
        [FromBody] SkillModificationUiModel updatedSkill) {
        if (id == Guid.Empty || string.IsNullOrEmpty(updatedSkill.Name) || string.IsNullOrEmpty(updatedSkill.Description)) {
            Log.Error(
                $"--Method:UpdateSkillAsync -- Message:ERROR_VALIDATION_ID_MODEL" +
                $" -- Datetime:{DateTime.Now}");
            return this.BadRequest("ERROR_VALIDATION_ID_MODEL");
        }

        await this._updateSkillProcessor.UpdateSkillAsync(id, updatedSkill);

        return this.Ok(await this._inquirySkillProcessor.GetSkillByIdAsync(id));
    }

    /// <summary>
    /// Delete - Delete an existing Skill 
    /// </summary>
    /// <param name="id">Skill Id for Deletion</param>
    /// <remarks>Delete Existing Skill </remarks>
    /// <response code="200">Resource retrieved correctly</response>
    /// <response code="400">Resource Not Found</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpDelete("{id}", Name = "DeleteSkillRoot")]
    public async Task<IActionResult> DeleteSkillRoot(Guid id) {
        var skillToBeSoftDeleted = await this._deleteSkillProcessor.SoftDeleteSkillAsync(id);

        return this.Ok(skillToBeSoftDeleted);
    }

    /// <summary>
    /// Get : Retrieve All/or Partial Paged Stored Skills 
    /// </summary>
    /// <remarks>Retrieve paged Skills providing Paging Query</remarks>
    /// <response code="200">Resource retrieved correctly.</response>
    /// <response code="500">Internal Server Error.</response>
    [HttpGet(Name = "GetSkills")]
    public async Task<IActionResult> GetSkillsAsync(
        [FromQuery] SkillsResourceParameters SkillsResourceParameters) {
        if (!this._propertyMappingService.ValidMappingExistsFor<SkillUiModel, Skill>
                (SkillsResourceParameters.OrderBy)) {
            return this.BadRequest("ERROR_VALIDATION_MODEL");
        }

        if (!this._typeHelperService.TypeHasProperties<SkillUiModel>
                (SkillsResourceParameters.Fields)) {
            return this.BadRequest("ERROR_RESOURCE_PARAMETER");
        }

        var skillsQueryable = await this._inquiryAllSkillsProcessor.GetSkillsAsync(SkillsResourceParameters);

        var skills = Mapper.Map<IEnumerable<SkillUiModel>>(skillsQueryable);

        var previousPageLink = skillsQueryable.HasPrevious
            ? this.CreateSkillsResourceUri(SkillsResourceParameters,
                ResourceUriType.PreviousPage)
            : null;

        var nextPageLink = skillsQueryable.HasNext
            ? this.CreateSkillsResourceUri(SkillsResourceParameters, ResourceUriType.NextPage)
            : null;

        var paginationMetadata = new {
            previousPageLink = previousPageLink,
            nextPageLink = nextPageLink,
            totalCount = skillsQueryable.TotalCount,
            pageSize = skillsQueryable.PageSize,
            currentPage = skillsQueryable.CurrentPage,
            totalPages = skillsQueryable.TotalPages
        };

        this.Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(paginationMetadata));

        return this.Ok(skills.ShapeData(SkillsResourceParameters.Fields));
    }

    #region Link Builder

    private IEnumerable<LinkDto> CreateLinksForSkill(Guid id, string fields) {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields)) {
            links.Add(
                new LinkDto(this._urlHelper.Link("GetSkill", new { id = id }),
                    "self",
                    "GET"));
        } else {
            links.Add(
                new LinkDto(this._urlHelper.Link("GetSkill", new { id = id, fields = fields }),
                    "self",
                    "GET"));
        }

        return links;
    }


    private IEnumerable<LinkDto> CreateLinksForSkills(
        SkillsResourceParameters skillsResourceParameters,
        bool hasNext, bool hasPrevious) {
        var links = new List<LinkDto>
        {
            new LinkDto(this.CreateSkillsResourceUri(skillsResourceParameters,
                    ResourceUriType.Current)
                , "self", "GET")
        };

        if (hasNext) {
            links.Add(
                new LinkDto(this.CreateSkillsResourceUri(skillsResourceParameters,
                        ResourceUriType.NextPage),
                    "nextPage", "GET"));
        }

        if (hasPrevious) {
            links.Add(
                new LinkDto(this.CreateSkillsResourceUri(skillsResourceParameters,
                        ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
        }

        return links;
    }

    private string CreateSkillsResourceUri(SkillsResourceParameters skillsResourceParameters,
        ResourceUriType type) {
        switch (type) {
            case ResourceUriType.PreviousPage:
                return this._urlHelper.Link("GetSkills",
                    new {
                        fields = skillsResourceParameters.Fields,
                        orderBy = skillsResourceParameters.OrderBy,
                        searchQuery = skillsResourceParameters.SearchQuery,
                        pageNumber = skillsResourceParameters.PageIndex - 1,
                        pageSize = skillsResourceParameters.PageSize
                    });
            case ResourceUriType.NextPage:
                return this._urlHelper.Link("GetSkills",
                    new {
                        fields = skillsResourceParameters.Fields,
                        orderBy = skillsResourceParameters.OrderBy,
                        searchQuery = skillsResourceParameters.SearchQuery,
                        pageNumber = skillsResourceParameters.PageIndex + 1,
                        pageSize = skillsResourceParameters.PageSize
                    });
            case ResourceUriType.Current:
            default:
                return this._urlHelper.Link("GetSkills",
                    new {
                        fields = skillsResourceParameters.Fields,
                        orderBy = skillsResourceParameters.OrderBy,
                        searchQuery = skillsResourceParameters.SearchQuery,
                        pageNumber = skillsResourceParameters.PageIndex,
                        pageSize = skillsResourceParameters.PageSize
                    });
        }
    }

    #endregion
}