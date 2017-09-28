<Query Kind="Program" />

class MessageFormatterState
{
    public MessageFormatterState(object loggedValue, string message)
    {
        LoggedValue = loggedValue;
        Message = message;
    }

    public object LoggedValue { get; }
    public string Message { get; }
}

static MessageFormatterState AddTimeStamp(MessageFormatterState state) =>
    new MessageFormatterState(
        state.LoggedValue,
        $"[{DateTime.UtcNow:o}] {state.Message}"
    );

static MessageFormatterState AddValue(MessageFormatterState state) =>
    new MessageFormatterState(
        state.LoggedValue,
        $"{state.Message}\"{state.LoggedValue}\""
    );
    
static Func<MessageFormatterState, MessageFormatterState> AddSeverity(string severity) =>
    state =>
        new MessageFormatterState(
            state.LoggedValue,
            $"{severity.ToUpper()} {state.Message}"
        );
        
static MessageFormatterState Trim(MessageFormatterState state) =>
    new MessageFormatterState(
        state.LoggedValue,
        state.Message.Trim()
    );
    
//static Func<object, string> FormatMessage(params Func<MessageFormatterState, MessageFormatterState>[] formatters) =>
//    value =>
//        formatters
//            .Aggregate(
//                new MessageFormatterState(value, string.Empty),
//                (state, formatter) => formatter(state))
//            .Message;

static Func<object, string> FormatMessage(Func<MessageFormatterState, MessageFormatterState> formatter) =>
    value =>
        new MessageFormatterState(value, string.Empty)
            .Map(formatter.InvokeChain)
            .Message;

static Func<object, string> CreateFormatter(params Func<MessageFormatterState, MessageFormatterState>[] formatters) =>
    formatters
        .Map(DelegateHelper.Combine)
        .Map(FormatMessage);

void Main()
{
    var warningFormatter =
        CreateFormatter(
            AddValue,
            AddSeverity("Warning"),
            AddTimeStamp,
            Trim
        );

    var debugFormatter =
        CreateFormatter(
            AddValue,
            AddSeverity("Debug"),
            AddTimeStamp,
            Trim
        );

    "Hello world".Map(warningFormatter).Dump();
    "Hello again".Map(debugFormatter).Dump();
}

