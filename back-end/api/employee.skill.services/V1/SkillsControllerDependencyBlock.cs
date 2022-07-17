using employees.skills.contracts.Skills;
using employees.skills.contracts.V1;

namespace Skills.skills.services.V1;

public class SkillsControllerDependencyBlock : ISkillsControllerDependencyBlock
{
    public SkillsControllerDependencyBlock(ICreateSkillProcessor createSkillProcessor
        ,IInquirySkillProcessor inquirySkillProcessor
        ,IUpdateSkillProcessor updateSkillProcessor
        ,IInquiryAllSkillsProcessor allSkillProcessor
        ,IDeleteSkillProcessor deleteSkillProcessor
    )

    {
        CreateSkillProcessor = createSkillProcessor;
        InquirySkillProcessor = inquirySkillProcessor;
        UpdateSkillProcessor = updateSkillProcessor;
        InquiryAllSkillsProcessor = allSkillProcessor;
        DeleteSkillProcessor = deleteSkillProcessor;
    }

    public ICreateSkillProcessor CreateSkillProcessor { get; private set; }
    public IInquirySkillProcessor InquirySkillProcessor { get; private set; }
    public IUpdateSkillProcessor UpdateSkillProcessor { get; private set; }
    public IInquiryAllSkillsProcessor InquiryAllSkillsProcessor { get; private set; }
    public IDeleteSkillProcessor DeleteSkillProcessor { get; private set; }
}