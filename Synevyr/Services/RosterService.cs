using System.Linq.Expressions;
using Synevyr.Infrastructure;
using Synevyr.Models;
using Synevyr.Models.Dtos;

namespace Synevyr.Services;

public class RosterService
{
    private readonly IRepository<GuildMemberModel> _membersRepo;
    private readonly IRepository<GuildRankModel> _rankRepo;

    public RosterService(IRepository<GuildMemberModel> membersRepo, IRepository<GuildRankModel> rankRepo)
    {
        _membersRepo = membersRepo;
        _rankRepo = rankRepo;
    }

    public IEnumerable<RosterDto> GetRoster(bool descending = false, string search = "", string sortField = "")
    {
        var members = _membersRepo.AsQuaryable();

        if (!string.IsNullOrEmpty(search))
        {
            members = members.Where(x => x.Name.Contains(search));
        }

        if (!string.IsNullOrEmpty(sortField))
        {
            var field = typeof(GuildMemberModel).GetProperties().FirstOrDefault(x => x.Name == sortField);
            if (field != null)
            {
                members = descending
                    ? members.OrderByDescending(x => field.GetValue(x))
                    : members.OrderBy(x => field.GetValue(x));
            }

        }

        var result = members.ToList();

        var ranks = _rankRepo.AsQuaryable().ToList();

        return result.Select(x => new RosterDto()
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