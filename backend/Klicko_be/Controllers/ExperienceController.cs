﻿using Klicko_be.DTOs.Account;
using Klicko_be.DTOs.CarryWith;
using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Experience;
using Klicko_be.DTOs.Image;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ExperienceService _experienceService;

        public ExperienceController(ExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllExperienceAsync();

                if (experiences == null)
                {
                    return NotFound(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "No experiences found",
                            Experiences = null,
                        }
                    );
                }

                var experiencesDto = experiences
                    .Select(exp => new ExperienceDto()
                    {
                        ExperienceId = exp.ExperienceId,
                        Title = exp.Title,
                        CategoryId = exp.CategoryId,
                        Duration = exp.Duration,
                        Place = exp.Place,
                        Price = exp.Price,
                        DescriptionShort = exp.DescriptionShort,
                        Description = exp.Description,
                        MaxParticipants = exp.MaxParticipants,
                        Organiser = exp.Organiser,
                        LoadingDate = exp.LoadingDate,
                        LastEditDate = exp.LastEditDate,
                        UserCreatorId = exp.UserCreatorId,
                        UserLastModifyId = exp.UserLastModifyId,
                        IsFreeCancellable = exp.IsFreeCancellable,
                        IncludedDescription = exp.IncludedDescription,
                        Sale = exp.Sale,
                        IsInEvidence = exp.IsInEvidence,
                        IsPopular = exp.IsPopular,
                        IsDeleted = exp.IsDeleted,
                        ValidityInMonths = exp.ValidityInMonths,
                        CoverImage = exp.CoverImage,
                        Category =
                            exp.Category != null
                                ? new CategorySimpleDto()
                                {
                                    CategoryId = exp.Category.CategoryId,
                                    Name = exp.Category.Name,
                                    Description = exp.Category.Description,
                                    Image = exp.Category.Image,
                                    Icon = exp.Category.Icon,
                                }
                                : null,
                        UserCreator =
                            exp.UserCreator != null
                                ? new UserSimpleDto()
                                {
                                    UserId = exp.UserCreator.Id,
                                    FirstName = exp.UserCreator.FirstName,
                                    LastName = exp.UserCreator.LastName,
                                    Email = exp.UserCreator.Email,
                                }
                                : null,
                        UserLastModify =
                            exp.UserLastModify != null
                                ? new UserSimpleDto()
                                {
                                    UserId = exp.UserLastModify.Id,
                                    FirstName = exp.UserLastModify.FirstName,
                                    LastName = exp.UserLastModify.LastName,
                                    Email = exp.UserLastModify.Email,
                                }
                                : null,
                        Images =
                            (exp.Images != null && exp.Images.Count > 0)
                                ? exp
                                    .Images.Select(img => new ImageSimpleDto()
                                    {
                                        ImageId = img.ImageId,
                                        Url = img.Url,
                                        AltText = img.AltText,
                                    })
                                    .ToList()
                                : null,
                        CarryWiths =
                            (exp.CarryWiths != null && exp.CarryWiths.Count > 0)
                                ? exp
                                    .CarryWiths.Select(carry => new CarryWithSimpleDto()
                                    {
                                        CarryWithId = carry.CarryWithId,
                                        Name = carry.Name,
                                    })
                                    .ToList()
                                : null,
                    })
                    .ToList();

                return experiencesDto != null
                    ? Ok(
                        new GetExperiencesListResponseDto()
                        {
                            Message = $"{experiencesDto.Count} experiences found!",
                            Experiences = experiencesDto,
                        }
                    )
                    : BadRequest(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "Something went wrong!",
                            Experiences = null,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Highlighted")]
        public async Task<IActionResult> GetAllHighlightedExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllHighlightedExperienceAsync();

                if (experiences == null)
                {
                    return NotFound(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "No experiences found",
                            Experiences = null,
                        }
                    );
                }

                var experiencesDto = experiences
                    .Select(exp => new ExperienceDto()
                    {
                        ExperienceId = exp.ExperienceId,
                        Title = exp.Title,
                        CategoryId = exp.CategoryId,
                        Duration = exp.Duration,
                        Place = exp.Place,
                        Price = exp.Price,
                        DescriptionShort = exp.DescriptionShort,
                        Description = exp.Description,
                        MaxParticipants = exp.MaxParticipants,
                        Organiser = exp.Organiser,
                        LoadingDate = exp.LoadingDate,
                        LastEditDate = exp.LastEditDate,
                        UserCreatorId = exp.UserCreatorId,
                        UserLastModifyId = exp.UserLastModifyId,
                        IsFreeCancellable = exp.IsFreeCancellable,
                        IncludedDescription = exp.IncludedDescription,
                        Sale = exp.Sale,
                        IsInEvidence = exp.IsInEvidence,
                        IsPopular = exp.IsPopular,
                        IsDeleted = exp.IsDeleted,
                        ValidityInMonths = exp.ValidityInMonths,
                        CoverImage = exp.CoverImage,
                        Category =
                            exp.Category != null
                                ? new CategorySimpleDto()
                                {
                                    CategoryId = exp.Category.CategoryId,
                                    Name = exp.Category.Name,
                                    Description = exp.Category.Description,
                                    Image = exp.Category.Image,
                                    Icon = exp.Category.Icon,
                                }
                                : null,
                        UserCreator = null,
                        UserLastModify = null,
                        Images = null,
                        CarryWiths = null,
                    })
                    .ToList();

                return experiencesDto != null
                    ? Ok(
                        new GetExperiencesListResponseDto()
                        {
                            Message = $"{experiencesDto.Count} experiences found!",
                            Experiences = experiencesDto,
                        }
                    )
                    : BadRequest(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "Something went wrong!",
                            Experiences = null,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Popular")]
        public async Task<IActionResult> GetAllPopularExperiences()
        {
            try
            {
                var experiences = await _experienceService.GetAllPopularExperienceAsync();

                if (experiences == null)
                {
                    return NotFound(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "No experiences found",
                            Experiences = null,
                        }
                    );
                }

                var experiencesDto = experiences
                    .Select(exp => new ExperienceDto()
                    {
                        ExperienceId = exp.ExperienceId,
                        Title = exp.Title,
                        CategoryId = exp.CategoryId,
                        Duration = exp.Duration,
                        Place = exp.Place,
                        Price = exp.Price,
                        DescriptionShort = exp.DescriptionShort,
                        Description = exp.Description,
                        MaxParticipants = exp.MaxParticipants,
                        Organiser = exp.Organiser,
                        LoadingDate = exp.LoadingDate,
                        LastEditDate = exp.LastEditDate,
                        UserCreatorId = exp.UserCreatorId,
                        UserLastModifyId = exp.UserLastModifyId,
                        IsFreeCancellable = exp.IsFreeCancellable,
                        IncludedDescription = exp.IncludedDescription,
                        Sale = exp.Sale,
                        IsInEvidence = exp.IsInEvidence,
                        IsPopular = exp.IsPopular,
                        IsDeleted = exp.IsDeleted,
                        ValidityInMonths = exp.ValidityInMonths,
                        CoverImage = exp.CoverImage,
                        Category =
                            exp.Category != null
                                ? new CategorySimpleDto()
                                {
                                    CategoryId = exp.Category.CategoryId,
                                    Name = exp.Category.Name,
                                    Description = exp.Category.Description,
                                    Image = exp.Category.Image,
                                    Icon = exp.Category.Icon,
                                }
                                : null,
                        UserCreator = null,
                        UserLastModify = null,
                        Images = null,
                        CarryWiths = null,
                    })
                    .ToList();

                return experiencesDto != null
                    ? Ok(
                        new GetExperiencesListResponseDto()
                        {
                            Message = $"{experiencesDto.Count} experiences found!",
                            Experiences = experiencesDto,
                        }
                    )
                    : BadRequest(
                        new GetExperiencesListResponseDto()
                        {
                            Message = "Something went wrong!",
                            Experiences = null,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Experience/{experienceId:guid}")]
        public async Task<IActionResult> GetSingleExperience(Guid experienceId)
        {
            try
            {
                var experience = await _experienceService.GetExperienceByIdAsync(experienceId);

                if (experience == null)
                {
                    return NotFound(
                        new GetExperienceResponseDto()
                        {
                            Message = "Experience not found!",
                            Experience = null,
                        }
                    );
                }

                var experienceDto = new ExperienceDto()
                {
                    ExperienceId = experience.ExperienceId,
                    Title = experience.Title,
                    CategoryId = experience.CategoryId,
                    Duration = experience.Duration,
                    Place = experience.Place,
                    Price = experience.Price,
                    DescriptionShort = experience.DescriptionShort,
                    Description = experience.Description,
                    MaxParticipants = experience.MaxParticipants,
                    Organiser = experience.Organiser,
                    LoadingDate = experience.LoadingDate,
                    LastEditDate = experience.LastEditDate,
                    UserCreatorId = experience.UserCreatorId,
                    UserLastModifyId = experience.UserLastModifyId,
                    IsFreeCancellable = experience.IsFreeCancellable,
                    IncludedDescription = experience.IncludedDescription,
                    Sale = experience.Sale,
                    IsInEvidence = experience.IsInEvidence,
                    IsPopular = experience.IsPopular,
                    IsDeleted = experience.IsDeleted,
                    ValidityInMonths = experience.ValidityInMonths,
                    CoverImage = experience.CoverImage,
                    Category =
                        experience.Category != null
                            ? new CategorySimpleDto()
                            {
                                CategoryId = experience.Category.CategoryId,
                                Name = experience.Category.Name,
                                Description = experience.Category.Description,
                                Image = experience.Category.Image,
                                Icon = experience.Category.Icon,
                            }
                            : null,
                    UserCreator =
                        experience.UserCreator != null
                            ? new UserSimpleDto()
                            {
                                UserId = experience.UserCreator.Id,
                                FirstName = experience.UserCreator.FirstName,
                                LastName = experience.UserCreator.LastName,
                                Email = experience.UserCreator.Email,
                            }
                            : null,
                    UserLastModify =
                        experience.UserLastModify != null
                            ? new UserSimpleDto()
                            {
                                UserId = experience.UserLastModify.Id,
                                FirstName = experience.UserLastModify.FirstName,
                                LastName = experience.UserLastModify.LastName,
                                Email = experience.UserLastModify.Email,
                            }
                            : null,
                    Images =
                        (experience.Images != null && experience.Images.Count > 0)
                            ? experience
                                .Images.Select(img => new ImageSimpleDto()
                                {
                                    ImageId = img.ImageId,
                                    Url = img.Url,
                                    AltText = img.AltText,
                                })
                                .ToList()
                            : null,
                    CarryWiths =
                        (experience.CarryWiths != null && experience.CarryWiths.Count > 0)
                            ? experience
                                .CarryWiths.Select(carry => new CarryWithSimpleDto()
                                {
                                    CarryWithId = carry.CarryWithId,
                                    Name = carry.Name,
                                })
                                .ToList()
                            : null,
                };

                return experienceDto != null
                    ? Ok(
                        new GetExperienceResponseDto()
                        {
                            Message = "Experience found!",
                            Experience = experienceDto,
                        }
                    )
                    : BadRequest(
                        new GetExperienceResponseDto()
                        {
                            Message = "Something went wrong!",
                            Experience = null,
                        }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperience(
            [FromBody] CreateExperienceRequestDto createExperience
        )
        {
            try
            {
                // TODO: aggiungere UserId automatico tramite riconoscimento token
                var newExperience = new Experience()
                {
                    ExperienceId = Guid.NewGuid(),
                    Title = createExperience.Title,
                    CategoryId = createExperience.CategoryId,
                    Duration = createExperience.Duration,
                    Place = createExperience.Place,
                    Price = createExperience.Price,
                    DescriptionShort = createExperience.DescriptionShort,
                    Description = createExperience.Description,
                    MaxParticipants = createExperience.MaxParticipants,
                    Organiser = createExperience.Organiser,
                    LoadingDate = DateTime.UtcNow,
                    LastEditDate = DateTime.UtcNow,
                    IsFreeCancellable = createExperience.IsFreeCancellable,
                    IncludedDescription = createExperience.IncludedDescription,
                    Sale = createExperience.Sale,
                    IsInEvidence = createExperience.IsInEvidence,
                    IsPopular = createExperience.IsPopular,
                    IsDeleted = createExperience.IsDeleted,
                    CoverImage = createExperience.CoverImage,
                    ValidityInMonths = createExperience.ValidityInMonths,
                    UserCreatorId = "70b579dd-c6a0-4075-8d7d-1326f2353c7b",
                    UserLastModifyId = "70b579dd-c6a0-4075-8d7d-1326f2353c7b",
                };

                if (createExperience.Images != null && createExperience.Images.Count > 0)
                {
                    newExperience.Images = createExperience
                        .Images.Select(img => new Image()
                        {
                            ImageId = Guid.NewGuid(),
                            Url = img.Url,
                            AltText = img.AltText,
                            ExperienceId = newExperience.ExperienceId,
                        })
                        .ToList();
                }

                if (createExperience.CarryWiths != null && createExperience.CarryWiths.Count > 0)
                {
                    newExperience.CarryWiths = createExperience
                        .CarryWiths.Select(carry => new CarryWith()
                        {
                            CarryWithId = Guid.NewGuid(),
                            Name = carry.Name,
                            ExperienceId = newExperience.ExperienceId,
                        })
                        .ToList();
                }

                var result = await _experienceService.CreateExperienceAsync(newExperience);

                return result
                    ? Ok(
                        new CreateExperienceResponseDto()
                        {
                            Message = "Experience created successfully!",
                        }
                    )
                    : BadRequest(
                        new CreateExperienceResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{experienceId:guid}")]
        public async Task<IActionResult> Edit(
            [FromBody] EditExperienceRequestDto experienceEdit,
            Guid experienceId
        )
        {
            try
            {
                var result = await _experienceService.EditExperienceByIdAsync(
                    experienceId,
                    experienceEdit
                );

                return result
                    ? Ok(
                        new EditExperienceResponseDto()
                        {
                            Message = "Experience edited successfully!",
                        }
                    )
                    : BadRequest(
                        new EditExperienceResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("softDelete/{experienceId:guid}")]
        public async Task<IActionResult> SoftDelete(Guid experienceId)
        {
            var result = await _experienceService.SoftDeleteExperienceByIdAsync(experienceId);
            return result
                ? Ok(
                    new DeleteExperienceResponseDto()
                    {
                        Message = "Experience soft deleted successfully!",
                    }
                )
                : BadRequest(
                    new DeleteExperienceResponseDto() { Message = "Something went wrong!" }
                );
        }

        [HttpDelete("{experienceId:guid}")]
        public async Task<IActionResult> Delete(Guid experienceId)
        {
            var result = await _experienceService.DeleteExperienceByIdAsync(experienceId);

            return result
                ? Ok(
                    new DeleteExperienceResponseDto()
                    {
                        Message = "Experience deleted successfully!",
                    }
                )
                : BadRequest(
                    new DeleteExperienceResponseDto() { Message = "Something went wrong!" }
                );
        }
    }
}
