using SuggestionMot.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SuggestionMotTest
{
    public class SuggestionServiceTests
    {

        /* Ce test est un test manuel de validation via la console.
           Il m’a servi à vérifier rapidement la logique et les cas limites pendant le développement.
           Ensuite, j’ai écrit de vrais tests unitaires avec xUnit pour automatiser la validation et garantir la non-régression.*/

        private readonly ISuggestionService _service;

        public SuggestionServiceTests()
        {
            _service = new SuggestionService();
        }



        [Fact]
        //compréhension de la similarité
        public void GetSuggestions_ShouldReturnExactMatchFirst()
        {
            // Arrange
            var liste = new List<string>
            {
                "gros", "gras", "graisse", "agressif"
            };

            // Act
            var result = _service.GetSuggestions("gros", liste, 2);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("gros", result[0]);
            Assert.Equal("gras", result[1]);
        }

        [Fact]
        //filtrage + performance
        public void GetSuggestions_ShouldIgnoreWordsThatAreTooShort()
        {
            // Arrange
            var liste = new List<string>
            {
                "go", "ros", "gro", "gros"
            };

            // Act
            var result = _service.GetSuggestions("gros", liste, 5);

            // Assert
            Assert.Single(result);
            Assert.Equal("gros", result[0]);
        }

        [Fact]
        //respect strict des règles
        public void GetSuggestions_ShouldRespectLengthPriorityWhenScoresAreEqual()
        {
            // Arrange
            var liste = new List<string>
            {
                "gras",      // même longueur
                "agressif"   // plus long
            };

            // Act
            var result = _service.GetSuggestions("gros", liste, 1);

            // Assert
            Assert.Equal("gras", result[0]);
        }

        [Fact]
        //gestion des égalités
        public void GetSuggestions_ShouldReturnAlphabeticalOrderWhenEverythingIsEqual()
        {
            // Arrange
            var liste = new List<string>
            {
                "bats",
                "cats"
            };

            // Act
            var result = _service.GetSuggestions("rats", liste, 2);

            // Assert
            Assert.Equal("bats", result[0]);
            Assert.Equal("cats", result[1]);
        }

        [Fact]
        //robustesse et sécurité
        public void GetSuggestions_ShouldReturnEmptyList_WhenInputIsInvalid()
        {
            // Arrange
            var liste = new List<string> { "test" };

            // Act
            var result1 = _service.GetSuggestions("", liste, 2);
            var result2 = _service.GetSuggestions("test", null, 2);
            var result3 = _service.GetSuggestions("test", liste, 0);

            // Assert
            Assert.Empty(result1);
            Assert.Empty(result2);
            Assert.Empty(result3);
        }
    }
}