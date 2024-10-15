using Avalonia.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pamac.UI.Hosting;
internal class LoggerSink : ILogSink
{
    private readonly ILogger<LoggerSink> _logger;
    private IReadOnlyCollection<string> selectedAreas;

    public LoggerSink(ILogger<LoggerSink> logger, params string[] areas)
    {
        _logger = logger;
        selectedAreas = areas ?? [];
    }

    bool ILogSink.IsEnabled(LogEventLevel level, string area) =>
        _logger.IsEnabled(FromLogEventLevel(level)) && selectedAreas.Contains(area);


    void ILogSink.Log(LogEventLevel level, string area, object? source, string messageTemplate)
    {
        throw new NotImplementedException();
    }

    void ILogSink.Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
    {
        var concreteLevel = FromLogEventLevel(level);
        if (!_logger.IsEnabled(concreteLevel)) return;

        var eventId = $"AvaloniaHost[{area}]";
        if (source is not null) eventId = $"{eventId}+{Convert.ToString(source)}";

        _logger.Log(concreteLevel, new EventId(1, eventId), messageTemplate, propertyValues);
    }

    private static LogLevel FromLogEventLevel(LogEventLevel eventLevel) => eventLevel switch
    {
        LogEventLevel.Verbose => LogLevel.Trace,
        LogEventLevel.Debug => LogLevel.Debug,
        LogEventLevel.Information => LogLevel.Information,
        LogEventLevel.Warning => LogLevel.Warning,
        LogEventLevel.Error => LogLevel.Error,
        LogEventLevel.Fatal => LogLevel.Critical,
        _ => LogLevel.None
    };
}
