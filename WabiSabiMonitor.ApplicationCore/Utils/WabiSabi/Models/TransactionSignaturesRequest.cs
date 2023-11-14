using NBitcoin;

namespace WabiSabiMonitor.ApplicationCore.Utils.WabiSabi.Models;

public record TransactionSignaturesRequest(uint256 RoundId, uint InputIndex, WitScript Witness);
