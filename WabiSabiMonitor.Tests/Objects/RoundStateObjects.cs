using NBitcoin;
using WabiSabi.Crypto;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Backend.Rounds;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Models;
using WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Models.MultipartyTransaction;

namespace WabiSabiMonitor.Tests.Objects;

public class RoundStateObjects
{
    static uint256 id =  1;
    static uint256 blameOf =  0;
    static CredentialIssuerParameters?
        amountCredentialIssuerParameters =  null;
    static CredentialIssuerParameters?
        vsizeCredentialIssuerParameters =  null;
    static Phase phase =  Phase.Ended;
    static EndRoundState endRoundState =  EndRoundState.TransactionBroadcasted;
    static DateTimeOffset inputRegistrationStart =  DateTimeOffset.Now;
    static TimeSpan inputRegistrationTimeout =  TimeSpan.Zero;
    static MultipartyTransactionState? coinjoinState = null;

// Creating RoundState object
    RoundState roundState = new RoundState(
        id,
        blameOf,
        amountCredentialIssuerParameters,
        vsizeCredentialIssuerParameters,
        phase,
        endRoundState,
        inputRegistrationStart,
        inputRegistrationTimeout,
        coinjoinState
    );
}