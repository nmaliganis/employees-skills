using employees.skills.contracts.Skills;

namespace employees.skills.contracts.V1;

public interface ISkillsControllerDependencyBlock
{
    ICreateSkillProcessor CreateSkillProcessor { get; }
    IInquirySkillProcessor InquirySkillProcessor { get; }
    IUpdateSkillProcessor UpdateSkillProcessor { get; }
    IInquiryAllSkillsProcessor InquiryAllSkillsProcessor { get; }
    IDeleteSkillProcessor DeleteSkillProcessor { get; }
}