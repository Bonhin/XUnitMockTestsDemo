using FluentAssertions;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using MockingUnitTestsDemoApp.Impl.Services.Interfaces;
using NSubstitute;

namespace MockingUnitTestsDemoApp.Test
{
    public class PlayerServicesTest
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IPlayerService _sut;

        public PlayerServicesTest()
        {
            _playerRepository = Substitute.For<IPlayerRepository>();
            _teamRepository = Substitute.For<ITeamRepository>();
            _leagueRepository = Substitute.For<ILeagueRepository>();
            _sut = new PlayerService(_playerRepository, _teamRepository, _leagueRepository);
        }

        [Fact]
        public void GetForLeague_LeagueRepoIsInvalid_ReturnEmptyPlayersList()
        {
            //Arrange
            _leagueRepository.IsValid(Arg.Any<int>()).Returns(false);


            //Act
            var result = _sut.GetForLeague(Arg.Any<int>());


            //Assert
            result.Should().Equal(new List<Player>());
        }

        [Fact]
        public void GetForLeague_LeagueRepoIsValid_ReturnPlayersList()
        {
            //Arrange
            _leagueRepository.IsValid(Arg.Any<int>()).Returns(true);
            _teamRepository.GetForLeague(Arg.Any<int>()).Returns(new List<Team>());
            _playerRepository.GetForTeam(Arg.Any<int>()).Returns(new List<Player>());


            //Act
            var result = _sut.GetForLeague(Arg.Any<int>());


            //Assert
            result.Should().Equal(new List<Player>());
        }
    }
}