using System.Linq.Expressions;
using Synevyr.Infrastructure;
using Synevyr.Models;
using Synevyr.Models.Dtos;

namespace Synevyr.Services;

public class RoosterService
{
    private readonly IRepository<GuildMemberModel> _membersRepo;
    private readonly IRepository<GuildRankModel> _rankRepo;

    public RoosterService(IRepository<GuildMemberModel> membersRepo, IRepository<GuildRankModel> rankRepo)
    {
        _membersRepo = membersRepo;
        _rankRepo = rankRepo;
    }

    public IEnumerable<RoosterDto> GetRooster()
    {
        var members = _membersRepo.AsQuaryable().ToList();
        var ranks = _rankRepo.AsQuaryable().ToList();


        return members.Select(x => new RoosterDto()
        {
            Rio = x.Rio,
            Name = x.Name,
            Image = x.Picture,
            Rank = ranks.FirstOrDefault(y => x.Rank == y.RankId)?.Name ?? ""
        });
    }

    public void InitRanks()
    {
        var ranks = new List<string>()
            {"Guild Master", "RL", "Officer", "Raider", "RaiderTrial", "Mythic+ Crew", "PeeVeePee", "Member", "Alt"};
        var result = ranks.Select((x, i) =>

            new GuildRankModel()
            {
                Name = x,
                RankId = i
            }
        );

        foreach (var item in result)
        {
            _rankRepo.Save(item);
        }
    }
}