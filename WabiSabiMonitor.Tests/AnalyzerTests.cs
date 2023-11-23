using System.Collections.Immutable;
using Moq;
using NBitcoin;
using NBitcoin.Secp256k1;
using WabiSabi.Crypto;
using WabiSabi.Crypto.Groups;
using WabiSabiMonitor.ApplicationCore.Data;
using WabiSabiMonitor.ApplicationCore.Interfaces;
using WabiSabiMonitor.ApplicationCore.Utils.Crypto;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Backend.Rounds;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Models;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Models.MultipartyTransaction;

namespace WabiSabiMonitor.Tests;

public class AnalyzerTests
{
    private readonly Mock<IRoundDataReaderService> _mockRoundReaderService;
    private readonly Mock<IRoundsDataFilter> _mockRoundDataFilter;
    private readonly Analyzer _analyzer;

    public AnalyzerTests()
    {
        _mockRoundReaderService = new Mock<IRoundDataReaderService>();
        _mockRoundDataFilter = new Mock<IRoundsDataFilter>();

        _analyzer = new Analyzer(_mockRoundReaderService.Object, _mockRoundDataFilter.Object);
    }

    [Fact]
    public void AnalyzeRoundStates_WhenCalled_ShouldReturnExpectedResult()
    {
        var g = new GroupElement(new GE(new FE(2), new FE(3)));
        var h = new GroupElement(new GE(new FE(4), new FE(5)));

        var roundParameters = new RoundParameters(
            Network.Main, 
            FeeRate.Zero, 
            CoordinationFeeRate.Zero,
            Money.Zero, 
            1, 1, 
            new MoneyRange(1, 3), 
            new MoneyRange(1, 3),
            RoundParameters.OnlyP2WPKH,
            RoundParameters.OnlyP2WPKH,
            TimeSpan.Zero,
            TimeSpan.Zero,
            TimeSpan.Zero,
            TimeSpan.Zero,
            TimeSpan.Zero,
            "test",
            false);
        
        var coin = new Coin
        {
            Outpoint = null,
            TxOut = null,
            Amount = null,
            ScriptPubKey = null
        };

        var ownershipProof = new OwnershipProof();
        var output = new TxOut();
        var roundCreated = new RoundCreated(roundParameters);
        var inputAdded = new InputAdded(coin, ownershipProof);
        var outputAdded = new OutputAdded(output);

        var concreteTransactionState = new ConcreteTransactionState(roundParameters);

        var roundStates = new RoundState(
            new uint256(1),
            new uint256(0),
            new CredentialIssuerParameters(g, h),
            new CredentialIssuerParameters(g, h),
            Phase.Ended,
            EndRoundState.TransactionBroadcasted,
            DateTimeOffset.UtcNow,
            TimeSpan.FromSeconds(5),
            concreteTransactionState
        );
        
        var secondRoundState = new RoundState(
            new uint256(1),
            new uint256(0),
            new CredentialIssuerParameters(g, h),
            new CredentialIssuerParameters(g, h),
            Phase.Ended,
            EndRoundState.TransactionBroadcasted,
            DateTimeOffset.UtcNow,
            TimeSpan.FromSeconds(5),
            concreteTransactionState
        );

        _mockRoundReaderService.Setup(rds => rds.Rounds);
        _mockRoundDataFilter.Setup(rdf => rdf.GetNbBanEstimation(It.IsAny<RoundState>())).Returns(0);

        var actual = _analyzer.AnalyzeRoundStates(new List<RoundState> { secondRoundState, roundStates });

        Assert.NotNull(actual);
    }

    public record ConcreteTransactionState : MultipartyTransactionState
    {
        public ConcreteTransactionState(RoundParameters parameters) : base(parameters)
        {
        }
    }
}